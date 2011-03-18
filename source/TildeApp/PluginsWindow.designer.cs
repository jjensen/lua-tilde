
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
	partial class PluginsWindow
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
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.listViewPlugins = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.richTextBoxDetails = new System.Windows.Forms.RichTextBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.buttonLoadPlugin = new System.Windows.Forms.Button();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.listViewPlugins);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.richTextBoxDetails);
			this.splitContainer1.Size = new System.Drawing.Size(292, 233);
			this.splitContainer1.SplitterDistance = 145;
			this.splitContainer1.TabIndex = 0;
			// 
			// listViewPlugins
			// 
			this.listViewPlugins.CheckBoxes = true;
			this.listViewPlugins.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
			this.listViewPlugins.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listViewPlugins.Location = new System.Drawing.Point(0, 0);
			this.listViewPlugins.Name = "listViewPlugins";
			this.listViewPlugins.Size = new System.Drawing.Size(292, 145);
			this.listViewPlugins.TabIndex = 0;
			this.listViewPlugins.UseCompatibleStateImageBehavior = false;
			this.listViewPlugins.View = System.Windows.Forms.View.Details;
			this.listViewPlugins.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.listViewPlugins_ItemChecked);
			this.listViewPlugins.SelectedIndexChanged += new System.EventHandler(this.listViewPlugins_SelectedIndexChanged);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Name";
			this.columnHeader1.Width = 196;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Version";
			this.columnHeader2.Width = 91;
			// 
			// richTextBoxDetails
			// 
			this.richTextBoxDetails.Dock = System.Windows.Forms.DockStyle.Fill;
			this.richTextBoxDetails.Location = new System.Drawing.Point(0, 0);
			this.richTextBoxDetails.Name = "richTextBoxDetails";
			this.richTextBoxDetails.Size = new System.Drawing.Size(292, 84);
			this.richTextBoxDetails.TabIndex = 0;
			this.richTextBoxDetails.Text = "";
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.buttonLoadPlugin);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 233);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(292, 40);
			this.panel1.TabIndex = 1;
			// 
			// buttonLoadPlugin
			// 
			this.buttonLoadPlugin.Location = new System.Drawing.Point(12, 6);
			this.buttonLoadPlugin.Name = "buttonLoadPlugin";
			this.buttonLoadPlugin.Size = new System.Drawing.Size(75, 23);
			this.buttonLoadPlugin.TabIndex = 0;
			this.buttonLoadPlugin.Text = "Load Plugin";
			this.buttonLoadPlugin.UseVisualStyleBackColor = true;
			// 
			// PluginsWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(292, 273);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.panel1);
			this.Name = "PluginsWindow";
			this.Text = "PluginsWindow";
			this.Load += new System.EventHandler(this.PluginsWindow_Load);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PluginsWindow_FormClosing);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.ListView listViewPlugins;
		private System.Windows.Forms.RichTextBox richTextBoxDetails;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button buttonLoadPlugin;
	}
}