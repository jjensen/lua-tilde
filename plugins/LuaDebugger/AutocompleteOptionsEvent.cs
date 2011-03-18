
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
	public delegate void AutocompleteOptionsEventHandler(Target sender, AutocompleteOptionsEventArgs args);

	public class AutocompleteResult
	{
		public LuaValue m_key;
		public LuaValueType m_valueType;

		public AutocompleteResult(LuaValue key, LuaValueType valueType)
		{
			m_key = key;
			m_valueType = valueType;
		}
	}

	public class AutocompleteOptionsEventArgs
	{
		public AutocompleteOptionsEventArgs(int seqid, AutocompleteResult[] options, string message)
		{
			m_sequenceID = seqid;
			m_options = options;
			m_message = message;
		}

		public int SequenceID
		{
			get { return m_sequenceID; }
		}

		public AutocompleteResult[] Options
		{
			get { return m_options; }
		}

		public string Message
		{
			get { return m_message; }
		}

		private int m_sequenceID;
		private AutocompleteResult[] m_options;
		private string m_message;
	}
}
