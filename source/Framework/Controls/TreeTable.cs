
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
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Tilde.Framework.Controls
{
	public delegate void TreeTableNodeOpenedDelegate(TreeTableNode node);
	public delegate void TreeTableNodeClosedDelegate(TreeTableNode node);

	public partial class TreeTable : UserControl
	{
		private TreeTableNode m_root;
		private List<TreeTableNode> m_nodes;
		private Dictionary<string, TreeTableNode> m_keyhash;
		private bool m_updateDirty = false;
		private bool m_updateInProgress = false;

		public TreeTable()
		{
			InitializeComponent();

			m_root = new TreeTableNode();
			m_root.InternalSetOwner(this);

			m_nodes = new List<TreeTableNode>();
			m_keyhash = new Dictionary<string, TreeTableNode>();

			listView1.BeforeLabelEdit += new LabelEditEventHandler(listView1_BeforeLabelEdit);
			listView1.AfterLabelEdit += new LabelEditEventHandler(listView1_AfterLabelEdit);
		}

		public event TreeTableNodeOpenedDelegate NodeOpened;
		public event TreeTableNodeClosedDelegate NodeClosed;
		public event LabelEditEventHandler BeforeLabelEdit;
		public event LabelEditEventHandler AfterLabelEdit;


		[DefaultValue("")]
		public ImageList SmallImageList
		{
			get { return listView1.SmallImageList; }
			set { listView1.SmallImageList = value; }
		}

		[DefaultValue(false)]
		public bool FullRowSelect
		{
			get { return listView1.FullRowSelect; }
			set { listView1.FullRowSelect = value; }
		}

		[DefaultValue(false)]
		public bool GridLines
		{
			get { return listView1.GridLines; }
			set { listView1.GridLines = value; }
		}

		[DefaultValue(false)]
		public bool LabelEdit
		{
			get { return listView1.LabelEdit; }
			set { listView1.LabelEdit = value; }
		}

		public ItemActivation Activation
		{
			get { return listView1.Activation; }
			set { listView1.Activation = value; }
		}

		public ListView.SelectedIndexCollection SelectedIndices
		{
			get { return listView1.SelectedIndices; }
		}

		[MergableProperty(false)]
		[Localizable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Editor("System.Windows.Forms.Design.ColumnHeaderCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(System.Drawing.Design.UITypeEditor))]
		public ListView.ColumnHeaderCollection Columns
		{
			get { return listView1.Columns; }
		}

		public TreeTableNode Root
		{
			get { return m_root; }
		}

		public List<TreeTableNode> Nodes
		{
			get { return m_nodes; }
		}

		public bool ContainsKey(string key)
		{
			return m_keyhash.ContainsKey(key);
		}

		public TreeTableNode this[string key]
		{
			get
			{
				TreeTableNode result;
				if (m_keyhash.TryGetValue(key, out result))
					return result;
				else
					return null;
			}
		}

		internal ListView ListView
		{
			get { return listView1; }
		}

		public void BeginUpdate()
		{
			m_updateDirty = false;
			m_updateInProgress = true;
		}

		public void EndUpdate()
		{
			m_updateInProgress = false;
			if (m_updateDirty)
			{
				UpdateItems();
				m_updateDirty = false;
			}
		}

		public ListViewHitTestInfo HitTest(Point point)
		{
			return listView1.HitTest(point);
		}

		public ListViewHitTestInfo HitTest(int x, int y)
		{
			return listView1.HitTest(x, y);
		}

		// Walk the tree and assign indexes to every item
		public void UpdateItems()
		{
			if (m_updateInProgress)
				m_updateDirty = true;
			else
			{
				TreeTableNode selItem = listView1.FocusedItem == null ? null : m_nodes[listView1.FocusedItem.Index]; //listView1.SelectedIndices.Count == 0 ? null : m_nodes[listView1.SelectedIndices[0]];
				TreeTableNode topItem = listView1.TopItem == null ? null : m_nodes[listView1.TopItem.Index];

				listView1.BeginUpdate();
				listView1.SelectedIndices.Clear();
				m_nodes.Clear();

				UpdateItemsRecursive(m_root, 0, 0);
				listView1.VirtualListSize = m_nodes.Count;

				if (selItem != null)
				{
					while (selItem.VirtualIndex < 0 && selItem != m_root)
						selItem = selItem.Parent;

					if (selItem != m_root && m_nodes.Contains(selItem))
					{
						ListViewItem item = listView1.Items[selItem.VirtualIndex];
						item.Focused = true;
						item.Selected = true;
						listView1.EnsureVisible(item.Index);
					}
				}

				if(topItem != null)
				{
					while(topItem.VirtualIndex < 0 && topItem != m_root)
						topItem = topItem.Parent;

					if (topItem != m_root && m_nodes.Contains(topItem))
					{
						ListViewItem item = listView1.Items[topItem.VirtualIndex];
						listView1.TopItem = item;
					}
				}

				listView1.EndUpdate();
			}
		}

		private int UpdateItemsRecursive(TreeTableNode root, int nextIndex, int depth)
		{
			foreach (TreeTableNode child in root.Items)
			{
				if (!child.Visible)
				{
					child.InternalSetVirtualIndex(-1);
					HideItemsRecursive(child);
				}
				else
				{
					child.Depth = depth;
					child.InternalSetVirtualIndex(nextIndex++);
					m_nodes.Add(child);
					if (child.Expandable && child.Expanded)
						nextIndex = UpdateItemsRecursive(child, nextIndex, depth + 1);
					else
						HideItemsRecursive(child);
				}
			}

			return nextIndex;
		}

		private void HideItemsRecursive(TreeTableNode root)
		{
			foreach (TreeTableNode child in root.Items)
			{
				child.InternalSetVirtualIndex(-1);
				HideItemsRecursive(child);
			}
		}

		private void TreeTable_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
		{
			//e.Item = m_nodes[e.ItemIndex].Item;

			TreeTableNode node = m_nodes[e.ItemIndex];
			ListViewItem item = new ListViewItem();
			for(int index = 0; index < node.SubItems.Count; ++ index)
			{
				TreeTableSubItem subnode = node.SubItems[index];
				ListViewItem.ListViewSubItem subitem = index == 0 ? item.SubItems[0] : new ListViewItem.ListViewSubItem();

				subitem.Text = subnode.Text;
				if (subnode.ForeColor != null) subitem.ForeColor = subnode.ForeColor;
				if (subnode.Font != null) subitem.Font = subnode.Font;

				if (index > 0)
					item.SubItems.Add(subitem);
			}

			if (!node.Expandable)
			{
				item.ImageIndex = -1;
			}
			else if (node.Expanded)
			{
				item.ImageIndex = 1;
			}
			else
			{
				item.ImageIndex = 0;
			}

			item.Tag = node;
			item.IndentCount = node.Depth;

			node.ListItem = item;

			e.Item = item;
		}

		private void TreeTable_Click(object sender, EventArgs e)
		{
			Point hitPos = listView1.PointToClient(Control.MousePosition);
			ListViewHitTestInfo info = listView1.HitTest(hitPos.X, hitPos.Y);

			if (info.Item != null && info.Location == ListViewHitTestLocations.Image)
			{
				TreeTableNode node = (TreeTableNode)info.Item.Tag;
				if (node.Expandable)
					node.Expanded = !node.Expanded;
			}
		}

		private void TreeTable_KeyDown(object sender, KeyEventArgs e)
		{
			TreeTableNode selNode;
			if (listView1.VirtualListSize == 0)
				selNode = null;
			else if (listView1.SelectedIndices.Count > 0)
				selNode = m_nodes[listView1.SelectedIndices[0]];
			else
				selNode = m_nodes[0];

			if (selNode != null)
			{
				if (e.KeyCode == Keys.Left)
				{
					if (selNode.Expanded)
					{
						selNode.Expanded = false;
					}
					else
					{
						TreeTableNode newSelNode = selNode.Parent;
						if (newSelNode != null && newSelNode != m_root)
						{
							listView1.SelectedIndices.Clear();
							newSelNode.ListItem.Selected = true;
							newSelNode.ListItem.Focused = true;
							listView1.EnsureVisible(newSelNode.VirtualIndex);
						}
					}
					e.Handled = true;
				}
				else if (e.KeyCode == Keys.Right)
				{
					if (selNode.Expandable && !selNode.Expanded)
						selNode.Expanded = true;

					e.Handled = true;
				}
			}
		}

		void listView1_BeforeLabelEdit(object sender, LabelEditEventArgs e)
		{
			if (BeforeLabelEdit != null)
				BeforeLabelEdit(sender, e);
		}

		void listView1_AfterLabelEdit(object sender, LabelEditEventArgs e)
		{
			if (AfterLabelEdit != null)
				AfterLabelEdit(sender, e);
		}

		private void OnNodeOpened(TreeTableNode node)
		{
			if (!m_updateInProgress && NodeOpened != null)
				NodeOpened(node);
		}

		private void OnNodeClosed(TreeTableNode node)
		{
			if (!m_updateInProgress && NodeClosed != null)
				NodeClosed(node);
		}

		internal void NodeChangedKey(TreeTableNode item, string oldKey)
		{
			if (oldKey != "")
			{
				System.Diagnostics.Debug.Assert(m_keyhash.ContainsKey(oldKey));
				m_keyhash.Remove(oldKey);
			}

			if (item.Key != "")
			{
				System.Diagnostics.Debug.Assert(!m_keyhash.ContainsKey(oldKey));
				m_keyhash.Add(item.Key, item);
			}
		}

		internal void NodeChangedVisible(TreeTableNode item)
		{
			UpdateItems();
		}

		internal void NodeChangedExpanded(TreeTableNode item)
		{
			if (item.Expanded)
				OnNodeOpened(item);
			else
				OnNodeClosed(item);

			UpdateItems();
		}

        internal void AddToHashRecursive(TreeTableNode root)
        {
			if (root.Key != "")
			{
				System.Diagnostics.Debug.Assert(!m_keyhash.ContainsKey(root.Key));
				m_keyhash.Add(root.Key, root);
			}

            foreach (TreeTableNode node in root.Items)
            {
                AddToHashRecursive(node);
            }
        }

        internal void RemoveFromHashRecursive(TreeTableNode root)
        {
			if (root.Key != "")
			{
				System.Diagnostics.Debug.Assert(m_keyhash.ContainsKey(root.Key));
				m_keyhash.Remove(root.Key);
			}

            foreach (TreeTableNode node in root.Items)
            {
                RemoveFromHashRecursive(node);
            }
        }

		internal void NodeAdded(TreeTableNode item)
		{
            AddToHashRecursive(item);
			UpdateItems();
		}

		internal void NodeRemoved(TreeTableNode item)
		{
            RemoveFromHashRecursive(item);
			UpdateItems();
		}

		internal void NodeReplaced(TreeTableNode oldItem, TreeTableNode newItem)
		{
            RemoveFromHashRecursive(oldItem);
            AddToHashRecursive(newItem);
			UpdateItems();
		}

		internal void NodeChangedExpandable(TreeTableNode node)
		{
			InvalidateNode(node);
		}

		internal void NodeChangedSubItem(TreeTableNode node)
		{
			InvalidateNode(node);
		}

		internal void InvalidateNode(TreeTableNode node)
		{
			if (node.VirtualIndex >= 0)
			{
				if (m_updateInProgress)
					m_updateDirty = true;
				else
				{
					Rectangle bounds = listView1.GetItemRect(node.VirtualIndex, ItemBoundsPortion.Entire);
					listView1.Invalidate(bounds);
				}
			}

		}
	}
}
