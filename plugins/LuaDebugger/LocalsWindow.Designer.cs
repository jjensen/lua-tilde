
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
	partial class LocalsWindow
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LocalsWindow));
			this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.copyToWatch = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.buttonRefresh = new System.Windows.Forms.ToolStripButton();
			this.buttonShowFunctions = new System.Windows.Forms.ToolStripButton();
			this.buttonShowMetadata = new System.Windows.Forms.ToolStripButton();
			this.contextMenuStrip.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// variablesListView
			// 
			this.variablesListView.ContextMenuStrip = this.contextMenuStrip;
			this.variablesListView.Location = new System.Drawing.Point(0, 25);
			this.variablesListView.Size = new System.Drawing.Size(292, 248);
			// 
			// contextMenuStrip
			// 
			this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToWatch});
			this.contextMenuStrip.Name = "contextMenuStrip";
			this.contextMenuStrip.Size = new System.Drawing.Size(147, 26);
			this.contextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip_Opening);
			// 
			// copyToWatch
			// 
			this.copyToWatch.Name = "copyToWatch";
			this.copyToWatch.Size = new System.Drawing.Size(146, 22);
			this.copyToWatch.Text = "Copy to Watch";
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonRefresh,
            this.buttonShowFunctions,
            this.buttonShowMetadata});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(292, 25);
			this.toolStrip1.TabIndex = 7;
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
			// LocalsWindow
			// 
			this.ClientSize = new System.Drawing.Size(292, 273);
			this.Controls.Add(this.toolStrip1);
			this.Name = "LocalsWindow";
			this.TabText = "Locals";
			this.Text = "Locals";
			this.Controls.SetChildIndex(this.toolStrip1, 0);
			this.Controls.SetChildIndex(this.variablesListView, 0);
			this.contextMenuStrip.ResumeLayout(false);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem copyToWatch;
		protected System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton buttonRefresh;
		private System.Windows.Forms.ToolStripButton buttonShowFunctions;
		private System.Windows.Forms.ToolStripButton buttonShowMetadata;
	}
}
