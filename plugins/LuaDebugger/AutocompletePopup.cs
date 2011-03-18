
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
using Tilde.Framework.Controls;

namespace Tilde.LuaDebugger
{
	public delegate void AutocompleteSelectionEventHandler(object sender, AutocompleteResult selection);

	public partial class AutocompletePopup : Form
	{
		DebugManager m_debugger;
		IWin32Window m_owner;
		string m_expr;
		string m_operator;
		string m_prefix;
		List<ListViewItem> m_options;

		public AutocompletePopup(DebugManager debugger, IWin32Window owner)
		{
			InitializeComponent();

			m_debugger = debugger;
			m_owner = owner;
			m_options = new List<ListViewItem>();
		}

		public event AutocompleteSelectionEventHandler Selection;


		public void Show(string expr, string op, string prefix, Point position)
		{
			waitingPanel.Visible = true;
			mainPanel.Visible = false;
			errorPanel.Visible = false;

			m_expr = expr + op;
			m_operator = op;
			m_prefix = prefix;

			valuesFilterButton.Checked = op != ":";
			functionsFilterButton.Checked = true;
			tablesFilterButton.Checked = op != ":";

			expressionLabel.Text = expr + op + prefix;
			optionsListView.Items.Clear();
			this.DesktopLocation = position;
			this.Show(m_owner);
			Win32.SetFocus(this.Handle);
			optionsListView.Focus();
		}

		public void SetOptions(AutocompleteResult [] values)
		{
			waitingPanel.Visible = false;
			mainPanel.Visible = true;
			errorPanel.Visible = false;

			m_options.Clear();
			foreach (AutocompleteResult value in values)
			{
				string label = m_debugger.GetValueString(value.m_key);
				ListViewItem item = new ListViewItem(label);
				item.SubItems.Add(value.m_valueType.ToString().Substring(0, 2));
				item.Tag = value;
				m_options.Add(item);
			}

			FilterOptions();

			optionsListView.Focus();
		}

		public void SetMessage(string message)
		{
			waitingPanel.Visible = false;
			mainPanel.Visible = false;
			errorPanel.Visible = true;

			errorLabel.Text = message;
		}

		private void optionsListView_ItemActivate(object sender, EventArgs e)
		{
			OnSelection((AutocompleteResult)(optionsListView.SelectedItems[0]).Tag);
			this.Hide();
		}

		void OnSelection(AutocompleteResult value)
		{
			if (Selection != null)
				Selection(this, value);
		}

		private void FilterOptions()
		{
			expressionLabel.Text = m_expr + m_prefix;

			ListViewItem selection = optionsListView.SelectedItems.Count == 0 ? null : optionsListView.SelectedItems[0];

			optionsListView.BeginUpdate();
			optionsListView.SelectedItems.Clear();
			optionsListView.Items.Clear();
			optionsListView.Sorting = SortOrder.None;
			foreach (ListViewItem item in m_options)
			{
				if (item.Text.IndexOf(m_prefix, StringComparison.InvariantCultureIgnoreCase) < 0)
					continue;

				AutocompleteResult info = (AutocompleteResult)item.Tag;
				if(info.m_valueType == LuaValueType.FUNCTION)
				{
					if (!functionsFilterButton.Checked)
						continue;
				}
				else if (info.m_valueType == LuaValueType.TABLE)
				{
					if(!tablesFilterButton.Checked)
						continue;
				}
				else if (!valuesFilterButton.Checked)
					continue;

				optionsListView.Items.Add(item);
			}
			optionsListView.Sorting = SortOrder.Ascending;

			if (selection == null || selection.ListView != optionsListView)
				selection = optionsListView.Items.Count == 0 ? null : optionsListView.Items[0];

			if(selection != null)
			{
				selection.Selected = true;
				selection.Focused = true;
				selection.EnsureVisible();
			}

			optionsListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

			if (optionsListView.Items.Count > 0)
			{
				int visibleItems = Math.Min(8, optionsListView.Items.Count);
				int width = optionsListView.Columns[0].Width;
				int height = visibleItems * optionsListView.Items[0].GetBounds(ItemBoundsPortion.Entire).Height;
				optionsListView.ClientSize = new Size(width, height);
				optionsListView.Width = width + 32;
			}

			optionsListView.EndUpdate();
		}

		private void AutocompletePopup_Deactivate(object sender, EventArgs e)
		{
			this.Hide();
		}

		private void optionsListView_KeyPress(object sender, KeyPressEventArgs e)
		{
			if(e.KeyChar == '\b')
			{
				if (m_prefix.Length > 0)
				{
					m_prefix = m_prefix.Substring(0, m_prefix.Length - 1);
					FilterOptions();
				}
				else
				{
					this.Hide();
				}
				e.Handled = true;
			}
			else if (!Char.IsControl(e.KeyChar))
			{
				m_prefix = m_prefix + e.KeyChar;
				FilterOptions();
				e.Handled = true;
			}
		}

		private void optionsListView_KeyDown(object sender, KeyEventArgs e)
		{
		}

		private void functionsFilterButton_Click(object sender, EventArgs e)
		{
			FilterOptions();
		}

		private void tablesFilterButton_Click(object sender, EventArgs e)
		{
			FilterOptions();
		}

		private void valuesFilterButton_Click(object sender, EventArgs e)
		{
			FilterOptions();
		}

		private void AutocompletePopup_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
				this.Hide();
		}
	}
}