
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
using Tilde.Framework.View;
using Tilde.Framework.Controller;
using System.IO;

// Invokes should be done asynchronously here so we don't deadlock (the FileSystemWatcher is implictly locked
// while an event is signalled). If the FileSystemWatcher is Dispose'd() by the user (via app shutdown) we deadlock!

namespace Tilde.TildeApp
{
	[ToolWindowAttribute]
	public partial class ExternalModificationsLogWindow : Tilde.Framework.View.ToolWindow
	{
		IManager m_manager;

		public ExternalModificationsLogWindow(IManager manager)
		{
			InitializeComponent();

			m_manager = manager;

			m_manager.FileWatcher.FileModified += new FileModifiedEventHandler(FileSystemWatcher_Changed);
			m_manager.FileWatcher.FileDeleted += new FileDeletedEventHandler(FileSystemWatcher_Deleted);
			m_manager.FileWatcher.FileAttributesChanged += new FileAttributesChangedEventHandler(FileAttributeWatcher_Changed);
		}

		void FileSystemWatcher_Deleted(object sender, string fileName)
		{
			if (listView.IsHandleCreated)
			{
				ListViewItem item = new ListViewItem(DateTime.Now.ToLongTimeString());
				item.SubItems.Add("Deleted");
				item.SubItems.Add(fileName);
				this.BeginInvoke(new MethodInvoker(delegate() { listView.Items.Add(item); }));
			}
		}

		void FileSystemWatcher_Changed(object sender, string fileName)
		{
			if (listView.IsHandleCreated)
			{
				ListViewItem item = new ListViewItem(DateTime.Now.ToLongTimeString());
				item.SubItems.Add("Modified");
				item.SubItems.Add(fileName);
				this.BeginInvoke(new MethodInvoker(delegate() { listView.Items.Add(item); }));
			}
		}

		void FileAttributeWatcher_Changed(object sender, string fileName, FileAttributes oldAttr, FileAttributes newAttr)
		{
			if (listView.IsHandleCreated)
			{
				string attrs = "Unknown";
				try
				{
					attrs = (oldAttr ^ newAttr).ToString();
				}
				catch(Exception)
				{
				}
				ListViewItem item = new ListViewItem(DateTime.Now.ToLongTimeString());
				item.SubItems.Add("Modified [" + attrs + "]");
				item.SubItems.Add(fileName);
				this.BeginInvoke(new MethodInvoker(delegate() { listView.Items.Add(item); }));
			}
		}
	}
}

