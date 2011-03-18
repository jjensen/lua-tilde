
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

using Tilde.Framework.Controller;
using Tilde.Framework.View;

namespace Tilde.LuaDebugger
{
	[ToolWindowAttribute(Group = "Debug")]
	public partial class ThreadsWindow : Tilde.Framework.View.ToolWindow
	{
		class ListItemTag
		{
			public ListItemTag(ThreadDetails details, int index, int indent)
			{
				m_details = details;
				m_index = index;
				m_indent = indent;
			}

			public ThreadDetails m_details;
			public int m_index;
			public int m_indent;
		}

		class ListViewItemComparer : System.Collections.IComparer
		{
			public int m_sortColumn;
			public int m_sortOrder;

			public ListViewItemComparer()
			{
				m_sortColumn = 1;
				m_sortOrder = 1;
			}

			public int Compare(object x, object y)
			{
				ListViewItem lhsItem = x as ListViewItem;
				ListViewItem rhsItem = y as ListViewItem;
				ListItemTag lhsTag = lhsItem.Tag as ListItemTag;
				ListItemTag rhsTag = rhsItem.Tag as ListItemTag;
				ThreadDetails lhs = lhsTag.m_details;
				ThreadDetails rhs = rhsTag.m_details;

				switch (m_sortColumn)
				{
					case 0: return m_sortOrder * String.Compare(lhs.Name, rhs.Name);
					case 1: return m_sortOrder * ((int)lhs.ID - (int)rhs.ID);
					case 2: return m_sortOrder * String.Compare(lhs.Location, rhs.Location);
					case 3: return m_sortOrder * ((int)lhs.State - (int)rhs.State);
					case 4: return m_sortOrder * Math.Sign(lhs.PeakTime - rhs.PeakTime);
					case 5: return m_sortOrder * Math.Sign(lhs.AverageTime - rhs.AverageTime);
					case 6: return m_sortOrder * (lhs.StackSize - rhs.StackSize);
					case 7: return m_sortOrder * (lhs.StackUsed - rhs.StackUsed);
					default: return 0;
				}
			}
		}

		protected DebugManager mDebugger;
		private ListViewItemComparer m_comparer;
		private LuaValue m_breakedThread;
		private Font m_boldFont;

		public ThreadsWindow(IManager manager)
		{
			InitializeComponent();

			m_comparer = new ListViewItemComparer();

			threadListView.ListViewItemSorter = m_comparer;

			mDebugger = ((LuaPlugin)manager.GetPlugin(typeof(LuaPlugin))).Debugger;

			mDebugger.DebuggerConnected += new DebuggerConnectedEventHandler(Debugger_DebuggerConnected);
			mDebugger.DebuggerDisconnecting += new DebuggerDisconnectingEventHandler(Debugger_DebuggerDisconnecting);

			m_boldFont = new Font(threadListView.Font, FontStyle.Bold);
		}

		void Debugger_DebuggerDisconnecting(DebugManager sender, Target target)
		{
			threadListView.Items.Clear();

			target.ThreadUpdate -= new ThreadUpdateEventHandler(target_ThreadUpdate);
			target.StateUpdate -= new StateUpdateEventHandler(target_StateUpdate);
		}

		void Debugger_DebuggerConnected(DebugManager sender, Target target)
		{
			target.StateUpdate += new StateUpdateEventHandler(target_StateUpdate);
			target.ThreadUpdate += new ThreadUpdateEventHandler(target_ThreadUpdate);
		}

		void target_StateUpdate(Target sender, StateUpdateEventArgs args)
		{
			m_breakedThread = args.Thread;
			UpdateStates();
		}

		void SetItemBackColor(ListViewItem item, Color colour)
		{
			foreach(ListViewItem.ListViewSubItem subitem in item.SubItems)
			{
				subitem.BackColor = colour;
			}
		}

		void SetItemForeColor(ListViewItem item, Color colour)
		{
			foreach (ListViewItem.ListViewSubItem subitem in item.SubItems)
			{
				subitem.ForeColor = colour;
			}
		}

		void target_ThreadUpdate(Target sender, ThreadUpdateEventArgs args)
		{
			threadListView.BeginUpdate();
			threadListView.ListViewItemSorter = null;

			for (int index = 0; index < threadListView.Items.Count; )
			{
				ListViewItem item = threadListView.Items[index];
				if (item.BackColor == Color.LightPink)
				{
					threadListView.Items.RemoveAt(index);
				}
				else
				{
					SetItemForeColor(item, threadListView.ForeColor);
					SetItemBackColor(item, threadListView.BackColor);
					++index;
				}
			}

			foreach (ThreadDetails thread in args.Threads)
			{
				ListViewItem item = threadListView.Items[thread.Thread.ToString()];

				if (!thread.Valid)
				{
					if(item != null)
						SetItemBackColor(item, Color.LightPink);
				}
				else
				{

					if (item == null)
					{
						item = CreateItem(thread, threadListView.Items.Count, 0);
						threadListView.Items.Add(item);
						SetItemBackColor(item, Color.LightBlue);
						UpdateItem(item, thread, false);
					}
					else
					{
						UpdateItem(item, thread, true);
					}
				}
			}

			UpdateStates();
			UpdateSorting();
			threadListView.EndUpdate();
		}

		private void UpdateStates()
		{
			foreach (ListViewItem item in threadListView.Items)
			{
				ListItemTag tag = item.Tag as ListItemTag;
				if (tag.m_details.Thread.Equals(mDebugger.CurrentThread))
					item.ImageIndex = 0;
				else
					item.ImageIndex = -1;

				if (tag.m_details.Thread.Equals(m_breakedThread))
					item.StateImageIndex = 1;
				else
					item.StateImageIndex = -1;
			}
		}

		private void UpdateItem(ListViewItem item, ThreadDetails thread, bool modified)
		{
			((ListItemTag)item.Tag).m_details = thread;

			string[] subitem = new string[item.SubItems.Count];

			subitem[1] = thread.ID < 0 ? "--" : thread.ID.ToString();
			subitem[2] = thread.Location;
			subitem[3] = thread.State.ToString();
			subitem[4] = thread.PeakTime < 0 ? "--" : (thread.PeakTime).ToString("0.0000");
			subitem[5] = thread.AverageTime < 0 ? "--" : (thread.AverageTime).ToString("0.0000");
			subitem[6] = thread.StackSize < 0 ? "--" : thread.StackSize.ToString();
			subitem[7] = thread.StackUsed < 0 ? "--" : thread.StackUsed.ToString();

			if (modified)
			{
				for (int index = 1; index < item.SubItems.Count; ++index)
					if (item.SubItems[index].Text != subitem[index])
						item.SubItems[index].ForeColor = Color.Red;
			}

			item.SubItems[1].Text = subitem[1];
			item.SubItems[2].Text = subitem[2];
			item.SubItems[3].Text = subitem[3];
			item.SubItems[4].Text = subitem[4];
			item.SubItems[5].Text = subitem[5];
			item.SubItems[6].Text = subitem[6];
			item.SubItems[7].Text = subitem[7];
		}

		private ListViewItem CreateItem(ThreadDetails thread, int index, int indent)
		{
			//ListViewItem item = new ListViewItem(mDebugger.ValueCache.Get(thread.Thread));
			ListViewItem item = new ListViewItem(thread.Name);//"Thread " + thread.Thread.Value.ToString() + " (" + thread.Parent.Value.ToString() + ")");
			item.Name = thread.Thread.ToString();
			item.Tag = new ListItemTag(thread, index, indent);
			item.UseItemStyleForSubItems = false;

			item.SubItems.Add("");
			item.SubItems.Add("");
			item.SubItems.Add("");
			item.SubItems.Add("");
			item.SubItems.Add("");
			item.SubItems.Add("");
			item.SubItems.Add("");

			return item;
		}

		private void InsertChildren(List<KeyValuePair<ThreadDetails, ListViewItem> > items, long parent, int parentIndex, int indent)
		{
			foreach (KeyValuePair<ThreadDetails, ListViewItem> pair in items)
			{
				ThreadDetails thread = pair.Key;
				ListViewItem item = pair.Value;

				if (thread.Parent.Value == parent)
				{
					item.IndentCount = indent;
					threadListView.Items.Add(item);
					InsertChildren(items, thread.Thread.Value, item.Index, indent + 1);
				}
			}
		}

		private void UpdateHierarchy()
		{
			List<KeyValuePair<ThreadDetails, ListViewItem>> items = new List<KeyValuePair<ThreadDetails, ListViewItem>>();
			foreach(ListViewItem item in threadListView.Items)
			{
				items.Add(new KeyValuePair<ThreadDetails, ListViewItem>(((ListItemTag) item.Tag).m_details, item));
			}
			items.Sort(
				delegate(KeyValuePair<ThreadDetails, ListViewItem> lhs, KeyValuePair<ThreadDetails, ListViewItem> rhs)
				{
					return lhs.Key.ID - rhs.Key.ID;
				}
			);

			threadListView.BeginUpdate();
			threadListView.Items.Clear();
			InsertChildren(items, 0, -1, 0);
			threadListView.EndUpdate();
		}

		private void UpdateSorting()
		{
			if (m_comparer.m_sortColumn == 0 && m_comparer.m_sortOrder == 0)
			{
				threadListView.ListViewItemSorter = null;
				UpdateHierarchy();
			}
			else
			{
				threadListView.ListViewItemSorter = m_comparer;
				foreach (ListViewItem item in threadListView.Items)
				{
					item.IndentCount = 0;
				}
				threadListView.Sort();
			}
		}

		private void threadListView_ColumnClick(object sender, ColumnClickEventArgs e)
		{
			int order = m_comparer.m_sortOrder;
			if (e.Column == 0)
			{
				if (e.Column == m_comparer.m_sortColumn)
				{
					order = order + 1;
					if (order > 1)
						order = -1;
				}
				else
					order = 0;
			}
			else
			{
				if (e.Column == m_comparer.m_sortColumn)
					order = order * -1;
				else
					order = 1;
			}

			m_comparer.m_sortColumn = e.Column;
			m_comparer.m_sortOrder = order;
			UpdateSorting();
		}

		private void threadListView_ItemActivate(object sender, EventArgs e)
		{
			if (threadListView.SelectedItems.Count == 1 && mDebugger.CurrentStackFrame != null)
			{
				ListItemTag tag = threadListView.SelectedItems[0].Tag as ListItemTag;
				mDebugger.CurrentThread = tag.m_details.Thread;
			}
		}

		private void buttonRefresh_Click(object sender, EventArgs e)
		{
			if (mDebugger.ConnectedTarget != null)
				mDebugger.ConnectedTarget.RetrieveThreads();
		}

	}
}

