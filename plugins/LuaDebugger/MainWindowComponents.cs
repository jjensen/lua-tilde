
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
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using Tilde.Framework.Controller;
using Tilde.Framework.Model;

namespace Tilde.LuaDebugger
{
	public partial class MainWindowComponents : UserControl
	{
		DebugManager mDebugger;

		public MainWindowComponents(DebugManager debugger)
		{
			mDebugger = debugger;

			InitializeComponent();

			mDebugger.DebuggerConnecting += new DebuggerConnectingEventHandler(Debugger_DebuggerConnecting);
			mDebugger.DebuggerConnected += new DebuggerConnectedEventHandler(Debugger_DebuggerConnected);
			mDebugger.DebuggerDisconnecting += new DebuggerDisconnectingEventHandler(Debugger_DebuggerDisconnecting);
			mDebugger.DebuggerDisconnected += new DebuggerDisconnectedEventHandler(Debugger_DebuggerDisconnected);
			mDebugger.Manager.ActiveDocumentChanged += new ActiveDocumentChangedEventHandler(Manager_ActiveDocumentChanged);

			UpdateMenuStatus();
		}

		void Debugger_DebuggerConnecting(DebugManager sender, Target target)
		{
			UpdateMenuStatus();
		}

		void Debugger_DebuggerDisconnecting(DebugManager sender, Target target)
		{
			target.StateUpdate -= new StateUpdateEventHandler(target_StateUpdate);
			UpdateMenuStatus();
		}

		void Debugger_DebuggerConnected(DebugManager sender, Target target)
		{
			target.StateUpdate += new StateUpdateEventHandler(target_StateUpdate);
			target.ExMessage += new ExMessageEventHandler(target_ExCommandResult);
			UpdateMenuStatus();
		}

		void Debugger_DebuggerDisconnected(DebugManager sender)
		{
			UpdateMenuStatus();
		}

		void target_StateUpdate(Target sender, StateUpdateEventArgs args)
		{
			UpdateMenuStatus();
		}

		void target_ExCommandResult(Target sender, ExMessageEventArgs args)
		{
			if (args.Command == "StartProfile")
				mDebugger.ShowStatusMessage("Lua Profile Started...");
			else if (args.Command == "StopProfile")
				mDebugger.ShowStatusMessage("Lua Profile Stopped!");
		}

		void Manager_ActiveDocumentChanged(object sender, Document document)
		{
			UpdateMenuStatus();
		}

		void UpdateMenuStatus()
		{
			bool isBreaked = (mDebugger.TargetStatus == TargetState.Breaked || mDebugger.TargetStatus == TargetState.Error);
			bool isRunning = (mDebugger.TargetStatus == TargetState.Running);

			btnConnect.Enabled = connectToolStripMenuItem.Enabled = (mDebugger.ConnectionStatus == ConnectionStatus.NotConnected);
			btnDisconnect.Enabled = disconnectToolStripMenuItem.Enabled = (mDebugger.ConnectionStatus == ConnectionStatus.Connected || mDebugger.ConnectionStatus == ConnectionStatus.Connecting);
			reconnectToolStripMenuItem.Enabled = (mDebugger.ConnectionStatus == ConnectionStatus.Connected);

			btnStartProfile.Enabled = (mDebugger.ConnectionStatus == ConnectionStatus.Connected);
			btnStopProfile.Enabled = (mDebugger.ConnectionStatus == ConnectionStatus.Connected);
			btnRun.Enabled = isBreaked;
			btnBreak.Enabled = isRunning;

			runToolStripMenuItem.Enabled = isBreaked || mDebugger.ConnectionStatus == ConnectionStatus.Disconnecting || mDebugger.ConnectionStatus == ConnectionStatus.NotConnected;
			stepToolStripMenuItem.Enabled = isBreaked;
			stepIntoToolStripMenuItem.Enabled = isBreaked;
			stepOutToolStripMenuItem.Enabled = isBreaked;
			breakToolStripMenuItem.Enabled = isRunning;

			buildAndRestartToolStripMenuItem.Enabled = !mDebugger.IsBuildInProgress;
			cancelBuildToolStripMenuItem.Enabled = mDebugger.IsBuildInProgress;
		}

		private void connectToolStripMenuItem_Click(object sender, EventArgs e)
		{
			TargetConnectionDialog dlg = new TargetConnectionDialog(mDebugger);
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				mDebugger.Connect(dlg.Selection);
			}
		}

		private void disconnectToolStripMenuItem_Click(object sender, EventArgs e)
		{
			mDebugger.Disconnect(false, true);
		}

		private void runToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (mDebugger.ConnectedTarget != null)
				mDebugger.ConnectedTarget.Run(ExecutionMode.Go);
			else
				mDebugger.Run();
		}

		private void stepToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (mDebugger.ConnectedTarget != null)
				mDebugger.ConnectedTarget.Run(ExecutionMode.StepOver);
		}

		private void stepIntoToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (mDebugger.ConnectedTarget != null)
				mDebugger.ConnectedTarget.Run(ExecutionMode.StepInto);
		}

		private void stepOutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (mDebugger.ConnectedTarget != null)
				mDebugger.ConnectedTarget.Run(ExecutionMode.StepOut);
		}

		private void breakToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (mDebugger.ConnectedTarget != null)
				mDebugger.ConnectedTarget.Run(ExecutionMode.Break);
		}

		private void reconnectToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if(mDebugger.ConnectedTarget != null && mDebugger.ConnectionStatus == ConnectionStatus.Connected)
			{
				HostInfo currHost = mDebugger.ConnectedTarget.HostInfo;

				mDebugger.Disconnect(false, false);
				mDebugger.Connect(currHost);
			}
		}

		private void btnStartProfile_Click(object sender, EventArgs e)
		{
			if (mDebugger.ConnectedTarget != null)
				mDebugger.ConnectedTarget.ExCommand("StartProfile", null);
		}

		private void btnStopProfile_Click(object sender, EventArgs e)
		{
			if (mDebugger.ConnectedTarget != null)
				mDebugger.ConnectedTarget.ExCommand("StopProfile", null);
		}

		private void notifyIcon_BalloonTipClicked(object sender, EventArgs e)
		{
			mDebugger.MainWindow.Activate();
		}

		private void notifyIcon_DoubleClick(object sender, EventArgs e)
		{
			mDebugger.MainWindow.Activate();
		}

		private void buildToolStripMenuItem_Click(object sender, EventArgs e)
		{
			mDebugger.Build();
		}

		private void buildAndRestartToolStripMenuItem_Click(object sender, EventArgs e)
		{
			mDebugger.BuildAndRun();
		}

		private void stopToolStripMenuItem_Click(object sender, EventArgs e)
		{
			mDebugger.StopTarget();
		}

		private void cancelBuildToolStripMenuItem_Click(object sender, EventArgs e)
		{
			mDebugger.CancelBuild();
		}
	}
}
