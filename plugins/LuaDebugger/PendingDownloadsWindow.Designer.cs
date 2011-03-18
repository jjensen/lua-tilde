
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

namespace Tilde.LuaDebugger
{
	partial class PendingDownloadsWindow
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
			this.pendingFileListView = new System.Windows.Forms.ListView();
			this.columnHeaderName = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderType = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderStatus = new System.Windows.Forms.ColumnHeader();
			this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.menuItemOpenInEditor = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemDownloadNow = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemRemove = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemAddFiles = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.progressBarDownloading = new System.Windows.Forms.ToolStripProgressBar();
			this.contextMenuStrip.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// pendingFileListView
			// 
			this.pendingFileListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderName,
            this.columnHeaderType,
            this.columnHeaderStatus});
			this.pendingFileListView.ContextMenuStrip = this.contextMenuStrip;
			this.pendingFileListView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pendingFileListView.FullRowSelect = true;
			this.pendingFileListView.Location = new System.Drawing.Point(0, 25);
			this.pendingFileListView.Name = "pendingFileListView";
			this.pendingFileListView.Size = new System.Drawing.Size(356, 248);
			this.pendingFileListView.TabIndex = 0;
			this.pendingFileListView.UseCompatibleStateImageBehavior = false;
			this.pendingFileListView.View = System.Windows.Forms.View.Details;
			this.pendingFileListView.ItemActivate += new System.EventHandler(this.pendingFileListView_ItemActivate);
			// 
			// columnHeaderName
			// 
			this.columnHeaderName.Text = "Name";
			this.columnHeaderName.Width = 228;
			// 
			// columnHeaderType
			// 
			this.columnHeaderType.Text = "Type";
			// 
			// columnHeaderStatus
			// 
			this.columnHeaderStatus.Text = "Status";
			// 
			// contextMenuStrip
			// 
			this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemOpenInEditor,
            this.menuItemDownloadNow,
            this.menuItemRemove,
            this.menuItemAddFiles});
			this.contextMenuStrip.Name = "contextMenuStrip";
			this.contextMenuStrip.Size = new System.Drawing.Size(170, 92);
			this.contextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip_Opening);
			// 
			// menuItemOpenInEditor
			// 
			this.menuItemOpenInEditor.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.menuItemOpenInEditor.Name = "menuItemOpenInEditor";
			this.menuItemOpenInEditor.Size = new System.Drawing.Size(169, 22);
			this.menuItemOpenInEditor.Text = "Open in Editor";
			this.menuItemOpenInEditor.Click += new System.EventHandler(this.menuItemOpenInEditor_Click);
			// 
			// menuItemDownloadNow
			// 
			this.menuItemDownloadNow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.menuItemDownloadNow.Name = "menuItemDownloadNow";
			this.menuItemDownloadNow.Size = new System.Drawing.Size(169, 22);
			this.menuItemDownloadNow.Text = "Download to Target";
			this.menuItemDownloadNow.Click += new System.EventHandler(this.menuItemDownloadNow_Click);
			// 
			// menuItemRemove
			// 
			this.menuItemRemove.Name = "menuItemRemove";
			this.menuItemRemove.ShortcutKeys = System.Windows.Forms.Keys.Delete;
			this.menuItemRemove.Size = new System.Drawing.Size(169, 22);
			this.menuItemRemove.Text = "Remove";
			this.menuItemRemove.Click += new System.EventHandler(this.menuItemRemove_Click);
			// 
			// menuItemAddFiles
			// 
			this.menuItemAddFiles.Name = "menuItemAddFiles";
			this.menuItemAddFiles.Size = new System.Drawing.Size(169, 22);
			this.menuItemAddFiles.Text = "Add Files...";
			this.menuItemAddFiles.Visible = false;
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.progressBarDownloading});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(356, 25);
			this.toolStrip1.TabIndex = 1;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// progressBarDownloading
			// 
			this.progressBarDownloading.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.progressBarDownloading.Name = "progressBarDownloading";
			this.progressBarDownloading.Size = new System.Drawing.Size(100, 22);
			this.progressBarDownloading.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
			// 
			// PendingDownloadsWindow
			// 
			this.ClientSize = new System.Drawing.Size(356, 273);
			this.Controls.Add(this.pendingFileListView);
			this.Controls.Add(this.toolStrip1);
			this.Name = "PendingDownloadsWindow";
			this.TabText = "Pending Downloads";
			this.Text = "Pending Downloads";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PendingDownloadsWindow_FormClosing);
			this.contextMenuStrip.ResumeLayout(false);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListView pendingFileListView;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ColumnHeader columnHeaderName;
		private System.Windows.Forms.ColumnHeader columnHeaderType;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem menuItemDownloadNow;
		private System.Windows.Forms.ToolStripMenuItem menuItemRemove;
		private System.Windows.Forms.ToolStripMenuItem menuItemAddFiles;
		private System.Windows.Forms.ColumnHeader columnHeaderStatus;
		private System.Windows.Forms.ToolStripMenuItem menuItemOpenInEditor;
		private System.Windows.Forms.ToolStripProgressBar progressBarDownloading;
	}
}
