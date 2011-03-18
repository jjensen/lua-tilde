
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
using System.IO;

using Tilde.Framework.Model;

namespace Tilde.CorePlugins.TextEditor
{
	[DocumentClassAttribute("Text Document",
		ViewType = typeof(TextView),
		FileExtensions = new string[] { ".txt", ".*" })
	]

	public class TextDocument : Document
	{
		public TextDocument(Tilde.Framework.Controller.IManager manager, string fileName)
			: base(manager, fileName)
		{
		}

		public string Text
		{
			get { return mText; }
			set { mText = value; }
		}

		protected override bool New(Stream stream)
		{
			if (stream == null)
			{
				mText = "";
				mEncoding = Encoding.Default;
			}
			else
			{
				StreamReader reader = new StreamReader(stream);
				mText = reader.ReadToEnd();
				mEncoding = reader.CurrentEncoding == Encoding.UTF8 ? Encoding.Default : reader.CurrentEncoding;
				reader.Close();
			}
			return true;
		}

		protected override bool Load()
		{
			try
			{
				StreamReader reader = new StreamReader(FileName, Encoding.UTF8);
				mText = reader.ReadToEnd();
				mEncoding = reader.CurrentEncoding == Encoding.UTF8 ? Encoding.Default : reader.CurrentEncoding;
				reader.Close();

				OnPropertyChange("Text", null, null);

				return true;
			}
			catch (Exception)
			{
				mText = "";
				return false;
			}
		}

		protected override bool Save()
		{
			StreamWriter writer = new StreamWriter(FileName, false, mEncoding);
			writer.Write(mText);
			writer.Close();

			Modified = false;
			return true;
		}

		private string mText;
		private Encoding mEncoding;
	}
}
