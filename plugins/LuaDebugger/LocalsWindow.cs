
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
using Tilde.Framework.Controls;

namespace Tilde.LuaDebugger
{
	[ToolWindowAttribute(Group = "Debug")]
	public partial class LocalsWindow : Tilde.LuaDebugger.VariablesWindow
	{
		public LocalsWindow(IManager manager)
			: base(manager)
		{
			InitializeComponent();

			mDebugger.DebuggerConnected += new DebuggerConnectedEventHandler(Debugger_DebuggerConnected);
			mDebugger.DebuggerDisconnecting += new DebuggerDisconnectingEventHandler(Debugger_DebuggerDisconnecting);
			mDebugger.CurrentStackFrameChanged += new CurrentStackFrameChangedEventHandler(Debugger_CurrentStackFrameChanged);

			variablesListView.NodeOpened += new TreeTableNodeOpenedDelegate(variablesListView_NodeOpened);
			variablesListView.NodeClosed += new TreeTableNodeClosedDelegate(variablesListView_NodeClosed);
			variablesListView.Enabled = false;

			ToolStripManager.Merge(baseContextMenuStrip, contextMenuStrip);
		}

		void Debugger_DebuggerDisconnecting(DebugManager sender, Target target)
		{
			target.VariableUpdate -= new VariableUpdateEventHandler(Target_VariableUpdate);
			target.StateUpdate -= new StateUpdateEventHandler(Target_StateUpdate);

			Clear();
		}

		void Debugger_DebuggerConnected(DebugManager sender, Target target)
		{
			target.StateUpdate += new StateUpdateEventHandler(Target_StateUpdate);
			target.VariableUpdate += new VariableUpdateEventHandler(Target_VariableUpdate);
		}

		void Debugger_CurrentStackFrameChanged(DebugManager sender, LuaStackFrame frame)
		{
			variablesListView.Enabled = frame != null;
		}

		void Target_StateUpdate(Target sender, StateUpdateEventArgs args)
		{
		}

		void Target_VariableUpdate(Target sender, VariableUpdateEventArgs args)
		{
			if (args.Type == VariableUpdateType.Locals)
			{
				mDebugger.Manager.StopProgressBarMarquee();

				if (args.CacheFlush)
					Clear();

				UpdateVariables(args.Variables);
			}
		}

		void variablesListView_NodeOpened(TreeTableNode node)
		{
			VariableDetails details = (VariableDetails)node.Tag;

			mDebugger.Manager.StartProgressBarMarquee();

			mDebugger.ConnectedTarget.ExpandLocal(details.MakePath());
			mDebugger.ConnectedTarget.RetrieveLocals(mDebugger.CurrentThread, mDebugger.CurrentStackFrame.Depth);
		}

		void variablesListView_NodeClosed(TreeTableNode node)
		{
			VariableDetails details = (VariableDetails)node.Tag;

			mDebugger.Manager.StartProgressBarMarquee();

			mDebugger.ConnectedTarget.CloseLocal(details.MakePath());
			mDebugger.ConnectedTarget.RetrieveLocals(mDebugger.CurrentThread, mDebugger.CurrentStackFrame.Depth);
		}

		private void contextMenuStrip_Opening(object sender, CancelEventArgs e)
		{
			baseContextMenuStrip_Opening(sender, e);
		}

		private void buttonRefresh_Click(object sender, EventArgs e)
		{
			if (mDebugger.ConnectedTarget != null)
				mDebugger.ConnectedTarget.RetrieveLocals(mDebugger.CurrentThread, mDebugger.CurrentStackFrame.Depth);
		}

		private void buttonShowFunctions_Click(object sender, EventArgs e)
		{
			buttonShowFunctions.Checked = !buttonShowFunctions.Checked;
			ShowFunctions = buttonShowFunctions.Checked;
		}

		private void buttonShowMetadata_Click(object sender, EventArgs e)
		{
			buttonShowMetadata.Checked = !buttonShowMetadata.Checked;
			ShowMetadata = buttonShowMetadata.Checked;
		}
	}
}

