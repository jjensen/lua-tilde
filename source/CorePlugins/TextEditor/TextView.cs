
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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Tilde.Framework.Controller;
using Tilde.Framework.View;
using Tilde.Framework.Model;
using System.Text.RegularExpressions;

namespace Tilde.CorePlugins.TextEditor
{
	public partial class TextView : Tilde.Framework.View.DocumentView
	{
		protected TextPlugin mTextPlugin;
		protected bool mSaving;

		[Flags]
		enum CaretPolicy
		{
			// Caret policy, used by SetXCaretPolicy and SetYCaretPolicy.
			// If CARET_SLOP is set, we can define a slop value: caretSlop.
			// This value defines an unwanted zone (UZ) where the caret is... unwanted.
			// This zone is defined as a number of pixels near the vertical margins,
			// and as a number of lines near the horizontal margins.
			// By keeping the caret away from the edges, it is seen within its context,
			// so it is likely that the identifier that the caret is on can be completely seen,
			// and that the current line is seen with some of the lines following it which are
			// often dependent on that line.
			CARET_SLOP=0x01,
			// If CARET_STRICT is set, the policy is enforced... strictly.
			// The caret is centred on the display if slop is not set,
			// and cannot go in the UZ if slop is set.
			CARET_STRICT=0x04,
			// If CARET_JUMPS is set, the display is moved more energetically
			// so the caret can move in the same direction longer before the policy is applied again.
			CARET_JUMPS=0x10,
			// If CARET_EVEN is not set, instead of having symmetrical UZs,
			// the left and bottom UZs are extended up to right and top UZs respectively.
			// This way, we favour the displaying of useful information: the begining of lines,
			// where most code reside, and the lines after the caret, eg. the body of a function.
			CARET_EVEN=0x08
		}

		static string [] s_propertiesToForward = 
		{
			"tab.timmy.whinge.level"
		};

		public TextView(IManager manager, Document doc)
			: base(manager, doc)
		{
			InitializeComponent();

			mTextPlugin = (TextPlugin)Manager.GetPlugin(typeof(TextPlugin));

			string ext = System.IO.Path.GetExtension(doc.FileName);
			if (ext == "")
				ext = System.IO.Path.GetFileName(doc.FileName);
			else
				ext = ext.Substring(1);
			string lang = TextPlugin.ScintillaProperties.GetLanguageFromExtension(ext);
			if (lang == null)
				lang = "txt";
			string filepattern = TextPlugin.ScintillaProperties.GetExtensionListFromLanguage(lang);
			if (filepattern != null && filepattern.Contains(";"))
				filepattern = filepattern.Split(new char[] { ';' }, 2)[0];

			scintillaControl.Configuration = new Scintilla.Configuration.ScintillaConfig(TextPlugin.ScintillaProperties, filepattern);
			scintillaControl.ConfigurationLanguage = lang;

			scintillaControl.UseMonospaceFont(TextPlugin.ScintillaProperties.GetByKey("font.monospace"));
			scintillaControl.TabWidth = 4;

            scintillaControl.EndOfLineMode = Scintilla.Enums.EndOfLine.LF;

			foreach (string param in s_propertiesToForward)
			{
				if(TextPlugin.ScintillaProperties.ContainsKey(param))
				{
					scintillaControl.Property(param, TextPlugin.ScintillaProperties[param]);
				}
			}

			mSaving = false;
			TextDocument textDoc = Document as TextDocument;

			doc.PropertyChange += new PropertyChangeEventHandler(doc_PropertyChange);
			doc.Saving += new DocumentSavingEventHandler(doc_Saving);
			doc.Saved += new DocumentSavedEventHandler(doc_Saved);

			scintillaControl.SetYCaretPolicy((int) (CaretPolicy.CARET_SLOP | CaretPolicy.CARET_STRICT | CaretPolicy.CARET_EVEN), 6);

			mTextPlugin.Options.OptionsChanged += new OptionsChangedDelegate(Options_OptionsChanged);
			UpdateOptions();

			scintillaControl.Text = textDoc.Text;
			scintillaControl.EmptyUndoBuffer();
			scintillaControl.SetSavePoint();
			scintillaControl.ModEventMask = (int) (Scintilla.Enums.ModificationFlags.User | Scintilla.Enums.ModificationFlags.InsertText | Scintilla.Enums.ModificationFlags.DeleteText);
			if (textDoc.ReadOnly)
				scintillaControl.IsReadOnly = true;
		}

		void Options_OptionsChanged(IOptions sender, string option)
		{
			UpdateOptions();
		}

		void UpdateOptions()
		{
			scintillaControl.ViewWhitespace = mTextPlugin.Options.Whitespace;
			scintillaControl.SetWhiteSpaceForeground(mTextPlugin.Options.WhitespaceForeground.A > 0, mTextPlugin.Options.WhitespaceForeground.R | (mTextPlugin.Options.WhitespaceForeground.G << 8) | (mTextPlugin.Options.WhitespaceForeground.B << 16));
			scintillaControl.SetWhiteSpaceBackground(mTextPlugin.Options.WhitespaceBackground.A > 0, mTextPlugin.Options.WhitespaceBackground.R | (mTextPlugin.Options.WhitespaceBackground.G << 8) | (mTextPlugin.Options.WhitespaceBackground.B << 16));
			scintillaControl.IsIndentationGuides = mTextPlugin.Options.IndentationGuides;
			scintillaControl.HighlightGuide = mTextPlugin.Options.IndentationGuideHighlight ? 1 : 0;
			scintillaControl.EdgeMode = (int)mTextPlugin.Options.LineEdgeMode;
			scintillaControl.EdgeColumn = mTextPlugin.Options.LineEdgeColumn;
			scintillaControl.EdgeColor = mTextPlugin.Options.LineEdgeColor.R | (mTextPlugin.Options.LineEdgeColor.G << 8) | (mTextPlugin.Options.LineEdgeColor.B << 16);
		}

		public TextView()
		{
			InitializeComponent();
		}

		public Scintilla.ScintillaControl ScintillaControl
		{
			get { return scintillaControl; }
		}

		public void ShowLine(int line)
		{
			scintillaControl.EnsureVisible(line - 1);
			scintillaControl.GotoLine(line - 1);
		}

		public void SelectText(int line, int start, int end)
		{
			int linePos = scintillaControl.PositionFromLine(line - 1);
			scintillaControl.SetSelection(linePos + start, linePos + end);
			scintillaControl.ScrollCaret();
		}

		void doc_PropertyChange(object sender, PropertyChangeEventArgs args)
		{
			if (!mSaving && args.Property == "Text")
			{
				scintillaControl.IsReadOnly = false;
				scintillaControl.Text = (Document as TextDocument).Text;
				scintillaControl.EmptyUndoBuffer();
				scintillaControl.IsReadOnly = Document.ReadOnly;
			}
			else if(args.Property == "ReadOnly")
			{
				scintillaControl.IsReadOnly = Document.ReadOnly;
			}
		}

		void doc_Saving(Document sender)
		{
			mSaving = true;
			try
			{
				(sender as TextDocument).Text = scintillaControl.Text;
			}
			finally
			{
				mSaving = false;
			}
		}

		void doc_Saved(Document sender, bool success)
		{
			if(success)
				scintillaControl.SetSavePoint();
		}

		protected void MergeMenu(MenuStrip target)
		{
			foreach(ToolStripMenuItem item in menuStripTextView.Items)
			{
				foreach(ToolStripMenuItem dest in target.Items)
				{
					if (item.Text == dest.Text)
					{
						MergeMenu(item, dest);
					}
				}
			}
		}

		private void MergeMenu(ToolStripMenuItem source, ToolStripMenuItem dest)
		{
			while(source.DropDownItems.Count > 0)
			{
				ToolStripItem item = source.DropDownItems[0];
				source.DropDownItems.Remove(item);
				dest.DropDownItems.Add(item);
			}
		}

		protected bool ToggleMarker(int marker, int line)
		{
			int markers = scintillaControl.MarkerGet(line);
			if ((markers & (1 << marker)) != 0)
			{
				scintillaControl.MarkerDelete(line, marker);
				return false;
			}
			else
			{
				scintillaControl.MarkerAdd(line, marker);
				return true;
			}
		}

		private void EditUndoItem_Click(object sender, EventArgs e)
		{
			scintillaControl.Undo();
		}

		private void EditRedoItem_Click(object sender, EventArgs e)
		{
			scintillaControl.Redo();
		}

		private void EditCutItem_Click(object sender, EventArgs e)
		{
			scintillaControl.Cut();
		}

		private void EditCopyItem_Click(object sender, EventArgs e)
		{
			scintillaControl.Copy();
		}

		private void EditPasteItem_Click(object sender, EventArgs e)
		{
			scintillaControl.Paste();
		}

		private void UpdateMenu()
		{
			bool isSelection = scintillaControl.SelectionEnd > scintillaControl.SelectionStart;

			EditUndoItem.Enabled = scintillaControl.CanUndo;
			EditRedoItem.Enabled = scintillaControl.CanRedo;
			EditCutItem.Enabled = !scintillaControl.IsReadOnly && isSelection;
			EditCopyItem.Enabled = isSelection;
			EditPasteItem.Enabled = scintillaControl.CanPaste;
		}

		private void scintillaControl_KeyDown(object sender, KeyEventArgs e)
		{
			if ((e.Modifiers & Keys.Control) != 0)
			{
				if (e.KeyCode >= Keys.A && e.KeyCode <= Keys.Z)
					e.SuppressKeyPress = true;
			}
		}

		/// <summary>
		/// This notification is sent when the text or styling of the document changes or is about to change. 
		/// You can set a mask for the notifications that are sent to the container with SCI_SETMODEVENTMASK. 
		/// The notification structure contains information about what changed, how the change occurred and 
		/// whether this changed the number of lines in the document. No modifications may be performed while 
		/// in a SCN_MODIFIED event. 
		/// </summary>
		private void scintillaControl_Modified(object sender, Scintilla.ModifiedEventArgs e)
		{
			if(this.IsHandleCreated && e.IsUserChange && ((e.ModificationType & (int) Scintilla.Enums.ModificationFlags.ChangeStyle) == 0))
				Document.Modified = true;

			UpdateMenu();
		}

		/// <summary>
		/// When in read-only mode, this notification is sent to the container if the user tries to change the text.
		/// This can be used to check the document out of a version control system. 
		/// </summary>
		private void scintillaControl_ModifyAttemptRO(object sender, Scintilla.ModifyAttemptROEventArgs e)
		{
			Document.Checkout();
		}

		private void scintillaControl_SavePointLeft(object sender, Scintilla.SavePointLeftEventArgs e)
		{
			Document.Modified = true;
		}

		private void scintillaControl_SavePointReached(object sender, Scintilla.SavePointReachedEventArgs e)
		{
			Document.Modified = false;
		}

		/// <summary>
		/// Either the text or styling of the document has changed or the selection range has changed. 
		/// Now would be a good time to update any container UI elements that depend on document or view state.
		/// </summary>
		private void scintillaControl_UpdateUI(object sender, Scintilla.UpdateUIEventArgs e)
		{
			UpdateMenu();
		}

		public void AutoSelect()
		{
			if (scintillaControl.SelectionStart == scintillaControl.SelectionEnd)
			{
				int pos = scintillaControl.SelectionStart;
				scintillaControl.SelectionStart = scintillaControl.WordStartPosition(pos, true);
				scintillaControl.SelectionEnd = scintillaControl.WordEndPosition(pos, true);
			}
		}

		private void EditFindItem_Click(object sender, EventArgs e)
		{
			AutoSelect();
			mTextPlugin.Find(scintillaControl.GetSelectedText());
		}

		private void EditReplaceItem_Click(object sender, EventArgs e)
		{
			AutoSelect();
			mTextPlugin.Replace(scintillaControl.GetSelectedText());
		}

		private void EditFindNextItem_Click(object sender, EventArgs e)
		{
			mTextPlugin.FindNext();
		}

		private void EditFindPreviousItem_Click(object sender, EventArgs e)
		{
			mTextPlugin.FindPrevious();
		}

		private void TextView_Activated(object sender, EventArgs e)
		{
		}

		private void EditGotoLineItem_Click(object sender, EventArgs e)
		{
			GotoLineForm form = new GotoLineForm(scintillaControl.LineCount);
			if (form.ShowDialog(this) == DialogResult.OK)
			{
				ShowLine(form.Selection);
			}
		}

	}
}

