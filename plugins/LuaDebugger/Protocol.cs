
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
	public class Protocol
	{
		public const int Version = 18;
	}

	[Flags]
	public enum VariableInfoFlag
	{
		Valid		= 0x0001,
		Expanded	= 0x0002,
		HasEntries	= 0x0004,
		HasMetadata	= 0x0008,
	}

	// From debugger -> target
	public enum DebuggerMessage
	{
		Connect,
		Disconnect,

		Run,
		Reset,
		AddBreakpoint,			// AddBreakpoint(int bkptid, string file, int line)
		RemoveBreakpoint,		// RemoveBreakpoint(int bkptid)
		ClearBreakpoints,
		IgnoreError,
		RetrieveStatus,
		RetrieveThreads,
		RetrieveLocals,
		ExpandLocal,
		CloseLocal,
		RetrieveWatches,
		UpdateWatch,
		AddWatch,
		RemoveWatch,
		ExpandWatch,
		CloseWatch,
		ClearWatches,
		RunSnippet,
		RetrieveAutocompleteOptions,
		ExCommand,
	};

	// From target -> debugger
	public enum TargetMessage
	{
		Connect,
		Disconnect,

		ValueCached,
		DebugPrint,
		ErrorMessage,
		StateUpdate,
		CallstackUpdate,
		LocalsUpdate,
		WatchUpdate,
		ThreadsUpdate,
		BreakpointAdded,
		BreakpointRemoved,
		UploadFile,
		ExMessage,
		SnippetResult,				// Resulting output from RunSnippet
		AutocompleteOptions			// Resulting output from RetrieveAutocompleteOptions
	};
}
