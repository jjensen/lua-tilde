
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
using System.Xml.XPath;
using System.Xml.Serialization;
using System.Reflection;

namespace Tilde.Framework.Model
{
	public delegate void ProjectItemAddedHandler(Project sender, ProjectItem addedItem);
	public delegate void ProjectItemRemovedHandler(Project sender, ProjectItem removedItem, ProjectItem itemParent);
	public delegate void ProjectItemRenamedHandler(Project sender, ProjectItem changedItem);
	public delegate void ProjectReloadedHandler(Project sender, ProjectDocumentItem reloadedItem);

	public abstract class Project
	{
		private IManager mManager;
		private ProjectDocument mRootDocument;
		private string mProjectName;
		private XmlDocument mUserDocument;
		private RootItem mRootItem;
		private int mLoading = 0;
		private bool mModified = false;
		private XmlOptionsDatabase mUserOptionsDatabase;
		private IVersionController mVCS;

		public Project(IManager manager)
		{
			mManager = manager;
			mRootItem = new RootItem(this);

			mProjectName = "New Project";
			mUserDocument = CreateEmptyUserDocument();
			mUserOptionsDatabase = new XmlOptionsDatabase((XmlElement) mUserDocument.SelectSingleNode("UserConfiguration"));
		}

		public Project(IManager manager, string fileName)
		{
			mManager = manager;
			mRootItem = new RootItem(this);

			mProjectName = Path.GetFileName(fileName);
			mUserDocument = LoadUserConfig(fileName);
			mUserOptionsDatabase = new XmlOptionsDatabase((XmlElement)mUserDocument.SelectSingleNode("UserConfiguration"));

			XmlElement vcsConfig = GetUserConfigurationXML("VersionControl");
			if (vcsConfig != null)
			{
				Type vcsType = ReflectionUtils.FindType(vcsConfig.GetAttribute("type"));
				if (vcsType != null)
				{
					XmlSerializer serializer = new XmlSerializer(vcsType);
					XmlReader reader = new XmlNodeReader(vcsConfig.FirstChild);
					VCS = (IVersionController)serializer.Deserialize(reader);
				}
			}

// 			XmlElement vcsConfig = GetUserConfigurationXML("VersionControl");
// 			Type vcsType = null;
// 			if (vcsConfig != null)
// 				vcsType = ReflectionUtils.FindType(vcsConfig.GetAttribute("type"));
// 
// 			if (vcsType != null)
// 			{
// 				XmlSerializer serializer = new XmlSerializer(vcsType);
// 				XmlReader reader = new XmlNodeReader(vcsConfig.FirstChild);
// 				VCS = (IVersionController) serializer.Deserialize(reader);
// 			}
// 			else
// 			{
// 				System.Windows.Forms.MessageBox.Show("Would you like to configure source control for this project?");
// 			}
		}

		public event PropertyChangeEventHandler PropertyChange;
		public event ProjectItemAddedHandler ItemAdded;
		public event ProjectItemRemovedHandler ItemRemoved;
		public event ProjectItemRenamedHandler ItemRenamed;
		public event ProjectReloadedHandler ProjectReloaded;

		public IManager Manager
		{
			get { return mManager; }
		}

		public string ProjectName
		{
			get { return mProjectName; }
			set { mProjectName = value; }
		}

		public List<ProjectDocument> Documents
		{
			get { return GetDocuments(); }
		}

		public RootItem RootItem
		{
			get { return mRootItem; }
		}

		public ProjectDocument RootDocument
		{
			get { return mRootDocument; }
		}

		public bool Modified
		{
			get { return mModified; }
			set
			{
				if (mModified != value)
				{
					bool oldvalue = mModified;
					mModified = value;
					OnPropertyChange("Modified", oldvalue, value);
				}
			}
		}

		private bool Loading
		{
			get { return mLoading > 0; }
		}

		public IVersionController VCS
		{
			get { return mVCS; }
			set
			{
				if (mVCS != null)
					mVCS.Message -= new MessageEventHandler(VCS_Message);
				mVCS = value;
				if (mVCS != null)
					mVCS.Message += new MessageEventHandler(VCS_Message);
			}
		}

		public string VCSType
		{
			get
			{
				if (mVCS != null)
					return mVCS.GetType().ToString();
				else
					return GetUserConfiguration("VersionControl/@type");
			}
		}

		public abstract IOptionsDatabase ProjectOptions
		{
			get;
		}

		public IOptionsDatabase UserOptions
		{
			get { return mUserOptionsDatabase; }
		}

		public void SetUserConfiguration(string name, string value)
		{
			XmlElement userConfig = (XmlElement)mUserDocument.SelectSingleNode("UserConfiguration");
			userConfig.SetAttribute(name, value);
		}

		/// <summary>
		/// Stores a named parameter into the user config file.
		/// </summary>
		/// <param name="name">The parameter name.</param>
		/// <param name="value">The value to set; this can be an Xml fragment.</param>
		/// <remarks>
		/// Note that unlike GetUserConfiguration() the name is not an xpath expression, 
		/// but is the name of the first element. It will be created if necessary.
		/// </remarks>
		public void SetUserConfigurationXML(string name, string value)
		{
			XmlElement userConfig = (XmlElement)mUserDocument.SelectSingleNode("UserConfiguration");
			XmlElement node = (XmlElement)userConfig.SelectSingleNode(name);
			if (node == null)
			{
				node = mUserDocument.CreateElement(name);
				userConfig.AppendChild(node);
			}
			node.InnerXml = value;
		}

		/// <summary>
		/// Retrieves a named parameter from the user config file.
		/// </summary>
		/// <param name="name">An XPath expression specifying the parameter eg. VersionControl/@type</param>
		/// <returns>The value or an empty string if not found.</returns>
		public string GetUserConfiguration(string name)
		{
			XmlElement userConfig = (XmlElement)mUserDocument.SelectSingleNode("UserConfiguration");
			XmlNode node = userConfig.SelectSingleNode(name);
			if (node is XmlElement)
				return ((XmlElement) node).InnerXml;
			else if (node != null)
				return node.InnerText;
			else
				return userConfig.GetAttribute(name);
		}

		public XmlElement GetUserConfigurationXML(string name)
		{
			XmlElement userConfig = (XmlElement)mUserDocument.SelectSingleNode("UserConfiguration");
			XmlElement node = (XmlElement)userConfig.SelectSingleNode(name);
			return node;
		}

		public XmlElement CreateUserConfigurationXML(string name)
		{
			XmlElement userConfig = (XmlElement)mUserDocument.SelectSingleNode("UserConfiguration");
			XmlElement node = (XmlElement)userConfig.SelectSingleNode(name);
			if (node == null)
			{
				node = mUserDocument.CreateElement(name);
				userConfig.AppendChild(node);
			}
			else
			{
				node.RemoveAll();
			}
			return node;
		}

		public DocumentItem FindDocument(string fileName)
		{
			foreach(ProjectDocument projDoc in Documents)
			{
				string docName = PathUtils.NormaliseFileName(fileName, projDoc.BaseDirectory);
				DocumentItem item = projDoc.Root.FindDocument(docName);
				if (item != null)
					return item;
			}
			return null;
		}

		public Folder FindFolder(string label)
		{
			foreach (ProjectDocument projDoc in Documents)
			{
				Folder folder = FindFolderRecursive(projDoc.Root, label);
				if (folder != null)
					return folder;
			}
			return null;
		}

		private Folder FindFolderRecursive(ProjectItem root, string label)
		{
			if (root is Folder && ((Folder)root).Label == label)
				return (Folder) root;
			else
			{
				foreach(ProjectItem item in root.Items)
				{
					Folder result = FindFolderRecursive(item, label);
					if (result != null)
						return result;
				}
			}
			return null;
		}

		public static void InsertSorted(XmlNode parent, XmlNode child, Comparison<XmlNode> comparer)
		{
			foreach(XmlNode node in parent.ChildNodes)
			{
				if (comparer(node, child) > 0)
				{
					parent.InsertBefore(child, node);
					return;
				}
			}
			parent.AppendChild(child);
		}

		public static void SortFolder(XmlElement folderElement)
		{
			List<XmlNode> nodes = new List<XmlNode>();
			foreach (XmlNode node in folderElement.ChildNodes)
				nodes.Add(node);
			nodes.Sort(delegate(XmlNode lhs, XmlNode rhs)
				{
					if (lhs is XmlElement && rhs is XmlElement)
					{
						string lhsName = (lhs as XmlElement).GetAttribute("name");
						string rhsName = (rhs as XmlElement).GetAttribute("name");
						return lhsName.CompareTo(rhsName);
					}
					else
						return 0;
				});

			while(folderElement.HasChildNodes)
			{
				folderElement.RemoveChild(folderElement.LastChild);
			}
			foreach(XmlNode node in nodes)
			{
				folderElement.AppendChild(node);
			}
		}

		/// <summary>
		/// Creates a DocumentItem node in the project tree.
		/// </summary>
		/// <param name="folder">ProjectItem to insert the document under.</param>
		/// <param name="fileName">Path to file; either absolute or relative to the folder's ProjectDocument.</param>
		/// <param name="fileType"></param>
		/// <returns></returns>
		public DocumentItem AddDocument(ProjectItem folder, string fileName, Type fileType)
		{
			ProjectDocument projDoc = folder.ProjectDocument;

			string normName = PathUtils.NormaliseFileName(fileName, projDoc.BaseDirectory);
			DocumentItem fileItem = new DocumentItem(normName, fileType);

			AddDocument(folder, fileItem);

			return fileItem;
		}

		public void AddDocument(ProjectItem folder, DocumentItem fileItem)
		{
			ProjectDocument projDoc = folder.ProjectDocument;
			if (projDoc.ReadOnly)
				throw new ReadOnlyDocumentException(projDoc);

			// Insert it into the tree
			folder.Items.InsertSorted(fileItem,
				delegate(ProjectItem lhs, ProjectItem rhs)
				{
					string lhsName = lhs is DocumentItem ? (lhs as DocumentItem).RelativeFileName : lhs.Label;
					string rhsName = rhs is DocumentItem ? (rhs as DocumentItem).RelativeFileName : rhs.Label;
					return String.Compare(lhsName, rhsName, true);
				});

			// Add to project file
			projDoc.AddDocument(folder, fileItem);

			projDoc.Modified = true;
		}

		public void RemoveDocument(DocumentItem doc)
		{
			ProjectDocument projDoc = doc.ProjectDocument;

			if (projDoc.ReadOnly)
				throw new ReadOnlyDocumentException(projDoc);

			// Remove from the tree
			doc.Parent.Items.Remove(doc);

			// Remove from project file
			projDoc.RemoveDocument(doc);

			projDoc.Modified = true;
		}

		public void RenameDocument(DocumentItem doc, string fileName)
		{
			Folder folder = (Folder) doc.Parent;
			ProjectDocument projDoc = doc.ProjectDocument;

			RemoveDocument(doc);
			doc.RelativeFileName = PathUtils.NormaliseFileName(fileName, projDoc.BaseDirectory);
			AddDocument(folder, doc);
		}

		public void SaveUserConfig()
		{
			XmlElement vcsRoot = CreateUserConfigurationXML("VersionControl");
			if (mVCS == null)
			{
				vcsRoot.SetAttribute("type", "None");
			}
			else
			{
				XmlSerializer serializer = new XmlSerializer(mVCS.GetType());
				StringWriter writer = new StringWriter();
				serializer.Serialize(writer, mVCS);
				vcsRoot.InnerXml = writer.GetStringBuilder().ToString();
				vcsRoot.RemoveChild(vcsRoot.FirstChild);
				vcsRoot.SetAttribute("type", mVCS.GetType().ToString());
			}

			if (mRootDocument != null)
			{
				string userFile = System.IO.Path.ChangeExtension(mRootDocument.FileName, ".tilde.xml");
				XmlTextWriter writer = new XmlTextWriter(userFile, System.Text.Encoding.ASCII);
				writer.Formatting = Formatting.Indented;
				writer.Indentation = 4;
				writer.IndentChar = ' ';
				mUserDocument.Save(writer);
				writer.Close();
			}
		}

		public ProjectDocument NewProjectDocument(string fileName, Type projType, ProjectDocument parentDoc)
		{
			System.Diagnostics.Debug.Assert(parentDoc != null || mRootDocument == null, "A Project can have only one root document!");

			if (!Path.IsPathRooted(fileName))
				fileName = Path.Combine(Environment.CurrentDirectory, fileName);

			ProjectDocument doc = (ProjectDocument)Manager.CreateDocument(fileName, projType, null, new object[] { this });
			ProjectItem parentItem = (parentDoc == null) ? (ProjectItem) this.RootItem : (ProjectItem) parentDoc.Root;
			AddProjectDocument(doc, parentDoc, parentItem);
			return doc;
		}

		public ProjectDocument LoadProjectDocument(string fileName, Type projType, ProjectDocument parentDoc)
		{
			try
			{
				++mLoading;
				ProjectDocument doc = (ProjectDocument)Manager.OpenDocument(fileName, projType, new object[] { this } );
				ProjectItem parentItem = (parentDoc == null) ? (ProjectItem) this.RootItem : (ProjectItem) parentDoc.Root;
				if (doc == null)
					parentItem.Items.Add(new ProjectDocumentItem(fileName));
				else
					AddProjectDocument(doc, parentDoc, parentItem);
				return doc;
			}
			finally
			{
				--mLoading;
			}
		}

		private void AddProjectDocument(ProjectDocument doc, ProjectDocument parentDoc, ProjectItem parentItem)
		{
			doc.PropertyChange += new PropertyChangeEventHandler(ProjectDocument_PropertyChange);
			doc.Loading += new DocumentLoadingEventHandler(ProjectDocument_Loading);
			doc.Loaded += new DocumentLoadedEventHandler(ProjectDocument_Loaded);
			if (parentDoc == null)
				mRootDocument = doc;
			else
				parentDoc.Imports.Add(doc);
			parentItem.Items.Add(doc.Root);
		}

		void VCS_Message(IVersionController sender, string message)
		{
			Manager.AddMessage("Version Control", message);
		}

		void ProjectDocument_Loading(Document sender)
		{
			++mLoading;
		}

		void ProjectDocument_Loaded(Document sender)
		{
			--mLoading;

			if (mLoading == 0)
			{
				ProjectDocumentItem item = RootItem.FindDocument(sender.FileName) as ProjectDocumentItem;
				OnProjectReloaded(item);
			}
		}

		void ProjectDocument_PropertyChange(object sender, PropertyChangeEventArgs args)
		{
			System.Diagnostics.Debug.Assert(sender is ProjectDocument);

			if(args.Property == "Modified")
			{
				bool modified = false;
				foreach (ProjectDocument projDoc in Documents)
				{
					modified |= projDoc.Modified;
				}
				Modified = modified;
			}
		}

		private void SaveProjectTree(XmlElement rootElement, ProjectItem rootItem)
		{
			foreach (ProjectItem item in rootItem.Items)
			{
				XmlElement element = rootElement.OwnerDocument.CreateElement("Item");
				element.SetAttribute("type", item.GetType().Name);
				rootElement.AppendChild(element);
				SaveProjectTree(element, item);
			}
		}

		XmlDocument LoadUserConfig(string fileName)
		{
			string userFile = System.IO.Path.ChangeExtension(fileName, ".tilde.xml");
			if (System.IO.File.Exists(userFile))
			{
				XmlTextReader reader = new XmlTextReader(userFile);
				XmlDocument doc = new XmlDocument();
				doc.Load(reader);
				reader.Close();

				return doc;
			}
			else
			{
				return CreateEmptyUserDocument();
			}
		}

		private XmlDocument CreateEmptyUserDocument()
		{
			XmlDocument doc = new XmlDocument();
			doc.AppendChild(doc.CreateElement("UserConfiguration"));

			return doc;
		}

		internal void OnItemAdded(ProjectItem item)
		{
			if (Loading)
				return;

			ProjectDocument projDoc = item.Parent.ProjectDocument;

			if (projDoc != null && projDoc.ReadOnly)
				throw new ReadOnlyDocumentException(projDoc);

			if (ItemAdded != null)
				ItemAdded(this, item);

			if(projDoc != null)
				projDoc.Modified = true;
		}

		internal void OnItemRemoved(ProjectItem item, ProjectItem parent)
		{
			if (Loading)
				return;

			ProjectDocument projDoc = parent.ProjectDocument;

			if (projDoc != null && projDoc.ReadOnly)
				throw new ReadOnlyDocumentException(projDoc);

			if (ItemRemoved != null)
				ItemRemoved(this, item, parent);

			if (projDoc != null)
				projDoc.Modified = true;
		}

		internal void OnItemRenamed(ProjectItem item)
		{
			if (ItemRenamed != null)
				ItemRenamed(this, item);
		}

		protected virtual void OnProjectReloaded(ProjectDocumentItem item)
		{
			if (ProjectReloaded != null)
				ProjectReloaded(this, item);
		}

		protected void OnPropertyChange(object sender, PropertyChangeEventArgs args)
		{
			if (PropertyChange != null)
				PropertyChange(sender, args);
		}

		protected void OnPropertyChange(string property, object oldValue, object newValue)
		{
			if (PropertyChange != null)
				PropertyChange(this, new PropertyChangeEventArgs(property, oldValue, newValue));
		}

		public List<string> GetFiles()
		{
			List<string> files = new List<string>();
			mRootDocument.Root.GetFiles(files);
			return files;
		}

		public List<ProjectDocument> GetDocuments()
		{
			List<ProjectDocument> docs = new List<ProjectDocument>();
			docs.Add(mRootDocument);
			mRootDocument.GetImports(docs);
			return docs;
		}

	}
}
