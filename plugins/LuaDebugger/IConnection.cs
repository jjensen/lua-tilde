
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

namespace Tilde.LuaDebugger
{
	public delegate void ConnectionClosedEventHandler(IConnection sender);
	public delegate void ConnectionAbortedEventHandler(IConnection sender, string message);
	public delegate void ConnectionDataReceivedEventHandler(IConnection sender, byte [] buffer, int bytes);

	public interface IConnection
	{
		void Connect(/*ThreadedTarget target*/);
		void Send(byte[] data, int start, int length);
		void OnSignalled(WaitHandle handle);
		void Shutdown();

		bool DownloadFile(string fileName);

		event ConnectionClosedEventHandler ConnectionClosed;
		event ConnectionAbortedEventHandler ConnectionAborted;
		event ConnectionDataReceivedEventHandler DataReceived;

		WaitHandle [] Handles
		{
			get;
		}

		HostInfo HostInfo
		{
			get;
		}
	}
}
