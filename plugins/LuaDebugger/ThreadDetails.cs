
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
using System.Text.RegularExpressions;

namespace Tilde.LuaDebugger
{
	public enum LuaThreadState
	{
		Unknown = -1,
		Ready,
		Blocked,
		Dead
	}

	public class ThreadDetails
	{
		public ThreadDetails(LuaValue thread, LuaValue parent, string name, int threadid, string location, LuaThreadState state, int stackSize, int stackUsed, double peaktime, double avgtime, bool valid)
		{
			m_thread = thread;
			m_parent = parent;
			m_name = name;
			m_threadID = threadid;
			m_location = location;
			m_state = state;
			m_stackSize = stackSize;
			m_stackUsed = stackUsed;
			m_peakTime = peaktime;
			m_averageTime = avgtime;
			m_valid = valid;

		}

		public double AverageTime
		{
			get { return m_averageTime; }
		}

		public LuaValue Thread
		{
			get { return m_thread; }
		}

		public LuaValue Parent
		{
			get { return m_parent; }
		}

		public LuaThreadState State
		{
			get { return m_state; }
		}

		public int StackSize
		{
			get { return m_stackSize; }
		}

		public int StackUsed
		{
			get { return m_stackUsed; }
		}

		public double PeakTime
		{
			get { return m_peakTime; }
		}

		public string Name
		{
			get { return m_name; }
		}

		public int ID
		{
			get { return m_threadID; }
		}

		public string Location
		{
			get { return m_location; }
		}

		public bool Valid
		{
			get { return m_valid; }
		}

		LuaValue m_thread;
		LuaValue m_parent;
		string m_name;
		int m_threadID;
		string m_location;
		LuaThreadState m_state;
		int m_stackSize;
		int m_stackUsed;
		double m_peakTime;
		double m_averageTime;
		bool m_valid;

	}
}
