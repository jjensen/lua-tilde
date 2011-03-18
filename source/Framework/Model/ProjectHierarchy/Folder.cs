
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

namespace Tilde.Framework.Model.ProjectHierarchy
{
	public class Folder : ProjectItem
	{
		private string mName;
		private string mLabel;
		private string mRelativePath;

		public Folder()
		{
			mName = "New Folder";
			mLabel = "New Folder";
			mRelativePath = "";
		}

		public Folder(string label)
		{
			mName = label;
			mLabel = label;
			mRelativePath = "";
		}

		public Folder(string name, string label, string relPath)
		{
			mName = name;
			mLabel = label;

			if (relPath != "" && !relPath.EndsWith("/"))
				mRelativePath = relPath + "/";
			else
				mRelativePath = relPath;
		}

		public override string Label
		{
			get { return mLabel; }
			set { mLabel = value; }
		}

		public override bool CanRename
		{
			get { return true; }
		}

		public string Name
		{
			get { return mName; }
			set { mName = value; }
		}

		public string RelativePath
		{
			get { return mRelativePath; }
		}

		public string AbsolutePath
		{
			get { return PathUtils.MakeCanonicalFileName(Path.Combine(ProjectDocument.BaseDirectory, mRelativePath)); }
		}

		public override string ToString()
		{
			return String.Format("Folder: {0}", mLabel);
		}

	}
}
