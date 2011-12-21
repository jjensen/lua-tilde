
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
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Reflection;

namespace Tilde.LuaDebugger
{
	public class Target
	{
		private DebugManager mDebugManager;
		private Form mForm;
		private IConnection mConnection;
		private Thread m_thread;
		private AutoResetEvent mHostEvent;
		private AutoResetEvent mTargetEvent;
		private MessageQueue mMessageQueue;
		private bool m_shutdown = false;

		StateUpdateEventArgs mLastUpdateMessage;
		ReceiveMessageBuffer m_readBuffer;
		SendMessageBuffer m_writeBuffer;

		private object m_lock = new object();

		public Target(DebugManager debugger, Form form, IConnection connection)
		{
			mDebugManager = debugger;
			mForm = form;
			mConnection = connection;

			mConnection.ConnectionClosed += new ConnectionClosedEventHandler(Connection_ConnectionClosed);
			mConnection.ConnectionAborted += new ConnectionAbortedEventHandler(Connection_ConnectionAborted);
			mConnection.DataReceived += new ConnectionDataReceivedEventHandler(Connection_DataReceived);

			mMessageQueue = new MessageQueue();
			mHostEvent = new AutoResetEvent(false);
			mTargetEvent = new AutoResetEvent(false);

			m_readBuffer = new ReceiveMessageBuffer(512 * 1024);
			m_writeBuffer = new SendMessageBuffer(4 * 1024);

			m_thread = new Thread(new ThreadStart(ThreadMain));
			m_thread.Name = "LuaDebuggerThread";
			m_thread.Start();
		}

		public Form Form
		{
			get { return mForm; }
		}

		public MessageQueue MessageQueue
		{
			get { return mMessageQueue; }
		}

		public AutoResetEvent Event
		{
			get { return mHostEvent; }
		}

		public bool IsShutdown
		{
			get { return m_shutdown; }
		}

		void ProcessEvents()
		{
			while(!mMessageQueue.Empty)
			{
				// Retrieve a message from the queue
				MessageQueue.Message message = mMessageQueue.Pop();

				// Execute it
				message.Invoke();
			}
		}

		void Abort()
		{
			m_shutdown = true;
			mHostEvent.Set();
		}

		void Abort(string message)
		{
			if (!message.Contains("forcibly closed"))
				OnErrorMessage("Connection error", message);
			Abort();
		}

		private delegate void VoidDelegate();
		private delegate object ReturnDelegate();

		/// <summary>
		/// Thread main loop. The thread blocks waiting for an event to be signalled, then grabs
		/// the lock before performing any further processing. Any functions called within this loop
		/// should complete as soon as possible so the lock can be retrieved.
		/// </summary>
		private void ThreadMain()
		{
			List<WaitHandle> handleList = new List<WaitHandle>();
			handleList.Add(mHostEvent);
			handleList.Add(mTargetEvent);
			handleList.AddRange(mConnection.Handles);
			WaitHandle [] handleArray = handleList.ToArray();
			handleList = null;

			while (!m_shutdown)
			{
				mConnection.Connect(/*this*/);

				try
				{
					// Wait for a signal
					int signalledIndex = WaitHandle.WaitAny(handleArray);
					WaitHandle signalledHandle = handleArray[signalledIndex];

					lock (m_lock)
					{
						if (signalledHandle == mHostEvent)
						{
							// Process any messages that have been sent from the debugger
							ProcessEvents();
						}
						else if(signalledHandle == mTargetEvent)
						{
							// Process any messages that have been sent from the target
							ProcessMessages();
						}
						else
						{
							mConnection.OnSignalled(signalledHandle);
						}
					}
				}
				catch (TargetException ex)
				{
					Abort(ex.ToString());
				}
			}
			
			mConnection.Shutdown();

			OnStateUpdate(TargetState.Disconnected);
		}

		void Connection_ConnectionClosed(IConnection sender)
		{
			Abort();
		}

		void Connection_ConnectionAborted(IConnection sender, string message)
		{
			Abort(message);
		}

		void Connection_DataReceived(IConnection sender, byte[] buffer, int bytes)
		{
			lock (m_lock)
			{
				Array.Copy(buffer, 0, m_readBuffer.Buffer, m_readBuffer.DataLength, bytes);
				m_readBuffer.DataLength += bytes;
				mTargetEvent.Set();
			}
		}

		internal void ProcessMessages()
		{
			if (m_readBuffer.DataLength > 0)
			{
				// Try and process as many complete messages as possible
				m_readBuffer.BeginRead();
				while (m_readBuffer.DataAvailable > 0 && !m_shutdown)
				{
					int msgstart = m_readBuffer.Position;
					int msglen = m_readBuffer.PeekInt32();
					if (m_readBuffer.DataAvailable < msglen)
						break;

					m_readBuffer.BeginMessage();
					TargetMessage cmd = (TargetMessage)m_readBuffer.ReadInt32();
					switch (cmd)
					{
						case TargetMessage.Connect: ProcessMessage_Connect(); break;
						case TargetMessage.Disconnect: ProcessMessage_Disconnect(); break;
						case TargetMessage.ErrorMessage: ProcessMessage_ErrorMessage(); break;
						case TargetMessage.StateUpdate: ProcessMessage_StateUpdate(); break;
						case TargetMessage.BreakpointAdded: ProcessMessage_BreakpointAdded(); break;
						case TargetMessage.BreakpointRemoved: ProcessMessage_BreakpointRemoved(); break;
						case TargetMessage.CallstackUpdate: ProcessMessage_CallstackUpdate(); break;
						case TargetMessage.ValueCached: ProcessMessage_ValueCached(); break;
						case TargetMessage.LocalsUpdate: ProcessMessage_LocalsUpdate(); break;
						case TargetMessage.WatchUpdate: ProcessMessage_WatchUpdate(); break;
						case TargetMessage.ThreadsUpdate: ProcessMessage_ThreadsUpdate(); break;
						case TargetMessage.UploadFile: ProcessMessage_UploadFile(); break;
						case TargetMessage.SnippetResult: ProcessMessage_SnippetResult(); break;
						case TargetMessage.AutocompleteOptions: ProcessMessage_AutocompleteOptions(); break;
						case TargetMessage.ExMessage: ProcessMessage_ExMessage(); break;
					}
					m_readBuffer.EndMessage();
				}

				m_readBuffer.EndRead();
			}
		}

		void ProcessMessage_Connect()
		{
			int version = m_readBuffer.ReadInt32();

			if (version != Protocol.Version)
			{
				m_readBuffer.Skip(m_readBuffer.MessageAvailable);
				Abort(String.Format("Tilde cannot connect to the target because the protocol versions are different.\r\n\r\nTilde protocol version {0};\r\nTarget protocol version {1}.", Protocol.Version, version));
				return;
			}

			// Get the object sizes from the target
			int sizeofObjectID = m_readBuffer.ReadInt32();
			int sizeofNumber = m_readBuffer.ReadInt32();

			m_readBuffer.SizeofObjectID = sizeofObjectID;
			m_readBuffer.SizeofNumber = sizeofNumber;

			m_writeBuffer.SizeofObjectID = sizeofObjectID;
			m_writeBuffer.SizeofNumber = sizeofNumber;

			// Let the debug manager do its thing (send breakpoints etc)
			OnStateUpdate(TargetState.Connected);

			// Now set the target running
			if(mDebugManager.Options.BreakOnConnection)
				CreateMessage_Run(ExecutionMode.Break);
			else
				CreateMessage_Run(ExecutionMode.Go);
		}

		void ProcessMessage_Disconnect()
		{
			string message = m_readBuffer.ReadString();

			Abort(String.Format("The target has disconnected because:\r\n\r\n{0}", message));
		}

		void ProcessMessage_StateUpdate()
		{
			TargetState state = (TargetState)m_readBuffer.ReadInt32();
			Int64 thread = m_readBuffer.ReadObjectID();
			int line = m_readBuffer.ReadInt32();
			string filename = m_readBuffer.ReadString();

			OnStateUpdate(state, new LuaValue(thread, LuaValueType.THREAD), mDebugManager.FindSourceFile(filename), line);
		}

		private void ProcessMessage_ErrorMessage()
		{
			string message = m_readBuffer.ReadString();

			OnErrorMessage("Lua Error", message);
		}

		void ProcessMessage_BreakpointAdded()
		{
			int bkptid = m_readBuffer.ReadInt32();
			int success = m_readBuffer.ReadInt32();

			OnBreakpointUpdate(bkptid, success != 0 ? BreakpointUpdateEventState.Added : BreakpointUpdateEventState.Invalid);
		}

		void ProcessMessage_BreakpointRemoved()
		{
			int bkptid = m_readBuffer.ReadInt32();

			OnBreakpointUpdate(bkptid, BreakpointUpdateEventState.Removed);
		}

		void ProcessMessage_CallstackUpdate()
		{
			Int64 currentThread = m_readBuffer.ReadObjectID();
			int currentFrame = m_readBuffer.ReadInt32();
			int stackCount = m_readBuffer.ReadInt32();
			LuaStackFrame[] stackFrames = new LuaStackFrame[stackCount];
			for (int index = 0; index < stackCount; ++index)
			{
				string function = m_readBuffer.ReadString();
				string filename = m_readBuffer.ReadString();
				int line = m_readBuffer.ReadInt32();

				stackFrames[index] = new LuaStackFrame(index, function, mDebugManager.FindSourceFile(filename), line);
			}

			OnCallstackUpdate(new CallstackUpdateEventArgs(new LuaValue(currentThread, LuaValueType.THREAD), stackFrames, currentFrame));
		}

		void ProcessMessage_ValueCached()
		{
			LuaValue value = m_readBuffer.ReadValue();
			string desc = m_readBuffer.ReadString();

			OnValueCached(value, desc);
		}

		void ProcessMessage_LocalsUpdate()
		{
			Int64 currentThread = m_readBuffer.ReadObjectID();
			int stackFrame = m_readBuffer.ReadInt32();
			bool cacheFlush = m_readBuffer.ReadInt32() != 0;
			int count = m_readBuffer.ReadInt32();

			List<VariableDetails> vars = new List<VariableDetails>();
			List<LuaValue> path = new List<LuaValue>();
			ReadVariables(vars, path, 0, count, 1);

			LuaValue threadValue = new LuaValue(currentThread, LuaValueType.THREAD);
			OnVariableUpdate(new VariableUpdateEventArgs(VariableUpdateType.Locals, threadValue, vars.ToArray(), cacheFlush));
		}

		void ProcessMessage_WatchUpdate()
		{
			Int64 currentThread = m_readBuffer.ReadObjectID();
			int count = m_readBuffer.ReadInt32();

			List<VariableDetails> vars = new List<VariableDetails>();

			for (int index = 0; index < count; ++index)
			{
				int watchid = m_readBuffer.ReadInt32();

				List<LuaValue> path = new List<LuaValue>();
				ReadVariables(vars, path, watchid, 1, 1);
			}

			LuaValue threadValue = new LuaValue(currentThread, LuaValueType.THREAD);
			OnVariableUpdate(new VariableUpdateEventArgs(VariableUpdateType.Watches, threadValue, vars.ToArray(), false));
		}

		void ProcessMessage_ThreadsUpdate()
		{
			int count = m_readBuffer.ReadInt32();

			List<ThreadDetails> threads = new List<ThreadDetails>();

			for (int index = 0; index < count; ++index)
			{
				LuaValue thread = new LuaValue(m_readBuffer.ReadObjectID(), LuaValueType.THREAD);
				LuaValue parent = new LuaValue(m_readBuffer.ReadObjectID(), LuaValueType.THREAD);
				string name = m_readBuffer.ReadString();
				int threadid = m_readBuffer.ReadInt32();
				string location = m_readBuffer.ReadString();
				LuaThreadState state = (LuaThreadState)m_readBuffer.ReadInt32();
				int stackSize = m_readBuffer.ReadInt16();
				int stackUsed = m_readBuffer.ReadInt16();
				double peak = m_readBuffer.ReadNumber();
				double average = m_readBuffer.ReadNumber();
				bool modified = m_readBuffer.ReadInt16() != 0;
				bool valid = m_readBuffer.ReadInt16() != 0;

				ThreadDetails details = new ThreadDetails(thread, parent, name, threadid, location, state, stackSize, stackUsed, peak, average, valid);
				threads.Add(details);
			}

			OnThreadUpdate(new ThreadUpdateEventArgs(threads.ToArray()));
		}

		void ProcessMessage_UploadFile()
		{
			string fileName = m_readBuffer.ReadString();
			int size = m_readBuffer.ReadInt32();
			byte[] data = m_readBuffer.ReadBytes(size);

			string contents = new String(Encoding.ASCII.GetChars(data));

			OnFileUpload(fileName, data);
		}

		void ProcessMessage_SnippetResult()
		{
			bool success = m_readBuffer.ReadInt32() != 0;
			string output = m_readBuffer.ReadString();
			string result = m_readBuffer.ReadString();

			OnSnippetResult(success, output, result);
		}

		void ProcessMessage_AutocompleteOptions()
		{
			AutocompleteResult[] options = null;

			int seqid = m_readBuffer.ReadInt32();
			int count = m_readBuffer.ReadInt32();
			if (count >= 0)
			{
				options = new AutocompleteResult[count];
				for (int index = 0; index < count; ++index)
				{
					LuaValue key = m_readBuffer.ReadValue();
					LuaValueType valueType = (LuaValueType)m_readBuffer.ReadInt32();
					options[index] = new AutocompleteResult(key, valueType);
				}
			}
			string message = m_readBuffer.ReadString();
			OnAutocompleteOptions(seqid, options, message);
		}

		void ProcessMessage_ExMessage()
		{
			string command = m_readBuffer.ReadString();
			int datasize = m_readBuffer.ReadInt32();
			byte[] data = null;
			if (datasize > 0)
				data = m_readBuffer.ReadBytes(datasize);

			OnExMessage(command, data);
		}

		void ReadVariables(List<VariableDetails> vars, List<LuaValue> path, int watchid, int count, int depth)
		{
			for (int index = 0; index < count; ++index)
			{
				LuaValue key = m_readBuffer.ReadValue();
				LuaValue value = m_readBuffer.ReadValue();
				VariableInfoFlag flags = (VariableInfoFlag)m_readBuffer.ReadInt16();
				bool valid = (flags & VariableInfoFlag.Valid) != 0;
				bool expanded = (flags & VariableInfoFlag.Expanded) != 0;
				bool hasEntries = (flags & VariableInfoFlag.HasEntries) != 0;
				bool hasMetadata = (flags & VariableInfoFlag.HasMetadata) != 0;
				VariableClass varclass = (VariableClass)m_readBuffer.ReadInt16();

				VariableDetails var = new VariableDetails(watchid, path.ToArray(), key, value, expanded, hasEntries, hasMetadata, valid, varclass);
				vars.Add(var);

				int expansionCount = m_readBuffer.ReadInt32();
				if (expansionCount > 0)
				{
					path.Add(key);
					ReadVariables(vars, path, watchid, expansionCount, depth + 1);
					path.RemoveAt(path.Count - 1);
				}
			}
		}

		void OnDebugPrint(String message)
		{
			DebugPrintEventArgs args = new DebugPrintEventArgs(message);
			if (mForm != null && mForm.IsHandleCreated)
				mForm.BeginInvoke(new MethodInvoker(delegate()
				{
					if (this.DebugPrint != null)
						this.DebugPrint(this, args);
				}));
		}

		// This message is synchronous, blocking the communications thread until the user has confirmed it.
		private void OnErrorMessage(String title, String message)
		{
			ErrorMessageEventArgs args = new ErrorMessageEventArgs(title, message);
			if (mForm != null && mForm.IsHandleCreated)
				mForm.Invoke(new MethodInvoker(delegate()
				{
					if (this.ErrorMessage != null)
						this.ErrorMessage(this, args);
				}));
		}

		private void OnStatusMessage(String message)
		{
			StatusMessageEventArgs args = new StatusMessageEventArgs(message);
			if (mForm != null && mForm.IsHandleCreated)
				mForm.BeginInvoke(new MethodInvoker(delegate()
				{
					if (this.StatusMessage != null)
						this.StatusMessage(this, args);
				}));
		}

		private void OnStateUpdate(TargetState targetState)
		{
			StateUpdateEventArgs args = new StateUpdateEventArgs(targetState);
			if (mLastUpdateMessage == null || !mLastUpdateMessage.Equals(args))
			{
				mLastUpdateMessage = args;

				if (mForm != null && mForm.IsHandleCreated)
					mForm.BeginInvoke(new MethodInvoker(delegate()
					{
						if (this.StateUpdate != null)
							this.StateUpdate(this, args);
					}));
			}
		}

		private void OnStateUpdate(TargetState targetState, LuaValue thread, String file, int line)
		{
			StateUpdateEventArgs args = new StateUpdateEventArgs(targetState, thread, file, line);
			if (mLastUpdateMessage == null || !mLastUpdateMessage.Equals(args))
			{
				mLastUpdateMessage = args;

				if (mForm != null && mForm.IsHandleCreated)
					mForm.BeginInvoke(new MethodInvoker(delegate()
					{
						if (this.StateUpdate != null)
							this.StateUpdate(this, args);
					}));
			}
		}

		void OnCallstackUpdate(CallstackUpdateEventArgs args)
		{
			if (mForm != null && mForm.IsHandleCreated)
				mForm.BeginInvoke(new MethodInvoker(delegate()
				{
					if (this.CallstackUpdate != null)
						this.CallstackUpdate(this, args);
				}));
		}

		void OnThreadUpdate(ThreadUpdateEventArgs args)
		{
			if (mForm != null && mForm.IsHandleCreated)
				mForm.BeginInvoke(new MethodInvoker(delegate()
				{
					if (this.ThreadUpdate != null)
						this.ThreadUpdate(this, args);
				}));
		}

		void OnVariableUpdate(VariableUpdateEventArgs args)
		{
			if (mForm != null && mForm.IsHandleCreated)
				mForm.BeginInvoke(new MethodInvoker(delegate()
				{
					if (this.VariableUpdate != null)
						this.VariableUpdate(this, args);
				}));
		}

		void OnBreakpointUpdate(int bkptid, BreakpointUpdateEventState state)
		{
			BreakpointUpdateEventArgs args = new BreakpointUpdateEventArgs(bkptid, state);
			if (mForm != null && mForm.IsHandleCreated)
				mForm.BeginInvoke(new MethodInvoker(delegate()
				{
					if (this.BreakpointUpdate != null)
						this.BreakpointUpdate(this, args);
				}));
		}

		void OnValueCached(LuaValue value, string desc)
		{
			ValueCachedEventArgs args = new ValueCachedEventArgs(value, desc);
			if (mForm != null && mForm.IsHandleCreated)
				mForm.BeginInvoke(new MethodInvoker(delegate()
				{
					if (this.ValueCached != null)
						this.ValueCached(this, args);
				}));
		}

		void OnFileUpload(string fileName, byte [] data)
		{
			FileUploadEventArgs args = new FileUploadEventArgs(fileName, data);
			if (mForm != null && mForm.IsHandleCreated)
				mForm.BeginInvoke(new MethodInvoker(delegate()
				{
					if (this.FileUpload != null)
						this.FileUpload(this, args);
				}));
		}

		void OnSnippetResult(bool success, string output, string result)
		{
			SnippetResultEventArgs args = new SnippetResultEventArgs(success, output, result);
			if (mForm != null && mForm.IsHandleCreated)
				mForm.BeginInvoke(new MethodInvoker(delegate()
				{
					if (this.SnippetResult != null)
						this.SnippetResult(this, args);
				}));
		}

		void OnAutocompleteOptions(int seqid, AutocompleteResult [] options, string message)
		{
			AutocompleteOptionsEventArgs args = new AutocompleteOptionsEventArgs(seqid, options, message);
			if (mForm != null && mForm.IsHandleCreated)
				mForm.BeginInvoke(new MethodInvoker(delegate()
				{
					if (this.AutocompleteOptions != null)
						this.AutocompleteOptions(this, args);
				}));
		}

		void OnExMessage(string command, byte [] data)
		{
			ExMessageEventArgs args = new ExMessageEventArgs(command, data);
			if (mForm != null && mForm.IsHandleCreated)
				mForm.BeginInvoke(new MethodInvoker(delegate()
				{
					if (this.ExMessage != null)
						this.ExMessage(this, args);
				}));
		}

		#region Target Members

		public HostInfo HostInfo
		{
			get { return mConnection.HostInfo; }
		}

		public event DebugPrintEventHandler DebugPrint;
		public event ErrorMessageEventHandler ErrorMessage;
		public event StatusMessageEventHandler StatusMessage;
		public event StateUpdateEventHandler StateUpdate;
		public event CallstackUpdateEventHandler CallstackUpdate;
		public event ThreadUpdateEventHandler ThreadUpdate;
		public event VariableUpdateEventHandler VariableUpdate;
		public event BreakpointUpdateEventHandler BreakpointUpdate;
		public event ValueCachedEventHandler ValueCached;
		public event FileUploadEventHandler FileUpload;
		public event SnippetResultEventHandler SnippetResult;
		public event AutocompleteOptionsEventHandler AutocompleteOptions;
		public event ExMessageEventHandler ExMessage;


		private object BlockingInvoke(ReturnDelegate del)
		{
			// Fire off the asynchronous operation
			MessageQueue.BlockingMessage message = new MessageQueue.BlockingMessage(del);
			mMessageQueue.Push(message);
			mHostEvent.Set();

			// Wait for it to finish
			message.AsyncWaitHandle.WaitOne();

			// Return the result, or re-throw the exception
			if (message.Exception != null)
				throw (message.Exception);
			else
				return message.Result;
		}

		private void AsyncInvoke(VoidDelegate del)
		{
			MessageQueue.Message message = new MessageQueue.Message(del);
			mMessageQueue.Push(message);
			mHostEvent.Set();
		}

		public void Disconnect()
		{
			AsyncInvoke(delegate() 
				{ 
					m_shutdown = true; 
				}
			);
		}

		public void Run(ExecutionMode mode)
		{
			AsyncInvoke(delegate()
				{
					CreateMessage_Run(mode);
					SendMessages();
				}
			);
		}

		public void AddBreakpoint(string fileName, int lineNumber, int bkptid)
		{
			AsyncInvoke(delegate()
				{
					CreateMessage_AddBreakpoint(fileName, lineNumber, bkptid);
					SendMessages();
				}
			);
		}

		public void RemoveBreakpoint(int bkptid)
		{
			AsyncInvoke(delegate()
				{
					CreateMessage_RemoveBreakpoint(bkptid);
					SendMessages();
				}
			);
		}

		public void ClearBreakpoints()
		{
			AsyncInvoke(delegate()
				{
					throw new Exception("The method or operation is not implemented.");
				}
			);
		}

		public void IgnoreError(string fileName, int lineNumber)
		{
			AsyncInvoke(delegate()
				{
					CreateMessage_IgnoreError(fileName, lineNumber);
					SendMessages();
				}
			);
		}

		public void RetrieveStatus(LuaValue thread, int stackFrame)
		{
			AsyncInvoke(delegate()
				{
					CreateMessage_RetrieveStatus(thread, stackFrame);
					SendMessages();
				}
			);
		}

		public void RetrieveThreads()
		{
			AsyncInvoke(delegate()
				{
					CreateMessage_RetrieveThreads();
					SendMessages();
				}
			);
		}

		public void RetrieveLocals(LuaValue thread, int stackFrame)
		{
			AsyncInvoke(delegate()
				{
					System.Diagnostics.Debug.Assert(!thread.IsNil());
					CreateMessage_RetrieveLocals(thread, stackFrame);
					SendMessages();
				}
			);
		}

		public void ExpandLocal(LuaValue[] path)
		{
			AsyncInvoke(delegate()
				{
					CreateMessage_ExpandLocal(path);
					SendMessages();
				}
			);
		}

		public void CloseLocal(LuaValue[] path)
		{
			AsyncInvoke(delegate()
				{
					CreateMessage_CloseLocal(path);
					SendMessages();
				}
			);
		}

		public void RetrieveWatches(LuaValue thread, int stackFrame)
		{
			AsyncInvoke(delegate()
				{
					System.Diagnostics.Debug.Assert(!thread.IsNil());
					CreateMessage_RetrieveWatches(thread, stackFrame);
					SendMessages();
				}
			);
		}

		public void UpdateWatch(int watchid, LuaValue thread, int stackFrame)
		{
			AsyncInvoke(delegate()
				{
					System.Diagnostics.Debug.Assert(!thread.IsNil());
					CreateMessage_UpdateWatch(watchid, thread, stackFrame);
					SendMessages();
				}
			);
		}

		public void AddWatch(string expr, int watchid)
		{
			AsyncInvoke(delegate()
				{
					CreateMessage_AddWatch(expr, watchid);
					SendMessages();
				}
			);
		}

		public void RemoveWatch(int watchid)
		{
			AsyncInvoke(delegate()
				{
					CreateMessage_RemoveWatch(watchid);
					SendMessages();
				}
			);
		}

		public void ExpandWatch(int watchid, LuaValue[] path)
		{
			AsyncInvoke(delegate()
				{
					CreateMessage_ExpandWatch(watchid, path);
					SendMessages();
				}
			);
		}

		public void CloseWatch(int watchid, LuaValue[] path)
		{
			AsyncInvoke(delegate()
				{
					CreateMessage_CloseWatch(watchid, path);
					SendMessages();
				}
			);
		}

		public void ClearWatches()
		{
			AsyncInvoke(delegate()
				{
					throw new Exception("The method or operation is not implemented.");
				}
			);
		}

		public void RunSnippet(string snippet, LuaValue thread, int stackFrame)
		{
			AsyncInvoke(delegate()
				{
					CreateMessage_RunSnippet(snippet, thread, stackFrame);
					SendMessages();
				}
			);
		}

		public void RetrieveAutocompleteOptions(int seqid, string expression, LuaValue thread, int stackFrame)
		{
			AsyncInvoke(delegate()
				{
					CreateMessage_RetrieveAutocompleteOptions(seqid, expression, thread, stackFrame);
					SendMessages();
				}
			);
		}

		public bool DownloadFile(string fileName)
		{
			return (bool) BlockingInvoke(delegate()
				{
					return mConnection.DownloadFile(fileName);
				}
			);
		}

		public void ExCommand(string command, byte[] data)
		{
			AsyncInvoke(delegate()
				{
					CreateMessage_ExCommand(command, data);
					SendMessages();
				}
			);
		}


		#endregion

		void SendMessages()
		{
			try
			{
				if (!m_shutdown)
					mConnection.Send(m_writeBuffer.Data, 0, m_writeBuffer.Length);
			}
			catch (System.Exception ex)
			{
				Abort(ex.ToString());
			}
			m_writeBuffer.Length = 0;
		}

		void CreateMessage_Run(ExecutionMode mode)
		{
			m_writeBuffer.Begin();
			m_writeBuffer.Write((int)DebuggerMessage.Run);
			m_writeBuffer.Write((int)mode);
			m_writeBuffer.End();
		}

		void CreateMessage_AddBreakpoint(string file, int line, int bkptid)
		{
			m_writeBuffer.Begin();
			m_writeBuffer.Write((int)DebuggerMessage.AddBreakpoint);
			m_writeBuffer.Write((int)bkptid);
			m_writeBuffer.Write(mDebugManager.FindTargetFile(file));
			m_writeBuffer.Write((int)line);
			m_writeBuffer.End();
		}

		void CreateMessage_RemoveBreakpoint(int bkptid)
		{
			m_writeBuffer.Begin();
			m_writeBuffer.Write((int)DebuggerMessage.RemoveBreakpoint);
			m_writeBuffer.Write((int)bkptid);
			m_writeBuffer.End();
		}

		void CreateMessage_IgnoreError(string file, int line)
		{
			m_writeBuffer.Begin();
			m_writeBuffer.Write((int)DebuggerMessage.IgnoreError);
			m_writeBuffer.Write(mDebugManager.FindTargetFile(file));
			m_writeBuffer.Write((int)line);
			m_writeBuffer.End();
		}

		private void CreateMessage_RetrieveStatus(LuaValue thread, int stackFrame)
		{
			m_writeBuffer.Begin();
			m_writeBuffer.Write((int)DebuggerMessage.RetrieveStatus);
			m_writeBuffer.WriteObjectID(thread.AsIdentifier());
			m_writeBuffer.Write((int)stackFrame);
			m_writeBuffer.End();
		}

		private void CreateMessage_RetrieveThreads()
		{
			m_writeBuffer.Begin();
			m_writeBuffer.Write((int)DebuggerMessage.RetrieveThreads);
			m_writeBuffer.End();
		}

		void CreateMessage_RetrieveLocals(LuaValue thread, int stackFrame)
		{
			m_writeBuffer.Begin();
			m_writeBuffer.Write((int)DebuggerMessage.RetrieveLocals);
			m_writeBuffer.WriteObjectID(thread.AsIdentifier());
			m_writeBuffer.Write((int)stackFrame);
			m_writeBuffer.End();
		}

		private void CreateMessage_ExpandLocal(LuaValue[] path)
		{
			m_writeBuffer.Begin();
			m_writeBuffer.Write((int)DebuggerMessage.ExpandLocal);
			m_writeBuffer.Write((int)path.Length);
			foreach (LuaValue value in path)
			{
				m_writeBuffer.WriteValue(value);
			}
			m_writeBuffer.End();
		}

		private void CreateMessage_CloseLocal(LuaValue[] path)
		{
			m_writeBuffer.Begin();
			m_writeBuffer.Write((int)DebuggerMessage.CloseLocal);
			m_writeBuffer.Write((int)path.Length);
			foreach (LuaValue value in path)
			{
				m_writeBuffer.WriteValue(value);
			}
			m_writeBuffer.End();
		}

		private void CreateMessage_RetrieveWatches(LuaValue thread, int stackFrame)
		{
			m_writeBuffer.Begin();
			m_writeBuffer.Write((int)DebuggerMessage.RetrieveWatches);
			m_writeBuffer.WriteObjectID(thread.AsIdentifier());
			m_writeBuffer.Write((int)stackFrame);
			m_writeBuffer.End();
		}

		private void CreateMessage_UpdateWatch(int watchid, LuaValue thread, int stackFrame)
		{
			m_writeBuffer.Begin();
			m_writeBuffer.Write((int)DebuggerMessage.UpdateWatch);
			m_writeBuffer.Write((int)watchid);
			m_writeBuffer.WriteObjectID(thread.AsIdentifier());
			m_writeBuffer.Write((int)stackFrame);
			m_writeBuffer.End();
		}

		private void CreateMessage_AddWatch(string expr, int watchid)
		{
			m_writeBuffer.Begin();
			m_writeBuffer.Write((int)DebuggerMessage.AddWatch);
			m_writeBuffer.Write(expr);
			m_writeBuffer.Write(watchid);
			m_writeBuffer.End();
		}

		private void CreateMessage_RemoveWatch(int watchid)
		{
			m_writeBuffer.Begin();
			m_writeBuffer.Write((int)DebuggerMessage.RemoveWatch);
			m_writeBuffer.Write(watchid);
			m_writeBuffer.End();
		}

		private void CreateMessage_ExpandWatch(int watchid, LuaValue[] path)
		{
			m_writeBuffer.Begin();
			m_writeBuffer.Write((int)DebuggerMessage.ExpandWatch);
			m_writeBuffer.Write((int)watchid);
			m_writeBuffer.Write((int)path.Length - 1);
			for (int index = 1; index < path.Length; ++index)
			{
				m_writeBuffer.WriteValue(path[index]);
			}
			m_writeBuffer.End();
		}

		private void CreateMessage_CloseWatch(int watchid, LuaValue[] path)
		{
			m_writeBuffer.Begin();
			m_writeBuffer.Write((int)DebuggerMessage.CloseWatch);
			m_writeBuffer.Write((int)watchid);
			m_writeBuffer.Write((int)path.Length - 1);
			for (int index = 1; index < path.Length; ++index)
			{
				m_writeBuffer.WriteValue(path[index]);
			}
			m_writeBuffer.End();
		}

		private void CreateMessage_RunSnippet(string snippet, LuaValue thread, int stackFrame)
		{
			m_writeBuffer.Begin();
			m_writeBuffer.Write((int)DebuggerMessage.RunSnippet);
			m_writeBuffer.WriteObjectID(thread.AsIdentifier());
			m_writeBuffer.Write((int)stackFrame);
			m_writeBuffer.Write(snippet);
			m_writeBuffer.End();
		}

		private void CreateMessage_RetrieveAutocompleteOptions(int seqid, string expression, LuaValue thread, int stackFrame)
		{
			m_writeBuffer.Begin();
			m_writeBuffer.Write((int)DebuggerMessage.RetrieveAutocompleteOptions);
			m_writeBuffer.Write((int)seqid);
			m_writeBuffer.WriteObjectID(thread.AsIdentifier());
			m_writeBuffer.Write((int)stackFrame);
			m_writeBuffer.Write(expression);
			m_writeBuffer.End();
		}

		private void CreateMessage_ExCommand(string command, byte[] data)
		{
			m_writeBuffer.Begin();
			m_writeBuffer.Write((int)DebuggerMessage.ExCommand);
			m_writeBuffer.Write(command);
			if (data != null)
				m_writeBuffer.Write(data);
			m_writeBuffer.End();
		}

	}
}
