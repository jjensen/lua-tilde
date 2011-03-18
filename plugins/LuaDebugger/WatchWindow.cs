
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
	public partial class WatchWindow : Tilde.LuaDebugger.VariablesWindow
	{
		bool m_editInProgress = false;

		public WatchWindow(IManager manager)
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

			UpdateConnectionState(false);
		}

		void Debugger_DebuggerDisconnecting(DebugManager sender, Target target)
		{
			target.VariableUpdate -= new VariableUpdateEventHandler(target_VariableUpdate);
			target.StateUpdate -= new StateUpdateEventHandler(target_StateUpdate);

			variablesListView.BeginUpdate();
			foreach(TreeTableNode node in variablesListView.Root.Items)
			{
				node.SubItems[1].Text = "";
				node.SubItems[2].Text = "";
				node.Items.Clear();
			}
			variablesListView.EndUpdate();

			UpdateConnectionState(false);
		}

		void Debugger_DebuggerConnected(DebugManager sender, Target target)
		{
			target.StateUpdate += new StateUpdateEventHandler(target_StateUpdate);
			target.VariableUpdate += new VariableUpdateEventHandler(target_VariableUpdate);

			UpdateConnectionState(true);
		}

		void Debugger_CurrentStackFrameChanged(DebugManager sender, LuaStackFrame frame)
		{
			variablesListView.Enabled = frame != null;
		}

		void target_StateUpdate(Target sender, StateUpdateEventArgs args)
		{
		}

		void target_VariableUpdate(Target sender, VariableUpdateEventArgs args)
		{
			if (args.Type == VariableUpdateType.Watches)
			{
				
				UpdateVariables(args.Variables);

				mDebugger.Manager.StopProgressBarMarquee();
			}
		}

		void UpdateConnectionState(bool connected)
		{
			//toolStrip1.Enabled = connected;
		}

		void variablesListView_NodeOpened(TreeTableNode node)
		{
			VariableDetails details = (VariableDetails)node.Tag;

			if (!mUpdateInProgress && mDebugger.ConnectedTarget != null && mDebugger.CurrentStackFrame != null)
			{
				mDebugger.Manager.StartProgressBarMarquee();

				mDebugger.ConnectedTarget.ExpandWatch(details.WatchID, details.MakePath());
				mDebugger.ConnectedTarget.UpdateWatch(details.WatchID, mDebugger.CurrentThread, mDebugger.CurrentStackFrame.Depth);
			}
		}

		void variablesListView_NodeClosed(TreeTableNode node)
		{
			VariableDetails details = (VariableDetails)node.Tag;

			if (!mUpdateInProgress && mDebugger.ConnectedTarget != null && mDebugger.CurrentStackFrame != null)
			{
				mDebugger.Manager.StartProgressBarMarquee();

				mDebugger.ConnectedTarget.CloseWatch(details.WatchID, details.MakePath());
				mDebugger.ConnectedTarget.UpdateWatch(details.WatchID, mDebugger.CurrentThread, mDebugger.CurrentStackFrame.Depth);
			}
		}

		private void DeleteSelectedWatches()
		{
			List<TreeTableNode> selection = new List<TreeTableNode>();
			foreach (int index in variablesListView.SelectedIndices)
			{
				TreeTableNode node = variablesListView.Nodes[index];
				if (node.Parent == variablesListView.Root)
					selection.Add(node);
			}

			foreach (TreeTableNode node in selection)
			{
				DeleteWatch(node);
			}

			if (mDebugger.ConnectedTarget != null && mDebugger.CurrentStackFrame != null)
				mDebugger.ConnectedTarget.RetrieveWatches(mDebugger.CurrentThread, mDebugger.CurrentStackFrame.Depth);
		}

		private void DeleteWatch(TreeTableNode node)
		{
			VariableDetails varInfo = (VariableDetails)node.Tag;
			WatchDetails watch = mDebugger.FindWatch(varInfo.WatchID);
			if (watch != null)
			{
				mDebugger.RemoveWatch(watch);
			}
			node.Parent.Items.Remove(node);
		}

		private void NewWatch()
		{
			TreeTableNode node = new TreeTableNode("");
			node.SubItems.Add("");
			node.SubItems.Add("");
			node.Font = mBoldFont;
			variablesListView.Root.Items.Add(node);
			node.BeginEdit(0);
		}

		private TreeTableNode AddWatch(string expression, int index)
		{
			WatchDetails watch = mDebugger.AddWatch(expression);
			TreeTableNode node = AddVariable(new VariableDetails(watch.ID, new LuaValue[] { }, null, null, false, false, false, false, VariableClass.Watch));
			node.Font = mBoldFont;
			if (index < 0)
				variablesListView.Root.Items.Add(node);
			else
				variablesListView.Root.Items.Insert(index, node);
			node.Text = expression;
			return node;
		}

		private void buttonNewWatch_Click(object sender, EventArgs e)
		{
			NewWatch();
		}

		private void buttonDeleteWatch_Click(object sender, EventArgs e)
		{
			DeleteSelectedWatches();
		}

		private void variablesListView_KeyPress(object sender, KeyPressEventArgs e)
		{
		}

		private void variablesListView_KeyDown(object sender, KeyEventArgs e)
		{
			if (!m_editInProgress)
			{
				if (e.KeyCode == Keys.Delete)
					DeleteSelectedWatches();
			}
		}

		private void buttonRefresh_Click(object sender, EventArgs e)
		{
			if (mDebugger.ConnectedTarget != null && mDebugger.CurrentStackFrame != null)
				mDebugger.ConnectedTarget.RetrieveWatches(mDebugger.CurrentThread, mDebugger.CurrentStackFrame.Depth);
		}

		private void contextMenuStrip_Opening(object sender, CancelEventArgs e)
		{
			baseContextMenuStrip_Opening(sender, e);
		}

		private void menuNewWatch_Click(object sender, EventArgs e)
		{
			NewWatch();
		}

		private void menuDeleteWatch_Click(object sender, EventArgs e)
		{
			if (!m_editInProgress)
			{
				DeleteSelectedWatches();
			}
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

		private void variablesListView_DragDrop(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(typeof(System.String)))
			{
				AddWatch(e.Data.GetData(typeof(System.String)) as String, -1);

				if (mDebugger.ConnectedTarget != null && mDebugger.CurrentStackFrame != null)
					mDebugger.ConnectedTarget.RetrieveWatches(mDebugger.CurrentThread, mDebugger.CurrentStackFrame.Depth);
			}
		}

		private void variablesListView_GiveFeedback(object sender, GiveFeedbackEventArgs e)
		{
		}

		private void variablesListView_DragOver(object sender, DragEventArgs e)
		{
			if(e.Data.GetDataPresent(typeof(System.String)))
			{
				e.Effect = DragDropEffects.Copy;
			}
			else
			{
				e.Effect = DragDropEffects.None;
			}
		}

		private void menuEditWatch_Click(object sender, EventArgs e)
		{
			if(variablesListView.SelectedIndices.Count == 1)
			{
				TreeTableNode node = variablesListView.Nodes[variablesListView.SelectedIndices[0]];
				if (node.Parent == variablesListView.Root)
					node.BeginEdit(0);
			}
		}

		private void variablesListView_BeforeLabelEdit(object sender, LabelEditEventArgs e)
		{
			TreeTableNode node = variablesListView.Nodes[e.Item];
			if (node.Parent != variablesListView.Root)
				e.CancelEdit = true;
			else
			{
				contextMenuStrip.Enabled = false;
				toolStrip1.Enabled = false;
				m_editInProgress = true;
			}
		}

		private void variablesListView_AfterLabelEdit(object sender, LabelEditEventArgs e)
		{
			m_editInProgress = false;
			contextMenuStrip.Enabled = true;
			toolStrip1.Enabled = true;

			TreeTableNode node = variablesListView.Nodes[e.Item];

			string expression = e.Label == null ? "" : e.Label.Trim();

			variablesListView.BeginUpdate();

			// Did the user cancel adding a new watch?
			if (node.Text == "" && expression == "")
			{
				e.CancelEdit = true;
				node.Parent.Items.Remove(node);
			}

			// Did the user cancel the change?
			else if (e.Label == null || expression == node.Text)
			{
				// Do nothing
			}

			// Did the user delete an existing watch?
			else if(expression == "")
			{
				e.CancelEdit = true;
				DeleteWatch(node);
			}

			// The user modified an existing watch
			else
			{
				VariableDetails oldVar = node.Tag as VariableDetails;
				if (oldVar != null)
				{
					WatchDetails oldWatch = mDebugger.FindWatch(oldVar.WatchID);
					if (oldWatch != null)
						mDebugger.RemoveWatch(oldWatch);
				}

				WatchDetails newWatch = mDebugger.AddWatch(expression);
				VariableDetails newVar = new VariableDetails(newWatch.ID, new LuaValue[] { }, null, null, false, false, false, false, VariableClass.Watch);
				node.Tag = newVar;
				node.Key = newVar.ToString();
				node.Text = expression;
				node.SubItems[1].Text = "";
				node.SubItems[2].Text = "";

				if (mDebugger.ConnectedTarget != null && mDebugger.CurrentStackFrame != null)
					mDebugger.ConnectedTarget.RetrieveWatches(mDebugger.CurrentThread, mDebugger.CurrentStackFrame.Depth);
			}

			variablesListView.EndUpdate();
		}

	}
}

