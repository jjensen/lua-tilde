
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
	partial class TextView
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
			this.scintillaControl = new Scintilla.ScintillaControl();
			this.menuStripTextView = new System.Windows.Forms.MenuStrip();
			this.EditMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.EditUndoItem = new System.Windows.Forms.ToolStripMenuItem();
			this.EditRedoItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.EditCutItem = new System.Windows.Forms.ToolStripMenuItem();
			this.EditCopyItem = new System.Windows.Forms.ToolStripMenuItem();
			this.EditPasteItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.EditFindItem = new System.Windows.Forms.ToolStripMenuItem();
			this.EditReplaceItem = new System.Windows.Forms.ToolStripMenuItem();
			this.EditFindNextItem = new System.Windows.Forms.ToolStripMenuItem();
			this.EditFindPreviousItem = new System.Windows.Forms.ToolStripMenuItem();
			this.EditGotoLineItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.menuStripTextView.SuspendLayout();
			this.SuspendLayout();
			// 
			// scintillaControl
			// 
			this.scintillaControl.Configuration = null;
			this.scintillaControl.ConfigurationLanguage = null;
			this.scintillaControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.scintillaControl.LegacyConfiguration = null;
			this.scintillaControl.Location = new System.Drawing.Point(0, 24);
			this.scintillaControl.Name = "scintillaControl";
			this.scintillaControl.Size = new System.Drawing.Size(292, 242);
			this.scintillaControl.SmartIndentingEnabled = true;
			this.scintillaControl.TabIndex = 0;
			this.scintillaControl.KeyDown += new System.Windows.Forms.KeyEventHandler(this.scintillaControl_KeyDown);
			this.scintillaControl.Modified += new System.EventHandler<Scintilla.ModifiedEventArgs>(this.scintillaControl_Modified);
			this.scintillaControl.ModifyAttemptRO += new System.EventHandler<Scintilla.ModifyAttemptROEventArgs>(this.scintillaControl_ModifyAttemptRO);
			this.scintillaControl.SavePointLeft += new System.EventHandler<Scintilla.SavePointLeftEventArgs>(this.scintillaControl_SavePointLeft);
			this.scintillaControl.SavePointReached += new System.EventHandler<Scintilla.SavePointReachedEventArgs>(this.scintillaControl_SavePointReached);
			this.scintillaControl.UpdateUI += new System.EventHandler<Scintilla.UpdateUIEventArgs>(this.scintillaControl_UpdateUI);
			// 
			// menuStripTextView
			// 
			this.menuStripTextView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.EditMenu});
			this.menuStripTextView.Location = new System.Drawing.Point(0, 0);
			this.menuStripTextView.Name = "menuStripTextView";
			this.menuStripTextView.Size = new System.Drawing.Size(292, 24);
			this.menuStripTextView.TabIndex = 1;
			this.menuStripTextView.Text = "menuStripForm";
			this.menuStripTextView.Visible = false;
			// 
			// EditMenu
			// 
			this.EditMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.EditGotoLineItem,
            this.toolStripSeparator3,
            this.EditUndoItem,
            this.EditRedoItem,
            this.toolStripSeparator2,
            this.EditCutItem,
            this.EditCopyItem,
            this.EditPasteItem,
            this.toolStripSeparator1,
            this.EditFindItem,
            this.EditReplaceItem,
            this.EditFindNextItem,
            this.EditFindPreviousItem});
			this.EditMenu.MergeAction = System.Windows.Forms.MergeAction.MatchOnly;
			this.EditMenu.Name = "EditMenu";
			this.EditMenu.Size = new System.Drawing.Size(37, 20);
			this.EditMenu.Text = "&Edit";
			// 
			// EditUndoItem
			// 
			this.EditUndoItem.Name = "EditUndoItem";
			this.EditUndoItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
			this.EditUndoItem.Size = new System.Drawing.Size(187, 22);
			this.EditUndoItem.Text = "&Undo";
			this.EditUndoItem.Click += new System.EventHandler(this.EditUndoItem_Click);
			// 
			// EditRedoItem
			// 
			this.EditRedoItem.Name = "EditRedoItem";
			this.EditRedoItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
			this.EditRedoItem.Size = new System.Drawing.Size(187, 22);
			this.EditRedoItem.Text = "&Redo";
			this.EditRedoItem.Click += new System.EventHandler(this.EditRedoItem_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(184, 6);
			// 
			// EditCutItem
			// 
			this.EditCutItem.Name = "EditCutItem";
			this.EditCutItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
			this.EditCutItem.Size = new System.Drawing.Size(187, 22);
			this.EditCutItem.Text = "Cu&t";
			this.EditCutItem.Click += new System.EventHandler(this.EditCutItem_Click);
			// 
			// EditCopyItem
			// 
			this.EditCopyItem.Name = "EditCopyItem";
			this.EditCopyItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
			this.EditCopyItem.Size = new System.Drawing.Size(187, 22);
			this.EditCopyItem.Text = "&Copy";
			this.EditCopyItem.Click += new System.EventHandler(this.EditCopyItem_Click);
			// 
			// EditPasteItem
			// 
			this.EditPasteItem.Name = "EditPasteItem";
			this.EditPasteItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
			this.EditPasteItem.Size = new System.Drawing.Size(187, 22);
			this.EditPasteItem.Text = "&Paste";
			this.EditPasteItem.Click += new System.EventHandler(this.EditPasteItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(184, 6);
			// 
			// EditFindItem
			// 
			this.EditFindItem.Name = "EditFindItem";
			this.EditFindItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
			this.EditFindItem.Size = new System.Drawing.Size(187, 22);
			this.EditFindItem.Text = "&Find";
			this.EditFindItem.Click += new System.EventHandler(this.EditFindItem_Click);
			// 
			// EditReplaceItem
			// 
			this.EditReplaceItem.Name = "EditReplaceItem";
			this.EditReplaceItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.H)));
			this.EditReplaceItem.Size = new System.Drawing.Size(187, 22);
			this.EditReplaceItem.Text = "Replace";
			this.EditReplaceItem.Click += new System.EventHandler(this.EditReplaceItem_Click);
			// 
			// EditFindNextItem
			// 
			this.EditFindNextItem.Name = "EditFindNextItem";
			this.EditFindNextItem.ShortcutKeys = System.Windows.Forms.Keys.F3;
			this.EditFindNextItem.Size = new System.Drawing.Size(187, 22);
			this.EditFindNextItem.Text = "Find &Next";
			this.EditFindNextItem.Click += new System.EventHandler(this.EditFindNextItem_Click);
			// 
			// EditFindPreviousItem
			// 
			this.EditFindPreviousItem.Name = "EditFindPreviousItem";
			this.EditFindPreviousItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.F3)));
			this.EditFindPreviousItem.Size = new System.Drawing.Size(187, 22);
			this.EditFindPreviousItem.Text = "Find Pre&vious";
			this.EditFindPreviousItem.Click += new System.EventHandler(this.EditFindPreviousItem_Click);
			// 
			// EditGotoLineItem
			// 
			this.EditGotoLineItem.Name = "EditGotoLineItem";
			this.EditGotoLineItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G)));
			this.EditGotoLineItem.Size = new System.Drawing.Size(187, 22);
			this.EditGotoLineItem.Text = "Go to Line";
			this.EditGotoLineItem.Click += new System.EventHandler(this.EditGotoLineItem_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(184, 6);
			// 
			// TextView
			// 
			this.ClientSize = new System.Drawing.Size(292, 266);
			this.Controls.Add(this.scintillaControl);
			this.Controls.Add(this.menuStripTextView);
			this.DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.Document;
			this.MainMenuStrip = this.menuStripTextView;
			this.Name = "TextView";
			this.Activated += new System.EventHandler(this.TextView_Activated);
			this.menuStripTextView.ResumeLayout(false);
			this.menuStripTextView.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		protected Scintilla.ScintillaControl scintillaControl;
		private System.Windows.Forms.ToolStripMenuItem EditMenu;
		private System.Windows.Forms.ToolStripMenuItem EditUndoItem;
		private System.Windows.Forms.ToolStripMenuItem EditRedoItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem EditCutItem;
		private System.Windows.Forms.ToolStripMenuItem EditCopyItem;
		private System.Windows.Forms.ToolStripMenuItem EditPasteItem;
		private System.Windows.Forms.MenuStrip menuStripTextView;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem EditFindItem;
		private System.Windows.Forms.ToolStripMenuItem EditReplaceItem;
		private System.Windows.Forms.ToolStripMenuItem EditFindNextItem;
		private System.Windows.Forms.ToolStripMenuItem EditFindPreviousItem;
		private System.Windows.Forms.ToolStripMenuItem EditGotoLineItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;

	}
}
