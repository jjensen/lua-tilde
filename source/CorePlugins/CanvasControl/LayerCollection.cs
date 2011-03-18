
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

namespace Tilde.CorePlugins.CanvasControl
{
	public class LayerCollection : IList<Layer>
	{
		private Canvas mCanvas;
		private List<Layer> mContainer;

		public LayerCollection(Canvas canvas)
		{
			mCanvas = canvas;
			mContainer = new List<Layer>();
		}

		internal void RemoveInternal(Layer item)
		{
			mContainer.Remove(item);
		}

		internal void AddInternal(Layer item)
		{
			mContainer.Add(item);
		}

		#region IList<Layer> Members

		public int IndexOf(Layer item)
		{
			return mContainer.IndexOf(item);
		}

		public void Insert(int index, Layer item)
		{
			if (mContainer.Contains(item))
				throw new ArgumentException("LayerCollection cannot hold duplicated Layers");
			else if (item.Canvas != null)
				throw new ArgumentException("A Layer can only be inserted into one Canvas at a time");

			mContainer.Insert(index, item);

			item.SetCanvasInternal(mCanvas);
		}

		public void RemoveAt(int index)
		{
			Layer item = mContainer[index];
			mContainer.RemoveAt(index);
			item.SetCanvasInternal(null);
		}

		public Layer this[int index]
		{
			get
			{
				return mContainer[index];
			}
			set
			{
				if (mContainer.Contains(value))
					throw new ArgumentException("LayerCollection cannot hold duplicated Layers");
				else if (value.Canvas != null)
					throw new ArgumentException("A Layer can only be inserted into one Canvas at a time");

				Layer item = mContainer[index];
				item.SetCanvasInternal(null);
				mContainer[index] = value;
				value.SetCanvasInternal(mCanvas);
			}
		}

		#endregion

		#region ICollection<Layer> Members

		public void Add(Layer item)
		{
			if (mContainer.Contains(item))
				throw new ArgumentException("LayerCollection cannot hold duplicated Layers");
			else if (item.Canvas != null)
				throw new ArgumentException("A Layer can only be inserted into one Canvas at a time");

			mContainer.Add(item);
			item.SetCanvasInternal(mCanvas);
		}

		public void Clear()
		{
			foreach(Layer item in mContainer)
			{
				item.SetCanvasInternal(null);
			}
			mContainer.Clear();
		}

		public bool Contains(Layer item)
		{
			return mContainer.Contains(item);
		}

		public void CopyTo(Layer[] array, int arrayIndex)
		{
			mContainer.CopyTo(array, arrayIndex);
		}

		public int Count
		{
			get { return mContainer.Count; }
		}

		public bool IsReadOnly
		{
			get { return false ; }
		}

		public bool Remove(Layer item)
		{
			if (mContainer.Remove(item))
			{
				item.SetCanvasInternal(null);
				return true;
			}
			return false;
		}

		#endregion

		#region IEnumerable<Layer> Members

		public IEnumerator<Layer> GetEnumerator()
		{
			return mContainer.GetEnumerator();
		}

		#endregion

		#region IEnumerable Members

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return mContainer.GetEnumerator();
		}

		#endregion
	}
}
