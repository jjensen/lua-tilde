
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
	public class ThreadedHost : ITarget
	{
		//private Thread mThread;
		private AutoResetEvent mEvent;
		private MessageQueue mMessageQueue;
		private ITarget mRealHost;
		private Thread mThread;
		private Form mForm;

		public ThreadedHost(Form form)
		{
			mMessageQueue = new MessageQueue();
			mEvent = new AutoResetEvent(false);
			mForm = form;
		}

		public ITarget Client
		{
			get { return mRealHost; }

			set 
			{
				mRealHost = value;

				mRealHost.DebugPrint += new DebugPrintEventHandler(realHost_DebugPrint);
				mRealHost.ErrorMessage += new ErrorMessageEventHandler(realHost_ErrorMessage);
				mRealHost.StatusMessage += new StatusMessageEventHandler(realHost_StatusMessage);
				mRealHost.StateUpdate += new StateUpdateEventHandler(realHost_StateUpdate);
				mRealHost.CallstackUpdate += new CallstackUpdateEventHandler(realHost_CallstackUpdate);
				mRealHost.ThreadUpdate += new ThreadUpdateEventHandler(realHost_ThreadUpdate);
				mRealHost.VariableUpdate += new VariableUpdateEventHandler(realHost_VariableUpdate);
				mRealHost.BreakpointUpdate += new BreakpointUpdateEventHandler(realHost_BreakpointUpdate);
				mRealHost.ValueCached += new ValueCachedEventHandler(realHost_ValueCached);
				mRealHost.FileUpload += new FileUploadEventHandler(realHost_FileUpload);
				mRealHost.SnippetResult += new SnippetResultEventHandler(realHost_SnippetResult);
				mRealHost.AutocompleteOptions += new AutocompleteOptionsEventHandler(realHost_AutocompleteOptions);
			}
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
			get { return mEvent; }
		}

		public Thread Thread
		{
			get { return mThread; }
			set { mThread = value; }
		}

		public void ProcessEvents()
		{
			while(!mMessageQueue.Empty)
			{
				// Retrieve a message from the queue
				KeyValuePair<Delegate, object []> message = mMessageQueue.Pop();

				// Execute it
				Delegate method = message.Key;
				object [] args = message.Value;
				try
				{
					method.DynamicInvoke();
				}
				catch (TargetInvocationException ex)
				{
					// Re-throw any exceptions
					throw ex.InnerException;
				}
			}
		}

		private void Invoke(Delegate del)
		{
			mMessageQueue.Push(del, null);
			mEvent.Set();
		}

		private void realHost_DebugPrint(ITarget sender, DebugPrintEventArgs args)
		{
			if (mForm != null && mForm.IsHandleCreated)
				mForm.BeginInvoke(new MethodInvoker(delegate()
				{
					if (this.DebugPrint != null)
						this.DebugPrint(this, args);
				}));
		}

		private void realHost_ErrorMessage(ITarget sender, ErrorMessageEventArgs args)
		{
			if (mForm != null && mForm.IsHandleCreated)
				mForm.BeginInvoke(new MethodInvoker(delegate()
				{
					if (this.ErrorMessage != null)
						this.ErrorMessage(this, args);
				}));
		}

		private void realHost_StatusMessage(ITarget sender, StatusMessageEventArgs args)
		{
			if (mForm != null && mForm.IsHandleCreated)
				mForm.BeginInvoke(new MethodInvoker(delegate()
				{
					if (this.StatusMessage != null)
						this.StatusMessage(this, args);
				}));
		}

		private void realHost_StateUpdate(ITarget sender, StateUpdateEventArgs args)
		{
			if (mForm != null && mForm.IsHandleCreated)
				mForm.BeginInvoke(new MethodInvoker(delegate()
				{
					if (this.StateUpdate != null)
						this.StateUpdate(this, args);
				}));
		}

		void realHost_CallstackUpdate(ITarget sender, CallstackUpdateEventArgs args)
		{
			if (mForm != null && mForm.IsHandleCreated)
				mForm.BeginInvoke(new MethodInvoker(delegate()
				{
					if (this.CallstackUpdate != null)
						this.CallstackUpdate(this, args);
				}));
		}

		void realHost_ThreadUpdate(ITarget sender, ThreadUpdateEventArgs args)
		{
			if (mForm != null && mForm.IsHandleCreated)
				mForm.BeginInvoke(new MethodInvoker(delegate()
				{
					if (this.ThreadUpdate != null)
						this.ThreadUpdate(this, args);
				}));
		}

		void realHost_VariableUpdate(ITarget sender, VariableUpdateEventArgs args)
		{
			if (mForm != null && mForm.IsHandleCreated)
				mForm.BeginInvoke(new MethodInvoker(delegate()
				{
					if (this.VariableUpdate != null)
						this.VariableUpdate(this, args);
				}));
		}

		void realHost_BreakpointUpdate(ITarget sender, BreakpointUpdateEventArgs args)
		{
			if (mForm != null && mForm.IsHandleCreated)
				mForm.BeginInvoke(new MethodInvoker(delegate()
				{
					if (this.BreakpointUpdate != null)
						this.BreakpointUpdate(this, args);
				}));
		}

		void realHost_ValueCached(ITarget host, ValueCachedEventArgs args)
		{
			if (mForm != null && mForm.IsHandleCreated)
				mForm.BeginInvoke(new MethodInvoker(delegate()
				{
					if (this.ValueCached != null)
						this.ValueCached(this, args);
				}));
		}

		void realHost_FileUpload(ITarget host, FileUploadEventArgs args)
		{
			if (mForm != null && mForm.IsHandleCreated)
				mForm.BeginInvoke(new MethodInvoker(delegate()
				{
					if (this.FileUpload != null)
						this.FileUpload(this, args);
				}));
		}

		void realHost_SnippetResult(ITarget host, SnippetResultEventArgs args)
		{
			if (mForm != null && mForm.IsHandleCreated)
				mForm.BeginInvoke(new MethodInvoker(delegate()
				{
					if (this.SnippetResult != null)
						this.SnippetResult(this, args);
				}));
		}

		void realHost_AutocompleteOptions(ITarget sender, AutocompleteOptionsEventArgs args)
		{
			if (mForm != null && mForm.IsHandleCreated)
				mForm.BeginInvoke(new MethodInvoker(delegate()
				{
					if (this.AutocompleteOptions != null)
						this.AutocompleteOptions(this, args);
				}));
		}
		
		#region IHost Members

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

		public void Reset(string fileName)
		{
			Invoke(new MethodInvoker(delegate() { mRealHost.Reset(fileName); }));
		}

		public void Attach()
		{
			Invoke(new MethodInvoker(delegate() { mRealHost.Attach(); }));
		}

		public void Disconnect()
		{
			Invoke(new MethodInvoker(delegate() { mRealHost.Disconnect(); }));
		}

		public void Run(ExecutionMode mode)
		{
			Invoke(new MethodInvoker(delegate() { mRealHost.Run(mode); }));
		}

		public void DownloadFile(int fileid, string filename)
		{
			Invoke(new MethodInvoker(delegate() { mRealHost.DownloadFile(fileid, filename); }));
		}

		public void ExecuteFile(int fileid)
		{
			Invoke(new MethodInvoker(delegate() { mRealHost.ExecuteFile(fileid); }));
		}

		public void DiscardFile(int fileid)
		{
			Invoke(new MethodInvoker(delegate() { mRealHost.DiscardFile(fileid); }));
		}

		public void StartProfile()
		{
			Invoke(new MethodInvoker(delegate() { mRealHost.StartProfile(); }));
		}

		public void StopProfile()
		{
			Invoke(new MethodInvoker(delegate() { mRealHost.StopProfile(); }));
		}

        public TargetInfo HostInfo
        {
            get { return mRealHost.HostInfo; }
        }

		public void AddBreakpoint(string fileName, int lineNumber, int bkptid)
		{
			Invoke(new MethodInvoker(delegate() { mRealHost.AddBreakpoint(fileName, lineNumber, bkptid); }));
		}

		public void RemoveBreakpoint(int bkptid)
		{
			Invoke(new MethodInvoker(delegate() { mRealHost.RemoveBreakpoint(bkptid); }));
		}

		public void ClearBreakpoints()
		{
			Invoke(new MethodInvoker(delegate() { mRealHost.ClearBreakpoints(); }));
		}

		public void IgnoreError(string fileName, int lineNumber)
		{
			Invoke(new MethodInvoker(delegate() { mRealHost.IgnoreError(fileName, lineNumber); }));
		}

		public void RetrieveStatus(LuaValue thread, int stackFrame)
		{
			Invoke(new MethodInvoker(delegate() { mRealHost.RetrieveStatus(thread, stackFrame); }));
		}

		public void RetrieveThreads()
		{
			Invoke(new MethodInvoker(delegate() { mRealHost.RetrieveThreads(); }));
		}

		public void RetrieveLocals(LuaValue thread, int stackFrame)
		{
			Invoke(new MethodInvoker(delegate() { mRealHost.RetrieveLocals(thread, stackFrame); }));
		}

		public void ExpandLocal(LuaValue[] path)
		{
			Invoke(new MethodInvoker(delegate() { mRealHost.ExpandLocal(path); }));
		}

		public void CloseLocal(LuaValue[] path)
		{
			Invoke(new MethodInvoker(delegate() { mRealHost.CloseLocal(path); }));
		}

		public void RetrieveWatches(LuaValue thread, int stackFrame)
		{
			Invoke(new MethodInvoker(delegate() { mRealHost.RetrieveWatches(thread, stackFrame); }));
		}

		public void AddWatch(string expr, int watchid)
		{
			Invoke(new MethodInvoker(delegate() { mRealHost.AddWatch(expr, watchid); }));
		}

		public void RemoveWatch(int watchid)
		{
			Invoke(new MethodInvoker(delegate() { mRealHost.RemoveWatch(watchid); }));
		}

		public void ExpandWatch(int watchid, LuaValue[] path)
		{
			Invoke(new MethodInvoker(delegate() { mRealHost.ExpandWatch(watchid, path); }));
		}

		public void CloseWatch(int watchid, LuaValue[] path)
		{
			Invoke(new MethodInvoker(delegate() { mRealHost.CloseWatch(watchid, path); }));
		}

		public void ClearWatches()
		{
			Invoke(new MethodInvoker(delegate() { mRealHost.ClearWatches(); }));
		}

		public void RunSnippet(string snippet, LuaValue thread, int stackFrame)
		{
			Invoke(new MethodInvoker(delegate() { mRealHost.RunSnippet(snippet, thread, stackFrame); }));
		}

		public void RetrieveAutocompleteOptions(int seqid, string expression, LuaValue thread, int stackFrame)
		{
			Invoke(new MethodInvoker(delegate() { mRealHost.RetrieveAutocompleteOptions(seqid, expression, thread, stackFrame); }));
		}


		#endregion


	}
}
