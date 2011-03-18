
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

namespace Tilde.Framework.Model.ProjectHierarchy
{
	public class ProjectItemCollection : ListCollectionBase<ProjectItem>
	{
		private ProjectItem mOwner;

		public ProjectItemCollection(ProjectItem owner)
		{
			mOwner = owner;
		}

		protected override void OnRemove(ProjectItem item)
		{
			System.Diagnostics.Debug.Assert(item.Parent == mOwner);

			item.Parent = null;
			
			if(mOwner != null && mOwner.Project != null)
			{
				mOwner.Project.OnItemRemoved(item, mOwner);
			}
		}

		protected override void OnAdd(ProjectItem item)
		{
			System.Diagnostics.Debug.Assert(item.Parent == null);

			item.Parent = mOwner;

			if (mOwner != null && mOwner.Project != null)
			{
				mOwner.Project.OnItemAdded(item);
			}
		}
	}
}
