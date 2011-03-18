
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

namespace Tilde.CorePlugins.Wizard
{
	partial class WizardForm
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
			this.panel1 = new System.Windows.Forms.Panel();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.buttonFinish = new System.Windows.Forms.Button();
			this.buttonPrevious = new System.Windows.Forms.Button();
			this.buttonNext = new System.Windows.Forms.Button();
			this.panelMain = new System.Windows.Forms.Panel();
			this.tabControlPages = new Tilde.CorePlugins.Wizard.WizardTabControl();
			this.panel2 = new System.Windows.Forms.Panel();
			this.labelHeading = new System.Windows.Forms.Label();
			this.panel1.SuspendLayout();
			this.panelMain.SuspendLayout();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.buttonCancel);
			this.panel1.Controls.Add(this.buttonFinish);
			this.panel1.Controls.Add(this.buttonPrevious);
			this.panel1.Controls.Add(this.buttonNext);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 225);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(504, 36);
			this.panel1.TabIndex = 1;
			// 
			// buttonCancel
			// 
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(425, 6);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.TabIndex = 3;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// buttonFinish
			// 
			this.buttonFinish.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonFinish.Location = new System.Drawing.Point(344, 6);
			this.buttonFinish.Name = "buttonFinish";
			this.buttonFinish.Size = new System.Drawing.Size(75, 23);
			this.buttonFinish.TabIndex = 2;
			this.buttonFinish.Text = "Finish";
			this.buttonFinish.UseVisualStyleBackColor = true;
			this.buttonFinish.Click += new System.EventHandler(this.buttonFinish_Click);
			// 
			// buttonPrevious
			// 
			this.buttonPrevious.Location = new System.Drawing.Point(182, 6);
			this.buttonPrevious.Name = "buttonPrevious";
			this.buttonPrevious.Size = new System.Drawing.Size(75, 23);
			this.buttonPrevious.TabIndex = 1;
			this.buttonPrevious.Text = "<< Previous";
			this.buttonPrevious.UseVisualStyleBackColor = true;
			this.buttonPrevious.Click += new System.EventHandler(this.buttonPrevious_Click);
			// 
			// buttonNext
			// 
			this.buttonNext.Location = new System.Drawing.Point(263, 6);
			this.buttonNext.Name = "buttonNext";
			this.buttonNext.Size = new System.Drawing.Size(75, 23);
			this.buttonNext.TabIndex = 0;
			this.buttonNext.Text = "Next >>";
			this.buttonNext.UseVisualStyleBackColor = true;
			this.buttonNext.Click += new System.EventHandler(this.buttonNext_Click);
			// 
			// panelMain
			// 
			this.panelMain.Controls.Add(this.tabControlPages);
			this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelMain.Location = new System.Drawing.Point(0, 38);
			this.panelMain.Name = "panelMain";
			this.panelMain.Size = new System.Drawing.Size(504, 187);
			this.panelMain.TabIndex = 4;
			// 
			// tabControlPages
			// 
			this.tabControlPages.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControlPages.Location = new System.Drawing.Point(0, 0);
			this.tabControlPages.Name = "tabControlPages";
			this.tabControlPages.SelectedIndex = 0;
			this.tabControlPages.Size = new System.Drawing.Size(504, 187);
			this.tabControlPages.TabIndex = 0;
			this.tabControlPages.SelectedIndexChanged += new System.EventHandler(this.tabControlPages_SelectedIndexChanged);
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.labelHeading);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel2.Location = new System.Drawing.Point(0, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(504, 38);
			this.panel2.TabIndex = 4;
			// 
			// labelHeading
			// 
			this.labelHeading.AutoSize = true;
			this.labelHeading.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelHeading.Location = new System.Drawing.Point(3, 9);
			this.labelHeading.Name = "labelHeading";
			this.labelHeading.Size = new System.Drawing.Size(69, 20);
			this.labelHeading.TabIndex = 0;
			this.labelHeading.Text = "Heading";
			// 
			// WizardForm
			// 
			this.AcceptButton = this.buttonFinish;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.buttonCancel;
			this.ClientSize = new System.Drawing.Size(504, 261);
			this.Controls.Add(this.panelMain);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.panel2);
			this.Name = "WizardForm";
			this.Text = "WizardForm";
			this.Load += new System.EventHandler(this.WizardForm_Load);
			this.panel1.ResumeLayout(false);
			this.panelMain.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panelMain;
		protected WizardTabControl tabControlPages;
		protected System.Windows.Forms.Button buttonCancel;
		protected System.Windows.Forms.Button buttonFinish;
		protected System.Windows.Forms.Button buttonPrevious;
		protected System.Windows.Forms.Button buttonNext;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Label labelHeading;
	}
}