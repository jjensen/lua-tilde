
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

namespace Tilde.LuaDebugger
{
	[ToolWindowAttribute(Group = "Debug")]
	public partial class BreakpointsWindow : Tilde.Framework.View.ToolWindow
	{
		DebugManager mDebugger;
		bool mUpdating;

		public BreakpointsWindow(IManager manager)
		{
			InitializeComponent();

			mDebugger = ((LuaPlugin)manager.GetPlugin(typeof(LuaPlugin))).Debugger;

			mDebugger.DebuggerDisconnecting += new DebuggerDisconnectingEventHandler(Debugger_DebuggerDisconnecting);
			mDebugger.DebuggerConnected += new DebuggerConnectedEventHandler(Debugger_DebuggerConnected);
			mDebugger.BreakpointChanged += new BreakpointChangedEventHandler(Debugger_BreakpointChanged);
		}

		void Debugger_DebuggerDisconnecting(DebugManager sender, Target target)
		{
			target.StateUpdate -= new StateUpdateEventHandler(target_StateUpdate);
		}

		void Debugger_DebuggerConnected(DebugManager sender, Target target)
		{
			target.StateUpdate += new StateUpdateEventHandler(target_StateUpdate);
		}

		void target_StateUpdate(Target sender, StateUpdateEventArgs args)
		{
		}

		void Debugger_BreakpointChanged(DebugManager sender, BreakpointDetails bkpt, bool valid)
		{
			ListViewItem item = FindBreakpoint(bkpt.FileName, bkpt.Line);

			mUpdating = true;
			if(item == null && valid)
			{
				item = new ListViewItem(bkpt.FileName);
				item.SubItems.Add(bkpt.Line.ToString());
				item.SubItems.Add(bkpt.TargetState.ToString());
				item.ImageIndex = 0;
				item.Checked = bkpt.Enabled;
				item.Tag = bkpt;
				breakpointListView.Items.Add(item);
			}
			else if(item != null && valid)
			{
				item.Checked = bkpt.Enabled;
				item.SubItems[2].Text = bkpt.TargetState.ToString();
			}
			else if(item != null && !valid)
			{
				breakpointListView.Items.Remove(item);
			}
			mUpdating = false;
		}

		ListViewItem FindBreakpoint(string filename, int line)
		{
			string linestr = line.ToString();
			foreach (ListViewItem item in breakpointListView.Items)
			{
				if (item.Text == filename && item.SubItems[1].Text == linestr)
				{
					return item;
				}
			}
			return null;
		}

		private void GotoSource()
		{
			if (breakpointListView.SelectedItems.Count > 0)
			{
				ListViewItem item = breakpointListView.SelectedItems[0];
				if (item != null)
				{
					mDebugger.ShowSource(item.Text, Int32.Parse(item.SubItems[1].Text));
				}
			}
		}

		private void breakpointListView_DoubleClick(object sender, EventArgs e)
		{
			GotoSource();
		}

		private void gotoSourceToolStripMenuItem_Click(object sender, EventArgs e)
		{
			GotoSource();
		}

		private void breakpointListView_ItemChecked(object sender, ItemCheckedEventArgs e)
		{
			if (!mUpdating)
			{
				BreakpointDetails bkpt = (BreakpointDetails)e.Item.Tag;
				if (e.Item.Checked)
					mDebugger.EnableBreakpoint(bkpt.FileName, bkpt.Line);
				else
					mDebugger.DisableBreakpoint(bkpt.FileName, bkpt.Line);
			}
		}

		private void removeBreakpointToolStripMenuItem_Click(object sender, EventArgs e)
		{
			List<BreakpointDetails> bkpts = new List<BreakpointDetails>();
			foreach (ListViewItem item in breakpointListView.SelectedItems)
			{
				BreakpointDetails bkpt = (BreakpointDetails)item.Tag;
				bkpts.Add(bkpt);
			}
			foreach (BreakpointDetails bkpt in bkpts)
			{
				mDebugger.RemoveBreakpoint(bkpt.FileName, bkpt.Line);
			}
		}

	}
}

