
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
using Tilde.CorePlugins.TextEditor;
using Tilde.Framework;

namespace Tilde.LuaDebugger
{
	public partial class LuaScriptView : Tilde.CorePlugins.TextEditor.TextView
	{
		private DebugManager mDebugger;
		private int mExecutionLine;

		enum LineMarker
		{
			Breakpoint,
			Execution
		}

		public LuaScriptView(IManager manager, Document doc)
			: base(manager, doc)
		{
			InitializeComponent();

			scintillaControl.MarginTypeN(0, (int) Scintilla.Enums.MarginType.Number);
			scintillaControl.MarginWidthN(0, scintillaControl.TextWidth((int) Scintilla.Enums.StylesCommon.LineNumber, "_99999"));
			scintillaControl.MarginWidthN(1, 12);			// Breakpoint
			scintillaControl.MarginWidthN(2, 12);			// Fold
			scintillaControl.MarginWidthN(3, 0);			// Execution point
			scintillaControl.MarginSensitiveN(0, true);
			scintillaControl.MarginSensitiveN(1, true);

			scintillaControl.MarginMaskN(1, 1 << ((int)LineMarker.Breakpoint));			// Show marker 0 in margin 1
			scintillaControl.MarginMaskN(3, 1 << ((int)LineMarker.Execution));			// Show marker 1 in margin 3

			scintillaControl.MarkerDefine((int) LineMarker.Breakpoint, Scintilla.Enums.MarkerSymbol.Circle);
			scintillaControl.MarkerSetForegroundColor((int)LineMarker.Breakpoint, 0x0000ff);
			scintillaControl.MarkerSetBackgroundColor((int)LineMarker.Breakpoint, 0x0000ff);

			scintillaControl.MarkerDefine((int)LineMarker.Execution, Scintilla.Enums.MarkerSymbol.Arrow);
			scintillaControl.MarkerSetForegroundColor((int)LineMarker.Execution, 0x00ffff);
			scintillaControl.MarkerSetBackgroundColor((int)LineMarker.Execution, 0x00ffff);

			scintillaControl.MarkerDefine((int)Scintilla.Enums.MarkerOutline.FolderOpen, Scintilla.Enums.MarkerSymbol.CircleMinus);
			scintillaControl.MarkerDefine((int)Scintilla.Enums.MarkerOutline.Folder, Scintilla.Enums.MarkerSymbol.CirclePlus);
			scintillaControl.MarkerDefine((int)Scintilla.Enums.MarkerOutline.FolderSub, Scintilla.Enums.MarkerSymbol.VLine);
			scintillaControl.MarkerDefine((int)Scintilla.Enums.MarkerOutline.FolderTail, Scintilla.Enums.MarkerSymbol.LCornerCurve);
			scintillaControl.MarkerDefine((int)Scintilla.Enums.MarkerOutline.FolderEnd, Scintilla.Enums.MarkerSymbol.CirclePlusConnected);
			scintillaControl.MarkerDefine((int)Scintilla.Enums.MarkerOutline.FolderOpenMid, Scintilla.Enums.MarkerSymbol.CircleMinusConnected);
			scintillaControl.MarkerDefine((int)Scintilla.Enums.MarkerOutline.FolderMidTail, Scintilla.Enums.MarkerSymbol.TCornerCurve);

			scintillaControl.MarginClick += new EventHandler<Scintilla.MarginClickEventArgs>(scintillaControl_MarginClick);
			scintillaControl.StyleNeeded += new EventHandler<Scintilla.StyleNeededEventArgs>(scintillaControl_StyleNeeded);

			mExecutionLine = 0;
			mDebugger = (manager.GetPlugin(typeof(LuaPlugin)) as LuaPlugin).Debugger;

			MergeMenu(this.MainMenuStrip);
		}


		private void LuaScriptView_Load(object sender, EventArgs e)
		{
			mDebugger.BreakpointChanged += new BreakpointChangedEventHandler(Debugger_BreakpointChanged);
			mDebugger.CurrentStackFrameChanged += new CurrentStackFrameChangedEventHandler(Debugger_CurrentStackFrameChanged);
		}

		private void LuaScriptView_FormClosed(object sender, FormClosedEventArgs e)
		{
			mDebugger.BreakpointChanged -= new BreakpointChangedEventHandler(Debugger_BreakpointChanged);
			mDebugger.CurrentStackFrameChanged -= new CurrentStackFrameChangedEventHandler(Debugger_CurrentStackFrameChanged);
		}

		void Debugger_BreakpointChanged(DebugManager sender, BreakpointDetails bkpt, bool valid)
		{
			if (PathUtils.Compare(Document.FileName, bkpt.FileName) == 0)
			{
				SetBreakpoint(bkpt.Line, valid);
			}
		}

		void Debugger_CurrentStackFrameChanged(DebugManager sender, LuaStackFrame frame)
		{
			if (frame != null && PathUtils.Compare(Document.FileName, frame.File) == 0)
			{
				ExecutionLine = frame.Line;
			}
			else
			{
				ExecutionLine = -1;
			}
		}

		void scintillaControl_StyleNeeded(object sender, Scintilla.StyleNeededEventArgs args)
		{
			scintillaControl.Colorize(scintillaControl.EndStyled, args.Position);
		}

		void scintillaControl_MarginClick(object sender, Scintilla.MarginClickEventArgs args)
		{
			int line = scintillaControl.LineFromPosition(args.Position);

			if(args.Margin == 0)
			{
				scintillaControl.SetSelection(scintillaControl.PositionFromLine(line), scintillaControl.PositionFromLine(line + 1));
			}
			else if (args.Margin == 1)
			{
				ToggleBreakpoint(line);
			}
		}

		private void toggleBreakpointToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ToggleBreakpoint(scintillaControl.LineFromPosition(scintillaControl.CurrentPos));
		}

		private void ToggleBreakpoint(int line)
		{
			mDebugger.ToggleBreakpoint(Document.FileName, line + 1);
			/*
			bool enabled = ((scintillaControl.MarkerGet(line) & (1 << ((int)LineMarker.Breakpoint))) == 0);

			if (enabled)
				mDebugger.AddBreakpoint(Document.NormalisedFileName, line + 1);
			else
				mDebugger.RemoveBreakpoint(Document.NormalisedFileName, line + 1);
			*/
		}

		public void SetBreakpoint(int line, bool enabled)
		{
			if(enabled)
				scintillaControl.MarkerAdd(line - 1, (int)LineMarker.Breakpoint);
			else
				scintillaControl.MarkerDelete(line - 1, (int)LineMarker.Breakpoint);
		}

		private void scintillaControl_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{

		}

		internal int ExecutionLine
		{
			get { return mExecutionLine; }
			set
			{
				if(mExecutionLine != value)
				{
					scintillaControl.MarkerDeleteAll((int)LineMarker.Execution);

					mExecutionLine = value;

					if (mExecutionLine > 0)
						scintillaControl.MarkerAdd(mExecutionLine - 1, (int)LineMarker.Execution);
				}
			}
		}



	}
}

