
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
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using Tilde.Framework.Model;

namespace Tilde.Framework.Controls
{
	public class TreeTableNode
	{
		private TreeTable m_owner = null;
		private TreeTableNode m_parent = null;
		private TreeTableNodeCollection m_items;
		private string m_key = "";
		private object m_tag = null;
		private int m_virtualIndex = -1;
		private bool m_visible = true;
		private bool m_expandable = false;
		private bool m_expanded = false;
		private int m_depth = 0;
		private ListViewItem m_listItem;

		private TreeTableSubItemCollection m_subItems;

		public TreeTableNode()
		{
			m_items = new TreeTableNodeCollection(this);
			m_subItems = new TreeTableSubItemCollection(this);
			m_subItems.Add(new TreeTableSubItem());
		}

		public TreeTableNode(string label)
		{
			m_items = new TreeTableNodeCollection(this);
			m_subItems = new TreeTableSubItemCollection(this);
			m_subItems.Add(new TreeTableSubItem(label));
		}

		public TreeTable Owner
		{
			get { return m_owner; }
		}

		public TreeTableNode Parent
		{
			get { return m_parent; }
		}

		public TreeTableNodeCollection Items
		{
			get { return m_items; }
		}

		public int VirtualIndex
		{
			get { return m_virtualIndex; }
		}

		public string Key
		{
			get { return m_key; }
			set
			{
				if(m_key != value)
				{
					string oldKey = m_key;
					m_key = value;
					if (m_owner != null)
						m_owner.NodeChangedKey(this, oldKey);
				}
			}
		}

		public object Tag
		{
			get { return m_tag; }
			set { m_tag = value; }
		}

		public bool Visible
		{
			get { return m_visible; }
			set 
			{ 
				if (m_visible != value) 
				{ 
					m_visible = value; 
					if (m_owner != null) 
						m_owner.NodeChangedVisible(this); 
				} 
			}
		}

		public bool Expandable
		{
			get { return m_expandable; }
			set
			{
				if (m_expandable != value)
				{
					m_expandable = value;
					if(m_owner != null)
						m_owner.NodeChangedExpandable(this);
				}
			}
		}

		public bool Expanded
		{
			get { return m_expanded; }
			set 
			{ 
				if (m_expanded != value) 
				{ 
					m_expanded = value;
					if (m_owner != null)
						m_owner.NodeChangedExpanded(this); 
				} 
			}
		}

		internal int Depth
		{
			get { return m_depth; }
			set { m_depth = value; }
		}

		internal ListViewItem ListItem
		{
			get { return m_listItem; }
			set { m_listItem = value; }
		}

		public TreeTableSubItemCollection SubItems
		{
			get { return m_subItems; }
			set { m_subItems = value; }
		}

		public string Text
		{
			get 
			{ 
				if(m_subItems.Count == 0)
					return "";
				else
					return m_subItems[0].Text;
			}
			set 
			{
				if (m_subItems.Count == 0)
					m_subItems.Add(new TreeTableSubItem(value));
				else
					m_subItems[0].Text = value;
			}
		}

		public Color ForeColor
		{
			get
			{
				if (m_subItems.Count == 0)
					return Color.Black;
				else
					return m_subItems[0].ForeColor;
			}
			set
			{
				m_subItems[0].ForeColor = value;
			}
		}

		public Font Font
		{
			get
			{
				if (m_subItems.Count == 0)
					return null;
				else
					return m_subItems[0].Font;
			}
			set
			{
				m_subItems[0].Font = value;
			}
		}

		public void BeginEdit(int subitem)
		{
			if (m_listItem == null)
			{
				ListViewItem item = m_owner.ListView.Items[this.VirtualIndex];
			}

			if (subitem == 0)
				m_listItem.BeginEdit();
		}

		public void ForEach(Action<TreeTableNode> action)
		{
			foreach(TreeTableNode child in m_items)
			{
				action(child);
				child.ForEach(action);
			}
		}

		internal void InternalSetOwner(TreeTable owner)
		{
			m_owner = owner;
		}

		internal void InternalSetParent(TreeTableNode parent)
		{
			m_parent = parent;
			if (parent == null)
				m_owner = null;
			else
				m_owner = parent.Owner;
		}

		internal void InternalSetVirtualIndex(int index)
		{
			m_virtualIndex = index;
		}
	}

	public class TreeTableNodeCollection : ListCollectionBase<TreeTableNode>
	{
		private TreeTableNode m_owner;

		public TreeTableNodeCollection(TreeTableNode node)
		{
			m_owner = node;
		}

		protected override void OnRemove(TreeTableNode item)
		{
			item.InternalSetParent(null);
			if (m_owner.Owner != null)
				m_owner.Owner.NodeRemoved(item);
		}

		protected override void OnAdd(TreeTableNode item)
		{
			if (item.Parent != null)
				throw new ArgumentException("A TreeTableNode can only be inserted into one TreeTableNode at a time");

			item.InternalSetParent(m_owner);
			if (m_owner.Owner != null)
				m_owner.Owner.NodeAdded(item);
		}
	}
}
