
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
	partial class AboutWindow
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
			this.label1 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.richTextBoxTilde = new System.Windows.Forms.RichTextBox();
			this.linkLabel = new System.Windows.Forms.LinkLabel();
			this.listBoxLicense = new System.Windows.Forms.ListBox();
			this.richTextBoxLicense = new System.Windows.Forms.RichTextBox();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(15, 21);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(403, 37);
			this.label1.TabIndex = 0;
			this.label1.Text = "Tilde v0.5";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.button1.Location = new System.Drawing.Point(167, 550);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(97, 23);
			this.button1.TabIndex = 2;
			this.button1.Text = "Close";
			this.button1.UseVisualStyleBackColor = true;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(15, 68);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(403, 22);
			this.label2.TabIndex = 5;
			this.label2.Text = "Copyright ©  2007 - 2008 Tantalus Media Pty";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// tabControl1
			// 
			this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Location = new System.Drawing.Point(15, 109);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(403, 435);
			this.tabControl1.TabIndex = 9;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.richTextBoxTilde);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(395, 409);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Tilde License";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.linkLabel);
			this.tabPage2.Controls.Add(this.listBoxLicense);
			this.tabPage2.Controls.Add(this.richTextBoxLicense);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(395, 409);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "3rd Party Libraries";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// richTextBoxTilde
			// 
			this.richTextBoxTilde.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.richTextBoxTilde.Dock = System.Windows.Forms.DockStyle.Fill;
			this.richTextBoxTilde.Location = new System.Drawing.Point(3, 3);
			this.richTextBoxTilde.Name = "richTextBoxTilde";
			this.richTextBoxTilde.ReadOnly = true;
			this.richTextBoxTilde.Size = new System.Drawing.Size(389, 403);
			this.richTextBoxTilde.TabIndex = 5;
			this.richTextBoxTilde.Text = "";
			// 
			// linkLabel
			// 
			this.linkLabel.Location = new System.Drawing.Point(0, 65);
			this.linkLabel.Name = "linkLabel";
			this.linkLabel.Size = new System.Drawing.Size(395, 26);
			this.linkLabel.TabIndex = 12;
			this.linkLabel.TabStop = true;
			this.linkLabel.Text = "linkLabel";
			this.linkLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.linkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_LinkClicked);
			// 
			// listBoxLicense
			// 
			this.listBoxLicense.FormattingEnabled = true;
			this.listBoxLicense.Location = new System.Drawing.Point(-1, 6);
			this.listBoxLicense.Name = "listBoxLicense";
			this.listBoxLicense.Size = new System.Drawing.Size(396, 56);
			this.listBoxLicense.TabIndex = 10;
			this.listBoxLicense.SelectedIndexChanged += new System.EventHandler(this.listBoxLicense_SelectedIndexChanged);
			// 
			// richTextBoxLicense
			// 
			this.richTextBoxLicense.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.richTextBoxLicense.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.richTextBoxLicense.Location = new System.Drawing.Point(0, 94);
			this.richTextBoxLicense.Name = "richTextBoxLicense";
			this.richTextBoxLicense.ReadOnly = true;
			this.richTextBoxLicense.Size = new System.Drawing.Size(395, 319);
			this.richTextBoxLicense.TabIndex = 9;
			this.richTextBoxLicense.Text = "";
			// 
			// AboutWindow
			// 
			this.AcceptButton = this.button1;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(430, 585);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AboutWindow";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "About";
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.RichTextBox richTextBoxTilde;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.LinkLabel linkLabel;
		private System.Windows.Forms.ListBox listBoxLicense;
		private System.Windows.Forms.RichTextBox richTextBoxLicense;
	}
}