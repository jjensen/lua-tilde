
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

namespace Tilde.TildeApp
{
	partial class ProjectPanel
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProjectPanel));
			this.projectViewContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.newFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.addExistingItemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.projectView = new System.Windows.Forms.TreeView();
			this.imageList = new System.Windows.Forms.ImageList(this.components);
			this.openExistingFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.projectViewContextMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// projectViewContextMenu
			// 
			this.projectViewContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem,
            this.removeToolStripMenuItem,
            this.renameToolStripMenuItem});
			this.projectViewContextMenu.Name = "projectViewContextMenu";
			this.projectViewContextMenu.Size = new System.Drawing.Size(136, 70);
			this.projectViewContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.projectViewContextMenu_Opening);
			// 
			// addToolStripMenuItem
			// 
			this.addToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newFolderToolStripMenuItem,
            this.toolStripSeparator1,
            this.addExistingItemToolStripMenuItem});
			this.addToolStripMenuItem.Name = "addToolStripMenuItem";
			this.addToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
			this.addToolStripMenuItem.Text = "Add";
			// 
			// newFolderToolStripMenuItem
			// 
			this.newFolderToolStripMenuItem.Name = "newFolderToolStripMenuItem";
			this.newFolderToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
			this.newFolderToolStripMenuItem.Text = "New Folder";
			this.newFolderToolStripMenuItem.Click += new System.EventHandler(this.newFolderToolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(145, 6);
			// 
			// addExistingItemToolStripMenuItem
			// 
			this.addExistingItemToolStripMenuItem.Name = "addExistingItemToolStripMenuItem";
			this.addExistingItemToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
			this.addExistingItemToolStripMenuItem.Text = "Existing Item...";
			this.addExistingItemToolStripMenuItem.Click += new System.EventHandler(this.addExistingItemToolStripMenuItem_Click);
			// 
			// removeToolStripMenuItem
			// 
			this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
			this.removeToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
			this.removeToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
			this.removeToolStripMenuItem.Text = "Remove";
			this.removeToolStripMenuItem.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);
			// 
			// renameToolStripMenuItem
			// 
			this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
			this.renameToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2;
			this.renameToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
			this.renameToolStripMenuItem.Text = "Rename";
			this.renameToolStripMenuItem.Click += new System.EventHandler(this.renameToolStripMenuItem_Click);
			// 
			// projectView
			// 
			this.projectView.BackColor = System.Drawing.SystemColors.Window;
			this.projectView.ContextMenuStrip = this.projectViewContextMenu;
			this.projectView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.projectView.HideSelection = false;
			this.projectView.LabelEdit = true;
			this.projectView.Location = new System.Drawing.Point(0, 0);
			this.projectView.Name = "projectView";
			this.projectView.ShowNodeToolTips = true;
			this.projectView.Size = new System.Drawing.Size(292, 266);
			this.projectView.TabIndex = 1;
			this.projectView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.projectView_NodeMouseDoubleClick);
			this.projectView.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.projectView_AfterCollapse);
			this.projectView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.projectView_MouseClick);
			this.projectView.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.projectView_AfterLabelEdit);
			this.projectView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.projectView_AfterSelect);
			this.projectView.BeforeLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.projectView_BeforeLabelEdit);
			this.projectView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.projectView_KeyDown);
			this.projectView.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.projectView_AfterExpand);
			// 
			// imageList
			// 
			this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
			this.imageList.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList.Images.SetKeyName(0, "FolderClosed_16x16.bmp");
			this.imageList.Images.SetKeyName(1, "FolderOpen_16x16.bmp");
			this.imageList.Images.SetKeyName(2, "Document_16x16.bmp");
			// 
			// openExistingFileDialog
			// 
			this.openExistingFileDialog.Multiselect = true;
			this.openExistingFileDialog.RestoreDirectory = true;
			// 
			// ProjectPanel
			// 
			this.ClientSize = new System.Drawing.Size(292, 266);
			this.Controls.Add(this.projectView);
			this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)(((((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft)
						| WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight)
						| WeifenLuo.WinFormsUI.Docking.DockAreas.DockTop)
						| WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom)));
			this.Name = "ProjectPanel";
			this.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.DockLeft;
			this.TabText = "Project";
			this.Text = "Project";
			this.projectViewContextMenu.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ContextMenuStrip projectViewContextMenu;
		private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem newFolderToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem addExistingItemToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem;
		private System.Windows.Forms.TreeView projectView;
		private System.Windows.Forms.ImageList imageList;
		private System.Windows.Forms.OpenFileDialog openExistingFileDialog;
	}
}
