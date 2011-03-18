
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

namespace Tilde.Framework.Model
{
	public abstract class ListCollectionBase<T> : IList<T>
	{
		protected List<T> m_list;

		public ListCollectionBase()
		{
			m_list = new List<T>();
		}

		public ListCollectionBase(ICollection<T> from)
		{
			m_list = new List<T>(from);
		}

		protected abstract void OnRemove(T item);
		protected abstract void OnAdd(T item);
		
		public void Sort()
		{
			m_list.Sort();
		}

		public void Sort(Comparison<T> comparison)
		{
			m_list.Sort(comparison);
		}

		public void Sort(IComparer<T> comparer)
		{
			m_list.Sort(comparer);
		}

		// TODO: This should be done properly!
		public void InsertSorted(T item)
		{
			m_list.Add(item);
			m_list.Sort();
			OnAdd(item);
		}

		public void InsertSorted(T item, Comparison<T> comparison)
		{
			m_list.Add(item);
			m_list.Sort(comparison);
			OnAdd(item);
		}

		public void InsertSorted(T item, IComparer<T> comparer)
		{
			m_list.Add(item);
			m_list.Sort(comparer);
			OnAdd(item);
		}

		#region IList<T> Members

		public int IndexOf(T item)
		{
			return m_list.IndexOf(item);
		}

		public void Insert(int index, T item)
		{
			m_list.Insert(index, item);
			OnAdd(item);
		}

		public void Insert(T item, IComparer<T> comparer)
		{
			int index = m_list.BinarySearch(item, comparer);
			if (index < 0)
				index = ~index;
			m_list.Insert(index, item);
			OnAdd(item);
		}

		public void RemoveAt(int index)
		{
			T item = m_list[index];
			m_list.RemoveAt(index);
			OnRemove(item);
		}

		public T this[int index]
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

		#region ICollection<T> Members

		public void Add(T item)
		{
			Insert(m_list.Count, item);
		}

		public void Clear()
		{
			while (m_list.Count > 0)
			{
				T item = m_list[m_list.Count - 1];
				m_list.RemoveAt(m_list.Count - 1);
				OnRemove(item);
			}
		}

		public bool Contains(T item)
		{
			return m_list.Contains(item);
		}

		public void CopyTo(T[] array, int arrayIndex)
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

		public bool Remove(T item)
		{
			bool result = m_list.Remove(item);
			OnRemove(item);
			return result;
		}

		#endregion

		#region IEnumerable<T> Members

		public IEnumerator<T> GetEnumerator()
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

	public class ListCollection<T> : ListCollectionBase<T>
	{
		public ListCollection()
			: base()
		{
		}

		public ListCollection(ICollection<T> from)
			: base(from)
		{
		}

		public delegate void ItemAddedDelegate(ListCollection<T> sender, T item);
		public delegate void ItemRemovedDelegate(ListCollection<T> sender, T item);

		public event ItemAddedDelegate ItemAdded;
		public event ItemRemovedDelegate ItemRemoved;

		protected override void OnAdd(T item)
		{
			if (ItemAdded != null)
				ItemAdded(this, item);
		}

		protected override void OnRemove(T item)
		{
			if (ItemRemoved != null)
				ItemRemoved(this, item);
		}
	}

}
