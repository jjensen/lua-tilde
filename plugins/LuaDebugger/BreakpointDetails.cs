
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

namespace Tilde.LuaDebugger
{
	public enum BreakpointState
	{
		NotSent,		// Breakpoint has not been sent to target yet
		PendingAdd,		// Breakpoint has been sent to target but we haven't heard back yet
		Accepted,		// Target has accepted the breakpoint
		Invalid,		// Target has rejected the breakpoint
		PendingRemove,	// Debugger has asked to remove the breakpoint, but we haven't heard back yet
		PendingDisable,	// Debugger has asked to remove the breakpoint, in order to disable it
		Removed			// Target and debugger have agreed to remove the breakpoint
	};

	public class BreakpointDetails
	{
		public BreakpointDetails(string fileName, int line, bool enabled)
		{
			mID = mNextID++;
			mFileName = fileName;
			mLine = line;
			mEnabled = enabled;
			mTargetState = BreakpointState.NotSent;
		}

		public int ID
		{
			get { return mID; }
		}

		public string FileName
		{
			get { return mFileName; }
		}

		public int Line
		{
			get { return mLine; }
		}

		public bool Enabled
		{
			get { return mEnabled; }
			set { mEnabled = value; }
		}

		public BreakpointState TargetState
		{
			get { return mTargetState; }
			set { mTargetState = value; }
		}

		private int mID;
		private string mFileName;
		private int mLine;
		private bool mEnabled;
		private BreakpointState mTargetState;

		private static int mNextID = 1;
	}
}
