
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
	public delegate void VariableUpdateEventHandler(Target sender, VariableUpdateEventArgs args);

	public enum VariableUpdateType
	{
		Locals,
		Upvalues,
		Watches
	}

	public enum VariableClass
	{
		Invalid,
		Local,
		Upvalue,
		Watch,
		TableEntry
	}

	public class VariableUpdateEventArgs
	{
		public VariableUpdateEventArgs(VariableUpdateType type, LuaValue thread, VariableDetails[] vars, bool cacheFlush)
		{
			mType = type;
			mThread = thread;
			mVariables = vars;
			mCacheFlush = cacheFlush;
		}

		public VariableUpdateType Type
		{
			get { return mType; }
		}

		public LuaValue Thread
		{
			get { return mThread; }
		}

		public VariableDetails [] Variables
		{
			get { return mVariables; }
		}

		public bool CacheFlush
		{
			get { return mCacheFlush; }
		}

		private VariableUpdateType mType;
		private LuaValue mThread;
		private VariableDetails [] mVariables;
		private bool mCacheFlush;
	}
}
