
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

namespace Tilde.CorePlugins.TextEditor
{
	public partial class GotoLineForm : Form
	{
		public GotoLineForm(int maxLines)
		{
			InitializeComponent();

			labelLineNumber.Text = String.Format("Line number (1 - {0}):", maxLines);
		}

		public int Selection
		{
			get 
			{ 
				int result;
				if (Int32.TryParse(textBoxLineNumber.Text, out result))
					return result;
				else
					return 0;
			}
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
		}

		private void textBoxLineNumber_TextChanged(object sender, EventArgs e)
		{
			string line = textBoxLineNumber.Text.Trim();
			int value;
			buttonOK.Enabled = line.Length > 0 && Int32.TryParse(line, out value);
		}
	}
}