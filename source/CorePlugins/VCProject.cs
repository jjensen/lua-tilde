
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

using Tilde.Framework.Model;
using System.IO;
using Tilde.Framework.Controller;
using System.Xml;

namespace Tilde.CorePlugins
{
	[ProjectClass("Visual Studio C++ Project")]
	public class VCProject : Project
	{
//		private XmlOptionsDatabase mProjectOptions;

		/*
		public VCProject(IManager manager)
			: base(manager)
		{
			VCProjectDocument doc = (VCProjectDocument) NewProjectDocument("New Visual Studio Project.vcproj", typeof(VCProjectDocument), null);
//			LoadProjectOptions(doc);
		}
		 */

		public VCProject(IManager manager, string fileName)
			: base(manager, fileName)
		{
			VCProjectDocument doc = (VCProjectDocument) LoadProjectDocument(fileName, typeof(VCProjectDocument), null);
//			LoadProjectOptions(doc);
		}

		public static bool CanLoad(string filename)
		{
			if (Path.GetExtension(filename).Equals(".vcproj", StringComparison.InvariantCultureIgnoreCase))
				return true;
			else
				return false;
		}

		public override IOptionsDatabase ProjectOptions
		{
			get { return UserOptions; }
		}

		/*
		public override IOptionsDatabase ProjectOptions
		{
			get { return mProjectOptions; }
		}

		private void LoadProjectOptions(VCProjectDocument doc)
		{
			XmlElement options = (XmlElement)doc.XmlDocument.SelectSingleNode("VisualStudioProject/Tilde");
			if (options == null)
			{
				options = doc.XmlDocument.CreateElement("Tilde");
				doc.XmlDocument.DocumentElement.AppendChild(options);
			}
			mProjectOptions = new XmlOptionsDatabase(options);
			mProjectOptions.Modified += new XmlOptionsDatabaseModifiedHandler(ProjectOptions_Modified);
			mProjectOptions.CanModify += new XmlOptionsDatabaseCanModifyHandler(ProjectOptions_CanModify);
		}

		bool ProjectOptions_CanModify(XmlOptionsDatabase sender, string path)
		{
			return !RootDocument.ReadOnly || RootDocument.Checkout();
		}

		void ProjectOptions_Modified(XmlOptionsDatabase sender, string path)
		{
			RootDocument.Modified = true;
		}
		*/
	}
}
