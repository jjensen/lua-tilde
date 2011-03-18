
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
	public enum WatchState
	{
		NotSent,		// Watch has not been sent to target yet
		PendingAdd,		// Watch has been sent to target but we haven't heard back yet
		Accepted,		// Target has accepted the Watch
		Invalid,		// Target has rejected the Watch
		PendingRemove,	// Debugger has asked to remove the Watch, but we haven't heard back yet
		PendingDisable,	// Debugger has asked to remove the Watch, in order to disable it
		Removed			// Target and debugger have agreed to remove the breakpoint
	};

	public class WatchDetails
	{
		public WatchDetails(string expr, bool enabled)
		{
			mID = mNextID++;
			mExpression = expr;
			mEnabled = enabled;
			mState = WatchState.NotSent;
		}

		public int ID
		{
			get { return mID; }
		}

		public string Expression
		{
			get { return mExpression; }
		}

		public bool Enabled
		{
			get { return mEnabled; }
			set { mEnabled = value; }
		}

		public WatchState State
		{
			get { return mState; }
			set { mState = value; }
		}

		public override string ToString()
		{
			return "[" + mID.ToString() + "]";
		}

		private int mID;
		private string mExpression;
		private bool mEnabled;
		private WatchState mState;

		private static int mNextID = 1;
	}
}
