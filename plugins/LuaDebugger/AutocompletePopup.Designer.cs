
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
	partial class AutocompletePopup
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AutocompletePopup));
			this.optionsListView = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.valuesFilterButton = new System.Windows.Forms.ToolStripButton();
			this.functionsFilterButton = new System.Windows.Forms.ToolStripButton();
			this.tablesFilterButton = new System.Windows.Forms.ToolStripButton();
			this.expressionLabel = new System.Windows.Forms.Label();
			this.waitingPanel = new System.Windows.Forms.Panel();
			this.progressBar = new System.Windows.Forms.ProgressBar();
			this.mainPanel = new System.Windows.Forms.TableLayoutPanel();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.errorPanel = new System.Windows.Forms.Panel();
			this.errorLabel = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.toolStrip1.SuspendLayout();
			this.waitingPanel.SuspendLayout();
			this.mainPanel.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.errorPanel.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// optionsListView
			// 
			this.optionsListView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.optionsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
			this.optionsListView.FullRowSelect = true;
			this.optionsListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.optionsListView.Location = new System.Drawing.Point(0, 0);
			this.optionsListView.Margin = new System.Windows.Forms.Padding(0);
			this.optionsListView.MaximumSize = new System.Drawing.Size(320, 240);
			this.optionsListView.Name = "optionsListView";
			this.optionsListView.Size = new System.Drawing.Size(191, 182);
			this.optionsListView.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.optionsListView.TabIndex = 0;
			this.optionsListView.UseCompatibleStateImageBehavior = false;
			this.optionsListView.View = System.Windows.Forms.View.Details;
			this.optionsListView.ItemActivate += new System.EventHandler(this.optionsListView_ItemActivate);
			this.optionsListView.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.optionsListView_KeyPress);
			this.optionsListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.optionsListView_KeyDown);
			// 
			// columnHeader1
			// 
			this.columnHeader1.DisplayIndex = 1;
			// 
			// columnHeader2
			// 
			this.columnHeader2.DisplayIndex = 0;
			this.columnHeader2.Text = "";
			// 
			// toolStrip1
			// 
			this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
			this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.functionsFilterButton,
            this.tablesFilterButton,
            this.valuesFilterButton});
			this.toolStrip1.Location = new System.Drawing.Point(0, 182);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(72, 25);
			this.toolStrip1.TabIndex = 1;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// valuesFilterButton
			// 
			this.valuesFilterButton.CheckOnClick = true;
			this.valuesFilterButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.valuesFilterButton.Image = ((System.Drawing.Image)(resources.GetObject("valuesFilterButton.Image")));
			this.valuesFilterButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.valuesFilterButton.Name = "valuesFilterButton";
			this.valuesFilterButton.Size = new System.Drawing.Size(23, 22);
			this.valuesFilterButton.Text = "Values";
			this.valuesFilterButton.Click += new System.EventHandler(this.valuesFilterButton_Click);
			// 
			// functionsFilterButton
			// 
			this.functionsFilterButton.CheckOnClick = true;
			this.functionsFilterButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.functionsFilterButton.Image = ((System.Drawing.Image)(resources.GetObject("functionsFilterButton.Image")));
			this.functionsFilterButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.functionsFilterButton.Name = "functionsFilterButton";
			this.functionsFilterButton.Size = new System.Drawing.Size(23, 22);
			this.functionsFilterButton.Text = "Functions";
			this.functionsFilterButton.Click += new System.EventHandler(this.functionsFilterButton_Click);
			// 
			// tablesFilterButton
			// 
			this.tablesFilterButton.CheckOnClick = true;
			this.tablesFilterButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tablesFilterButton.Image = ((System.Drawing.Image)(resources.GetObject("tablesFilterButton.Image")));
			this.tablesFilterButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tablesFilterButton.Name = "tablesFilterButton";
			this.tablesFilterButton.Size = new System.Drawing.Size(23, 22);
			this.tablesFilterButton.Text = "Tables";
			this.tablesFilterButton.Click += new System.EventHandler(this.tablesFilterButton_Click);
			// 
			// expressionLabel
			// 
			this.expressionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.expressionLabel.Location = new System.Drawing.Point(3, 0);
			this.expressionLabel.Margin = new System.Windows.Forms.Padding(3, 0, 3, 4);
			this.expressionLabel.Name = "expressionLabel";
			this.expressionLabel.Size = new System.Drawing.Size(185, 15);
			this.expressionLabel.TabIndex = 2;
			this.expressionLabel.Text = "label1";
			// 
			// waitingPanel
			// 
			this.waitingPanel.AutoSize = true;
			this.waitingPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.waitingPanel.Controls.Add(this.progressBar);
			this.waitingPanel.Location = new System.Drawing.Point(0, 19);
			this.waitingPanel.Margin = new System.Windows.Forms.Padding(0);
			this.waitingPanel.Name = "waitingPanel";
			this.waitingPanel.Size = new System.Drawing.Size(191, 25);
			this.waitingPanel.TabIndex = 2;
			// 
			// progressBar
			// 
			this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.progressBar.Location = new System.Drawing.Point(3, 3);
			this.progressBar.Name = "progressBar";
			this.progressBar.Size = new System.Drawing.Size(188, 19);
			this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
			this.progressBar.TabIndex = 0;
			// 
			// mainPanel
			// 
			this.mainPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.mainPanel.AutoSize = true;
			this.mainPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.mainPanel.BackColor = System.Drawing.Color.Transparent;
			this.mainPanel.ColumnCount = 1;
			this.mainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.mainPanel.Controls.Add(this.optionsListView, 0, 0);
			this.mainPanel.Controls.Add(this.toolStrip1, 0, 1);
			this.mainPanel.Location = new System.Drawing.Point(0, 44);
			this.mainPanel.Margin = new System.Windows.Forms.Padding(0);
			this.mainPanel.Name = "mainPanel";
			this.mainPanel.RowCount = 3;
			this.mainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.mainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.mainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.mainPanel.Size = new System.Drawing.Size(191, 227);
			this.mainPanel.TabIndex = 2;
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.AutoSize = true;
			this.tableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel2.BackColor = System.Drawing.Color.Transparent;
			this.tableLayoutPanel2.ColumnCount = 1;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel2.Controls.Add(this.expressionLabel, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.waitingPanel, 0, 1);
			this.tableLayoutPanel2.Controls.Add(this.mainPanel, 0, 2);
			this.tableLayoutPanel2.Controls.Add(this.errorPanel, 0, 3);
			this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 4;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.Size = new System.Drawing.Size(191, 290);
			this.tableLayoutPanel2.TabIndex = 4;
			// 
			// errorPanel
			// 
			this.errorPanel.AutoSize = true;
			this.errorPanel.Controls.Add(this.errorLabel);
			this.errorPanel.Location = new System.Drawing.Point(3, 274);
			this.errorPanel.Name = "errorPanel";
			this.errorPanel.Size = new System.Drawing.Size(29, 13);
			this.errorPanel.TabIndex = 6;
			// 
			// errorLabel
			// 
			this.errorLabel.AutoSize = true;
			this.errorLabel.ForeColor = System.Drawing.Color.Red;
			this.errorLabel.Location = new System.Drawing.Point(-3, 0);
			this.errorLabel.MaximumSize = new System.Drawing.Size(188, 0);
			this.errorLabel.Name = "errorLabel";
			this.errorLabel.Size = new System.Drawing.Size(29, 13);
			this.errorLabel.TabIndex = 0;
			this.errorLabel.Text = "Error";
			// 
			// panel1
			// 
			this.panel1.AutoSize = true;
			this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.panel1.BackColor = System.Drawing.Color.Transparent;
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel1.Controls.Add(this.tableLayoutPanel2);
			this.panel1.Location = new System.Drawing.Point(1, 1);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(195, 294);
			this.panel1.TabIndex = 5;
			// 
			// imageList1
			// 
			this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// AutocompletePopup
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.ClientSize = new System.Drawing.Size(475, 358);
			this.Controls.Add(this.panel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.KeyPreview = true;
			this.Name = "AutocompletePopup";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "AutocompletePopup";
			this.Deactivate += new System.EventHandler(this.AutocompletePopup_Deactivate);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AutocompletePopup_KeyDown);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.waitingPanel.ResumeLayout(false);
			this.mainPanel.ResumeLayout(false);
			this.mainPanel.PerformLayout();
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel2.PerformLayout();
			this.errorPanel.ResumeLayout(false);
			this.errorPanel.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListView optionsListView;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton valuesFilterButton;
		private System.Windows.Forms.ToolStripButton functionsFilterButton;
		private System.Windows.Forms.ToolStripButton tablesFilterButton;
		private System.Windows.Forms.Label expressionLabel;
		private System.Windows.Forms.Panel waitingPanel;
		private System.Windows.Forms.ProgressBar progressBar;
		private System.Windows.Forms.TableLayoutPanel mainPanel;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.Panel errorPanel;
		private System.Windows.Forms.Label errorLabel;
	}
}