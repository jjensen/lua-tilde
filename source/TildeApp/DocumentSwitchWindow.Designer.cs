
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
	partial class DocumentSwitchWindow
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
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.panelActiveToolWindows = new System.Windows.Forms.FlowLayoutPanel();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.panelActiveFiles = new System.Windows.Forms.FlowLayoutPanel();
			this.button1 = new System.Windows.Forms.Button();
			this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
			this.labelDocumentName = new System.Windows.Forms.Label();
			this.labelDocumentType = new System.Windows.Forms.Label();
			this.labelDocumentPath = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.tableLayoutPanel1.SuspendLayout();
			this.panelActiveFiles.SuspendLayout();
			this.flowLayoutPanel3.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.AutoSize = true;
			this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.Controls.Add(this.panelActiveToolWindows, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.label1, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.panelActiveFiles, 1, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(4, 4);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(518, 253);
			this.tableLayoutPanel1.TabIndex = 3;
			// 
			// panelActiveToolWindows
			// 
			this.panelActiveToolWindows.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.panelActiveToolWindows.AutoSize = true;
			this.panelActiveToolWindows.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.panelActiveToolWindows.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.panelActiveToolWindows.Location = new System.Drawing.Point(1, 14);
			this.panelActiveToolWindows.Margin = new System.Windows.Forms.Padding(1);
			this.panelActiveToolWindows.MaximumSize = new System.Drawing.Size(1000, 300);
			this.panelActiveToolWindows.MinimumSize = new System.Drawing.Size(160, 240);
			this.panelActiveToolWindows.Name = "panelActiveToolWindows";
			this.panelActiveToolWindows.Size = new System.Drawing.Size(160, 240);
			this.panelActiveToolWindows.TabIndex = 3;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(165, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(73, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Active Files";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(3, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(127, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "Active Tool Windows";
			// 
			// panelActiveFiles
			// 
			this.panelActiveFiles.AutoSize = true;
			this.panelActiveFiles.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.panelActiveFiles.Controls.Add(this.button1);
			this.panelActiveFiles.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.panelActiveFiles.Location = new System.Drawing.Point(163, 14);
			this.panelActiveFiles.Margin = new System.Windows.Forms.Padding(1);
			this.panelActiveFiles.MaximumSize = new System.Drawing.Size(1000, 300);
			this.panelActiveFiles.MinimumSize = new System.Drawing.Size(380, 240);
			this.panelActiveFiles.Name = "panelActiveFiles";
			this.panelActiveFiles.Size = new System.Drawing.Size(380, 240);
			this.panelActiveFiles.TabIndex = 2;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(3, 3);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 0;
			this.button1.Text = "button1";
			this.button1.UseVisualStyleBackColor = true;
			// 
			// flowLayoutPanel3
			// 
			this.flowLayoutPanel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.flowLayoutPanel3.Controls.Add(this.labelDocumentName);
			this.flowLayoutPanel3.Controls.Add(this.labelDocumentType);
			this.flowLayoutPanel3.Controls.Add(this.labelDocumentPath);
			this.flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.flowLayoutPanel3.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.flowLayoutPanel3.Location = new System.Drawing.Point(4, 257);
			this.flowLayoutPanel3.Name = "flowLayoutPanel3";
			this.flowLayoutPanel3.Size = new System.Drawing.Size(518, 64);
			this.flowLayoutPanel3.TabIndex = 0;
			// 
			// labelDocumentName
			// 
			this.labelDocumentName.AutoSize = true;
			this.labelDocumentName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelDocumentName.Location = new System.Drawing.Point(3, 3);
			this.labelDocumentName.Margin = new System.Windows.Forms.Padding(3);
			this.labelDocumentName.Name = "labelDocumentName";
			this.labelDocumentName.Size = new System.Drawing.Size(57, 13);
			this.labelDocumentName.TabIndex = 0;
			this.labelDocumentName.Text = "Filename";
			// 
			// labelDocumentType
			// 
			this.labelDocumentType.AutoSize = true;
			this.labelDocumentType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelDocumentType.Location = new System.Drawing.Point(3, 22);
			this.labelDocumentType.Margin = new System.Windows.Forms.Padding(3);
			this.labelDocumentType.Name = "labelDocumentType";
			this.labelDocumentType.Size = new System.Drawing.Size(83, 13);
			this.labelDocumentType.TabIndex = 1;
			this.labelDocumentType.Text = "Document Type";
			// 
			// labelDocumentPath
			// 
			this.labelDocumentPath.AutoSize = true;
			this.labelDocumentPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelDocumentPath.Location = new System.Drawing.Point(3, 41);
			this.labelDocumentPath.Margin = new System.Windows.Forms.Padding(3);
			this.labelDocumentPath.Name = "labelDocumentPath";
			this.labelDocumentPath.Size = new System.Drawing.Size(81, 13);
			this.labelDocumentPath.TabIndex = 2;
			this.labelDocumentPath.Text = "Document Path";
			// 
			// panel1
			// 
			this.panel1.AutoSize = true;
			this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.panel1.BackColor = System.Drawing.Color.Transparent;
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.tableLayoutPanel1);
			this.panel1.Controls.Add(this.flowLayoutPanel3);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Padding = new System.Windows.Forms.Padding(4);
			this.panel1.Size = new System.Drawing.Size(528, 327);
			this.panel1.TabIndex = 0;
			// 
			// DocumentSwitchWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.ClientSize = new System.Drawing.Size(528, 327);
			this.Controls.Add(this.panel1);
			this.DoubleBuffered = true;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.KeyPreview = true;
			this.Name = "DocumentSwitchWindow";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "DocumentSwitchWindow";
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.DocumentSwitchWindow_KeyUp);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DocumentSwitchWindow_KeyDown);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.panelActiveFiles.ResumeLayout(false);
			this.flowLayoutPanel3.ResumeLayout(false);
			this.flowLayoutPanel3.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.FlowLayoutPanel panelActiveFiles;
		private System.Windows.Forms.FlowLayoutPanel panelActiveToolWindows;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
		private System.Windows.Forms.Label labelDocumentName;
		private System.Windows.Forms.Label labelDocumentType;
		private System.Windows.Forms.Label labelDocumentPath;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button button1;
	}
}