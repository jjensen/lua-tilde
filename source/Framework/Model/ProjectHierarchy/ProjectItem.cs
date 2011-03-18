
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
using System.Xml;

namespace Tilde.Framework.Model.ProjectHierarchy
{
	public abstract class ProjectItem
	{
		private ProjectItem mParent;
		private ProjectItemCollection mItems;
		private object mProjectTag;

		public ProjectItem()
		{
			mProjectTag = null;
			mParent = null;
			mItems = new ProjectItemCollection(this);
		}

		public object ProjectTag
		{
			get { return mProjectTag; }
			set { mProjectTag = value; }
		}

		public ProjectItemCollection Items
		{
			get { return mItems; }
		}

		public abstract String Label
		{
			get;
			set;
		}

		public abstract bool CanRename
		{
			get;
		}

		public ProjectItem Parent
		{
			get { return mParent; }
			set { mParent = value; }
		}

		public virtual ProjectDocument ProjectDocument
		{
			get 
			{
				if (mParent != null)
					return mParent.ProjectDocument;
				else
					return null;
			}
		}

		public virtual Project Project
		{
			get
			{
				System.Diagnostics.Debug.Assert(mParent != this);

				if (mParent != null)
					return mParent.Project;
				else
					return null;
			}
		}

		public virtual DocumentItem FindDocument(string fileName)
		{
			foreach(ProjectItem item in mItems)
			{
				DocumentItem result = item.FindDocument(fileName);
				if (result != null)
					return result;
			}

			return null;
		}

		public virtual void GetFiles(List<string> files)
		{
			foreach (ProjectItem item in mItems)
			{
				item.GetFiles(files);
			}
		}
	}
}
