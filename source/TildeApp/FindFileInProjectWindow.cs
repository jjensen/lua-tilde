
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
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Runtime.InteropServices;

using Tilde.Framework.View;
using Tilde.Framework.Model;
using Tilde.Framework.Model.ProjectHierarchy;
using Tilde.Framework.Controls;

namespace Tilde.TildeApp
{
	public partial class FindFileInProjectWindow : Form
	{
		delegate bool WndProcMessageHandler(ref Message m);

		class MyTextBox : TextBox
		{
			public MyTextBox()
			{
			}

			public event WndProcMessageHandler WndProcMessage;

			protected override void WndProc(ref Message m)
			{
				if (WndProcMessage == null || !WndProcMessage(ref m))
					base.WndProc(ref m);
			}
		}

		MainWindow mMainWindow;
		List<ListViewItem> mFileItems;
		List<ListViewItem> mFilteredItems;
		int mSortColumn = 0;
		int mSortOrder = 1;

		ListViewItem mPopupItem;
		ListViewItem mSelectedItem;

		public FindFileInProjectWindow(MainWindow mainWindow)
		{
			InitializeComponent();

			((MyTextBox) textBoxSearchString).WndProcMessage += new WndProcMessageHandler(textBoxSearchString_WndProcMessage);

			this.DoubleBuffered = true;

			mMainWindow = mainWindow;

			mFileItems = new List<ListViewItem>();
			mFilteredItems = new List<ListViewItem>();

			mMainWindow.Manager.ProjectOpened += new Tilde.Framework.Controller.ProjectOpenedEventHandler(Manager_ProjectOpened);
		}


		bool textBoxSearchString_WndProcMessage(ref Message m)
		{
			if (m.Msg == Win32.WM_KEYDOWN && ((Keys)m.WParam == Keys.Return || (Keys)m.WParam == Keys.Up || (Keys)m.WParam == Keys.Down || (Keys)m.WParam == Keys.PageDown || (Keys)m.WParam == Keys.PageUp))
			{
				HandleRef hnd = new HandleRef(listViewFiles, listViewFiles.Handle);

				Win32.SendMessage(hnd, (uint)m.Msg, m.WParam, m.LParam);
				return true;
			}

			return false;
		}

		void Manager_ProjectOpened(object sender, Project project)
		{
			UpdateFileList(project);
			project.ItemAdded += new ProjectItemAddedHandler(Project_ItemAdded);
			project.ItemRemoved += new ProjectItemRemovedHandler(Project_ItemRemoved);
			project.ProjectReloaded += new ProjectReloadedHandler(Project_ProjectReloaded);
		}

		void Project_ItemRemoved(Project sender, ProjectItem removedItem, ProjectItem itemParent)
		{
			UpdateFileList(sender);
		}

		void Project_ItemAdded(Project sender, ProjectItem addedItem)
		{
			UpdateFileList(sender);
		}

		void Project_ProjectReloaded(Project sender, ProjectDocumentItem reloadedItem)
		{
			UpdateFileList(sender);
		}

		private void UpdateFileList(Project project)
		{
			mFileItems.Clear();
			UpdateFileList(project, project.RootItem);

			FilterFileList();
		}

		private void UpdateFileList(Project project, ProjectItem root)
		{
			foreach(ProjectItem item in root.Items)
			{
				if(item is DocumentItem)
				{
					AddFile(project, item as DocumentItem);
				}
				UpdateFileList(project, item);
			}
		}

		private void AddFile(Project project, DocumentItem documentItem)
		{
			ListViewItem listItem = new ListViewItem(Path.GetFileName(documentItem.RelativeFileName));
			listItem.Tag = documentItem;
			listItem.SubItems.Add(documentItem.AbsoluteFileName);

			mFileItems.Add(listItem);
		}

		private void FindFileInProjectWindow_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (e.CloseReason == CloseReason.UserClosing)
			{
				e.Cancel = true;
				this.Hide();
			}
		}

		private void listViewFiles_ItemActivate(object sender, EventArgs e)
		{
			DocumentItem item = mSelectedItem.Tag as DocumentItem;
			mMainWindow.Manager.ShowDocument(item);
			this.Hide();
		}

		private void SelectItem(ListViewItem item)
		{
			listViewFiles.SelectedIndices.Clear();
			for (int index = 0; index < mFilteredItems.Count; ++index)
			{
				if (mFilteredItems[index] == item)
				{
					item = listViewFiles.Items[index];
					item.Selected = true;
					item.Focused = true;
					listViewFiles.EnsureVisible(index);
					break;
				}
			}
		}

		private void textBoxSearchString_TextChanged(object sender, EventArgs e)
		{
			FilterFileList();

			Regex regex = new Regex("^" + Regex.Escape(textBoxSearchString.Text).Replace("\\*", ".*").Replace("\\?", "."), RegexOptions.IgnoreCase);

			foreach (ListViewItem item in mFilteredItems)
			{
				if(regex.IsMatch(item.Text))
				{
					SelectItem(item);
					break;
				}
			}

			if (mSelectedItem == null && mFilteredItems.Count > 0)
				SelectItem(mFilteredItems[0]);
		}

		private int CompareListItems(ListViewItem lhsItem, ListViewItem rhsItem)
		{
			DocumentItem lhs = lhsItem.Tag as DocumentItem;
			DocumentItem rhs = rhsItem.Tag as DocumentItem;

			switch (mSortColumn)
			{
				case 0: return mSortOrder * String.Compare(lhsItem.Text, rhsItem.Text);
				case 1: return mSortOrder * String.Compare(lhsItem.SubItems[1].Text, rhsItem.SubItems[1].Text);
				default: return 0;
			}
		}

		private void FilterFileList()
		{
			Regex regex = new Regex(Regex.Escape(textBoxSearchString.Text).Replace("\\*", ".*").Replace("\\?", "."), RegexOptions.IgnoreCase);

			ListViewItem oldSelection = mSelectedItem;

			listViewFiles.SelectedIndices.Clear();
			mFilteredItems.Clear();
			foreach(ListViewItem item in mFileItems)
			{
				DocumentItem docItem = item.Tag as DocumentItem;
				if (regex.IsMatch(docItem.RelativeFileName))
				{
					mFilteredItems.Add(item);
				}
			}

			mFilteredItems.Sort(CompareListItems);

			listViewFiles.VirtualListSize = mFilteredItems.Count;
			listViewFiles.Refresh();
			if (oldSelection != null)
				SelectItem(oldSelection);
		}

		private void buttonClose_Click(object sender, EventArgs e)
		{
			this.Hide();
		}

		private void exploreToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DocumentItem item = mSelectedItem.Tag as DocumentItem;

			Process proc = new Process();
			proc.StartInfo.FileName = Path.GetDirectoryName(mSelectedItem.SubItems[1].Text);
			proc.StartInfo.UseShellExecute = true;
			proc.Start();
		}

		private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
		{
			mPopupItem = mSelectedItem;
		}

		private void FindFileInProjectWindow_Shown(object sender, EventArgs e)
		{
			textBoxSearchString.Select();
			textBoxSearchString.SelectAll();
		}

		private void FindFileInProjectWindow_VisibleChanged(object sender, EventArgs e)
		{
			textBoxSearchString.Select();
			textBoxSearchString.SelectAll();
		} 

		private void listViewFiles_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
		{
			e.Item = mFilteredItems[e.ItemIndex];
		}

		private void listViewFiles_VirtualItemsSelectionRangeChanged(object sender, ListViewVirtualItemsSelectionRangeChangedEventArgs e)
		{
			if (e.IsSelected)
				mSelectedItem = mFilteredItems[e.StartIndex];
			else
				mSelectedItem = null;
		}

		private void listViewFiles_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
		{
			if (e.IsSelected)
				mSelectedItem = mFilteredItems[e.ItemIndex];
		}

		private void listViewFiles_SearchForVirtualItem(object sender, SearchForVirtualItemEventArgs e)
		{

		}

		private void listViewFiles_ColumnClick(object sender, ColumnClickEventArgs e)
		{
			if (mSortColumn == e.Column)
				mSortOrder *= -1;
			else
			{
				mSortColumn = e.Column;
				mSortOrder = 1;
			}
			FilterFileList();
		}


	}
}