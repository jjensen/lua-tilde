
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
	partial class LuaProfileResultsView
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
			this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
			this.profileListView = new System.Windows.Forms.ListView();
			this.columnHeaderFunction = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderSource = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderCount = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderSelf = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderChildren = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderTotalTime = new System.Windows.Forms.ColumnHeader();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripComboBox1 = new System.Windows.Forms.ToolStripComboBox();
			this.columnHeaderLanguage = new System.Windows.Forms.ColumnHeader();
			this.toolStripContainer1.ContentPanel.SuspendLayout();
			this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
			this.toolStripContainer1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStripContainer1
			// 
			// 
			// toolStripContainer1.ContentPanel
			// 
			this.toolStripContainer1.ContentPanel.Controls.Add(this.profileListView);
			this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(862, 437);
			this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
			this.toolStripContainer1.Name = "toolStripContainer1";
			this.toolStripContainer1.Size = new System.Drawing.Size(862, 462);
			this.toolStripContainer1.TabIndex = 0;
			this.toolStripContainer1.Text = "toolStripContainer1";
			// 
			// toolStripContainer1.TopToolStripPanel
			// 
			this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
			// 
			// profileListView
			// 
			this.profileListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderFunction,
            this.columnHeaderSource,
            this.columnHeaderLanguage,
            this.columnHeaderCount,
            this.columnHeaderSelf,
            this.columnHeaderChildren,
            this.columnHeaderTotalTime});
			this.profileListView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.profileListView.FullRowSelect = true;
			this.profileListView.Location = new System.Drawing.Point(0, 0);
			this.profileListView.Name = "profileListView";
			this.profileListView.Size = new System.Drawing.Size(862, 437);
			this.profileListView.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.profileListView.TabIndex = 0;
			this.profileListView.UseCompatibleStateImageBehavior = false;
			this.profileListView.View = System.Windows.Forms.View.Details;
			this.profileListView.ItemActivate += new System.EventHandler(this.profileListView_ItemActivate);
			this.profileListView.SelectedIndexChanged += new System.EventHandler(this.profileListView_SelectedIndexChanged);
			this.profileListView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.profileListView_ColumnClick);
			// 
			// columnHeaderFunction
			// 
			this.columnHeaderFunction.Text = "Function";
			this.columnHeaderFunction.Width = 210;
			// 
			// columnHeaderSource
			// 
			this.columnHeaderSource.Text = "Source";
			this.columnHeaderSource.Width = 176;
			// 
			// columnHeaderCount
			// 
			this.columnHeaderCount.DisplayIndex = 3;
			this.columnHeaderCount.Text = "Count";
			this.columnHeaderCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeaderCount.Width = 76;
			// 
			// columnHeaderSelf
			// 
			this.columnHeaderSelf.DisplayIndex = 4;
			this.columnHeaderSelf.Text = "Self";
			this.columnHeaderSelf.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeaderSelf.Width = 74;
			// 
			// columnHeaderChildren
			// 
			this.columnHeaderChildren.DisplayIndex = 5;
			this.columnHeaderChildren.Text = "Children";
			this.columnHeaderChildren.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeaderChildren.Width = 68;
			// 
			// columnHeaderTotalTime
			// 
			this.columnHeaderTotalTime.DisplayIndex = 6;
			this.columnHeaderTotalTime.Text = "Total Time";
			this.columnHeaderTotalTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeaderTotalTime.Width = 82;
			// 
			// toolStrip1
			// 
			this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBox1});
			this.toolStrip1.Location = new System.Drawing.Point(3, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(133, 25);
			this.toolStrip1.TabIndex = 0;
			// 
			// toolStripComboBox1
			// 
			this.toolStripComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.toolStripComboBox1.Items.AddRange(new object[] {
            "Seconds",
            "Milliseconds",
            "Percentages"});
			this.toolStripComboBox1.Name = "toolStripComboBox1";
			this.toolStripComboBox1.Size = new System.Drawing.Size(121, 25);
			this.toolStripComboBox1.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBox1_SelectedIndexChanged);
			// 
			// columnHeaderLanguage
			// 
			this.columnHeaderLanguage.DisplayIndex = 2;
			this.columnHeaderLanguage.Text = "Language";
			// 
			// LuaProfileResultsView
			// 
			this.ClientSize = new System.Drawing.Size(862, 462);
			this.Controls.Add(this.toolStripContainer1);
			this.Name = "LuaProfileResultsView";
			this.toolStripContainer1.ContentPanel.ResumeLayout(false);
			this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
			this.toolStripContainer1.TopToolStripPanel.PerformLayout();
			this.toolStripContainer1.ResumeLayout(false);
			this.toolStripContainer1.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ToolStripContainer toolStripContainer1;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ListView profileListView;
		private System.Windows.Forms.ColumnHeader columnHeaderFunction;
		private System.Windows.Forms.ColumnHeader columnHeaderCount;
		private System.Windows.Forms.ColumnHeader columnHeaderSelf;
		private System.Windows.Forms.ColumnHeader columnHeaderChildren;
		private System.Windows.Forms.ColumnHeader columnHeaderSource;
		private System.Windows.Forms.ColumnHeader columnHeaderTotalTime;
		private System.Windows.Forms.ToolStripComboBox toolStripComboBox1;
		private System.Windows.Forms.ColumnHeader columnHeaderLanguage;
	}
}
