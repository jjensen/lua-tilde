
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
using System.IO;

using Tilde.Framework.Controller;
using Tilde.Framework.Model.ProjectHierarchy;

namespace Tilde.Framework.Model
{
	public abstract class ProjectDocument : Document
	{
		protected Project mProject;
		protected string mBaseDirectory;
		protected ProjectHierarchy.ProjectDocumentItem mRootItem;
		protected ListCollection<ProjectDocument> mImports;

		public ProjectDocument(IManager manager, string fileName, Project project)
			: base(manager, fileName)
		{
			mBaseDirectory = Path.GetDirectoryName(fileName); //System.Windows.Forms.Application.StartupPath;
			mRootItem = new ProjectDocumentItem(this);
			mProject = project;
			mImports = new ListCollection<ProjectDocument>();
		}

		public Project Project
		{
			get { return mProject; }
		}

		public ProjectHierarchy.ProjectDocumentItem Root
		{
			get { return mRootItem; }
		}

		public string BaseDirectory
		{
			get { return mBaseDirectory; }
		}

		public ListCollection<ProjectDocument> Imports
		{
			get { return mImports; }
		}

		public abstract string Name
		{
			get;
		}

		public abstract void AddDocument(ProjectItem folder, DocumentItem doc);
		public abstract void RemoveDocument(DocumentItem doc);

		internal void GetImports(List<ProjectDocument> docs)
		{
			foreach (ProjectDocument import in mImports)
			{
				docs.Add(import);
				import.GetImports(docs);
			}
		}
	}
}
