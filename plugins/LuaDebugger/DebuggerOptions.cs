
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
using System.ComponentModel;

using Tilde.Framework.Controller;

namespace Tilde.LuaDebugger
{
	[OptionsCollection(Path = "Lua Debugger", Editor = typeof(OptionsGridPanel))]
	public class DebuggerOptions : IOptions
	{
		[Category("Build")]
		[Description("Command to run to build the project.")]
		[Option(Location = OptionLocation.User, Path = "LuaDebugger/BuildCommand", DefaultValue = "")]
		[DisplayName("Build Command")]
		public string BuildCommand
		{
			get { return m_buildCommand; }
			set { if (m_buildCommand != value) { m_buildCommand = value; OnOptionsChanged("BuildCommand"); } }
		}
		private string m_buildCommand;

		[Category("Build")]
		[Description("Command to run to start executing the project on the target.")]
		[Option(Location = OptionLocation.User, Path = "LuaDebugger/RunCommand", DefaultValue = "")]
		[DisplayName("Run Command")]
		public string RunCommand
		{
			get { return m_runCommand; }
			set { if (m_runCommand != value) { m_runCommand = value; OnOptionsChanged("RunCommand"); } }
		}
		private string m_runCommand;

		[Category("Build")]
		[Description("Command to stop the target.")]
		[Option(Location = OptionLocation.User, Path = "LuaDebugger/StopCommand", DefaultValue = "")]
		[DisplayName("Stop Command")]
		public string StopCommand
		{
			get { return m_stopCommand; }
			set { if (m_stopCommand != value) { m_stopCommand = value; OnOptionsChanged("StopCommand"); } }
		}
		private string m_stopCommand;

		[Category("Lua Console")]
		[Description("The path to the folder containing lua source snippets for the lua console.")]
		[Option(Location = OptionLocation.Project, Path = "LuaDebugger/SnippetsFolder", DefaultValue = @"LuaSnippets")]
		[DisplayName("Snippets Folder")]
		public string SnippetsFolder
		{
			get { return m_snippetsFolder; }
			set { if (m_snippetsFolder != value) { m_snippetsFolder = value; OnOptionsChanged("SnippetsFolder"); } }
		}
		private string m_snippetsFolder;

		[Category("Lua Console")]
		[Description("The number of console commands persisted in the project history.")]
		[Option(Location = OptionLocation.Registry, Path = "LuaDebugger/ConsoleHistorySize", DefaultValue = 50)]
		[DisplayName("Console History Size")]
		public int ConsoleHistorySize
		{
			get { return m_consoleHistorySize; }
			set { if (m_consoleHistorySize != value) { m_consoleHistorySize = value; OnOptionsChanged("ConsoleHistorySize"); } }
		}
		private int m_consoleHistorySize;

		[Category("Debugger")]
		[Description("A list of directories in which Tilde should search to find Lua filenames sent from the target.")]
		[Option(Location = OptionLocation.Project, Path = "LuaDebugger/LuaSourceSearchPath", DefaultValue = new string[] { })]
		[DisplayName("Lua Source Search Path")]
		public string [] LuaSourceSearchPath
		{
			get { return m_luaSourceSearchPath; }
			set { if (m_luaSourceSearchPath != value) { m_luaSourceSearchPath = value; OnOptionsChanged("LuaSourceSearchPath"); } }
		}
		private string [] m_luaSourceSearchPath;

		[Category("Debugger")]
		[Description("The port number to listen on for target ping-backs to Tilde.")]
		[Option(Location = OptionLocation.Project, Path = "LuaDebugger/PingbackPort", DefaultValue = 52000)]
		[DisplayName("Target Ping-back Port")]
		public int PingbackPort
		{
			get { return m_pingbackPort; }
			set { if (m_pingbackPort != value) { m_pingbackPort = value; OnOptionsChanged("PingbackPort"); } }
		}
		private int m_pingbackPort;

		[Category("Debugger")]
		[Description("The target will break as soon as Tilde connects to it.")]
		[Option(Location = OptionLocation.User, Path = "LuaDebugger/BreakOnConnection", DefaultValue = false)]
		[DisplayName("Break on Connection")]
		public bool BreakOnConnection
		{
			get { return m_breakOnConnection; }
			set { if (m_breakOnConnection != value) { m_breakOnConnection = value; OnOptionsChanged("BreakOnConnection"); } }
		}
		private bool m_breakOnConnection;

		public DebuggerOptions()
		{
		}
	}
}
