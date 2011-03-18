
/****************************************************************************

Tilde

Copyright (c) 2008 Tantalus Media Pty

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.

****************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

using Tilde.Framework.Controller;
using Tilde.Framework.View;

namespace Tilde.LuaDebugger
{
	[ToolWindowAttribute(Group = "Debug")]
	public partial class PendingDownloadsWindow : Tilde.Framework.View.ToolWindow
	{
		IManager m_manager;
		DebugManager m_debugger;
		bool m_downloadInProgress = false;

		enum FileType
		{
			Unknown,
			LuaScript
		}

		class ListItemTag
		{
			public ListItemTag(string name, FileType type)
			{
				m_name = name;
				m_type = type;
				m_locked = false;
			}

			public string m_name;
			public FileType m_type;
			public bool m_locked;
		}

		public PendingDownloadsWindow(IManager manager)
		{
			InitializeComponent();

			m_manager = manager;
			m_debugger = ((LuaPlugin)manager.GetPlugin(typeof(LuaPlugin))).Debugger;

			m_debugger.DebuggerConnected += new DebuggerConnectedEventHandler(Debugger_DebuggerConnected);
			m_debugger.DebuggerDisconnected += new DebuggerDisconnectedEventHandler(Debugger_DebuggerDisconnected);

			pendingFileListView.Enabled = false;
		}

		public bool DownloadInProgress
		{
			get { return m_downloadInProgress; }
		}

		void SetDownloadInProgress(bool inprogress)
		{
			m_downloadInProgress = inprogress;
			progressBarDownloading.Style = inprogress ? ProgressBarStyle.Marquee : ProgressBarStyle.Continuous;
		}

		void Debugger_DebuggerConnected(DebugManager sender, Target target)
		{
			pendingFileListView.Items.Clear();
			pendingFileListView.Enabled = true;
			m_manager.FileWatcher.FileModified += new FileModifiedEventHandler(FileWatcher_FileModified);
		}

		void Debugger_DebuggerDisconnected(DebugManager sender)
		{
			m_manager.FileWatcher.FileModified -= new FileModifiedEventHandler(FileWatcher_FileModified);
			pendingFileListView.Enabled = false;
			pendingFileListView.Items.Clear();
		}

		private void FileWatcher_FileModified(object sender, string fileName)
		{
			if (IsHandleCreated && !IsDisposed && InvokeRequired)
				BeginInvoke(new FileModifiedEventHandler(FileWatcher_FileModified), new object[] { sender, fileName });
			else
			{
				if (m_debugger.ConnectionStatus == ConnectionStatus.Connected && m_manager.Project != null && m_manager.Project.FindDocument(fileName) != null)
				{
					if (fileName.EndsWith(".lua", StringComparison.InvariantCultureIgnoreCase))
					{
						AddFile(fileName, FileType.LuaScript);
					}
				}
			}
		}

		private void AddFile(string fileName, FileType type)
		{
			if(!pendingFileListView.Items.ContainsKey(fileName.ToLowerInvariant()))
			{
				ListViewItem item = new ListViewItem(fileName);
				item.Tag = new ListItemTag(fileName, type);
				item.Name = fileName.ToLowerInvariant();
				item.SubItems.Add(type.ToString());
				item.SubItems.Add("");
				pendingFileListView.Items.Add(item);
			}
		}

		private bool DownloadLuaFile(string fileName)
		{
			try
			{
				return m_debugger.ConnectedTarget.DownloadFile(fileName);
			}
			catch (Exception ex)
			{
				m_manager.MainWindow.Invoke(new MethodInvoker(delegate() { MessageBox.Show(m_manager.MainWindow, String.Format("Could not download file\r\n\r\n{0}\r\n\r\n{1}", fileName, ex.ToString()), "Download Failed"); }));
				return false;
			}
		}

		private void PendingDownloadsWindow_FormClosing(object sender, FormClosingEventArgs e)
		{
			m_manager.FileWatcher.FileModified -= new FileModifiedEventHandler(FileWatcher_FileModified);
		}

		private void contextMenuStrip_Opening(object sender, CancelEventArgs e)
		{
			bool selection = pendingFileListView.SelectedItems.Count > 0;
			menuItemDownloadNow.Enabled = selection;
			menuItemRemove.Enabled = selection;
		}

		private void menuItemDownloadNow_Click(object sender, EventArgs e)
		{
			List<ListViewItem> items = new List<ListViewItem>(pendingFileListView.SelectedItems.Count);
			foreach (ListViewItem item in pendingFileListView.SelectedItems)
			{
				((ListItemTag) item.Tag).m_locked = true;
				item.SubItems[2].Text = "Queued";
				items.Add(item);
			}

			SetDownloadInProgress(true);
			ThreadPool.QueueUserWorkItem(new WaitCallback(delegate(object state) { DownloadThreadProc(items); }));
		}

		void DownloadThreadProc(List<ListViewItem> items)
		{
			try
			{
				foreach (ListViewItem item in items)
				{
					ListItemTag tag = (ListItemTag)item.Tag;
					if (tag.m_type == FileType.LuaScript)
					{
						this.Invoke(new MethodInvoker(delegate() { item.SubItems[2].Text = "Downloading..."; }));
						bool result = DownloadLuaFile(tag.m_name);
						if (!result)
							break;
					}

					this.Invoke(new MethodInvoker(delegate() { if(pendingFileListView.Items.Contains(item)) pendingFileListView.Items.Remove(item); }));
				}
			}
			finally
			{
				this.Invoke(new MethodInvoker(delegate() { SetDownloadInProgress(false); }));
			}
		}

		private void menuItemRemove_Click(object sender, EventArgs e)
		{
			List<ListViewItem> items = new List<ListViewItem>(pendingFileListView.SelectedItems.Count);
			foreach (ListViewItem item in pendingFileListView.SelectedItems)
			{
				if (!((ListItemTag)item.Tag).m_locked)
					items.Add(item);
			}

			foreach (ListViewItem item in items)
				pendingFileListView.Items.Remove(item);
		}

		private void menuItemOpenInEditor_Click(object sender, EventArgs e)
		{
			foreach(ListViewItem item in pendingFileListView.Items)
			{
				m_manager.OpenDocument(((ListItemTag)item.Tag).m_name);
			}
		}

		private void pendingFileListView_ItemActivate(object sender, EventArgs e)
		{
			m_manager.ShowDocument(((ListItemTag) (pendingFileListView.SelectedItems[0].Tag)).m_name);
		}
	}
}

