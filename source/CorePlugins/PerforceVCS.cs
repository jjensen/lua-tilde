
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
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.ComponentModel.Design.Serialization;
using System.ComponentModel;

using Tilde.Framework.Controller;
using System.Xml.Serialization;

namespace Tilde.CorePlugins
{
	[Description("Perforce")]
	[Serializable]
	public class PerforceVCS : IVersionController
	{

		private string mClient;
		private string mUser;
		private string mServer;

		#region IVersionController Members

		[field: NonSerialized]
		public event MessageEventHandler Message;

		public bool IsAvailable()
		{
			string output;
			if (Execute("help", out output) != 0)
				return false;
			else
				return true;
		}

		public bool IsVersionControlled(string fileName)
		{
			Dictionary<string, string> fstat = ExecuteFstat(fileName);
			return fstat != null;
		}

		public bool IsCheckedOut(string fileName)
		{
			Dictionary<string, string> fstat = ExecuteFstat(fileName);
			return fstat != null && fstat.ContainsKey("action");
		}

		public bool Checkout(string fileName)
		{
			string output;
			if (Execute("edit " + fileName, out output) != 0)
				return false;
			else
				return (System.IO.File.GetAttributes(fileName) & System.IO.FileAttributes.ReadOnly) == 0;
		}

		[XmlIgnore]
		[Browsable(false)]
		public string ConfigurationMessage
		{
			get { return "If no configuration parameters are set then the current Perforce defaults will be used."; }
		}

		#endregion

		#region ICloneable Members

		object ICloneable.Clone()
		{
			return new PerforceVCS(this);
		}

		#endregion

		public PerforceVCS()
		{
			mClient = Environment.GetEnvironmentVariable("P4CLIENT");
			mUser = Environment.GetEnvironmentVariable("P4PORT");
			mServer = Environment.GetEnvironmentVariable("P4USER");
		}

		public PerforceVCS(PerforceVCS rhs)
		{
			mClient = rhs.Client;
			mUser = rhs.User;
			mServer = rhs.Server;
		}

		[Description("Perforce workspace name.")]
		public string Client
		{
			get { return mClient; }
			set { mClient = value; }
		}

		[Description("Perforce user name.")]
		public string User
		{
			get { return mUser; }
			set { mUser = value; }
		}

		[Description("Perforce server and port.")]
		public string Server
		{
			get { return mServer; }
			set { mServer = value; }
		}

		private int Execute(string cmdargs, out string output)
		{
			StringBuilder args = new StringBuilder();
			if (mClient != null && mClient != "")
				args.AppendFormat(" -c {0}", mClient);
			if (mUser != null && mUser != "")
				args.AppendFormat(" -u {0}", mUser);
			if (mServer != null && mServer != "")
				args.AppendFormat(" -p {0}", mServer);

			Process process = new Process();
			process.StartInfo.CreateNoWindow = true;
			process.StartInfo.ErrorDialog = false;
			process.StartInfo.UseShellExecute = false;
			process.StartInfo.RedirectStandardInput = false;
			process.StartInfo.RedirectStandardOutput = true;
			process.StartInfo.FileName = "cmd";
			process.StartInfo.Arguments = "/c p4 " + args + " " + cmdargs + " 2>&1";

			try
			{
				OnMessage("Executing \"" + process.StartInfo.FileName + " " + process.StartInfo.Arguments + "\"\n");
				process.Start();
				output = process.StandardOutput.ReadToEnd();
				OnMessage(output);

				if (output.StartsWith("Perforce client error:\r\n"))
				{
					MessageBox.Show(output, "Perforce Integration", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}

				return process.ExitCode;
			}
			catch (Exception ex)
			{
				OnMessage(ex.ToString());
				output = "";
				return -1;
			}
		}

		private Dictionary<string, string> ExecuteFstat(string file)
		{
			string output;
			if (Execute("fstat " + file, out output) != 0)
				return null;

			Dictionary<string, string> result = new Dictionary<string, string>();

			Regex fstatRegex = new Regex("\\.\\.\\. (\\w*) (.*)");

			foreach (string line in output.Split(new char[] { '\n' }))
			{
				Match match = fstatRegex.Match(line);
				if(match.Success)
				{
					result.Add(match.Groups[1].Value, match.Groups[2].Value);
				}
			}
			if (result.Count == 0)
				return null;
			else
				return result;
		}

		private void OnMessage(string message)
		{
			if (Message != null)
				Message(this, message);
		}
	}
}
