
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
	partial class BreakpointsWindow
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BreakpointsWindow));
			this.breakpointListView = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.removeBreakpointToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.gotoSourceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.breakpointImageList = new System.Windows.Forms.ImageList(this.components);
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.buttonNewBreakpoint = new System.Windows.Forms.ToolStripButton();
			this.buttonDisableBreakpoint = new System.Windows.Forms.ToolStripButton();
			this.buttonEnableAllBreakpoints = new System.Windows.Forms.ToolStripButton();
			this.buttonRemoveAllBreakpoints = new System.Windows.Forms.ToolStripButton();
			this.contextMenuStrip1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// breakpointListView
			// 
			this.breakpointListView.CheckBoxes = true;
			this.breakpointListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
			this.breakpointListView.ContextMenuStrip = this.contextMenuStrip1;
			this.breakpointListView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.breakpointListView.FullRowSelect = true;
			this.breakpointListView.Location = new System.Drawing.Point(0, 25);
			this.breakpointListView.Name = "breakpointListView";
			this.breakpointListView.Size = new System.Drawing.Size(465, 429);
			this.breakpointListView.SmallImageList = this.breakpointImageList;
			this.breakpointListView.TabIndex = 0;
			this.breakpointListView.UseCompatibleStateImageBehavior = false;
			this.breakpointListView.View = System.Windows.Forms.View.Details;
			this.breakpointListView.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.breakpointListView_ItemChecked);
			this.breakpointListView.DoubleClick += new System.EventHandler(this.breakpointListView_DoubleClick);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "File";
			this.columnHeader1.Width = 174;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Line";
			this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "State";
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.removeBreakpointToolStripMenuItem,
            this.toolStripSeparator1,
            this.gotoSourceToolStripMenuItem});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(139, 54);
			// 
			// removeBreakpointToolStripMenuItem
			// 
			this.removeBreakpointToolStripMenuItem.Name = "removeBreakpointToolStripMenuItem";
			this.removeBreakpointToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
			this.removeBreakpointToolStripMenuItem.Text = "Delete";
			this.removeBreakpointToolStripMenuItem.Click += new System.EventHandler(this.removeBreakpointToolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(135, 6);
			// 
			// gotoSourceToolStripMenuItem
			// 
			this.gotoSourceToolStripMenuItem.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.gotoSourceToolStripMenuItem.Name = "gotoSourceToolStripMenuItem";
			this.gotoSourceToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
			this.gotoSourceToolStripMenuItem.Text = "Go To Source";
			this.gotoSourceToolStripMenuItem.Click += new System.EventHandler(this.gotoSourceToolStripMenuItem_Click);
			// 
			// breakpointImageList
			// 
			this.breakpointImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("breakpointImageList.ImageStream")));
			this.breakpointImageList.TransparentColor = System.Drawing.Color.Fuchsia;
			this.breakpointImageList.Images.SetKeyName(0, "Breakpoint.bmp");
			this.breakpointImageList.Images.SetKeyName(1, "EmptyBreakpoint.bmp");
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonNewBreakpoint,
            this.buttonDisableBreakpoint,
            this.buttonEnableAllBreakpoints,
            this.buttonRemoveAllBreakpoints});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(465, 25);
			this.toolStrip1.TabIndex = 2;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// buttonNewBreakpoint
			// 
			this.buttonNewBreakpoint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.buttonNewBreakpoint.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.buttonNewBreakpoint.Name = "buttonNewBreakpoint";
			this.buttonNewBreakpoint.Size = new System.Drawing.Size(86, 22);
			this.buttonNewBreakpoint.Text = "New breakpoint";
			// 
			// buttonDisableBreakpoint
			// 
			this.buttonDisableBreakpoint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.buttonDisableBreakpoint.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.buttonDisableBreakpoint.Name = "buttonDisableBreakpoint";
			this.buttonDisableBreakpoint.Size = new System.Drawing.Size(99, 22);
			this.buttonDisableBreakpoint.Text = "Disable breakpoint";
			// 
			// buttonEnableAllBreakpoints
			// 
			this.buttonEnableAllBreakpoints.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.buttonEnableAllBreakpoints.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.buttonEnableAllBreakpoints.Name = "buttonEnableAllBreakpoints";
			this.buttonEnableAllBreakpoints.Size = new System.Drawing.Size(115, 22);
			this.buttonEnableAllBreakpoints.Text = "Enable all breakpoints";
			// 
			// buttonRemoveAllBreakpoints
			// 
			this.buttonRemoveAllBreakpoints.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.buttonRemoveAllBreakpoints.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.buttonRemoveAllBreakpoints.Name = "buttonRemoveAllBreakpoints";
			this.buttonRemoveAllBreakpoints.Size = new System.Drawing.Size(122, 22);
			this.buttonRemoveAllBreakpoints.Text = "Remove all breakpoints";
			// 
			// BreakpointsWindow
			// 
			this.ClientSize = new System.Drawing.Size(465, 454);
			this.Controls.Add(this.breakpointListView);
			this.Controls.Add(this.toolStrip1);
			this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)(((((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft)
						| WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight)
						| WeifenLuo.WinFormsUI.Docking.DockAreas.DockTop)
						| WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom)));
			this.Name = "BreakpointsWindow";
			this.TabText = "Breakpoints";
			this.contextMenuStrip1.ResumeLayout(false);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListView breakpointListView;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ImageList breakpointImageList;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton buttonNewBreakpoint;
		private System.Windows.Forms.ToolStripButton buttonDisableBreakpoint;
		private System.Windows.Forms.ToolStripButton buttonEnableAllBreakpoints;
		private System.Windows.Forms.ToolStripButton buttonRemoveAllBreakpoints;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.ToolStripMenuItem gotoSourceToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem removeBreakpointToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ColumnHeader columnHeader3;
	}
}
