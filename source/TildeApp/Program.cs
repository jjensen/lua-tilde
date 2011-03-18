
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
using System.Windows.Forms;
using System.Threading;
using Microsoft.Win32;

using Tilde.TildeApp;
using Tilde.Framework.Controller;

namespace Tilde.Framework
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string [] args)
		{
			Thread.CurrentThread.Name = "Main Thread";
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			// Check for the .NET Framework 2.0 Service Pack 1
			RegistryKey regkey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\NET Framework Setup\\NDP\\v2.0.50727");
			if (regkey == null || regkey.GetValue("Increment") == null)
			{
				MessageBox.Show(
					"Tilde requires the .NET Framework 2.0 to be installed. Please install:\r\n\r\n"
					+ "\t" + "http://www.microsoft.com/downloads/details.aspx?FamilyID=0856eacb-4362-4b0d-8edd-aab15c5e04f5&displaylang=en" + "\r\n"
					+ "\t" + "http://www.microsoft.com/downloads/details.aspx?familyid=79BC3B77-E02C-4AD3-AACF-A7633F706BA5&displaylang=en",
					".NET Framework Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}
			else if(Int32.Parse(regkey.GetValue("Increment").ToString()) < 1433)
			{
				MessageBox.Show(
					"Tilde requires the .NET Framework 2.0 Service Pack 1 to be installed. Please install:\r\n\r\n"
					+ "\t" + "http://www.microsoft.com/downloads/details.aspx?familyid=79BC3B77-E02C-4AD3-AACF-A7633F706BA5&displaylang=en",
					".NET Framework Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}

			CommandLineArguments param = new CommandLineArguments(args);
			Manager manager = new Manager(param);
			MainWindow mainWindow = new MainWindow(manager);
			mainWindow.CreateToolWindows();

			Application.Run(mainWindow);
		}
	}
}