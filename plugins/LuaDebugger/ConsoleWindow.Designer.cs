
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
	partial class ConsoleWindow
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConsoleWindow));
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.panel1 = new System.Windows.Forms.Panel();
			this.outputBrowser = new System.Windows.Forms.WebBrowser();
			this.browserContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.clearBrowserToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.panel2 = new System.Windows.Forms.Panel();
			this.inputBox = new Scintilla.ScintillaControl();
			this.inputContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.saveSnippetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.clearInputToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.snippetsMenuButton = new System.Windows.Forms.ToolStripDropDownButton();
			this.saveSnippetButton = new System.Windows.Forms.ToolStripButton();
			this.progressBarWaiting = new System.Windows.Forms.ToolStripProgressBar();
			this.saveSnippetDialog = new System.Windows.Forms.SaveFileDialog();
			this.autocompleteMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.autoCompleteExpressionItem = new System.Windows.Forms.ToolStripMenuItem();
			this.autocompleteSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.autocompleteProgressBar = new System.Windows.Forms.ToolStripProgressBar();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.panel1.SuspendLayout();
			this.browserContextMenuStrip.SuspendLayout();
			this.panel2.SuspendLayout();
			this.inputContextMenuStrip.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.autocompleteMenuStrip.SuspendLayout();
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
			this.splitContainer1.Panel1.Controls.Add(this.panel1);
			this.splitContainer1.Panel1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 0);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.panel2);
			this.splitContainer1.Panel2.Padding = new System.Windows.Forms.Padding(4, 0, 4, 4);
			this.splitContainer1.Size = new System.Drawing.Size(618, 403);
			this.splitContainer1.SplitterDistance = 263;
			this.splitContainer1.TabIndex = 0;
			// 
			// panel1
			// 
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel1.Controls.Add(this.outputBrowser);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(4, 4);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(610, 259);
			this.panel1.TabIndex = 1;
			// 
			// outputBrowser
			// 
			this.outputBrowser.ContextMenuStrip = this.browserContextMenuStrip;
			this.outputBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
			this.outputBrowser.IsWebBrowserContextMenuEnabled = false;
			this.outputBrowser.Location = new System.Drawing.Point(0, 0);
			this.outputBrowser.MinimumSize = new System.Drawing.Size(20, 20);
			this.outputBrowser.Name = "outputBrowser";
			this.outputBrowser.Size = new System.Drawing.Size(606, 255);
			this.outputBrowser.TabIndex = 1;
			this.outputBrowser.Url = new System.Uri("", System.UriKind.Relative);
			// 
			// browserContextMenuStrip
			// 
			this.browserContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearBrowserToolStripMenuItem1});
			this.browserContextMenuStrip.Name = "browserContextMenuStrip";
			this.browserContextMenuStrip.Size = new System.Drawing.Size(100, 26);
			// 
			// clearBrowserToolStripMenuItem1
			// 
			this.clearBrowserToolStripMenuItem1.Name = "clearBrowserToolStripMenuItem1";
			this.clearBrowserToolStripMenuItem1.Size = new System.Drawing.Size(99, 22);
			this.clearBrowserToolStripMenuItem1.Text = "Clear";
			this.clearBrowserToolStripMenuItem1.Click += new System.EventHandler(this.clearBrowserToolStripMenuItem1_Click);
			// 
			// panel2
			// 
			this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel2.Controls.Add(this.inputBox);
			this.panel2.Controls.Add(this.toolStrip1);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point(4, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(610, 132);
			this.panel2.TabIndex = 2;
			// 
			// inputBox
			// 
			this.inputBox.Configuration = null;
			this.inputBox.ConfigurationLanguage = null;
			this.inputBox.ContextMenuStrip = this.inputContextMenuStrip;
			this.inputBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.inputBox.LegacyConfiguration = null;
			this.inputBox.Location = new System.Drawing.Point(0, 25);
			this.inputBox.Name = "inputBox";
			this.inputBox.Size = new System.Drawing.Size(606, 103);
			this.inputBox.SmartIndentingEnabled = false;
			this.inputBox.TabIndex = 1;
			this.inputBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.inputBox_KeyDown);
			this.inputBox.Modified += new System.EventHandler<Scintilla.ModifiedEventArgs>(this.inputBox_Modified);
			this.inputBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.inputBox_KeyPress);
			// 
			// inputContextMenuStrip
			// 
			this.inputContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveSnippetToolStripMenuItem,
            this.clearInputToolStripMenuItem});
			this.inputContextMenuStrip.Name = "inputContextMenuStrip";
			this.inputContextMenuStrip.Size = new System.Drawing.Size(137, 48);
			// 
			// saveSnippetToolStripMenuItem
			// 
			this.saveSnippetToolStripMenuItem.Name = "saveSnippetToolStripMenuItem";
			this.saveSnippetToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.saveSnippetToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
			this.saveSnippetToolStripMenuItem.Text = "Save";
			this.saveSnippetToolStripMenuItem.Click += new System.EventHandler(this.saveSnippetToolStripMenuItem_Click);
			// 
			// clearInputToolStripMenuItem
			// 
			this.clearInputToolStripMenuItem.Name = "clearInputToolStripMenuItem";
			this.clearInputToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
			this.clearInputToolStripMenuItem.Text = "Clear";
			this.clearInputToolStripMenuItem.Click += new System.EventHandler(this.clearInputToolStripMenuItem_Click);
			// 
			// toolStrip1
			// 
			this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.snippetsMenuButton,
            this.saveSnippetButton,
            this.progressBarWaiting});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(606, 25);
			this.toolStrip1.TabIndex = 0;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// snippetsMenuButton
			// 
			this.snippetsMenuButton.Image = ((System.Drawing.Image)(resources.GetObject("snippetsMenuButton.Image")));
			this.snippetsMenuButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.snippetsMenuButton.Name = "snippetsMenuButton";
			this.snippetsMenuButton.Size = new System.Drawing.Size(77, 22);
			this.snippetsMenuButton.Text = "Snippets";
			// 
			// saveSnippetButton
			// 
			this.saveSnippetButton.Image = ((System.Drawing.Image)(resources.GetObject("saveSnippetButton.Image")));
			this.saveSnippetButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.saveSnippetButton.Name = "saveSnippetButton";
			this.saveSnippetButton.Size = new System.Drawing.Size(90, 22);
			this.saveSnippetButton.Text = "Save Snippet";
			this.saveSnippetButton.Click += new System.EventHandler(this.saveSnippetButton_Click);
			// 
			// progressBarWaiting
			// 
			this.progressBarWaiting.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.progressBarWaiting.Name = "progressBarWaiting";
			this.progressBarWaiting.Size = new System.Drawing.Size(100, 22);
			this.progressBarWaiting.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
			// 
			// saveSnippetDialog
			// 
			this.saveSnippetDialog.DefaultExt = "lua";
			this.saveSnippetDialog.Filter = "Lua files|*.lua|All files|*.*";
			this.saveSnippetDialog.RestoreDirectory = true;
			this.saveSnippetDialog.SupportMultiDottedExtensions = true;
			this.saveSnippetDialog.Title = "Save Lua Snippet";
			// 
			// autocompleteMenuStrip
			// 
			this.autocompleteMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.autoCompleteExpressionItem,
            this.autocompleteSeparator,
            this.autocompleteProgressBar});
			this.autocompleteMenuStrip.MaximumSize = new System.Drawing.Size(192, 224);
			this.autocompleteMenuStrip.Name = "autocompleteMenuStrip";
			this.autocompleteMenuStrip.Size = new System.Drawing.Size(161, 45);
			// 
			// autoCompleteExpressionItem
			// 
			this.autoCompleteExpressionItem.Enabled = false;
			this.autoCompleteExpressionItem.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.autoCompleteExpressionItem.Name = "autoCompleteExpressionItem";
			this.autoCompleteExpressionItem.Size = new System.Drawing.Size(160, 22);
			this.autoCompleteExpressionItem.Text = "Expression";
			// 
			// autocompleteSeparator
			// 
			this.autocompleteSeparator.Name = "autocompleteSeparator";
			this.autocompleteSeparator.Size = new System.Drawing.Size(157, 6);
			// 
			// autocompleteProgressBar
			// 
			this.autocompleteProgressBar.AutoSize = false;
			this.autocompleteProgressBar.BackColor = System.Drawing.SystemColors.Window;
			this.autocompleteProgressBar.Name = "autocompleteProgressBar";
			this.autocompleteProgressBar.Size = new System.Drawing.Size(100, 10);
			this.autocompleteProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
			// 
			// ConsoleWindow
			// 
			this.ClientSize = new System.Drawing.Size(618, 403);
			this.Controls.Add(this.splitContainer1);
			this.Name = "ConsoleWindow";
			this.TabText = "Lua Console";
			this.Text = "Lua Console";
			this.Load += new System.EventHandler(this.ConsoleWindow_Load);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.browserContextMenuStrip.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.inputContextMenuStrip.ResumeLayout(false);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.autocompleteMenuStrip.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.Panel panel1;
		private Scintilla.ScintillaControl inputBox;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.ToolStripDropDownButton snippetsMenuButton;
		private System.Windows.Forms.WebBrowser outputBrowser;
		private System.Windows.Forms.SaveFileDialog saveSnippetDialog;
		private System.Windows.Forms.ToolStripButton saveSnippetButton;
		private System.Windows.Forms.ContextMenuStrip inputContextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem saveSnippetToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem clearInputToolStripMenuItem;
		private System.Windows.Forms.ToolStripProgressBar progressBarWaiting;
		private System.Windows.Forms.ContextMenuStrip browserContextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem clearBrowserToolStripMenuItem1;
		private System.Windows.Forms.ContextMenuStrip autocompleteMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem autoCompleteExpressionItem;
		private System.Windows.Forms.ToolStripSeparator autocompleteSeparator;
		private System.Windows.Forms.ToolStripProgressBar autocompleteProgressBar;
	}
}
