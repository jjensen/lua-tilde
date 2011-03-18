
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
using System.Xml;

namespace Tilde.Framework.Model.ProjectHierarchy
{
	public class DocumentItem : ProjectItem
	{
		public DocumentItem(string relFileName, Type doctype)
		{
			/*
			if (Path.IsPathRooted(relFileName))
				throw new ArgumentException("DocumentItem file names must be relative to project");

			if (relFileName.Contains("\\"))
				throw new ArgumentException("DocumentItem file names cannot contain backslashes");
			*/

			mRelativeFileName = relFileName;
			mDocumentType = doctype;
		}

		public override String Label
		{
			get { return Path.GetFileName(mRelativeFileName); }
			set 
			{
				throw new Exception("Can't change the name of a document");
			}
		}

		public override bool CanRename
		{
			get { return false; }
		}

		public string RelativeFileName
		{
			get { return mRelativeFileName;  }
			set { mRelativeFileName = value; }
		}

		public string AbsoluteFileName
		{
			get 
			{
				if(!Path.IsPathRooted(mRelativeFileName) && ProjectDocument != null && ProjectDocument.BaseDirectory != null)
					return PathUtils.MakeCanonicalFileName(Path.Combine(ProjectDocument.BaseDirectory, mRelativeFileName)); 
				else
					return mRelativeFileName;
			}
		}

		public Type DocumentType
		{
			get { return mDocumentType; }
		}

		public override string ToString()
		{
			return String.Format("DocumentItem: {0} ({1})", Label, mDocumentType != null ? mDocumentType.ToString() : "unknown");
		}

		public override DocumentItem FindDocument(string fileName)
		{
			if (PathUtils.Compare(fileName, this.RelativeFileName) == 0)
				return this;
			else
				return base.FindDocument(fileName);
		}

		public override void GetFiles(List<string> files)
		{
			files.Add(AbsoluteFileName);
			base.GetFiles(files);
		}

		/*
		public void Rename(string newFileName)
		{
			XmlElement fileElement = (XmlElement) ProjectTag;
			fileElement.SetAttribute("name", Path.GetFileName(newFileName));

			mRelativeFileName = Path.Combine(Path.GetDirectoryName(mRelativeFileName), newFileName);
			Project.OnItemRenamed(this);
		}
		*/
		private string mRelativeFileName;
		private Type mDocumentType;

	}
}
