
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
	partial class WatchWindow
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WatchWindow));
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.buttonRefresh = new System.Windows.Forms.ToolStripButton();
			this.buttonShowFunctions = new System.Windows.Forms.ToolStripButton();
			this.buttonShowMetadata = new System.Windows.Forms.ToolStripButton();
			this.buttonNewWatch = new System.Windows.Forms.ToolStripButton();
			this.buttonDeleteWatch = new System.Windows.Forms.ToolStripButton();
			this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.menuNewWatch = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.menuEditWatch = new System.Windows.Forms.ToolStripMenuItem();
			this.menuDeleteWatch = new System.Windows.Forms.ToolStripMenuItem();
			this.menuCopyWatch = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStrip1.SuspendLayout();
			this.contextMenuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// variablesListView
			// 
			this.variablesListView.AllowDrop = true;
			this.variablesListView.ContextMenuStrip = this.contextMenuStrip;
			this.variablesListView.LabelEdit = true;
			this.variablesListView.Location = new System.Drawing.Point(0, 25);
			this.variablesListView.Size = new System.Drawing.Size(292, 248);
			this.variablesListView.DragOver += new System.Windows.Forms.DragEventHandler(this.variablesListView_DragOver);
			this.variablesListView.BeforeLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.variablesListView_BeforeLabelEdit);
			this.variablesListView.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.variablesListView_AfterLabelEdit);
			this.variablesListView.DragDrop += new System.Windows.Forms.DragEventHandler(this.variablesListView_DragDrop);
			this.variablesListView.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.variablesListView_KeyPress);
			this.variablesListView.GiveFeedback += new System.Windows.Forms.GiveFeedbackEventHandler(this.variablesListView_GiveFeedback);
			this.variablesListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.variablesListView_KeyDown);
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonRefresh,
            this.buttonShowFunctions,
            this.buttonShowMetadata,
            this.toolStripSeparator2,
            this.buttonNewWatch,
            this.buttonDeleteWatch});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(292, 25);
			this.toolStrip1.TabIndex = 1;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// buttonRefresh
			// 
			this.buttonRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.buttonRefresh.Image = global::Tilde.LuaDebugger.Properties.Resources.BrwRefresh;
			this.buttonRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.buttonRefresh.Name = "buttonRefresh";
			this.buttonRefresh.Size = new System.Drawing.Size(23, 22);
			this.buttonRefresh.Text = "Refresh";
			this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
			// 
			// buttonShowFunctions
			// 
			this.buttonShowFunctions.Checked = true;
			this.buttonShowFunctions.CheckState = System.Windows.Forms.CheckState.Checked;
			this.buttonShowFunctions.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.buttonShowFunctions.Image = ((System.Drawing.Image)(resources.GetObject("buttonShowFunctions.Image")));
			this.buttonShowFunctions.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.buttonShowFunctions.Name = "buttonShowFunctions";
			this.buttonShowFunctions.Size = new System.Drawing.Size(23, 22);
			this.buttonShowFunctions.Text = "Show Functions";
			this.buttonShowFunctions.Click += new System.EventHandler(this.buttonShowFunctions_Click);
			// 
			// buttonShowMetadata
			// 
			this.buttonShowMetadata.Checked = true;
			this.buttonShowMetadata.CheckState = System.Windows.Forms.CheckState.Checked;
			this.buttonShowMetadata.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.buttonShowMetadata.Image = ((System.Drawing.Image)(resources.GetObject("buttonShowMetadata.Image")));
			this.buttonShowMetadata.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.buttonShowMetadata.Name = "buttonShowMetadata";
			this.buttonShowMetadata.Size = new System.Drawing.Size(23, 22);
			this.buttonShowMetadata.Text = "Show Metadata";
			this.buttonShowMetadata.Click += new System.EventHandler(this.buttonShowMetadata_Click);
			// 
			// buttonNewWatch
			// 
			this.buttonNewWatch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.buttonNewWatch.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.buttonNewWatch.Name = "buttonNewWatch";
			this.buttonNewWatch.Size = new System.Drawing.Size(23, 22);
			this.buttonNewWatch.Text = "New Watch";
			this.buttonNewWatch.Click += new System.EventHandler(this.buttonNewWatch_Click);
			// 
			// buttonDeleteWatch
			// 
			this.buttonDeleteWatch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.buttonDeleteWatch.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.buttonDeleteWatch.Name = "buttonDeleteWatch";
			this.buttonDeleteWatch.Size = new System.Drawing.Size(23, 22);
			this.buttonDeleteWatch.Text = "Delete Watch";
			this.buttonDeleteWatch.Click += new System.EventHandler(this.buttonDeleteWatch_Click);
			// 
			// contextMenuStrip
			// 
			this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuNewWatch,
            this.toolStripSeparator1,
            this.menuEditWatch,
            this.menuDeleteWatch,
            this.menuCopyWatch});
			this.contextMenuStrip.Name = "contextMenuStrip1";
			this.contextMenuStrip.Size = new System.Drawing.Size(162, 98);
			this.contextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip_Opening);
			// 
			// menuNewWatch
			// 
			this.menuNewWatch.Name = "menuNewWatch";
			this.menuNewWatch.Size = new System.Drawing.Size(161, 22);
			this.menuNewWatch.Text = "New Watch";
			this.menuNewWatch.Click += new System.EventHandler(this.menuNewWatch_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(158, 6);
			// 
			// menuEditWatch
			// 
			this.menuEditWatch.Name = "menuEditWatch";
			this.menuEditWatch.ShortcutKeys = System.Windows.Forms.Keys.F2;
			this.menuEditWatch.Size = new System.Drawing.Size(161, 22);
			this.menuEditWatch.Text = "Edit Watch";
			this.menuEditWatch.Click += new System.EventHandler(this.menuEditWatch_Click);
			// 
			// menuDeleteWatch
			// 
			this.menuDeleteWatch.Name = "menuDeleteWatch";
			this.menuDeleteWatch.ShortcutKeys = System.Windows.Forms.Keys.Delete;
			this.menuDeleteWatch.Size = new System.Drawing.Size(161, 22);
			this.menuDeleteWatch.Text = "Delete Watch";
			this.menuDeleteWatch.Click += new System.EventHandler(this.menuDeleteWatch_Click);
			// 
			// menuCopyWatch
			// 
			this.menuCopyWatch.Name = "menuCopyWatch";
			this.menuCopyWatch.Size = new System.Drawing.Size(161, 22);
			this.menuCopyWatch.Text = "Copy Watch";
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// WatchWindow
			// 
			this.ClientSize = new System.Drawing.Size(292, 273);
			this.Controls.Add(this.toolStrip1);
			this.Name = "WatchWindow";
			this.TabText = "Watch";
			this.Text = "Watches";
			this.Controls.SetChildIndex(this.toolStrip1, 0);
			this.Controls.SetChildIndex(this.variablesListView, 0);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.contextMenuStrip.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton buttonRefresh;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem menuNewWatch;
		private System.Windows.Forms.ToolStripMenuItem menuEditWatch;
		private System.Windows.Forms.ToolStripMenuItem menuDeleteWatch;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem menuCopyWatch;
		private System.Windows.Forms.ToolStripButton buttonShowFunctions;
		private System.Windows.Forms.ToolStripButton buttonNewWatch;
		private System.Windows.Forms.ToolStripButton buttonDeleteWatch;
		private System.Windows.Forms.ToolStripButton buttonShowMetadata;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
	}
}
