
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
	public delegate void CallstackUpdateEventHandler(Target sender, CallstackUpdateEventArgs args);

	public class LuaStackFrame
	{
		public LuaStackFrame(int depth, string function, string filename, int line)
		{
			mDepth = depth;
			mFunction = function;
			mFile = filename;
			mLine = line;
		}

		public int Depth
		{
			get { return mDepth; }
		}

		public string Function
		{
			get { return mFunction; }
		}

		public string File
		{
			get { return mFile; }
		}

		public int Line
		{
			get { return mLine; }
		}

		private int mDepth;
		private string mFunction;
		private string mFile;
		private int mLine;
	}

	public class CallstackUpdateEventArgs
	{
		public CallstackUpdateEventArgs(LuaValue thread, LuaStackFrame [] stackFrames, int currentFrame)
		{
			mThread = thread;
			mStackFrames = stackFrames;
			mCurrentFrame = currentFrame;
		}

		public LuaValue Thread
		{
			get { return mThread; }
		}

		public LuaStackFrame [] StackFrames
		{
			get { return mStackFrames; }
		}

		public int CurrentFrame
		{
			get { return mCurrentFrame; }
		}

		private LuaValue mThread;
		private LuaStackFrame[] mStackFrames;
		private int mCurrentFrame;

	}
}
