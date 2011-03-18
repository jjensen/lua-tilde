
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
	public interface Target
	{
		#region Connection and setting up
		//-----------------------------------------------------------------------------------------------

		/// <summary>
		/// Disconnects from the target.
		/// </summary>
		void Disconnect();

		/// <summary>
		/// Starts the Lua VM in the specified execution mode.
		/// </summary>
		/// <param name="mode">The execution mode to run the VM in.</param>
		void Run(ExecutionMode mode);

		void IgnoreError(string fileName, int lineNumber);

        HostInfo HostInfo
        {
            get;
        }

		void ExCommand(string command, byte[] data);

		//-----------------------------------------------------------------------------------------------
		#endregion

		#region Breakpoints
		//-----------------------------------------------------------------------------------------------

		void AddBreakpoint(string fileName, int lineNumber, int bkptid);
		void RemoveBreakpoint(int bkptid);
		void ClearBreakpoints();

		//-----------------------------------------------------------------------------------------------
		#endregion 

		#region Variables and Watches
		//-----------------------------------------------------------------------------------------------

		void RetrieveStatus(LuaValue thread, int stackFrame);
		void RetrieveThreads();

		void RetrieveLocals(LuaValue thread, int stackFrame);
		void ExpandLocal(LuaValue [] path);
		void CloseLocal(LuaValue [] path);

		void RetrieveWatches(LuaValue thread, int stackFrame);
		void UpdateWatch(int watchid, LuaValue thread, int stackFrame);
		void AddWatch(string expr, int watchid);
		void RemoveWatch(int watchid);
		void ExpandWatch(int watchid, LuaValue [] path);
		void CloseWatch(int watchid, LuaValue [] path);
		void ClearWatches();

		void RunSnippet(string snippet, LuaValue thread, int stackFrame);
		void RetrieveAutocompleteOptions(int seqid, string expression, LuaValue thread, int stackFrame);

		//-----------------------------------------------------------------------------------------------
		#endregion

		#region Events
		//-----------------------------------------------------------------------------------------------
		
		event DebugPrintEventHandler DebugPrint;
		event ErrorMessageEventHandler ErrorMessage;
		event StatusMessageEventHandler StatusMessage;
		event StateUpdateEventHandler StateUpdate;
		event CallstackUpdateEventHandler CallstackUpdate;
		event ThreadUpdateEventHandler ThreadUpdate;
		event VariableUpdateEventHandler VariableUpdate;
		event ValueCachedEventHandler ValueCached;
		event FileUploadEventHandler FileUpload;
		event SnippetResultEventHandler SnippetResult;
		event AutocompleteOptionsEventHandler AutocompleteOptions;

		/// <summary>
		/// Signals that a breakpoint has been added or removed on the lua machine.
		/// </summary>
		event BreakpointUpdateEventHandler BreakpointUpdate;

		//-----------------------------------------------------------------------------------------------
		#endregion

	}
}
