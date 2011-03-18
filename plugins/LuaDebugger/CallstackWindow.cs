
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
using System.Text.RegularExpressions;

namespace Tilde.LuaDebugger
{
	[ToolWindowAttribute(Group="Debug")]
	public partial class CallstackWindow : ToolWindow
	{
		DebugManager mDebugger;

		public CallstackWindow(IManager manager)
		{
			InitializeComponent();

			mDebugger = ((LuaPlugin) manager.GetPlugin(typeof(LuaPlugin))).Debugger;

			mDebugger.DebuggerConnected += new DebuggerConnectedEventHandler(Debugger_DebuggerConnected);
			mDebugger.DebuggerDisconnecting += new DebuggerDisconnectingEventHandler(Debugger_DebuggerDisconnecting);
			mDebugger.CurrentStackFrameChanged += new CurrentStackFrameChangedEventHandler(Debugger_CurrentStackFrameChanged);
		}

		void Debugger_DebuggerDisconnecting(DebugManager sender, Target target)
		{
			target.CallstackUpdate -= new CallstackUpdateEventHandler(target_CallstackUpdate);
			target.StateUpdate -= new StateUpdateEventHandler(target_StateUpdate);

			callstackListView.Items.Clear();
		}

		void Debugger_DebuggerConnected(DebugManager sender, Target target)
		{
			target.StateUpdate += new StateUpdateEventHandler(target_StateUpdate);
			target.CallstackUpdate += new CallstackUpdateEventHandler(target_CallstackUpdate);
		}

		void Debugger_CurrentStackFrameChanged(DebugManager sender, LuaStackFrame frame)
		{
			foreach(ListViewItem item in callstackListView.Items)
			{
				if ((LuaStackFrame) item.Tag == frame)
					item.StateImageIndex = 0;
				else
					item.StateImageIndex = -1;
			}
		}

		void target_CallstackUpdate(Target sender, CallstackUpdateEventArgs args)
		{
			// name (class::method) [C]
			// name (C function 0x00000000) [C]
			// name (file:line) [Lua]
			string languageExpr = @"\[(?<language>\w*)\]";
			string detailsExpr = @"(\((?<details>[^)]*)\))?";
			string nameExpr = @"(?<name>\w*)?";
			Regex functionRegex = new Regex(@"^" + nameExpr + @"\s*" + detailsExpr + @"\s*" + languageExpr + "$");

			callstackListView.Items.Clear();
			foreach(LuaStackFrame stackFrame in args.StackFrames)
			{
				string funcName = stackFrame.Function;
				string source = stackFrame.File;
				string language = "?";
				string line = stackFrame.Line.ToString();

				Match match = functionRegex.Match(stackFrame.Function);
				if(match.Success)
				{
					string details = match.Groups["details"].Value;
					funcName = match.Groups["name"].Value;
					language = match.Groups["language"].Value;
					if (funcName == "")
						funcName = details;
					if (language == "C" && details != "")
						source = details;
					if (language == "C")
						line = "";
				}

				ListViewItem item = new ListViewItem(funcName);
				item.Tag = stackFrame;
				item.SubItems.Add(source);
				item.SubItems.Add(line);
				item.SubItems.Add(language);
				if (stackFrame == mDebugger.CurrentStackFrame)
					item.StateImageIndex = 0;
				if (stackFrame.File.StartsWith("=[C]"))
					item.ForeColor = SystemColors.GrayText;
				callstackListView.Items.Add(item);
			}
		}

		void target_StateUpdate(Target sender, StateUpdateEventArgs args)
		{
			if (args.TargetState != TargetState.Breaked && args.TargetState != TargetState.Error)
				callstackListView.Items.Clear();
		}

		private void callstackListView_ItemActivate(object sender, EventArgs e)
		{
			if (callstackListView.SelectedIndices.Count > 0)
				mDebugger.CurrentStackFrame = (LuaStackFrame)callstackListView.SelectedItems[0].Tag;
		}
	}
}

