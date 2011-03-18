
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
	partial class LuaScriptView
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
			this.menuStripLuaScriptView = new System.Windows.Forms.MenuStrip();
			this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.debugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toggleBreakpointToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStripLuaScriptView.SuspendLayout();
			this.SuspendLayout();
			// 
			// scintillaControl
			// 
			this.scintillaControl.Location = new System.Drawing.Point(0, 24);
			this.scintillaControl.Size = new System.Drawing.Size(292, 242);
			this.scintillaControl.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.scintillaControl_PreviewKeyDown);
			// 
			// menuStripLuaScriptView
			// 
			this.menuStripLuaScriptView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem,
            this.debugToolStripMenuItem});
			this.menuStripLuaScriptView.Location = new System.Drawing.Point(0, 24);
			this.menuStripLuaScriptView.Name = "menuStripLuaScriptView";
			this.menuStripLuaScriptView.Size = new System.Drawing.Size(292, 24);
			this.menuStripLuaScriptView.TabIndex = 1;
			this.menuStripLuaScriptView.Text = "menuStripForm";
			this.menuStripLuaScriptView.Visible = false;
			// 
			// editToolStripMenuItem
			// 
			this.editToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.MatchOnly;
			this.editToolStripMenuItem.Name = "editToolStripMenuItem";
			this.editToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.editToolStripMenuItem.Text = "&Edit";
			// 
			// debugToolStripMenuItem
			// 
			this.debugToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toggleBreakpointToolStripMenuItem});
			this.debugToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.MatchOnly;
			this.debugToolStripMenuItem.Name = "debugToolStripMenuItem";
			this.debugToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
			this.debugToolStripMenuItem.Text = "Debug";
			// 
			// toggleBreakpointToolStripMenuItem
			// 
			this.toggleBreakpointToolStripMenuItem.Name = "toggleBreakpointToolStripMenuItem";
			this.toggleBreakpointToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F9;
			this.toggleBreakpointToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
			this.toggleBreakpointToolStripMenuItem.Text = "Toggle Breakpoint";
			this.toggleBreakpointToolStripMenuItem.Click += new System.EventHandler(this.toggleBreakpointToolStripMenuItem_Click);
			// 
			// LuaScriptView
			// 
			this.ClientSize = new System.Drawing.Size(292, 266);
			this.Controls.Add(this.menuStripLuaScriptView);
			this.MainMenuStrip = this.menuStripLuaScriptView;
			this.Name = "LuaScriptView";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.LuaScriptView_FormClosed);
			this.Load += new System.EventHandler(this.LuaScriptView_Load);
			this.Controls.SetChildIndex(this.menuStripLuaScriptView, 0);
			this.Controls.SetChildIndex(this.scintillaControl, 0);
			this.menuStripLuaScriptView.ResumeLayout(false);
			this.menuStripLuaScriptView.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip menuStripLuaScriptView;
		private System.Windows.Forms.ToolStripMenuItem debugToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem toggleBreakpointToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;

	}
}
