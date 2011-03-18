
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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;

using Tilde.Framework.Controller;
using Tilde.Framework.Model;
using Tilde.Framework.Model.ProjectHierarchy;
using Tilde.Framework.View;
using Tilde.Framework;
using Tilde.Framework.Controls;

namespace Tilde.TildeApp
{
	[ToolWindowAttribute]
	public partial class ProjectPanel : ToolWindow
	{
		private IManager m_manager;
		private MainWindow m_mainWindow;
		private SystemImageList m_systemImageList;
		private Font m_italicFont;
		private Font m_boldFont;

		public ProjectPanel(IManager manager)
		{
			InitializeComponent();

			m_manager = manager;
			m_mainWindow = (MainWindow) manager.MainWindow;
			m_manager.PropertyChange += new PropertyChangeEventHandler(Manager_PropertyChange);

			manager.ProjectOpened += new ProjectOpenedEventHandler(Manager_ProjectOpened);

			m_systemImageList = new SystemImageList();
			m_systemImageList.SetImageList(projectView);

			m_italicFont = new Font(projectView.Font, FontStyle.Italic);
			m_boldFont = new Font(projectView.Font, FontStyle.Bold);

			UpdateTree();

			foreach (Type docType in m_manager.GetPluginImplementations(typeof(Document)))
			{
				DocumentClassAttribute docAttr = DocumentClassAttribute.ForType(docType);
				if(docAttr != null)
				{
					ToolStripMenuItem item = new ToolStripMenuItem();
					item.Text = "New " + docAttr.Name;
					item.Click += new EventHandler(AddNewDocument_Click);
					item.Tag = docType;
					addToolStripMenuItem.DropDownItems.Insert(0, item);
				}
			}
		}

		void Manager_ProjectOpened(object sender, Project project)
		{
			project.ItemAdded += new ProjectItemAddedHandler(Project_ItemAdded);
			project.ItemRemoved += new ProjectItemRemovedHandler(Project_ItemRemoved);
			project.ItemRenamed += new ProjectItemRenamedHandler(Project_ItemRenamed);
			project.ProjectReloaded += new ProjectReloadedHandler(Project_ProjectReloaded);
		}

		void Manager_PropertyChange(object sender, PropertyChangeEventArgs args)
		{
			if (sender is Project)
			{
				if (args.Property == "ProjectName")
				{
					UpdateNodeImage(projectView.Nodes[0]);
				}
			}
			else if(sender is Manager)
			{
				if(args.Property == "Project")
				{
					UpdateTree();
				}
			}
		}

		void Project_ItemAdded(Project sender, ProjectItem addedItem)
		{
			TreeNode parent = FindItem(delegate(ProjectItem item) { return item == addedItem.Parent; });
			if (parent != null)
			{
				TreeNode newNode = CreateNode(addedItem);
				UpdateNodeImage(newNode);
				int index = addedItem.Parent.Items.IndexOf(addedItem);
				parent.Nodes.Insert(index, newNode);
				UpdateTree(newNode, addedItem);
				projectView.SelectedNode = newNode;
				newNode.EnsureVisible();
			}
		}

		void Project_ItemRemoved(Project sender, ProjectItem removedItem, ProjectItem itemParent)
		{
			TreeNode node = FindItem(delegate(ProjectItem item) { return item == removedItem; });
			if (node != null)
				node.Parent.Nodes.Remove(node);
		}

		void Project_ItemRenamed(Project sender, ProjectItem changedItem)
		{
			TreeNode node = FindItem(
				delegate(ProjectItem item)
				{
					return item == changedItem;
				}
			);

			if (node != null)
				UpdateNode(node);
		}

		void Project_ProjectReloaded(Project sender, ProjectDocumentItem reloadedItem)
		{
			TreeNode node = FindItem(delegate(ProjectItem item) { return item == reloadedItem; });
			if (node != null)
			{
				node.Collapse();
				node.Nodes.Clear();
				UpdateTree(node, reloadedItem);
			}
		}

		private TreeNode FindItem(Predicate<ProjectItem> predicate)
		{
			foreach(TreeNode node in projectView.Nodes)
			{
				TreeNode result = FindItem(node, predicate);
				if (result != null)
					return result;
			}

			return null;
		}

		private TreeNode FindItem(TreeNode root, Predicate<ProjectItem> predicate)
		{
			if (predicate((ProjectItem)root.Tag))
				return root;

			foreach (TreeNode node in root.Nodes)
			{
				TreeNode result = FindItem(node, predicate);
				if (result != null)
					return result;
			}

			return null;
		}

		private TreeNode CreateNode(ProjectItem item)
		{
			TreeNode node = new TreeNode(item.Label);
			node.Tag = item;
			node.ImageIndex = -1;

			UpdateNode(node);
			return node;
		}

		private void UpdateNode(TreeNode node)
		{
			if (node.Tag != null)
			{
				ProjectItem item = (ProjectItem)node.Tag;

				node.Text = item.Label;

				if (item is ProjectDocumentItem)
				{
					if (((ProjectDocumentItem)item).ProjectDocument == null)
						node.NodeFont = m_italicFont;
					else
						node.NodeFont = m_boldFont;
				}
				else if (item is DocumentItem)
				{
					node.ToolTipText = (item as DocumentItem).AbsoluteFileName;
				}
			}
		}

		private void UpdateNodeImage(TreeNode node)
		{
			int imageIndex = -1;
			if (node.Tag != null)
			{
				ProjectItem item = (ProjectItem)node.Tag;
				if (item is Folder || item is ProjectDocumentItem || item is RootItem)
				{
					ShellAPI.SHFILEINFO shInfo = new ShellAPI.SHFILEINFO();
					ShellAPI.SHGFI attrs = ShellAPI.SHGFI.SHGFI_SMALLICON | ShellAPI.SHGFI.SHGFI_SYSICONINDEX;
					if (node.IsExpanded)
						attrs |= ShellAPI.SHGFI.SHGFI_OPENICON;

					IntPtr handle = ShellAPI.SHGetFileInfo(
						System.Windows.Forms.Application.StartupPath,
						ShellAPI.FILE_ATTRIBUTE_NORMAL,
						out shInfo, (uint)Marshal.SizeOf(shInfo),
						attrs);

					imageIndex = shInfo.iIcon;
				}

				else if (item is DocumentItem)
				{
					string fileName = ((DocumentItem)item).AbsoluteFileName;
					ShellAPI.SHFILEINFO shInfo = new ShellAPI.SHFILEINFO();
					ShellAPI.SHGFI attrs = ShellAPI.SHGFI.SHGFI_USEFILEATTRIBUTES | ShellAPI.SHGFI.SHGFI_SMALLICON | ShellAPI.SHGFI.SHGFI_SYSICONINDEX;
					IntPtr handle = ShellAPI.SHGetFileInfo(
						fileName,
						ShellAPI.FILE_ATTRIBUTE_NORMAL,
						out shInfo, (uint)Marshal.SizeOf(shInfo),
						attrs);

					imageIndex = shInfo.iIcon;
				}

				else
				{
					ShellAPI.SHFILEINFO shInfo = new ShellAPI.SHFILEINFO();
					ShellAPI.SHGFI attrs = ShellAPI.SHGFI.SHGFI_USEFILEATTRIBUTES | ShellAPI.SHGFI.SHGFI_SMALLICON | ShellAPI.SHGFI.SHGFI_SYSICONINDEX;
					IntPtr handle = ShellAPI.SHGetFileInfo(
						"",
						ShellAPI.FILE_ATTRIBUTE_NORMAL,
						out shInfo, (uint)Marshal.SizeOf(shInfo),
						attrs);
					imageIndex = shInfo.iIcon;
				}

				node.Text = item.Label;
			}
			node.ImageIndex = imageIndex;
			node.SelectedImageIndex = imageIndex;
		}

		private void UpdateTree()
		{
			projectView.Nodes.Clear();

			if (m_manager.Project != null)
			{
				ProjectItem item = m_manager.Project.RootItem;
				UpdateTree(null, item);
				foreach (TreeNode node in projectView.Nodes)
				{
					node.Expand();
				}
			}
		}

		private void UpdateTree(TreeNode rootnode, ProjectItem rootitem)
		{
			foreach (ProjectItem item in rootitem.Items)
			{
				TreeNode node = CreateNode(item);
				if (rootnode == null)
					projectView.Nodes.Add(node);
				else
					rootnode.Nodes.Add(node);
				UpdateTree(node, item);
			}
		}

		private void projectView_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				projectView.SelectedNode = projectView.GetNodeAt(e.Location);
		}

		private void newFolderToolStripMenuItem_Click(object sender, EventArgs e)
		{
		}

		private void projectView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			ProjectItem projectItem = (ProjectItem) e.Node.Tag;
			if (projectItem is DocumentItem && e.Node.Nodes.Count == 0)
			{
				DocumentItem docItem = (DocumentItem)projectItem;
				m_manager.ShowDocument(docItem);
			}
		}

		private void projectView_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
		{
			if (e.Label != null && e.Label != e.Node.Text)
			{
				ProjectItem projectItem = (ProjectItem)e.Node.Tag;
				projectItem.Label = e.Label;
			}
		}

		private void renameToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (projectView.SelectedNode != null)
			{
				projectView.SelectedNode.BeginEdit();
			}
		}

		private void projectView_AfterCollapse(object sender, TreeViewEventArgs e)
		{
			UpdateNodeImage(e.Node);
		}

		private void projectView_AfterExpand(object sender, TreeViewEventArgs e)
		{
			UpdateNodeImage(e.Node);

			foreach(TreeNode child in e.Node.Nodes)
			{
				if (child.ImageIndex == -1)
					UpdateNodeImage(child);
			}
		}

		void AddNewDocument_Click(object sender, EventArgs e)
		{
			if (projectView.SelectedNode == null)
				return;
				
			Folder folder = FindFolder(projectView.SelectedNode);

			if (folder == null)
				return;

			if (folder.ProjectDocument.ReadOnly && !folder.ProjectDocument.Checkout())
				return;

			Type docType = (sender as ToolStripMenuItem).Tag as Type;
			DocumentClassAttribute attr = DocumentClassAttribute.ForType(docType);

			string fileName = Path.Combine(folder.AbsolutePath, "New " + attr.Name + attr.FileExtensions[0]);
			DocumentItem docItem = m_manager.Project.AddDocument(folder, fileName, docType);
			Document doc = m_manager.CreateDocument(fileName, docType);

			m_mainWindow.ShowDocument(doc);
		}

		private Folder FindFolder(TreeNode treeNode)
		{
			if (treeNode.Tag is Folder)
				return (Folder)treeNode.Tag;
			else if (treeNode.Parent != null && treeNode.Parent.Tag != null && treeNode.Parent.Tag is Folder)
				return (Folder)treeNode.Parent.Tag;
			else
				return null;
		}

		private void addExistingItemToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (projectView.SelectedNode == null)
				return;

			Folder folder = FindFolder(projectView.SelectedNode);

			if (folder == null)
				return;

			if (folder.ProjectDocument.ReadOnly && !folder.ProjectDocument.Checkout())
				return;

			DialogResult result = openExistingFileDialog.ShowDialog();
			if(result == DialogResult.OK)
			{
				foreach(string fileName in openExistingFileDialog.FileNames)
				{
					string ext = Path.GetExtension(fileName);
					Type docType = m_manager.FindFileDocumentType(ext);
					if(docType == null)
					{
						MessageBox.Show(m_mainWindow, "Unknown file type for '" + fileName + "'");
						continue;
					}
					DocumentItem docItem = m_manager.Project.AddDocument(folder, fileName, docType);
					m_manager.ShowDocument(docItem);
				}
			}
		}

		private void removeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			RemoveItem(projectView.SelectedNode);
		}

		private void RemoveItem(TreeNode item)
		{
			if (item.Tag is Folder)
			{

			}
			else if (item.Tag is DocumentItem)
			{
				DocumentItem docItem = (DocumentItem)item.Tag;

				if (docItem.ProjectDocument.ReadOnly && !docItem.ProjectDocument.Checkout())
					return;

				m_manager.Project.RemoveDocument(docItem);
			}
		}

		private void projectView_BeforeLabelEdit(object sender, NodeLabelEditEventArgs e)
		{
			ProjectItem projItem = (ProjectItem)e.Node.Tag;
			if (!projItem.CanRename)
				e.CancelEdit = true;
		}

		private void projectView_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyData == Keys.Delete)
			{
				RemoveItem(projectView.SelectedNode);
				e.Handled = true;
			}
		}

		private void projectViewContextMenu_Opening(object sender, CancelEventArgs e)
		{
			// Don't show the context menu if there isn't even a project root
			if (projectView.Nodes.Count == 0)
				e.Cancel = true;

			bool nodeSelected = projectView.SelectedNode != null;
			bool folderSelected = nodeSelected && projectView.SelectedNode.Tag is Folder;
			bool documentSelected = nodeSelected && projectView.SelectedNode.Tag is DocumentItem;
			addToolStripMenuItem.Enabled = folderSelected || documentSelected;
			removeToolStripMenuItem.Enabled = documentSelected;
			renameToolStripMenuItem.Enabled = false;
		}

		private void projectView_AfterSelect(object sender, TreeViewEventArgs e)
		{
			if (e.Node != null)
				m_manager.SelectedObject = e.Node.Tag;
			else
				m_manager.SelectedObject = null;
		}
	}
}


