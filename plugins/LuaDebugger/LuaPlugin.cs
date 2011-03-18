
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

using Tilde.Framework.Controller;

namespace Tilde.LuaDebugger
{
	public class LuaPlugin : IPlugin
	{
		#region IPlugin Members

		public void Initialise(IManager manager)
		{
			mManager = manager;

			mOptions = new DebuggerOptions();
			manager.OptionsManager.Options.Add(mOptions);

			mDebugger = new DebugManager(manager);
		}

		public string Name
		{
			get { return "Lua Debugger"; }
		}

		public Version Version
		{
			get { return new Version(0, 1); }
		}

		#endregion

		public IManager Manager
		{
			get { return mManager; }
		}

		public DebugManager Debugger
		{
			get { return mDebugger; }
		}

		public DebuggerOptions Options
		{
			get { return mOptions; }
		}


		private IManager mManager;
		private DebugManager mDebugger;
		private DebuggerOptions mOptions;
	}
}
