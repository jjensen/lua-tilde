
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
using System.Drawing;

namespace Tilde.Framework.Controls
{
	public class TreeTableSubItem
	{
		private TreeTableNode m_owner;
		private string m_text = "";
		private Color m_foreColor;
		private Font m_font;

		public TreeTableSubItem()
		{
		}

		public TreeTableSubItem(string label)
		{
			m_text = label;
		}

		public string Text
		{
			get { return m_text; }
			set
			{
				if (m_text != value)
				{
					m_text = value;
					if (m_owner != null && m_owner.Owner != null)
						m_owner.Owner.NodeChangedSubItem(m_owner);
				}
			}
		}

		public Color ForeColor
		{
			get { return m_foreColor; }
			set { m_foreColor = value; }
		}

		public Font Font
		{
			get { return m_font; }
			set { m_font = value; }
		}

		internal TreeTableNode Owner
		{
			get { return m_owner; }
			set { m_owner = value; }
		}
	}

	public class TreeTableSubItemCollection : IList<TreeTableSubItem>
	{
		private TreeTableNode m_owner;
		private List<TreeTableSubItem> m_list;

		public TreeTableSubItemCollection(TreeTableNode owner)
		{
			m_owner = owner;
			m_list = new List<TreeTableSubItem>();
		}

		private void OnRemove(TreeTableSubItem item)
		{
			item.Owner = null;
			if (m_owner.Owner != null)
				m_owner.Owner.NodeChangedSubItem(m_owner);
		}

		private void OnAdd(TreeTableSubItem item)
		{
			item.Owner = m_owner;
			if (m_owner.Owner != null)
				m_owner.Owner.NodeChangedSubItem(m_owner);
		}

		#region IList<TreeTableSubItemCollection> Members

		public int IndexOf(TreeTableSubItem item)
		{
			return m_list.IndexOf(item);
		}

		public void Insert(int index, TreeTableSubItem item)
		{
			m_list.Insert(index, item);
			OnAdd(item);
		}

		public void RemoveAt(int index)
		{
			TreeTableSubItem item = m_list[index];
			m_list.RemoveAt(index);
			OnRemove(item);
		}

		public TreeTableSubItem this[int index]
		{
			get
			{
				return m_list[index];
			}
			set
			{
				RemoveAt(index);
				Insert(index, value);
			}
		}

		#endregion

		#region ICollection<TreeTableSubItemCollection> Members

		public void Add(TreeTableSubItem item)
		{
			Insert(m_list.Count, item);
		}

		public void Add(string label)
		{
			Insert(m_list.Count, new TreeTableSubItem(label));
		}

		public void Clear()
		{
			foreach (TreeTableSubItem item in m_list)
			{
				OnRemove(item);
			}
			m_list.Clear();
		}

		public bool Contains(TreeTableSubItem item)
		{
			return m_list.Contains(item);
		}

		public void CopyTo(TreeTableSubItem[] array, int arrayIndex)
		{
			m_list.CopyTo(array, arrayIndex);
		}

		public int Count
		{
			get { return m_list.Count; }
		}

		public bool IsReadOnly
		{
			get { return false; }
		}

		public bool Remove(TreeTableSubItem item)
		{
			bool result = m_list.Remove(item);
			if (result)
				OnRemove(item);
			return result;
		}

		#endregion

		#region IEnumerable<TreeTableSubItemCollection> Members

		public IEnumerator<TreeTableSubItem> GetEnumerator()
		{
			return m_list.GetEnumerator();
		}

		#endregion

		#region IEnumerable Members

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return m_list.GetEnumerator();
		}

		#endregion
	}
}
