
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
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Windows.Forms;

using Tilde.Framework.Controller;
using Tilde.Framework.View;
using Tilde.Framework.Model;
using Tilde.Framework.Model.ProjectHierarchy;
using System.Threading;
using Tilde.CorePlugins.TextEditor;
using Tilde.Framework;

namespace Tilde.LuaDebugger
{
	public delegate void DebuggerConnectingEventHandler(DebugManager sender, Target target);
	public delegate void DebuggerConnectedEventHandler(DebugManager sender, Target target);
	public delegate void DebuggerDisconnectingEventHandler(DebugManager sender, Target target);
	public delegate void DebuggerDisconnectedEventHandler(DebugManager sender);
	public delegate void CurrentStackFrameChangedEventHandler(DebugManager sender, LuaStackFrame frame);
	public delegate void BreakpointChangedEventHandler(DebugManager sender, BreakpointDetails bkpt, bool valid);

	public enum ConnectionStatus
	{
		NotConnected,
		Connecting,
		Connected,
		Disconnecting
	}

	public class DebugManager
	{
		MainWindowComponents mMainWindowComponents;
		bool mBuildInProgress = false;

		public DebugManager(IManager manager)
		{
			mManager = manager;
			mPlugin = (LuaPlugin) manager.GetPlugin(typeof(LuaPlugin));
			mTransports = new List<ITransport>();
			mConnectionStatus = ConnectionStatus.NotConnected;
			mTargetStatus = TargetState.Disconnected;
			mConnectedTarget = null;
			mBreakpoints = new List<BreakpointDetails>();
			mWatches = new Dictionary<int, WatchDetails>();
			mValueCache = new ValueCache();

			mMainWindowComponents = new MainWindowComponents(this);

			InitialiseTransports();

			Manager.AddToMenuStrip(mMainWindowComponents.menuStrip.Items);
			Manager.AddToStatusStrip(mMainWindowComponents.statusStrip.Items);
			Manager.AddToolStrip(mMainWindowComponents.toolStrip, DockStyle.Top, 1);

			Manager.ProjectOpened += new ProjectOpenedEventHandler(Manager_ProjectOpened);

			if(Manager.MainWindow != null)
				Manager.MainWindow.FormClosing += new FormClosingEventHandler(MainWindow_FormClosing);
		}

		void Manager_ProjectOpened(IManager sender, Project project)
		{
			if(Options.LuaSourceSearchPath.Length == 0)
			{
				Options.LuaSourceSearchPath = new string[] { project.RootDocument.BaseDirectory };
			}
		}

		public event DebuggerConnectingEventHandler DebuggerConnecting;
		public event DebuggerConnectedEventHandler DebuggerConnected;
		public event DebuggerDisconnectingEventHandler DebuggerDisconnecting;
		public event DebuggerDisconnectedEventHandler DebuggerDisconnected;
		public event CurrentStackFrameChangedEventHandler CurrentStackFrameChanged;
		public event BreakpointChangedEventHandler BreakpointChanged;

		public IManager Manager
		{
			get { return mManager; }
		}

		public Form MainWindow
		{
			get { return mManager.MainWindow; }
		}

		public DebuggerOptions Options
		{
			get { return mPlugin.Options; }
		}

		public List<ITransport> Transports
		{
			get { return mTransports; }
		}

		public Target ConnectedTarget
		{
			get { return mConnectedTarget;  }
		}

		public ConnectionStatus ConnectionStatus
		{
			get { return mConnectionStatus; }
		}

		public TargetState TargetStatus
		{
			get { return mTargetStatus; }
		}

		public List<BreakpointDetails> Breakpoints
		{
			get { return mBreakpoints; }
		}

		public ICollection<WatchDetails> Watches
		{
			get { return mWatches.Values; }
		}

		public LuaValue MainThread
		{
			get { return new LuaValue(0, LuaValueType.THREAD); }
		}

		public LuaValue CurrentThread
		{
			get { return mCurrentThread; }
			set
			{
				if (value == null)
					mCurrentThread = LuaValue.nil;
				else
					mCurrentThread = value;

				if (mConnectedTarget != null && mCurrentStackFrame != null && !mCurrentThread.IsNil())
				{
					mConnectedTarget.RetrieveStatus(mCurrentThread, mCurrentStackFrame.Depth);
				}
			}
		}

		public LuaStackFrame CurrentStackFrame
		{
			get { return mCurrentStackFrame; }
			set
			{
				// Broadcast a stackframe change regardless, so we can click on the active frame to return to the source line
				mCurrentStackFrame = value;
				OnCurrentStackFrameChanged(mCurrentStackFrame, true);
			}
		}

		public ValueCache ValueCache
		{
			get { return mValueCache; }
		}

		public bool IsBuildInProgress
		{
			get { return mBuildInProgress; }
		}

		public void Connect(HostInfo hostInfo)
		{
			if (mConnectedTarget == null)
			{
				IConnection connection = hostInfo.Transport.Connect(hostInfo);

				if (connection != null)
				{
					Target target = new Target(this, MainWindow, connection);

					mStatusMessage = new DebuggerStatusDialog("Establishing connection to " + target.HostInfo.ToString() + "...", true);
					mStatusMessage.Cancel.Click += new EventHandler(StatusMessage_Cancel_Click);
					mStatusMessage.Show(MainWindow);

					SetNotification(Tilde.LuaDebugger.Properties.Resources.SystrayConnected, "Tilde Connecting...");

					mConnectedTarget = target;
					mConnectedTarget.DebugPrint += new DebugPrintEventHandler(Target_DebugPrint);
					mConnectedTarget.ErrorMessage += new ErrorMessageEventHandler(Target_ErrorMessage);
					mConnectedTarget.StatusMessage += new StatusMessageEventHandler(Target_StatusMessage);
					mConnectedTarget.StateUpdate += new StateUpdateEventHandler(Target_StateUpdate);
					mConnectedTarget.CallstackUpdate += new CallstackUpdateEventHandler(Target_CallstackUpdate);
					mConnectedTarget.BreakpointUpdate += new BreakpointUpdateEventHandler(Target_BreakpointUpdate);
					mConnectedTarget.ValueCached += new ValueCachedEventHandler(Target_ValueCached);
					mConnectedTarget.FileUpload += new FileUploadEventHandler(Target_FileUpload);

					OnDebuggerConnecting(mConnectedTarget);
				}
			}
		}

		public void Disconnect(bool silent)
		{
			if (mConnectedTarget != null)
			{
				HideStatusMessage();

				OnDebuggerDisconnecting(mConnectedTarget);

				mConnectedTarget.ValueCached -= new ValueCachedEventHandler(Target_ValueCached);
				mConnectedTarget.BreakpointUpdate -= new BreakpointUpdateEventHandler(Target_BreakpointUpdate);
				mConnectedTarget.CallstackUpdate -= new CallstackUpdateEventHandler(Target_CallstackUpdate);
				mConnectedTarget.StateUpdate -= new StateUpdateEventHandler(Target_StateUpdate);
				mConnectedTarget.StatusMessage -= new StatusMessageEventHandler(Target_StatusMessage);
				mConnectedTarget.ErrorMessage -= new ErrorMessageEventHandler(Target_ErrorMessage);
				mConnectedTarget.DebugPrint -= new DebugPrintEventHandler(Target_DebugPrint);
				mConnectedTarget.Disconnect();

				mTargetStatus = TargetState.Disconnected;

				// Don't send messages about changes if we're shutting down the app
				if (!silent)
				{
					SetNotification(Tilde.LuaDebugger.Properties.Resources.SystrayDisconnected, "Tilde disconnected", "Connection to " + mConnectedTarget.HostInfo.ToString() + " has been closed.");

					// Tell everyone we no longer have a stack frame
					CurrentStackFrame = null;

					// Reset breakpoint state
					foreach (BreakpointDetails bkpt in mBreakpoints)
					{
						bkpt.TargetState = BreakpointState.NotSent;
						OnBreakpointChanged(bkpt, true);
					}

					// Reset breakpoint state
					foreach (WatchDetails watch in mWatches.Values)
					{
						watch.State = WatchState.NotSent;
					}
				}

				mConnectedTarget = null;
				mMainWindowComponents.targetStateLabel.Text = "";

				OnDebuggerDisconnected();
			}
		}

		public BreakpointDetails FindBreakpoint(string file, int line)
		{
			foreach(BreakpointDetails bkpt in mBreakpoints)
			{
				if (bkpt.FileName == file && bkpt.Line == line)
					return bkpt;
			}
			return null;
		}

		public BreakpointDetails FindBreakpoint(int bkptid)
		{
			foreach (BreakpointDetails bkpt in mBreakpoints)
			{
				if (bkpt.ID == bkptid)
					return bkpt;
			}
			return null;
		}

		public BreakpointDetails ToggleBreakpoint(string file, int line)
		{
			BreakpointDetails bkpt = FindBreakpoint(file, line);
			if (bkpt == null)
				return AddBreakpoint(file, line);
			else
				RemoveBreakpoint(file, line);

			return null;
		}

		public BreakpointDetails AddBreakpoint(string file, int line)
		{
			BreakpointDetails bkpt = FindBreakpoint(file, line);
			if (bkpt == null)
			{
				bkpt = new BreakpointDetails(file, line, true);
				mBreakpoints.Add(bkpt);

				if (mConnectedTarget != null)
				{
					bkpt.TargetState = BreakpointState.PendingAdd;
					mConnectedTarget.AddBreakpoint(file, line, bkpt.ID);
				}
			}
			OnBreakpointChanged(bkpt, true);
			return bkpt;
		}

		public void RemoveBreakpoint(string file, int line)
		{
			BreakpointDetails bkpt = FindBreakpoint(file, line);
			if (bkpt != null)
			{
				if (bkpt.Enabled && mConnectedTarget != null && bkpt.TargetState != BreakpointState.PendingRemove)
				{
					bkpt.TargetState = BreakpointState.PendingRemove;
					mConnectedTarget.RemoveBreakpoint(bkpt.ID);
					OnBreakpointChanged(bkpt, true);
				}
				else if(mConnectedTarget == null)
				{
					mBreakpoints.Remove(bkpt);
					OnBreakpointChanged(bkpt, false);
				}
			}
		}

		public void EnableBreakpoint(string file, int line)
		{
			BreakpointDetails bkpt = FindBreakpoint(file, line);
			if (bkpt == null)
			{
				bkpt = new BreakpointDetails(file, line, true);
				mBreakpoints.Add(bkpt);
			}
			else if (!bkpt.Enabled)
			{
				bkpt.Enabled = true;
			}
			else
			{
				return;
			}

			if (mConnectedTarget != null)
			{
				bkpt.TargetState = BreakpointState.PendingAdd;
				mConnectedTarget.AddBreakpoint(file, line, bkpt.ID);
			}

			OnBreakpointChanged(bkpt, true);
		}

		public void DisableBreakpoint(string file, int line)
		{
			BreakpointDetails bkpt = FindBreakpoint(file, line);
			if (bkpt == null)
			{
				bkpt = new BreakpointDetails(file, line, false);
				mBreakpoints.Add(bkpt);
			}
			else if (bkpt.Enabled)
			{
				bkpt.Enabled = false;

				if (mConnectedTarget != null)
				{
					bkpt.TargetState = BreakpointState.PendingDisable;
					mConnectedTarget.RemoveBreakpoint(bkpt.ID);
				}
			}

			OnBreakpointChanged(bkpt, true);
		}

		public WatchDetails FindWatch(int watchid)
		{
			WatchDetails watch;
			mWatches.TryGetValue(watchid, out watch);
			return watch;
		}

		public WatchDetails AddWatch(string expr)
		{
			WatchDetails watch = new WatchDetails(expr, true);
			mWatches.Add(watch.ID, watch);

			if (mConnectedTarget != null)
			{
				watch.State = WatchState.PendingAdd;
				mConnectedTarget.AddWatch(watch.Expression, watch.ID);
			}

			return watch;
		}

		public void RemoveWatch(WatchDetails watch)
		{
			mWatches.Remove(watch.ID);
			if(mConnectedTarget != null && watch.Enabled)
			{
				watch.State = WatchState.PendingRemove;
				mConnectedTarget.RemoveWatch(watch.ID);
			}
		}

		public string GetValueString(LuaValue luaValue)
		{
			if (luaValue == null)
				return "";

			switch (luaValue.Type)
			{
				case LuaValueType.NIL:
					return "nil";

				case LuaValueType.BOOLEAN:
					return luaValue.AsBoolean().ToString();

				case LuaValueType.NUMBER:
					return luaValue.AsNumber().ToString("R");

				case LuaValueType.TILDE_METATABLE:
					return "metatable";

				case LuaValueType.TILDE_ENVIRONMENT:
					return "environment";

				case LuaValueType.TILDE_UPVALUES:
					return "upvalues";

				default:
					if (mValueCache.Contains(luaValue))
						return mValueCache.Get(luaValue);
					else
						return "Unknown:" + luaValue.ToString();
			}
		}

		public void ShowStatusMessage(string message)
		{
			Manager.ShowMessages("Debug");
			Manager.AddMessage("Debug", message + "\r\n");
			Manager.FlashMainWindow();
			MessageBox.Show(Manager.MainWindow, message, "Lua Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		public string FindTargetFile(string sourceFile)
		{
			if (sourceFile == null)
				return null;

			string fileName = PathUtils.MakeCanonicalFileName(sourceFile);
			foreach(string path in Options.LuaSourceSearchPath)
			{
				string normPath = PathUtils.MakeCanonicalFileName(path);
				if (normPath.Length < fileName.Length && String.Compare(normPath, 0, fileName, 0, normPath.Length, true) == 0)
				{
					return fileName.Substring(path.Length + 1).Replace('\\', '/');
				}
			}
			return sourceFile;
		}

		public string FindSourceFile(string targetFile)
		{
			if (targetFile == null)
				return null;

			string fileName = PathUtils.MakeCanonicalFileName(targetFile);
			foreach (string path in Options.LuaSourceSearchPath)
			{
				string sourceFile = Path.Combine(path, fileName);
				if (File.Exists(sourceFile))
					return sourceFile;
			}
			return targetFile;
		}

		void OnDebuggerConnecting(Target target)
		{
			mConnectionStatus = ConnectionStatus.Connecting;

			if (DebuggerConnecting != null)
				DebuggerConnecting(this, target);
		}

		void OnDebuggerConnected(Target target)
		{
			mConnectionStatus = ConnectionStatus.Connected;

			if (DebuggerConnected != null)
				DebuggerConnected(this, target);
		}

		void OnDebuggerDisconnecting(Target target)
		{
			mConnectionStatus = ConnectionStatus.Disconnecting;

			if (DebuggerDisconnecting != null)
				DebuggerDisconnecting(this, target);
		}

		void OnDebuggerDisconnected()
		{
			mConnectionStatus = ConnectionStatus.NotConnected;

			if (DebuggerDisconnected != null)
				DebuggerDisconnected(this);
		}

		void OnCurrentStackFrameChanged(LuaStackFrame frame, bool getlocals)
		{
			if (frame != null)
			{
				ShowSource(frame.File, frame.Line);
				if (getlocals)
				{
					mConnectedTarget.RetrieveLocals(mCurrentThread, frame.Depth);
					mConnectedTarget.RetrieveWatches(mCurrentThread, frame.Depth);
				}
			}

			if (CurrentStackFrameChanged != null)
				CurrentStackFrameChanged(this, frame);
		}

		void OnBreakpointChanged(BreakpointDetails bkpt, bool valid)
		{
			if (BreakpointChanged != null)
				BreakpointChanged(this, bkpt, valid);
		}

		void StatusMessage_Cancel_Click(object sender, EventArgs e)
		{
			Disconnect(false);
		}

		void Target_DebugPrint(Target sender, DebugPrintEventArgs args)
		{
			Manager.AddMessage("Debug", args.Message);
		}

		void Target_ErrorMessage(Target sender, ErrorMessageEventArgs args)
		{
			Manager.ShowMessages("Debug");
			Manager.AddMessage("Debug", "\r\n" + args.Message + "\r\n");
			Manager.FlashMainWindow();

			if (mTargetStatus != TargetState.Error)
				MessageBox.Show(Manager.MainWindow, args.Message, "Lua Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			else
			{
				string result = Tilde.Framework.Controls.MessageBoxEx.Show(Manager.MainWindow, args.Message, "Lua Error", new string[] { "Break", "Continue", "Ignore" }, MessageBoxIcon.Error, "Break");
				if (result == "Continue")
					mConnectedTarget.Run(ExecutionMode.Continue);
				else if (result == "Ignore")
				{
					// Skip over any C functions on the stack to get to the top-most lua function
					foreach(LuaStackFrame frame in mCurrentStack)
					{
						if (!frame.File.StartsWith("=[C]"))
						{
							mConnectedTarget.IgnoreError(frame.File, frame.Line);
							break;
						}
					}
					mConnectedTarget.Run(ExecutionMode.Continue);
				}
			}
		}

		void Target_StatusMessage(Target sender, StatusMessageEventArgs args)
		{
			ShowStatusMessage(args.Message);
		}

		void Target_StateUpdate(Target sender, StateUpdateEventArgs args)
		{
			mTargetStatus = args.TargetState;

			if(args.TargetState == TargetState.Disconnected)
			{
				Disconnect(false);
				mMainWindowComponents.targetStateLabel.Text = "";
			}
			else if (args.TargetState == TargetState.Connected)
			{
				HideStatusMessage();

				// Tell everyone the good news
				OnDebuggerConnected(mConnectedTarget);

				SetNotification(Tilde.LuaDebugger.Properties.Resources.SystrayConnected, "Tilde connected", "Connection established to " + mConnectedTarget.HostInfo.ToString());

				// Send breakpoints
				foreach (BreakpointDetails bkpt in mBreakpoints)
				{
					if (bkpt.Enabled)
					{
						bkpt.TargetState = BreakpointState.PendingAdd;
						mConnectedTarget.AddBreakpoint(bkpt.FileName, bkpt.Line, bkpt.ID);
					}
					else
					{
						bkpt.TargetState = BreakpointState.NotSent;
					}
					OnBreakpointChanged(bkpt, true);
				}

				// Send watches
				foreach (WatchDetails watch in mWatches.Values)
				{
					if (watch.Enabled)
					{
						watch.State = WatchState.PendingAdd;
						mConnectedTarget.AddWatch(watch.Expression, watch.ID);
					}
					else
					{
						watch.State = WatchState.NotSent;
					}
				}

				// Request threads
				mConnectedTarget.RetrieveThreads();

				mMainWindowComponents.targetStateLabel.Text = "CONNECTED";
				mCurrentThread = LuaValue.nil;
			}
			else if (args.TargetState == TargetState.Running)
			{
				mMainWindowComponents.targetStateLabel.Text = "RUN";
				mCurrentThread = LuaValue.nil;
				CurrentStackFrame = null;
			}
			else if (args.TargetState == TargetState.Breaked)
			{
				mMainWindowComponents.targetStateLabel.Text = "BREAK";
				mCurrentThread = args.Thread;
				Manager.FlashMainWindow();
			}
			else if (args.TargetState == TargetState.Error)
			{
				mMainWindowComponents.targetStateLabel.Text = "ERROR";
				mCurrentThread = args.Thread;
			}
			else if (args.TargetState == TargetState.Finished)
			{
				mMainWindowComponents.targetStateLabel.Text = "FINISH";
				mCurrentThread = LuaValue.nil;
				CurrentStackFrame = null;
			}
		}

		private void HideStatusMessage()
		{
			if (mStatusMessage != null)
			{
				mStatusMessage.Close();
				mStatusMessage.Dispose();
				mStatusMessage = null;
			}
		}

		void Target_CallstackUpdate(Target sender, CallstackUpdateEventArgs args)
		{
			if(args.StackFrames.Length > 0)
			{
				mCurrentStack = args.StackFrames;
				mCurrentStackFrame = args.StackFrames[args.CurrentFrame < args.StackFrames.Length ? args.CurrentFrame : 0];
				OnCurrentStackFrameChanged(mCurrentStackFrame, false);
			}
		}

		/// <summary>
		/// Triggered when the target has accepted or rejected a breakpoint.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="args"></param>
		void Target_BreakpointUpdate(Target sender, BreakpointUpdateEventArgs args)
		{
			BreakpointDetails bkpt = FindBreakpoint(args.ID);
			if(bkpt != null)
			{
				if (args.State == BreakpointUpdateEventState.Added)
				{
					// Force it to be displayed as enabled, 'cos the debugger is gonna break on it anyway!
					bkpt.Enabled = true;
					bkpt.TargetState = BreakpointState.Accepted;
				}

				else if (args.State == BreakpointUpdateEventState.Invalid)
					bkpt.TargetState = BreakpointState.Invalid;

				else if (args.State == BreakpointUpdateEventState.Removed)
				{
					if (bkpt.TargetState == BreakpointState.PendingRemove)
						bkpt.TargetState = BreakpointState.Removed;
					else if (bkpt.TargetState == BreakpointState.PendingDisable)
						bkpt.TargetState = BreakpointState.NotSent;
				}

				if(bkpt.TargetState == BreakpointState.Removed)
					mBreakpoints.Remove(bkpt);

				OnBreakpointChanged(bkpt, bkpt.TargetState != BreakpointState.Removed);
			}
		}

		void Target_ValueCached(Target target, ValueCachedEventArgs args)
		{
			mValueCache.Add(args.Value, args.Description);
		}

		void Target_FileUpload(Target sender, FileUploadEventArgs args)
		{
			MemoryStream stream = new MemoryStream(args.Data);
			Document doc = Manager.CreateDocument(args.FileName, null, stream);
			stream.Dispose();

			if(doc != null)
				Manager.ShowDocument(doc);
		}

		public void ShowSource(string file, int line)
		{
			DocumentItem docItem = Manager.Project.FindDocument(file);
			Document doc = docItem != null ? Manager.OpenDocument(docItem) : null;
			if (doc == null)
			{
				doc = Manager.OpenDocument(file);
			}

			if (doc == null)
			{
				MessageBox.Show(Manager.MainWindow, String.Format("No source is available for \r\n\r\n{0}", file), "File not found", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			else
			{
				DocumentView docView = Manager.ShowDocument(doc);
				TextView textView = docView as TextView;

				if (textView != null)
				{
					textView.ShowLine(line);
				}
			}
		}

		void InitialiseTransports()
		{
			foreach (Type type in Manager.GetPluginImplementations(typeof(ITransport)))
			{
				mTransports.Add((ITransport)Activator.CreateInstance(type, new object[] { this }));
			}
		}

		void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
		{
			Disconnect(true);
			mMainWindowComponents.notifyIcon.Visible = false;
		}

		public void SetNotification(System.Drawing.Icon icon, String title, String message)
		{
			mMainWindowComponents.notifyIcon.Icon = icon;
			mMainWindowComponents.notifyIcon.Text = title;
			mMainWindowComponents.notifyIcon.ShowBalloonTip(5000, title, message, ToolTipIcon.Info);
			mMainWindowComponents.notifyIcon.Visible = true;
		}

		public void SetNotification(System.Drawing.Icon icon, String title)
		{
			mMainWindowComponents.notifyIcon.Icon = icon;
			mMainWindowComponents.notifyIcon.Text = title;
			mMainWindowComponents.notifyIcon.Visible = true;
		}

		public void RemoveNotification()
		{
			mMainWindowComponents.notifyIcon.Visible = false;
		}

		internal void Build()
		{
			if (mPlugin.Options.BuildCommand.Length == 0)
			{
				MessageBox.Show("No build command has been specified in the Options!");
			}
			else if (mBuildInProgress)
			{
				MessageBox.Show("A build is already in progress!");
			}
			else
			{
				mBuildInProgress = true;
				mManager.SaveAllDocuments();
				ThreadPool.QueueUserWorkItem(new WaitCallback(delegate(object state) { BuildAndRunThreadProc(true, false); }));
			}
		}

		internal void Run()
		{
			if (mPlugin.Options.RunCommand.Length == 0)
			{
				MessageBox.Show("No run command has been specified in the Options!");
			}
			else if (mBuildInProgress)
			{
				MessageBox.Show("A build is already in progress!");
			}
			else
			{
				ThreadPool.QueueUserWorkItem(new WaitCallback(delegate(object state) { BuildAndRunThreadProc(false, true); }));
			}
		}


		internal void BuildAndRun()
		{
			if (mPlugin.Options.BuildCommand.Length == 0)
			{
				MessageBox.Show("No build command has been specified in the Options!");
			}
			else if(mBuildInProgress)
			{
				MessageBox.Show("A build is already in progress!");
			}
			else
			{
				mBuildInProgress = true;
				mManager.SaveAllDocuments();
				ThreadPool.QueueUserWorkItem(new WaitCallback(delegate(object state) { BuildAndRunThreadProc(true, true); }));
			}
		}

		internal void CancelBuild()
		{
		}

		void BuildAndRunThreadProc(bool build, bool run)
		{
			try
			{
				mManager.ShowMessages("Build Output");
				int result = 0;
				if (build)
				{
					result = mManager.Execute("Build Output", mPlugin.Options.BuildCommand);
					mBuildInProgress = false;
				}

				if (run && result == 0 && mPlugin.Options.RunCommand.Length > 0)
				{
					result = mManager.Execute("Build Output", mPlugin.Options.RunCommand);
				}
			}
			catch (System.Exception)
			{				
			}
		}

		internal void StopTarget()
		{
			if (mPlugin.Options.StopCommand.Length == 0)
			{
				MessageBox.Show("No stop command has been specified in the Options!");
			}
			else
			{
				try
				{
					mManager.ShowMessages("Build Output");
					int result = mManager.Execute("Build Output", mPlugin.Options.StopCommand);
				}
				catch (System.Exception)
				{
				}
			}
		}

		private IManager mManager;
		private LuaPlugin mPlugin;
		private List<ITransport> mTransports;
		private ConnectionStatus mConnectionStatus;
		private TargetState mTargetStatus;

		private Target mConnectedTarget;
		private LuaValue mCurrentThread = LuaValue.nil;
		private LuaStackFrame[] mCurrentStack;
		private LuaStackFrame mCurrentStackFrame;
		private List<BreakpointDetails> mBreakpoints;
		private Dictionary<int, WatchDetails> mWatches;
		private ValueCache mValueCache;

		private DebuggerStatusDialog mStatusMessage;

	}
}
