
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

namespace Tilde.CorePlugins.TextEditor
{
	partial class FindReplaceDialog
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FindReplaceDialog));
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.buttonFindReplace = new System.Windows.Forms.ToolStripDropDownButton();
			this.findToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.replaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.findInFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.replaceInFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.groupBoxFindOptions = new System.Windows.Forms.GroupBox();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.comboBoxFileTypes = new System.Windows.Forms.ComboBox();
			this.checkBoxIncludeSubFolders = new System.Windows.Forms.CheckBox();
			this.labelFileTypes = new System.Windows.Forms.Label();
			this.checkBoxMatchCase = new System.Windows.Forms.CheckBox();
			this.checkBoxUseRegularExpressions = new System.Windows.Forms.CheckBox();
			this.checkBoxMatchWholeWord = new System.Windows.Forms.CheckBox();
			this.checkBoxSearchUp = new System.Windows.Forms.CheckBox();
			this.labelReplaceWith = new System.Windows.Forms.Label();
			this.comboBoxReplaceWith = new System.Windows.Forms.ComboBox();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.labelFindWhat = new System.Windows.Forms.Label();
			this.comboBoxFindWhat = new System.Windows.Forms.ComboBox();
			this.labelSearchIn = new System.Windows.Forms.Label();
			this.panelSearchIn = new System.Windows.Forms.Panel();
			this.buttonBrowseSearchIn = new System.Windows.Forms.Button();
			this.comboBoxSearchIn = new System.Windows.Forms.ComboBox();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.buttonFindNext = new System.Windows.Forms.Button();
			this.buttonFindAll = new System.Windows.Forms.Button();
			this.buttonSkipFile = new System.Windows.Forms.Button();
			this.buttonReplace = new System.Windows.Forms.Button();
			this.buttonReplaceAll = new System.Windows.Forms.Button();
			this.buttonStopFind = new System.Windows.Forms.Button();
			this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
			this.toolStrip1.SuspendLayout();
			this.groupBoxFindOptions.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.panelSearchIn.SuspendLayout();
			this.flowLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonFindReplace});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(370, 25);
			this.toolStrip1.TabIndex = 1;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// buttonFindReplace
			// 
			this.buttonFindReplace.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.buttonFindReplace.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.findToolStripMenuItem,
            this.replaceToolStripMenuItem,
            this.findInFilesToolStripMenuItem,
            this.replaceInFilesToolStripMenuItem});
			this.buttonFindReplace.Image = ((System.Drawing.Image)(resources.GetObject("buttonFindReplace.Image")));
			this.buttonFindReplace.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.buttonFindReplace.Name = "buttonFindReplace";
			this.buttonFindReplace.Size = new System.Drawing.Size(40, 22);
			this.buttonFindReplace.Text = "Find";
			// 
			// findToolStripMenuItem
			// 
			this.findToolStripMenuItem.Name = "findToolStripMenuItem";
			this.findToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
			this.findToolStripMenuItem.Text = "Find";
			this.findToolStripMenuItem.Click += new System.EventHandler(this.findToolStripMenuItem_Click);
			// 
			// replaceToolStripMenuItem
			// 
			this.replaceToolStripMenuItem.Name = "replaceToolStripMenuItem";
			this.replaceToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
			this.replaceToolStripMenuItem.Text = "Replace";
			this.replaceToolStripMenuItem.Click += new System.EventHandler(this.replaceToolStripMenuItem_Click);
			// 
			// findInFilesToolStripMenuItem
			// 
			this.findInFilesToolStripMenuItem.Name = "findInFilesToolStripMenuItem";
			this.findInFilesToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
			this.findInFilesToolStripMenuItem.Text = "Find in Files";
			this.findInFilesToolStripMenuItem.Click += new System.EventHandler(this.findInFilesToolStripMenuItem_Click);
			// 
			// replaceInFilesToolStripMenuItem
			// 
			this.replaceInFilesToolStripMenuItem.Name = "replaceInFilesToolStripMenuItem";
			this.replaceInFilesToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
			this.replaceInFilesToolStripMenuItem.Text = "Replace in Files";
			this.replaceInFilesToolStripMenuItem.Click += new System.EventHandler(this.replaceInFilesToolStripMenuItem_Click);
			// 
			// groupBoxFindOptions
			// 
			this.groupBoxFindOptions.AutoSize = true;
			this.groupBoxFindOptions.Controls.Add(this.tableLayoutPanel2);
			this.groupBoxFindOptions.Dock = System.Windows.Forms.DockStyle.Top;
			this.groupBoxFindOptions.Location = new System.Drawing.Point(3, 126);
			this.groupBoxFindOptions.Name = "groupBoxFindOptions";
			this.groupBoxFindOptions.Size = new System.Drawing.Size(364, 174);
			this.groupBoxFindOptions.TabIndex = 3;
			this.groupBoxFindOptions.TabStop = false;
			this.groupBoxFindOptions.Text = "Options";
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.AutoSize = true;
			this.tableLayoutPanel2.ColumnCount = 1;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.Controls.Add(this.comboBoxFileTypes, 0, 6);
			this.tableLayoutPanel2.Controls.Add(this.checkBoxIncludeSubFolders, 0, 4);
			this.tableLayoutPanel2.Controls.Add(this.labelFileTypes, 0, 5);
			this.tableLayoutPanel2.Controls.Add(this.checkBoxMatchCase, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.checkBoxUseRegularExpressions, 0, 3);
			this.tableLayoutPanel2.Controls.Add(this.checkBoxMatchWholeWord, 0, 1);
			this.tableLayoutPanel2.Controls.Add(this.checkBoxSearchUp, 0, 2);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 16);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 7;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.Size = new System.Drawing.Size(358, 155);
			this.tableLayoutPanel2.TabIndex = 4;
			// 
			// comboBoxFileTypes
			// 
			this.comboBoxFileTypes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxFileTypes.Location = new System.Drawing.Point(3, 131);
			this.comboBoxFileTypes.Name = "comboBoxFileTypes";
			this.comboBoxFileTypes.Size = new System.Drawing.Size(352, 21);
			this.comboBoxFileTypes.TabIndex = 6;
			this.comboBoxFileTypes.Tag = "FileTypes";
			// 
			// checkBoxIncludeSubFolders
			// 
			this.checkBoxIncludeSubFolders.AutoSize = true;
			this.checkBoxIncludeSubFolders.Location = new System.Drawing.Point(3, 95);
			this.checkBoxIncludeSubFolders.Name = "checkBoxIncludeSubFolders";
			this.checkBoxIncludeSubFolders.Size = new System.Drawing.Size(115, 17);
			this.checkBoxIncludeSubFolders.TabIndex = 7;
			this.checkBoxIncludeSubFolders.Text = "Include sub-folders";
			this.checkBoxIncludeSubFolders.UseVisualStyleBackColor = true;
			// 
			// labelFileTypes
			// 
			this.labelFileTypes.AutoSize = true;
			this.labelFileTypes.Location = new System.Drawing.Point(3, 115);
			this.labelFileTypes.Name = "labelFileTypes";
			this.labelFileTypes.Size = new System.Drawing.Size(128, 13);
			this.labelFileTypes.TabIndex = 4;
			this.labelFileTypes.Text = "Search in these file types:";
			// 
			// checkBoxMatchCase
			// 
			this.checkBoxMatchCase.AutoSize = true;
			this.checkBoxMatchCase.Location = new System.Drawing.Point(3, 3);
			this.checkBoxMatchCase.Name = "checkBoxMatchCase";
			this.checkBoxMatchCase.Size = new System.Drawing.Size(82, 17);
			this.checkBoxMatchCase.TabIndex = 0;
			this.checkBoxMatchCase.Text = "Match case";
			this.checkBoxMatchCase.UseVisualStyleBackColor = true;
			// 
			// checkBoxUseRegularExpressions
			// 
			this.checkBoxUseRegularExpressions.AutoSize = true;
			this.checkBoxUseRegularExpressions.Location = new System.Drawing.Point(3, 72);
			this.checkBoxUseRegularExpressions.Name = "checkBoxUseRegularExpressions";
			this.checkBoxUseRegularExpressions.Size = new System.Drawing.Size(138, 17);
			this.checkBoxUseRegularExpressions.TabIndex = 3;
			this.checkBoxUseRegularExpressions.Text = "Use regular expressions";
			this.checkBoxUseRegularExpressions.UseVisualStyleBackColor = true;
			// 
			// checkBoxMatchWholeWord
			// 
			this.checkBoxMatchWholeWord.AutoSize = true;
			this.checkBoxMatchWholeWord.Location = new System.Drawing.Point(3, 26);
			this.checkBoxMatchWholeWord.Name = "checkBoxMatchWholeWord";
			this.checkBoxMatchWholeWord.Size = new System.Drawing.Size(113, 17);
			this.checkBoxMatchWholeWord.TabIndex = 1;
			this.checkBoxMatchWholeWord.Text = "Match whole word";
			this.checkBoxMatchWholeWord.UseVisualStyleBackColor = true;
			// 
			// checkBoxSearchUp
			// 
			this.checkBoxSearchUp.AutoSize = true;
			this.checkBoxSearchUp.Location = new System.Drawing.Point(3, 49);
			this.checkBoxSearchUp.Name = "checkBoxSearchUp";
			this.checkBoxSearchUp.Size = new System.Drawing.Size(75, 17);
			this.checkBoxSearchUp.TabIndex = 2;
			this.checkBoxSearchUp.Text = "Search up";
			this.checkBoxSearchUp.UseVisualStyleBackColor = true;
			// 
			// labelReplaceWith
			// 
			this.labelReplaceWith.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelReplaceWith.Location = new System.Drawing.Point(3, 40);
			this.labelReplaceWith.Name = "labelReplaceWith";
			this.labelReplaceWith.Size = new System.Drawing.Size(364, 13);
			this.labelReplaceWith.TabIndex = 3;
			this.labelReplaceWith.Text = "Replace with:";
			// 
			// comboBoxReplaceWith
			// 
			this.comboBoxReplaceWith.Dock = System.Windows.Forms.DockStyle.Fill;
			this.comboBoxReplaceWith.Location = new System.Drawing.Point(3, 56);
			this.comboBoxReplaceWith.Name = "comboBoxReplaceWith";
			this.comboBoxReplaceWith.Size = new System.Drawing.Size(364, 21);
			this.comboBoxReplaceWith.TabIndex = 2;
			this.comboBoxReplaceWith.Tag = "Replace";
			this.comboBoxReplaceWith.KeyDown += new System.Windows.Forms.KeyEventHandler(this.comboBoxReplaceWith_KeyDown);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.labelFindWhat, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.comboBoxFindWhat, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.labelReplaceWith, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.comboBoxReplaceWith, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.labelSearchIn, 0, 4);
			this.tableLayoutPanel1.Controls.Add(this.panelSearchIn, 0, 5);
			this.tableLayoutPanel1.Controls.Add(this.groupBoxFindOptions, 0, 6);
			this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 8);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 25);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 9;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(370, 494);
			this.tableLayoutPanel1.TabIndex = 5;
			// 
			// labelFindWhat
			// 
			this.labelFindWhat.Location = new System.Drawing.Point(3, 0);
			this.labelFindWhat.Name = "labelFindWhat";
			this.labelFindWhat.Size = new System.Drawing.Size(223, 13);
			this.labelFindWhat.TabIndex = 2;
			this.labelFindWhat.Text = "Find what:";
			// 
			// comboBoxFindWhat
			// 
			this.comboBoxFindWhat.Dock = System.Windows.Forms.DockStyle.Fill;
			this.comboBoxFindWhat.Location = new System.Drawing.Point(3, 16);
			this.comboBoxFindWhat.Name = "comboBoxFindWhat";
			this.comboBoxFindWhat.Size = new System.Drawing.Size(364, 21);
			this.comboBoxFindWhat.TabIndex = 1;
			this.comboBoxFindWhat.Tag = "Find";
			this.comboBoxFindWhat.KeyDown += new System.Windows.Forms.KeyEventHandler(this.comboBoxFindWhat_KeyDown);
			this.comboBoxFindWhat.TextChanged += new System.EventHandler(this.comboBoxFindWhat_TextChanged);
			// 
			// labelSearchIn
			// 
			this.labelSearchIn.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelSearchIn.Location = new System.Drawing.Point(3, 80);
			this.labelSearchIn.Name = "labelSearchIn";
			this.labelSearchIn.Size = new System.Drawing.Size(364, 13);
			this.labelSearchIn.TabIndex = 6;
			this.labelSearchIn.Text = "Search in:";
			// 
			// panelSearchIn
			// 
			this.panelSearchIn.AutoSize = true;
			this.panelSearchIn.Controls.Add(this.buttonBrowseSearchIn);
			this.panelSearchIn.Controls.Add(this.comboBoxSearchIn);
			this.panelSearchIn.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelSearchIn.Location = new System.Drawing.Point(3, 96);
			this.panelSearchIn.Name = "panelSearchIn";
			this.panelSearchIn.Size = new System.Drawing.Size(364, 24);
			this.panelSearchIn.TabIndex = 4;
			// 
			// buttonBrowseSearchIn
			// 
			this.buttonBrowseSearchIn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonBrowseSearchIn.Location = new System.Drawing.Point(338, 0);
			this.buttonBrowseSearchIn.Name = "buttonBrowseSearchIn";
			this.buttonBrowseSearchIn.Size = new System.Drawing.Size(26, 21);
			this.buttonBrowseSearchIn.TabIndex = 8;
			this.buttonBrowseSearchIn.Text = "...";
			this.buttonBrowseSearchIn.UseVisualStyleBackColor = true;
			this.buttonBrowseSearchIn.Click += new System.EventHandler(this.buttonBrowseSearchIn_Click);
			// 
			// comboBoxSearchIn
			// 
			this.comboBoxSearchIn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxSearchIn.Location = new System.Drawing.Point(0, 0);
			this.comboBoxSearchIn.Name = "comboBoxSearchIn";
			this.comboBoxSearchIn.Size = new System.Drawing.Size(332, 21);
			this.comboBoxSearchIn.TabIndex = 7;
			this.comboBoxSearchIn.Tag = "SearchIn";
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.flowLayoutPanel1.AutoSize = true;
			this.flowLayoutPanel1.Controls.Add(this.buttonFindNext);
			this.flowLayoutPanel1.Controls.Add(this.buttonFindAll);
			this.flowLayoutPanel1.Controls.Add(this.buttonSkipFile);
			this.flowLayoutPanel1.Controls.Add(this.buttonReplace);
			this.flowLayoutPanel1.Controls.Add(this.buttonReplaceAll);
			this.flowLayoutPanel1.Controls.Add(this.buttonStopFind);
			this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 306);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(364, 58);
			this.flowLayoutPanel1.TabIndex = 4;
			// 
			// buttonFindNext
			// 
			this.buttonFindNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonFindNext.Location = new System.Drawing.Point(3, 3);
			this.buttonFindNext.Name = "buttonFindNext";
			this.buttonFindNext.Size = new System.Drawing.Size(75, 23);
			this.buttonFindNext.TabIndex = 4;
			this.buttonFindNext.Text = "Find Next";
			this.buttonFindNext.UseVisualStyleBackColor = true;
			this.buttonFindNext.Click += new System.EventHandler(this.buttonFindNext_Click);
			// 
			// buttonFindAll
			// 
			this.buttonFindAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonFindAll.Location = new System.Drawing.Point(84, 3);
			this.buttonFindAll.Name = "buttonFindAll";
			this.buttonFindAll.Size = new System.Drawing.Size(75, 23);
			this.buttonFindAll.TabIndex = 7;
			this.buttonFindAll.Text = "Find All";
			this.buttonFindAll.UseVisualStyleBackColor = true;
			this.buttonFindAll.Click += new System.EventHandler(this.buttonFindAll_Click);
			// 
			// buttonSkipFile
			// 
			this.buttonSkipFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonSkipFile.Location = new System.Drawing.Point(165, 3);
			this.buttonSkipFile.Name = "buttonSkipFile";
			this.buttonSkipFile.Size = new System.Drawing.Size(75, 23);
			this.buttonSkipFile.TabIndex = 8;
			this.buttonSkipFile.Text = "Skip File";
			this.buttonSkipFile.UseVisualStyleBackColor = true;
			// 
			// buttonReplace
			// 
			this.buttonReplace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonReplace.Location = new System.Drawing.Point(246, 3);
			this.buttonReplace.Name = "buttonReplace";
			this.buttonReplace.Size = new System.Drawing.Size(75, 23);
			this.buttonReplace.TabIndex = 5;
			this.buttonReplace.Text = "Replace";
			this.buttonReplace.UseVisualStyleBackColor = true;
			this.buttonReplace.Click += new System.EventHandler(this.buttonReplace_Click);
			// 
			// buttonReplaceAll
			// 
			this.buttonReplaceAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonReplaceAll.Location = new System.Drawing.Point(3, 32);
			this.buttonReplaceAll.Name = "buttonReplaceAll";
			this.buttonReplaceAll.Size = new System.Drawing.Size(75, 23);
			this.buttonReplaceAll.TabIndex = 6;
			this.buttonReplaceAll.Text = "Replace All";
			this.buttonReplaceAll.UseVisualStyleBackColor = true;
			this.buttonReplaceAll.Click += new System.EventHandler(this.buttonReplaceAll_Click);
			// 
			// buttonStopFind
			// 
			this.buttonStopFind.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonStopFind.Location = new System.Drawing.Point(84, 32);
			this.buttonStopFind.Name = "buttonStopFind";
			this.buttonStopFind.Size = new System.Drawing.Size(75, 23);
			this.buttonStopFind.TabIndex = 9;
			this.buttonStopFind.Text = "Stop Find";
			this.buttonStopFind.UseVisualStyleBackColor = true;
			this.buttonStopFind.Click += new System.EventHandler(this.buttonStopFind_Click);
			// 
			// FindReplaceDialog
			// 
			this.ClientSize = new System.Drawing.Size(370, 519);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Controls.Add(this.toolStrip1);
			this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)(((((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft)
						| WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight)
						| WeifenLuo.WinFormsUI.Docking.DockAreas.DockTop)
						| WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom)));
			this.KeyPreview = true;
			this.Name = "FindReplaceDialog";
			this.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.Float;
			this.TabText = "Find/Replace";
			this.Text = "Find/Replace";
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FindReplaceDialog_KeyUp);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.groupBoxFindOptions.ResumeLayout(false);
			this.groupBoxFindOptions.PerformLayout();
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel2.PerformLayout();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.panelSearchIn.ResumeLayout(false);
			this.flowLayoutPanel1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.GroupBox groupBoxFindOptions;
		private System.Windows.Forms.CheckBox checkBoxUseRegularExpressions;
		private System.Windows.Forms.CheckBox checkBoxSearchUp;
		private System.Windows.Forms.CheckBox checkBoxMatchWholeWord;
		private System.Windows.Forms.CheckBox checkBoxMatchCase;
		private System.Windows.Forms.Label labelReplaceWith;
		private System.Windows.Forms.ComboBox comboBoxReplaceWith;
		private System.Windows.Forms.ToolStripDropDownButton buttonFindReplace;
		private System.Windows.Forms.ToolStripMenuItem findToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem replaceToolStripMenuItem;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Label labelFindWhat;
		private System.Windows.Forms.ComboBox comboBoxFindWhat;
		private System.Windows.Forms.Button buttonFindNext;
		private System.Windows.Forms.Button buttonReplace;
		private System.Windows.Forms.Button buttonReplaceAll;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.Label labelSearchIn;
		private System.Windows.Forms.ToolStripMenuItem findInFilesToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem replaceInFilesToolStripMenuItem;
		private System.Windows.Forms.ComboBox comboBoxFileTypes;
		private System.Windows.Forms.Label labelFileTypes;
		private System.Windows.Forms.Button buttonFindAll;
		private System.Windows.Forms.Button buttonSkipFile;
		private System.Windows.Forms.Panel panelSearchIn;
		private System.Windows.Forms.Button buttonBrowseSearchIn;
		private System.Windows.Forms.ComboBox comboBoxSearchIn;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
		private System.Windows.Forms.Button buttonStopFind;
		private System.Windows.Forms.CheckBox checkBoxIncludeSubFolders;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;

	}
}
