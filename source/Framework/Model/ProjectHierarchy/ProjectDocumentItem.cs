
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
using System.IO;

using Tilde.Framework.Model;

namespace Tilde.Framework.Model.ProjectHierarchy
{
	public class ProjectDocumentItem : DocumentItem
	{
		public ProjectDocumentItem(ProjectDocument project)
			: base(project.FileName, null)
		{
			mProject = project;
			mFileName = project.FileName;
		}

		public ProjectDocumentItem(string fileName)
			: base(fileName, null)
		{
			mProject = null;
			mFileName = fileName;
		}

		public override String Label
		{
			get 
			{
				if (mProject != null)
					return mProject.Name + " (" + Path.GetFileName(mProject.FileName) + ")";
				else
					return "Error loading " + mFileName;
			}
			set { throw new ArgumentException("Can't set Label on Project"); }
		}

		public override ProjectDocument ProjectDocument
		{
			get	{ return mProject; }
		}

		private ProjectDocument mProject;
		private string mFileName;
	}
}
