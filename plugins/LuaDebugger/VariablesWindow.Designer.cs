
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
	partial class VariablesWindow
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VariablesWindow));
			this.variablesListView = new Tilde.Framework.Controls.TreeTable();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.baseContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.modifyValue = new System.Windows.Forms.ToolStripMenuItem();
			this.goToSource = new System.Windows.Forms.ToolStripMenuItem();
			this.baseContextMenuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// variablesListView
			// 
			this.variablesListView.Activation = System.Windows.Forms.ItemActivation.Standard;
			this.variablesListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
			this.variablesListView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.variablesListView.FullRowSelect = true;
			this.variablesListView.GridLines = true;
			this.variablesListView.Location = new System.Drawing.Point(0, 0);
			this.variablesListView.Name = "variablesListView";
			this.variablesListView.Size = new System.Drawing.Size(292, 273);
			this.variablesListView.SmallImageList = this.imageList1;
			this.variablesListView.TabIndex = 0;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Name";
			this.columnHeader1.Width = 117;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Value";
			this.columnHeader2.Width = 80;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Type";
			this.columnHeader3.Width = 91;
			// 
			// imageList1
			// 
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Fuchsia;
			this.imageList1.Images.SetKeyName(0, "tree_open.bmp");
			this.imageList1.Images.SetKeyName(1, "tree_closed.bmp");
			// 
			// baseContextMenuStrip
			// 
			this.baseContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this.modifyValue,
            this.goToSource});
			this.baseContextMenuStrip.Name = "baseContextMenuStrip";
			this.baseContextMenuStrip.Size = new System.Drawing.Size(139, 54);
			this.baseContextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.baseContextMenuStrip_Opening);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(135, 6);
			// 
			// modifyValue
			// 
			this.modifyValue.Name = "modifyValue";
			this.modifyValue.Size = new System.Drawing.Size(138, 22);
			this.modifyValue.Text = "Modify Value";
			// 
			// goToSource
			// 
			this.goToSource.Name = "goToSource";
			this.goToSource.Size = new System.Drawing.Size(138, 22);
			this.goToSource.Text = "Go To Source";
			this.goToSource.Click += new System.EventHandler(this.goToSource_Click);
			// 
			// VariablesWindow
			// 
			this.ClientSize = new System.Drawing.Size(292, 273);
			this.Controls.Add(this.variablesListView);
			this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)(((((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft)
						| WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight)
						| WeifenLuo.WinFormsUI.Docking.DockAreas.DockTop)
						| WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom)));
			this.Name = "VariablesWindow";
			this.baseContextMenuStrip.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		protected Tilde.Framework.Controls.TreeTable variablesListView;
		private System.Windows.Forms.ToolTip toolTip;
		private System.Windows.Forms.ToolStripMenuItem modifyValue;
		private System.Windows.Forms.ToolStripMenuItem goToSource;
		protected System.Windows.Forms.ContextMenuStrip baseContextMenuStrip;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
	}
}
