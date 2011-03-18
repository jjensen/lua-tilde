
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
using System.Windows.Forms;

using Tilde.Framework.Controller;

using Scintilla.Configuration;
using Scintilla.Configuration.SciTE;
using Scintilla.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Tilde.CorePlugins.TextEditor
{
	public enum EdgeMode
	{
		None = 0,
		Line = 1,
		Background = 2
	}

	public class TextPlugin : IPlugin
	{
		private static SciTEProperties mScintillaProperties;

		private IManager mManager;
		private FindReplaceDialog mFindReplaceDialog;
		private MainWindowComponents m_components;
		private TextOptions mOptions;

		static public SciTEProperties ScintillaProperties
		{
			get { return mScintillaProperties; }
		}

		#region IPlugin Members

		public void Initialise(IManager manager)
		{
			mManager = manager;

			FileInfo exeFile = new FileInfo(Application.ExecutablePath);
			FileInfo globalConfigFile = new FileInfo(exeFile.Directory.FullName + @"\TextEditor\Properties\Tilde.properties");
			if (globalConfigFile.Exists)
			{
				mScintillaProperties = new SciTEProperties();
				mScintillaProperties.Load(globalConfigFile);
			}

			mOptions = new TextOptions();
			manager.OptionsManager.Options.Add(mOptions);

			m_components = new MainWindowComponents(this);
			manager.AddToMenuStrip(m_components.MenuStrip.Items);
		}

		public string Name
		{
			get { return "Text Editor"; }
		}

		public Version Version
		{
			get { return new Version(0, 1); }
		}

		public TextOptions Options
		{
			get { return mOptions; }
		}

		public FindReplaceDialog FindReplaceDialog
		{
			get { return mFindReplaceDialog; }
			set { mFindReplaceDialog = value; }
		}

		public void FindInFiles()
		{
			TextView view = mManager.ActiveView as TextView;
			if (view != null)
			{
				Scintilla.ScintillaControl scintilla = view.ScintillaControl;
				view.AutoSelect();
				mFindReplaceDialog.FindString = scintilla.GetSelectedText();
			}
			mFindReplaceDialog.FindInFiles();
		}

		public void Find(string text)
		{
			mFindReplaceDialog.FindString = text;
			mFindReplaceDialog.Find();
		}

		public void Replace(string text)
		{
			mFindReplaceDialog.FindString = text;
			mFindReplaceDialog.Replace();
		}

		public void FindNext()
		{
			mFindReplaceDialog.FindNext();
		}

		public void FindPrevious()
		{
			mFindReplaceDialog.FindPrevious();
		}

		#endregion
	}
}
