
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
using Tilde.Framework.Model;
using Tilde.Framework.View;

using System.Text.RegularExpressions;

namespace Tilde.LuaDebugger
{
	public partial class LuaProfileResultsView : Tilde.Framework.View.DocumentView
	{
		class ListViewItemComparer : System.Collections.IComparer
		{
			public int m_sortColumn;
			public int m_sortOrder;

			public ListViewItemComparer()
			{
				m_sortColumn = 0;
				m_sortOrder = 1;
			}

			public int Compare(object x, object y)
			{
				ListViewItem lhsItem = x as ListViewItem;
				ListViewItem rhsItem = y as ListViewItem;
				LuaProfileEntry lhs = lhsItem.Tag as LuaProfileEntry;
				LuaProfileEntry rhs = rhsItem.Tag as LuaProfileEntry;

				switch (m_sortColumn)
				{
					case 0: return m_sortOrder * String.Compare(lhs.m_function, rhs.m_function);
					case 1:
					{
						int result = String.Compare(lhs.m_file, rhs.m_file);
						if (result == 0)
							result = lhs.m_line - rhs.m_line;
						return m_sortOrder * result;
					}
					case 2: return m_sortOrder * String.Compare(lhs.m_language, rhs.m_language);
					case 3: return m_sortOrder * (lhs.m_count - rhs.m_count);
					case 4: return m_sortOrder * Math.Sign(lhs.m_timeSelf - rhs.m_timeSelf);
					case 5: return m_sortOrder * Math.Sign(lhs.m_timeChildren - rhs.m_timeChildren);
					case 6: return m_sortOrder * Math.Sign((lhs.m_timeSelf + lhs.m_timeChildren) - (rhs.m_timeSelf + rhs.m_timeChildren));
					default: return 0;
				}
			}
		}

		enum DisplayMode
		{
			Seconds,
			Milliseconds,
			Percentages
		}

		private ListViewItemComparer m_comparer;
		private int m_totalCount;
		private double m_totalTime;
		private double m_totalTimeChildren;
		private DisplayMode m_displayMode;

		public LuaProfileResultsView(IManager manager, Document doc)
			: base(manager, doc)
		{
			InitializeComponent();

			m_comparer = new ListViewItemComparer();
			profileListView.ListViewItemSorter = m_comparer;
			m_displayMode = DisplayMode.Seconds;

			m_totalTime = 0;
			LuaProfileResultsDocument profileDoc = (LuaProfileResultsDocument) Document;
			foreach (LuaProfileEntry entry in profileDoc.Functions)
			{
				m_totalCount += entry.m_count;
				m_totalTime += entry.m_timeSelf;
				m_totalTimeChildren += entry.m_timeChildren;
			}

			toolStripComboBox1.Text = m_displayMode.ToString();

			UpdateList();
		}

		private void UpdateList()
		{
			LuaProfileResultsDocument doc = (LuaProfileResultsDocument) Document;

			profileListView.BeginUpdate();
			profileListView.Items.Clear();

			foreach(LuaProfileEntry entry in doc.Functions)
			{
				ListViewItem item = new ListViewItem(entry.m_function);
				item.Tag = entry;

				if(entry.m_line > 0)
					item.SubItems.Add(entry.m_file + ":" + entry.m_line);
				else
					item.SubItems.Add(entry.m_file);

				item.SubItems.Add(entry.m_language);

				if (m_displayMode == DisplayMode.Percentages)
				{
					item.SubItems.Add(((double)entry.m_count / (double)m_totalCount).ToString("0.0000%"));
					item.SubItems.Add((entry.m_timeSelf / m_totalTime).ToString("0.0000%"));
					item.SubItems.Add((entry.m_timeChildren / m_totalTimeChildren).ToString("0.0000%"));
					item.SubItems.Add(((entry.m_timeSelf + entry.m_timeChildren) / (m_totalTime + m_totalTimeChildren) ).ToString("0.0000%"));
				}
				else if (m_displayMode == DisplayMode.Milliseconds)
				{
					item.SubItems.Add(entry.m_count.ToString());
					item.SubItems.Add((entry.m_timeSelf * 1000.0).ToString("0.00000"));
					item.SubItems.Add((entry.m_timeChildren * 1000.0).ToString("0.00000"));
					item.SubItems.Add(((entry.m_timeSelf + entry.m_timeChildren) * 1000.0).ToString("0.00000"));
				}
				else
				{
					item.SubItems.Add(entry.m_count.ToString());
					item.SubItems.Add((entry.m_timeSelf).ToString("0.00000"));
					item.SubItems.Add((entry.m_timeChildren).ToString("0.00000"));
					item.SubItems.Add(((entry.m_timeSelf + entry.m_timeChildren)).ToString("0.00000"));
				}

				profileListView.Items.Add(item);
			}

			profileListView.Sort();
			profileListView.EndUpdate();
		}

		private void profileListView_ColumnClick(object sender, ColumnClickEventArgs e)
		{
			if (e.Column == m_comparer.m_sortColumn)
				m_comparer.m_sortOrder = m_comparer.m_sortOrder * -1;
			else
				m_comparer.m_sortOrder = 1;

			m_comparer.m_sortColumn = e.Column;
			profileListView.Sort();
		}

		private void profileListView_ItemActivate(object sender, EventArgs e)
		{
			if(profileListView.SelectedItems.Count > 0)
			{
				LuaProfileEntry entry = profileListView.SelectedItems[0].Tag as LuaProfileEntry;
				if (entry.m_language == "Lua")
				{
					Document doc = Manager.OpenDocument(entry.m_file);
					if (doc != null)
					{
						DocumentView docView = Manager.ShowDocument(doc);

						Tilde.LuaDebugger.LuaScriptView luaView = docView as Tilde.LuaDebugger.LuaScriptView;

						if (luaView != null)
						{
							luaView.ShowLine(entry.m_line);
						}
					}
				}
			}
		}

		private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			m_displayMode = (DisplayMode) Enum.Parse(typeof(DisplayMode), toolStripComboBox1.Text);
			UpdateList();
		}

		private void profileListView_SelectedIndexChanged(object sender, EventArgs e)
		{
			int count = 0;
			double self = 0;
			double children = 0;
			foreach(ListViewItem item in profileListView.SelectedItems)
			{
				LuaProfileEntry entry = item.Tag as LuaProfileEntry;
				count += entry.m_count;
				self += entry.m_timeSelf;
				children += entry.m_timeChildren;
			}

			string countStr, selfStr, childrenStr, totalTimeStr;

			if (m_displayMode == DisplayMode.Percentages)
			{
				countStr =		((double)count / (double)m_totalCount).ToString("0.0000%");
				selfStr =		(self / m_totalTime).ToString("0.0000%");
				childrenStr =	(children / m_totalTimeChildren).ToString("0.0000%");
				totalTimeStr =	((self + children) / (m_totalTime + m_totalTimeChildren) ).ToString("0.0000%");
			}
			else if (m_displayMode == DisplayMode.Milliseconds)
			{
				countStr =		count.ToString();
				selfStr =		(self * 1000.0).ToString("0.00000");
				childrenStr =	(children * 1000.0).ToString("0.00000");
				totalTimeStr =	((self + children) * 1000.0).ToString("0.00000");
			}
			else
			{
				countStr = count.ToString();
				selfStr = (self).ToString("0.00000");
				childrenStr = (children).ToString("0.00000");
				totalTimeStr = ((self + children)).ToString("0.00000");
			}

			Manager.SetStatusMessage(
				String.Format("TOTAL Count={0}, Self={1}, Children={2}, Total Time={3}", countStr, selfStr, childrenStr, totalTimeStr),
				0f);
		}
	}
}

