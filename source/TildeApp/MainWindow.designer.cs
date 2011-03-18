
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
	partial class MainWindow
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private WeifenLuo.WinFormsUI.Docking.DockPanel mDockPanel;

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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
			this.menuStrip = new System.Windows.Forms.MenuStrip();
			this.tsiFile = new System.Windows.Forms.ToolStripMenuItem();
			this.tsiFileNewProject = new System.Windows.Forms.ToolStripMenuItem();
			this.tsiFileOpenProject = new System.Windows.Forms.ToolStripMenuItem();
			this.tsiFileSaveProject = new System.Windows.Forms.ToolStripMenuItem();
			this.tsiFileCloseProject = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.tsiFileOpenFile = new System.Windows.Forms.ToolStripMenuItem();
			this.tsiFileSave = new System.Windows.Forms.ToolStripMenuItem();
			this.tsiFileSaveAs = new System.Windows.Forms.ToolStripMenuItem();
			this.tsiFileSaveAll = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.tsiFileRecentProjects = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsiFileExit = new System.Windows.Forms.ToolStripMenuItem();
			this.tsiEdit = new System.Windows.Forms.ToolStripMenuItem();
			this.goToNextLocationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.goToPreviousLocationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.pluginsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.sourceControlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.windowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.closeAllDocumentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.findFileInProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.projectWebSiteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.reportBugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.requestFeatureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.statusStrip = new System.Windows.Forms.StatusStrip();
			this.statusMessage = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
			this.saveProjectDialog = new System.Windows.Forms.SaveFileDialog();
			this.openProjectDialog = new System.Windows.Forms.OpenFileDialog();
			this.statusMessageTimer = new System.Windows.Forms.Timer(this.components);
			this.saveDocumentDialog = new System.Windows.Forms.SaveFileDialog();
			this.openDocumentDialog = new System.Windows.Forms.OpenFileDialog();
			this.toolStripPanelTop = new System.Windows.Forms.ToolStripPanel();
			this.toolStripPanelBottom = new System.Windows.Forms.ToolStripPanel();
			this.toolStripPanelLeft = new System.Windows.Forms.ToolStripPanel();
			this.toolStripPanelRight = new System.Windows.Forms.ToolStripPanel();
			this.mDockPanel = new WeifenLuo.WinFormsUI.Docking.DockPanel();
			this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.persistWindowComponent = new Tilde.Framework.View.PersistWindowComponent(this.components);
			this.menuStrip.SuspendLayout();
			this.statusStrip.SuspendLayout();
			this.toolStripPanelTop.SuspendLayout();
			this.toolStripPanelBottom.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip
			// 
			this.menuStrip.BackColor = System.Drawing.SystemColors.Control;
			this.menuStrip.Dock = System.Windows.Forms.DockStyle.None;
			this.menuStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
			this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsiFile,
            this.tsiEdit,
            this.viewToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.windowToolStripMenuItem,
            this.helpToolStripMenuItem});
			this.menuStrip.Location = new System.Drawing.Point(0, 0);
			this.menuStrip.MdiWindowListItem = this.windowToolStripMenuItem;
			this.menuStrip.Name = "menuStrip";
			this.menuStrip.Size = new System.Drawing.Size(843, 24);
			this.menuStrip.TabIndex = 0;
			this.menuStrip.Text = "menuStrip1";
			// 
			// tsiFile
			// 
			this.tsiFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsiFileNewProject,
            this.tsiFileOpenProject,
            this.tsiFileSaveProject,
            this.tsiFileCloseProject,
            this.toolStripSeparator2,
            this.tsiFileOpenFile,
            this.tsiFileSave,
            this.tsiFileSaveAs,
            this.tsiFileSaveAll,
            this.toolStripSeparator4,
            this.tsiFileRecentProjects,
            this.toolStripSeparator1,
            this.tsiFileExit});
			this.tsiFile.MergeIndex = 1;
			this.tsiFile.Name = "tsiFile";
			this.tsiFile.Size = new System.Drawing.Size(35, 20);
			this.tsiFile.Text = "&File";
			// 
			// tsiFileNewProject
			// 
			this.tsiFileNewProject.Name = "tsiFileNewProject";
			this.tsiFileNewProject.Size = new System.Drawing.Size(180, 22);
			this.tsiFileNewProject.Text = "New Project";
			// 
			// tsiFileOpenProject
			// 
			this.tsiFileOpenProject.Name = "tsiFileOpenProject";
			this.tsiFileOpenProject.Size = new System.Drawing.Size(180, 22);
			this.tsiFileOpenProject.Text = "Open Project...";
			this.tsiFileOpenProject.Click += new System.EventHandler(this.tsiFileOpenProject_Click);
			// 
			// tsiFileSaveProject
			// 
			this.tsiFileSaveProject.Name = "tsiFileSaveProject";
			this.tsiFileSaveProject.Size = new System.Drawing.Size(180, 22);
			this.tsiFileSaveProject.Text = "Save Project";
			this.tsiFileSaveProject.Click += new System.EventHandler(this.tsiFileSaveProject_Click);
			// 
			// tsiFileCloseProject
			// 
			this.tsiFileCloseProject.Name = "tsiFileCloseProject";
			this.tsiFileCloseProject.Size = new System.Drawing.Size(180, 22);
			this.tsiFileCloseProject.Text = "Close Project";
			this.tsiFileCloseProject.Click += new System.EventHandler(this.tsiFileCloseProject_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.MergeIndex = 1;
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(177, 6);
			// 
			// tsiFileOpenFile
			// 
			this.tsiFileOpenFile.Name = "tsiFileOpenFile";
			this.tsiFileOpenFile.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.tsiFileOpenFile.Size = new System.Drawing.Size(180, 22);
			this.tsiFileOpenFile.Text = "&Open File...";
			this.tsiFileOpenFile.Click += new System.EventHandler(this.tsiFileOpenFile_Click);
			// 
			// tsiFileSave
			// 
			this.tsiFileSave.Name = "tsiFileSave";
			this.tsiFileSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.tsiFileSave.Size = new System.Drawing.Size(180, 22);
			this.tsiFileSave.Text = "&Save";
			this.tsiFileSave.Click += new System.EventHandler(this.tsiFileSave_Click);
			// 
			// tsiFileSaveAs
			// 
			this.tsiFileSaveAs.Name = "tsiFileSaveAs";
			this.tsiFileSaveAs.Size = new System.Drawing.Size(180, 22);
			this.tsiFileSaveAs.Text = "Save As...";
			this.tsiFileSaveAs.Click += new System.EventHandler(this.tsiFileSaveAs_Click);
			// 
			// tsiFileSaveAll
			// 
			this.tsiFileSaveAll.Name = "tsiFileSaveAll";
			this.tsiFileSaveAll.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
						| System.Windows.Forms.Keys.S)));
			this.tsiFileSaveAll.Size = new System.Drawing.Size(180, 22);
			this.tsiFileSaveAll.Text = "Save &All";
			this.tsiFileSaveAll.Click += new System.EventHandler(this.tsiFileSaveAll_Click);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.MergeIndex = 2;
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(177, 6);
			// 
			// tsiFileRecentProjects
			// 
			this.tsiFileRecentProjects.Name = "tsiFileRecentProjects";
			this.tsiFileRecentProjects.Size = new System.Drawing.Size(180, 22);
			this.tsiFileRecentProjects.Text = "Recent Projects";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
			// 
			// tsiFileExit
			// 
			this.tsiFileExit.Name = "tsiFileExit";
			this.tsiFileExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
			this.tsiFileExit.Size = new System.Drawing.Size(180, 22);
			this.tsiFileExit.Text = "E&xit";
			this.tsiFileExit.Click += new System.EventHandler(this.tsiFileExit_Click);
			// 
			// tsiEdit
			// 
			this.tsiEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.goToNextLocationToolStripMenuItem,
            this.goToPreviousLocationToolStripMenuItem});
			this.tsiEdit.MergeIndex = 2;
			this.tsiEdit.Name = "tsiEdit";
			this.tsiEdit.Size = new System.Drawing.Size(37, 20);
			this.tsiEdit.Text = "&Edit";
			// 
			// goToNextLocationToolStripMenuItem
			// 
			this.goToNextLocationToolStripMenuItem.Name = "goToNextLocationToolStripMenuItem";
			this.goToNextLocationToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F8;
			this.goToNextLocationToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
			this.goToNextLocationToolStripMenuItem.Text = "Go to next location";
			this.goToNextLocationToolStripMenuItem.Click += new System.EventHandler(this.goToNextLocationToolStripMenuItem_Click);
			// 
			// goToPreviousLocationToolStripMenuItem
			// 
			this.goToPreviousLocationToolStripMenuItem.Name = "goToPreviousLocationToolStripMenuItem";
			this.goToPreviousLocationToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.F8)));
			this.goToPreviousLocationToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
			this.goToPreviousLocationToolStripMenuItem.Text = "Go to previous location";
			this.goToPreviousLocationToolStripMenuItem.Click += new System.EventHandler(this.goToPreviousLocationToolStripMenuItem_Click);
			// 
			// viewToolStripMenuItem
			// 
			this.viewToolStripMenuItem.MergeIndex = 3;
			this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
			this.viewToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
			this.viewToolStripMenuItem.Text = "&View";
			this.viewToolStripMenuItem.DropDownOpening += new System.EventHandler(this.viewToolStripMenuItem_DropDownOpening);
			// 
			// toolsToolStripMenuItem
			// 
			this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pluginsToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.sourceControlToolStripMenuItem});
			this.toolsToolStripMenuItem.MergeIndex = 4;
			this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
			this.toolsToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
			this.toolsToolStripMenuItem.Text = "Tools";
			// 
			// pluginsToolStripMenuItem
			// 
			this.pluginsToolStripMenuItem.Name = "pluginsToolStripMenuItem";
			this.pluginsToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
			this.pluginsToolStripMenuItem.Text = "Plugins...";
			this.pluginsToolStripMenuItem.Click += new System.EventHandler(this.pluginsToolStripMenuItem_Click);
			// 
			// optionsToolStripMenuItem
			// 
			this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
			this.optionsToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
			this.optionsToolStripMenuItem.Text = "Options...";
			this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
			// 
			// sourceControlToolStripMenuItem
			// 
			this.sourceControlToolStripMenuItem.Name = "sourceControlToolStripMenuItem";
			this.sourceControlToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
			this.sourceControlToolStripMenuItem.Text = "Source Control...";
			this.sourceControlToolStripMenuItem.Click += new System.EventHandler(this.sourceControlToolStripMenuItem_Click);
			// 
			// windowToolStripMenuItem
			// 
			this.windowToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeAllDocumentsToolStripMenuItem,
            this.findFileInProjectToolStripMenuItem,
            this.toolStripSeparator3});
			this.windowToolStripMenuItem.MergeIndex = 5;
			this.windowToolStripMenuItem.Name = "windowToolStripMenuItem";
			this.windowToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
			this.windowToolStripMenuItem.Text = "Window";
			// 
			// closeAllDocumentsToolStripMenuItem
			// 
			this.closeAllDocumentsToolStripMenuItem.Name = "closeAllDocumentsToolStripMenuItem";
			this.closeAllDocumentsToolStripMenuItem.Size = new System.Drawing.Size(239, 22);
			this.closeAllDocumentsToolStripMenuItem.Text = "Close All Documents";
			this.closeAllDocumentsToolStripMenuItem.Click += new System.EventHandler(this.closeAllDocumentsToolStripMenuItem_Click);
			// 
			// findFileInProjectToolStripMenuItem
			// 
			this.findFileInProjectToolStripMenuItem.Name = "findFileInProjectToolStripMenuItem";
			this.findFileInProjectToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Shift)
						| System.Windows.Forms.Keys.O)));
			this.findFileInProjectToolStripMenuItem.Size = new System.Drawing.Size(239, 22);
			this.findFileInProjectToolStripMenuItem.Text = "Find File in Project...";
			this.findFileInProjectToolStripMenuItem.Click += new System.EventHandler(this.findFileInProjectToolStripMenuItem_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(236, 6);
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem,
            this.projectWebSiteToolStripMenuItem,
            this.reportBugToolStripMenuItem,
            this.requestFeatureToolStripMenuItem});
			this.helpToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.Insert;
			this.helpToolStripMenuItem.MergeIndex = 6;
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
			this.helpToolStripMenuItem.Text = "Help";
			// 
			// aboutToolStripMenuItem
			// 
			this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
			this.aboutToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
			this.aboutToolStripMenuItem.Text = "About";
			this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
			// 
			// projectWebSiteToolStripMenuItem
			// 
			this.projectWebSiteToolStripMenuItem.Name = "projectWebSiteToolStripMenuItem";
			this.projectWebSiteToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
			this.projectWebSiteToolStripMenuItem.Text = "Project Web Site...";
			this.projectWebSiteToolStripMenuItem.Click += new System.EventHandler(this.projectWebSiteToolStripMenuItem_Click);
			// 
			// reportBugToolStripMenuItem
			// 
			this.reportBugToolStripMenuItem.Name = "reportBugToolStripMenuItem";
			this.reportBugToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
			this.reportBugToolStripMenuItem.Text = "Report Bug...";
			this.reportBugToolStripMenuItem.Click += new System.EventHandler(this.reportBugToolStripMenuItem_Click);
			// 
			// requestFeatureToolStripMenuItem
			// 
			this.requestFeatureToolStripMenuItem.Name = "requestFeatureToolStripMenuItem";
			this.requestFeatureToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
			this.requestFeatureToolStripMenuItem.Text = "Request Feature...";
			this.requestFeatureToolStripMenuItem.Click += new System.EventHandler(this.requestFeatureToolStripMenuItem_Click);
			// 
			// statusStrip
			// 
			this.statusStrip.Dock = System.Windows.Forms.DockStyle.None;
			this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusMessage,
            this.toolStripProgressBar});
			this.statusStrip.Location = new System.Drawing.Point(0, 0);
			this.statusStrip.Name = "statusStrip";
			this.statusStrip.Size = new System.Drawing.Size(843, 22);
			this.statusStrip.TabIndex = 1;
			this.statusStrip.Text = "statusStrip1";
			// 
			// statusMessage
			// 
			this.statusMessage.Name = "statusMessage";
			this.statusMessage.Size = new System.Drawing.Size(828, 17);
			this.statusMessage.Spring = true;
			this.statusMessage.Text = "Status message";
			this.statusMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// toolStripProgressBar
			// 
			this.toolStripProgressBar.Name = "toolStripProgressBar";
			this.toolStripProgressBar.Size = new System.Drawing.Size(100, 16);
			this.toolStripProgressBar.Visible = false;
			// 
			// saveProjectDialog
			// 
			this.saveProjectDialog.DefaultExt = "vcproj";
			this.saveProjectDialog.Filter = "Visual Studio Project Files (*.vcproj)|*.vcproj|XML Project Files (*.xml)|*.xml|A" +
				"ll Files (*.*)|*.*";
			this.saveProjectDialog.RestoreDirectory = true;
			this.saveProjectDialog.Title = "Save Project";
			// 
			// openProjectDialog
			// 
			this.openProjectDialog.DefaultExt = "vcproj";
			this.openProjectDialog.Filter = "Visual Studio Project Files (*.vcproj)|*.vcproj|XML Project Files (*.xml)|*.xml|A" +
				"ll Files (*.*)|*.*";
			this.openProjectDialog.RestoreDirectory = true;
			// 
			// statusMessageTimer
			// 
			this.statusMessageTimer.Tick += new System.EventHandler(this.statusMessageTimer_Tick);
			// 
			// saveDocumentDialog
			// 
			this.saveDocumentDialog.RestoreDirectory = true;
			this.saveDocumentDialog.SupportMultiDottedExtensions = true;
			// 
			// openDocumentDialog
			// 
			this.openDocumentDialog.Multiselect = true;
			this.openDocumentDialog.RestoreDirectory = true;
			this.openDocumentDialog.SupportMultiDottedExtensions = true;
			// 
			// toolStripPanelTop
			// 
			this.toolStripPanelTop.Controls.Add(this.menuStrip);
			this.toolStripPanelTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.toolStripPanelTop.Location = new System.Drawing.Point(0, 0);
			this.toolStripPanelTop.Name = "toolStripPanelTop";
			this.toolStripPanelTop.Orientation = System.Windows.Forms.Orientation.Horizontal;
			this.toolStripPanelTop.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
			this.toolStripPanelTop.Size = new System.Drawing.Size(843, 24);
			// 
			// toolStripPanelBottom
			// 
			this.toolStripPanelBottom.Controls.Add(this.statusStrip);
			this.toolStripPanelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.toolStripPanelBottom.Location = new System.Drawing.Point(0, 372);
			this.toolStripPanelBottom.Name = "toolStripPanelBottom";
			this.toolStripPanelBottom.Orientation = System.Windows.Forms.Orientation.Horizontal;
			this.toolStripPanelBottom.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
			this.toolStripPanelBottom.Size = new System.Drawing.Size(843, 22);
			// 
			// toolStripPanelLeft
			// 
			this.toolStripPanelLeft.Dock = System.Windows.Forms.DockStyle.Left;
			this.toolStripPanelLeft.Location = new System.Drawing.Point(0, 24);
			this.toolStripPanelLeft.Name = "toolStripPanelLeft";
			this.toolStripPanelLeft.Orientation = System.Windows.Forms.Orientation.Vertical;
			this.toolStripPanelLeft.RowMargin = new System.Windows.Forms.Padding(0, 3, 0, 0);
			this.toolStripPanelLeft.Size = new System.Drawing.Size(0, 348);
			// 
			// toolStripPanelRight
			// 
			this.toolStripPanelRight.Dock = System.Windows.Forms.DockStyle.Right;
			this.toolStripPanelRight.Location = new System.Drawing.Point(843, 24);
			this.toolStripPanelRight.Name = "toolStripPanelRight";
			this.toolStripPanelRight.Orientation = System.Windows.Forms.Orientation.Vertical;
			this.toolStripPanelRight.RowMargin = new System.Windows.Forms.Padding(0, 3, 0, 0);
			this.toolStripPanelRight.Size = new System.Drawing.Size(0, 348);
			// 
			// mDockPanel
			// 
			this.mDockPanel.ActiveAutoHideContent = null;
			this.mDockPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mDockPanel.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
			this.mDockPanel.Location = new System.Drawing.Point(0, 0);
			this.mDockPanel.Name = "mDockPanel";
			this.mDockPanel.Size = new System.Drawing.Size(843, 394);
			this.mDockPanel.TabIndex = 3;
			this.mDockPanel.ActiveContentChanged += new System.EventHandler(this.mDockPanel_ActiveContentChanged);
			this.mDockPanel.ContentAdded += new System.EventHandler<WeifenLuo.WinFormsUI.Docking.DockContentEventArgs>(this.mDockPanel_ContentAdded);
			this.mDockPanel.ActiveDocumentChanged += new System.EventHandler(this.mDockPanel_ActiveDocumentChanged);
			this.mDockPanel.ContentRemoved += new System.EventHandler<WeifenLuo.WinFormsUI.Docking.DockContentEventArgs>(this.mDockPanel_ContentRemoved);
			// 
			// notifyIcon
			// 
			this.notifyIcon.Text = "Notify Icon";
			this.notifyIcon.Visible = true;
			this.notifyIcon.BalloonTipClicked += new System.EventHandler(this.notifyIcon_BalloonTipClicked);
			this.notifyIcon.DoubleClick += new System.EventHandler(this.notifyIcon_DoubleClick);
			// 
			// persistWindowComponent
			// 
			this.persistWindowComponent.Form = this;
			this.persistWindowComponent.RegistryKey = "MainWindow";
			// 
			// MainWindow
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(843, 394);
			this.Controls.Add(this.toolStripPanelRight);
			this.Controls.Add(this.toolStripPanelLeft);
			this.Controls.Add(this.toolStripPanelBottom);
			this.Controls.Add(this.toolStripPanelTop);
			this.Controls.Add(this.mDockPanel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.IsMdiContainer = true;
			this.MainMenuStrip = this.menuStrip;
			this.Name = "MainWindow";
			this.Text = "Tilde";
			this.Load += new System.EventHandler(this.MainWindow_Load);
			this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainWindow_DragDrop);
			this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainWindow_DragEnter);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
			this.menuStrip.ResumeLayout(false);
			this.menuStrip.PerformLayout();
			this.statusStrip.ResumeLayout(false);
			this.statusStrip.PerformLayout();
			this.toolStripPanelTop.ResumeLayout(false);
			this.toolStripPanelTop.PerformLayout();
			this.toolStripPanelBottom.ResumeLayout(false);
			this.toolStripPanelBottom.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStripMenuItem tsiFile;
		private System.Windows.Forms.ToolStripMenuItem tsiFileOpenProject;
		private System.Windows.Forms.ToolStripMenuItem tsiFileSaveProject;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem tsiFileExit;
		private System.Windows.Forms.ToolStripMenuItem tsiEdit;
		private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem tsiFileNewProject;
		private System.Windows.Forms.SaveFileDialog saveProjectDialog;
		private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem pluginsToolStripMenuItem;
		public System.Windows.Forms.MenuStrip menuStrip;
		private System.Windows.Forms.ToolStripStatusLabel statusMessage;
		public System.Windows.Forms.StatusStrip statusStrip;
		private System.Windows.Forms.OpenFileDialog openProjectDialog;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem tsiFileRecentProjects;
		private System.Windows.Forms.ToolStripMenuItem windowToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem closeAllDocumentsToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem tsiFileOpenFile;
		private System.Windows.Forms.ToolStripMenuItem tsiFileSaveAll;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private Tilde.Framework.View.PersistWindowComponent persistWindowComponent;
		private System.Windows.Forms.ToolStripMenuItem tsiFileSave;
		private System.Windows.Forms.Timer statusMessageTimer;
		private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
		private System.Windows.Forms.SaveFileDialog saveDocumentDialog;
		private System.Windows.Forms.OpenFileDialog openDocumentDialog;
		public System.Windows.Forms.ToolStripPanel toolStripPanelTop;
		public System.Windows.Forms.ToolStripPanel toolStripPanelRight;
		public System.Windows.Forms.ToolStripPanel toolStripPanelLeft;
		public System.Windows.Forms.ToolStripPanel toolStripPanelBottom;
		private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
		internal System.Windows.Forms.NotifyIcon notifyIcon;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem findFileInProjectToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem reportBugToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem requestFeatureToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem tsiFileCloseProject;
		private System.Windows.Forms.ToolStripMenuItem goToNextLocationToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem goToPreviousLocationToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem tsiFileSaveAs;
		private System.Windows.Forms.ToolStripMenuItem sourceControlToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem projectWebSiteToolStripMenuItem;
	}
}

