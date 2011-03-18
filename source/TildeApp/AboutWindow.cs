
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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Diagnostics;

namespace Tilde.TildeApp
{
	public partial class AboutWindow : Form
	{
		class LicenseInfo
		{
			public string mName;
			public string mURL;
			public string mLicense;

			public LicenseInfo(string name, string url, string license)
			{
				mName = name;
				mURL = url;
				mLicense = license;
			}

			public override string ToString()
			{
				return mName;
			}
		}

		private LicenseInfo[] mLicenses = 
		{
			new LicenseInfo("DockPanel Suite v2.2", "http://sourceforge.net/projects/dockpanelsuite/", Tilde.TildeApp.Properties.Resources.DockPanelSuiteLicense),
			new LicenseInfo("Scintilla v1.72", "http://scintilla.sourceforge.net/index.html", Tilde.TildeApp.Properties.Resources.ScintillaLicense),
			new LicenseInfo("Scintilla.NET v6-b1.1", "http://www.codeplex.com/ScintillaNET", Tilde.TildeApp.Properties.Resources.ScintillaNETLicense)
		};

		public AboutWindow()
		{
			InitializeComponent();

			foreach(LicenseInfo license in mLicenses)
			{
				listBoxLicense.Items.Add(license);
			}

			richTextBoxTilde.Text = Tilde.TildeApp.Properties.Resources.TildeLicense;
			linkLabel.Text = "";
		}

		private void listBoxLicense_SelectedIndexChanged(object sender, EventArgs e)
		{
			LicenseInfo license = (LicenseInfo)listBoxLicense.SelectedItem;
			if (license == null)
			{
				richTextBoxLicense.Text = "";
				linkLabel.Text = "";
			}
			else
			{
				richTextBoxLicense.Text = license.mLicense;
				linkLabel.Text = license.mURL;
				linkLabel.Links[0].LinkData = license.mURL;
			}
		}

		private void linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process proc = new Process();
			proc.StartInfo.FileName = e.Link.LinkData as string;
			proc.StartInfo.UseShellExecute = true;
			proc.Start();

		}
	}
}