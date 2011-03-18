
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

using Tilde.Framework.Model;
using Tilde.Framework.Model.ProjectHierarchy;
using Tilde.Framework;
using Tilde.Framework.Controller;
using System.IO;

namespace Tilde.CorePlugins
{
	class VCProjectDocument : ProjectDocument
	{
		protected XmlDocument mXmlDocument;

		public VCProjectDocument(IManager manager, string fileName, Project project)
			: base(manager, fileName, project)
		{

		}

		public override string Name
		{
			get { return mXmlDocument.SelectSingleNode("VisualStudioProject/@Name").InnerText; }
		}

		public XmlDocument XmlDocument
		{
			get { return mXmlDocument; }
		}

		protected override bool New(System.IO.Stream stream)
		{
			return false;
		}

		protected override bool Load()
		{
			LoadProjectFromVCProj(FileName);
			return true;
		}

		protected override bool Save()
		{
			XmlTextWriter writer = new XmlTextWriter(FileName, System.Text.Encoding.ASCII);
			writer.Formatting = Formatting.Indented;
			writer.Indentation = 4;
			writer.IndentChar = ' ';
			mXmlDocument.Save(writer);
			writer.Close();

			return true;
		}

		private void LoadProjectFromVCProj(string projFile)
		{
			XmlTextReader reader = new XmlTextReader(projFile);
			XmlDocument doc = new XmlDocument();
			doc.Load(reader);
			reader.Close();

			ProjectDocumentItem newRoot = new ProjectDocumentItem(this);
			newRoot.ProjectTag = doc;
			LoadFilesFromVCProj((XmlElement)doc.SelectSingleNode("VisualStudioProject/Files"), newRoot);

			mXmlDocument = doc;
			mRootItem = newRoot;
		}

		private void LoadFilesFromVCProj(XmlElement rootNode, ProjectItem rootItem)
		{
			foreach (XmlElement element in rootNode.SelectNodes("Filter|File"))
			{
				if (element.Name == "Filter")
				{
					Folder folder = new Folder(element.GetAttribute("Name"));
					folder.ProjectTag = element;
					rootItem.Items.Add(folder);
					LoadFilesFromVCProj(element, folder);
				}
				else if (element.Name == "File")
				{
					string fileName = element.GetAttribute("RelativePath");
					DocumentItem doc = new DocumentItem(PathUtils.NormaliseFileName(fileName, BaseDirectory), Manager.FindFileDocumentType(fileName));
					doc.ProjectTag = element;
					rootItem.Items.Add(doc);
				}
			}
		}

		public override void AddDocument(ProjectItem folder, DocumentItem fileItem)
		{
			XmlElement folderElement = (XmlElement)folder.ProjectTag;
			XmlElement fileElement = folderElement.OwnerDocument.CreateElement("File");
			fileElement.SetAttribute("RelativePath", ".\\" + fileItem.RelativeFileName.Replace('/', '\\'));
			fileItem.ProjectTag = fileElement;

			folderElement.AppendChild(fileElement);
		}

		public override void RemoveDocument(DocumentItem doc)
		{
			XmlElement folderElement = (XmlElement)((XmlElement)doc.ProjectTag).ParentNode;
			folderElement.RemoveChild((XmlElement)doc.ProjectTag);
		}
	}
}
