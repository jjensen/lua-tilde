
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

using Tilde.Framework.View;
using Tilde.Framework.Controller;
using Scintilla;
using System.Media;
using Microsoft.Win32;
using System.Threading;
using System.IO;
using Tilde.Framework.Model;
using System.Text.RegularExpressions;

namespace Tilde.CorePlugins.TextEditor
{
	[ToolWindowAttribute]
	public partial class FindReplaceDialog : Tilde.Framework.View.ToolWindow
	{
		enum Mode
		{
			Find,
			Replace,
			FindInFiles,
			ReplaceInFiles
		};

		enum FindInFilesState
		{
			Stopped,
			InProgress,
			Cancelling
		};

		IManager mManager;
		Mode m_mode;

		object m_lock = new object();
		FindInFilesState m_findInFilesState = FindInFilesState.Stopped;
		Thread m_findInFilesThread;

		public FindReplaceDialog(IManager manager)
		{
			InitializeComponent();

			mManager = manager;

			DialogMode = Mode.Find;

			TextPlugin plugin = (TextPlugin)mManager.GetPlugin(typeof(TextPlugin));
			plugin.FindReplaceDialog = this;

			LoadFromRegistry(comboBoxFindWhat);
			LoadFromRegistry(comboBoxReplaceWith);
			LoadFromRegistry(comboBoxSearchIn);
			LoadFromRegistry(comboBoxFileTypes);

			if (comboBoxSearchIn.Items.Count == 0)
				comboBoxSearchIn.Items.AddRange(new string[] { "Current Project", "All Open Documents" });

			comboBoxSearchIn.Text = (string) comboBoxSearchIn.Items[0];

			if (comboBoxFileTypes.Items.Count > 0)
				comboBoxFileTypes.Text = (string) comboBoxFileTypes.Items[0];
			else
				FileTypes = "*.*";

			checkBoxIncludeSubFolders.Checked = true;
		}

		private FindInFilesState FindThreadStatus
		{
			set 
			{
				lock (m_lock)
				{
					m_findInFilesState = value;
				}
				UpdateDialog();
			}
		}

		private Mode DialogMode
		{
			set { m_mode = value; UpdateDialog(); }
		}

		public string FindString
		{
			get { return comboBoxFindWhat.Text; }
			set { comboBoxFindWhat.Text = value; UpdateFindHistory(); }
		}

		public string ReplaceString
		{
			get { return comboBoxReplaceWith.Text; }
			set { comboBoxReplaceWith.Text = value; UpdateReplaceHistory(); }
		}

		public string SearchInString
		{
			get { return comboBoxSearchIn.Text; }
			set { comboBoxSearchIn.Text = value; UpdateSearchInHistory(); }
		}

		public string FileTypes
		{
			get { return comboBoxFileTypes.Text; }
			set { comboBoxFileTypes.Text = value; UpdateFileTypesHistory(); }
		}

		public void Find()
		{
			TextView view = mManager.ActiveView as TextView;
			if (view != null)
			{
				DialogMode = Mode.Find;
				Show(mManager.DockPanel);
				comboBoxFindWhat.Focus();
			}
		}

		public void FindInFiles()
		{
			DialogMode = Mode.FindInFiles;
			Show(mManager.DockPanel);
			comboBoxFindWhat.Focus();
		}

		public bool FindNext()
		{
			TextView view = mManager.ActiveView as TextView;
			if (view != null)
			{
				checkBoxSearchUp.Checked = false;
				return FindAndHighlight(!checkBoxSearchUp.Checked);
			}
			return false;
		}

		public bool FindPrevious()
		{
			TextView view = mManager.ActiveView as TextView;
			if (view != null)
			{
				checkBoxSearchUp.Checked = true;
				return FindAndHighlight(!checkBoxSearchUp.Checked);
			}
			return false;
		}

		public void Replace()
		{
			TextView view = mManager.ActiveView as TextView;
			if (view != null)
			{
				DialogMode = Mode.Replace;
				Show(mManager.DockPanel);
				comboBoxFindWhat.Focus();
			}
		}
		
		private void UpdateHistory(ComboBox comboBox)
		{
			if (comboBox.Text != "")
			{
				string text = comboBox.Text;
				if (comboBox.Items.Contains(text))
					comboBox.Items.Remove(text);

				comboBox.Items.Insert(0, text);
				comboBox.Text = text;
				StoreInRegistry(comboBox);
			}
		}

		private void UpdateFindHistory()
		{
			UpdateHistory(comboBoxFindWhat);
		}

		private void UpdateReplaceHistory()
		{
			UpdateHistory(comboBoxReplaceWith);
		}

		private void UpdateSearchInHistory()
		{
			UpdateHistory(comboBoxSearchIn);
		}

		private void UpdateFileTypesHistory()
		{
			UpdateHistory(comboBoxFileTypes);
		}

		private void StoreInRegistry(ComboBox combobox)
		{
			RegistryKey root = mManager.RegistryRoot.CreateSubKey("FindReplaceHistory");
			string path = (string)combobox.Tag;
			string[] array = new string[combobox.Items.Count];
			combobox.Items.CopyTo(array, 0);
			root.SetValue(path, array, RegistryValueKind.MultiString);
		}

		private void LoadFromRegistry(ComboBox combobox)
		{
			RegistryKey root = mManager.RegistryRoot.CreateSubKey("FindReplaceHistory");
			string path = (string)combobox.Tag;
			string[] values = (string[]) root.GetValue(path);
			if (values != null)
			{
				foreach (string value in values)
				{
					combobox.Items.Add(value);
				}
			}
		}

		private bool FindAndHighlight(bool dirn)
		{
			TextView view = mManager.ActiveView as TextView;

			if (view == null || comboBoxFindWhat.Text == "")
				return false;

			ScintillaControl editControl = view.ScintillaControl;

			UpdateFindHistory();

			mManager.SetStatusMessage(String.Format("Find '{0}'", comboBoxFindWhat.Text), 10.0f);

			bool found = FindAndHighlight(dirn, 0, editControl.Length);
			if (!found)
			{
				int savedPos = editControl.CurrentPos;

				editControl.CurrentPos =
					editControl.SelectionStart =
					editControl.SelectionEnd = dirn ? 0 : (editControl.Length - 1);

				found = FindAndHighlight(dirn, 0, editControl.Length);

				if (!found)
				{
					editControl.CurrentPos =
					editControl.SelectionStart =
					editControl.SelectionEnd = savedPos;

					SystemSounds.Exclamation.Play();
					mManager.SetStatusMessage(String.Format("Not found '{0}'", comboBoxFindWhat.Text), 10.0f);
				}
				else
				{
					mManager.SetStatusMessage("Search wrapped at end of document", 10.0f);
				}
			}

			return found;
		}

		private bool FindAndHighlight(bool dirn, int min, int max)
		{
			TextView view = mManager.ActiveView as TextView;
			ScintillaControl editControl = view.ScintillaControl;

			bool matchCase = checkBoxMatchCase.Checked;
			bool matchWholeWord = checkBoxMatchWholeWord.Checked;
			bool useRegex = checkBoxUseRegularExpressions.Checked;
			string findString = comboBoxFindWhat.Text;

			int flags = 0;

			if (matchCase)
				flags = flags | (int)Scintilla.Enums.FindOption.MatchCase;
			if (matchWholeWord)
				flags = flags | (int)Scintilla.Enums.FindOption.WholeWord;
			if (useRegex)
				flags = flags | (int)Scintilla.Enums.FindOption.RegularExpression;

			int startPos, endPos;

			int prevStartPos = (editControl.SelectionStart < editControl.SelectionEnd) ? editControl.SelectionStart : editControl.SelectionEnd;
            int prevEndPos = (editControl.SelectionStart < editControl.SelectionEnd) ? editControl.SelectionEnd : editControl.SelectionStart;

			if(dirn)
			{
				editControl.CurrentPos =
				editControl.SelectionStart =
				editControl.SelectionEnd =
					(editControl.SelectionStart < editControl.SelectionEnd) ? editControl.SelectionEnd : editControl.SelectionStart;

				editControl.SearchAnchor();

				startPos = editControl.SearchNext(flags, findString);
			}
			else
			{
				editControl.CurrentPos =
				editControl.SelectionStart =
				editControl.SelectionEnd =
					(editControl.SelectionStart < editControl.SelectionEnd) ? editControl.SelectionStart : editControl.SelectionEnd;

				editControl.SearchAnchor();

				startPos = editControl.SearchPrevious(flags, findString);
			}

			bool foundMatch = (startPos >= 0);

			if (foundMatch)
			{
				startPos = (editControl.SelectionStart < editControl.SelectionEnd) ? editControl.SelectionStart : editControl.SelectionEnd;
				endPos = (editControl.SelectionStart < editControl.SelectionEnd) ? editControl.SelectionEnd : editControl.SelectionStart;

				if (startPos >= min && endPos <= max)
				{
					int lineIndex = editControl.LineFromPosition(editControl.CurrentPos);
					editControl.EnsureVisibleEnforcePolicy(lineIndex);
					editControl.ScrollCaret();

					editControl.CurrentPos = endPos;
					editControl.SelectionStart = startPos;
					editControl.SelectionEnd = endPos;
				}
				else
				{
					foundMatch = false;
					editControl.CurrentPos = prevEndPos;
					editControl.SelectionStart = prevStartPos;
					editControl.SelectionEnd = prevEndPos;
				}
			}

			return foundMatch;
		}

		private bool FindAndReplace(bool dirn)
		{
			if (comboBoxFindWhat.Text == "")
				return false;

			TextView view = mManager.ActiveView as TextView;
			ScintillaControl editControl = view.ScintillaControl;

			UpdateReplaceHistory();

			// Make sure we find the currently selected text again
			if(dirn)
				editControl.SelectionStart = editControl.SelectionEnd = editControl.SelectionStart - 1;
			else
				editControl.SelectionStart = editControl.SelectionEnd = editControl.SelectionEnd + 1;

			if(FindAndHighlight(dirn))
				ReplaceHighlightedText();

			return FindAndHighlight(dirn);
		}

		private void ReplaceAll()
		{
			if (comboBoxFindWhat.Text == "")
				return;

			TextView view = mManager.ActiveView as TextView;
			ScintillaControl editControl = view.ScintillaControl;

			bool matchCase = checkBoxMatchCase.Checked;
			bool matchWholeWord = checkBoxMatchWholeWord.Checked;
			bool useRegex = checkBoxUseRegularExpressions.Checked;
			string findString = comboBoxFindWhat.Text;

			int flags = 0;

			if (matchCase)
				flags = flags | (int)Scintilla.Enums.FindOption.MatchCase;
			if (matchWholeWord)
				flags = flags | (int)Scintilla.Enums.FindOption.WholeWord;
			if (useRegex)
				flags = flags | (int)Scintilla.Enums.FindOption.RegularExpression;

			editControl.SearchFlags = flags;
			editControl.TargetStart = 0;
			editControl.TargetEnd = editControl.Length;

			int count = 0;

			try
			{
				editControl.BeginUndoAction();

				while (editControl.SearchInTarget(comboBoxFindWhat.Text) >= 0)
				{
					if (useRegex)
						editControl.ReplaceTargetRE(comboBoxReplaceWith.Text);
					else
						editControl.ReplaceTarget(comboBoxReplaceWith.Text);

					editControl.TargetStart = editControl.TargetEnd + 1;
					editControl.TargetEnd = editControl.Length;

					// Abort after first attempt, if file wasn't made writable
					if (editControl.IsReadOnly)
					{
						mManager.SetStatusMessage("Replace all cancelled", 10.0f);
						return;
					}

					++count;
				}
			}
			finally
			{
				editControl.EndUndoAction();
			}

			if(count == 0)
			{
				SystemSounds.Exclamation.Play();
				mManager.SetStatusMessage(String.Format("Not found '{0}'", comboBoxFindWhat.Text), 10.0f);
			}
			else
			{
				mManager.SetStatusMessage(String.Format("Replaced {0} strings", count), 10.0f);
			}

		}

		private void ReplaceHighlightedText()
		{
			TextView view = mManager.ActiveView as TextView;
			ScintillaControl editControl = view.ScintillaControl;

			bool useRegex = checkBoxUseRegularExpressions.Checked;

			if (editControl.GetSelectedText().Length > 0)
            {
				editControl.TargetStart = editControl.SelectionStart;
				editControl.TargetEnd = editControl.SelectionEnd;

				if (useRegex)
					editControl.ReplaceTargetRE(comboBoxReplaceWith.Text);
				else
					editControl.ReplaceTarget(comboBoxReplaceWith.Text);
				
				editControl.CurrentPos = editControl.TargetEnd + 1;
				editControl.SelectionStart = editControl.CurrentPos;
                editControl.SelectionEnd = editControl.CurrentPos;
            }
		}

		private void findToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DialogMode = Mode.Find;
		}

		private void replaceToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DialogMode = Mode.Replace;
		}

		private void findInFilesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DialogMode = Mode.FindInFiles;
		}

		private void replaceInFilesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DialogMode = Mode.ReplaceInFiles;
		}

		private void UpdateDialog()
		{
			FindInFilesState findState;
			lock(m_lock)
			{
				findState = m_findInFilesState;
			}

			if (m_mode == Mode.Find)
				buttonFindReplace.Text = findToolStripMenuItem.Text;
			else if(m_mode == Mode.Replace)
				buttonFindReplace.Text = replaceToolStripMenuItem.Text;
			else if (m_mode == Mode.FindInFiles)
				buttonFindReplace.Text = findInFilesToolStripMenuItem.Text;
			else if (m_mode == Mode.ReplaceInFiles)
				buttonFindReplace.Text = replaceInFilesToolStripMenuItem.Text;

			bool replace = m_mode == Mode.Replace || m_mode == Mode.ReplaceInFiles;
			bool inFiles = m_mode == Mode.FindInFiles || m_mode == Mode.ReplaceInFiles;
			labelReplaceWith.Visible = replace;
			comboBoxReplaceWith.Visible = replace;
			labelSearchIn.Visible = inFiles;
			panelSearchIn.Visible = inFiles;

			checkBoxIncludeSubFolders.Visible = inFiles;
			checkBoxSearchUp.Visible = !inFiles;
			labelFileTypes.Visible = inFiles;
			comboBoxFileTypes.Visible = inFiles;

			buttonReplace.Visible = replace;
			buttonReplaceAll.Visible = replace;
			buttonFindNext.Visible = (m_mode != Mode.FindInFiles);
			buttonFindAll.Visible = (m_mode == Mode.FindInFiles && findState == FindInFilesState.Stopped);
			buttonStopFind.Visible = (m_mode == Mode.FindInFiles && findState != FindInFilesState.Stopped);
			buttonSkipFile.Visible = (m_mode == Mode.ReplaceInFiles);

			this.CancelButton = (m_mode == Mode.FindInFiles && findState != FindInFilesState.Stopped) ? buttonStopFind : null;

			if (m_mode == Mode.FindInFiles && findState == FindInFilesState.Stopped)
				this.AcceptButton = buttonFindAll;
			else if (m_mode == Mode.FindInFiles && findState != FindInFilesState.Stopped)
				this.AcceptButton = buttonStopFind;
			else if (m_mode == Mode.Find)
				this.AcceptButton = buttonFindNext;
			else if (m_mode == Mode.Replace)
				this.AcceptButton = buttonReplace;
			else
				this.AcceptButton = null;
		}

		private void FindReplaceDialog_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape && !comboBoxFindWhat.DroppedDown && !comboBoxReplaceWith.DroppedDown && !comboBoxSearchIn.DroppedDown)
			{
				if (this.DockState == WeifenLuo.WinFormsUI.Docking.DockState.Float)
					Hide();
				else
					this.DockPanel.ActiveDocumentPane.Activate();
			}
		}

		private void comboBoxFindWhat_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Down)
				comboBoxFindWhat.DroppedDown = true;
		}

		private void comboBoxReplaceWith_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Down)
				comboBoxReplaceWith.DroppedDown = true;
		}

		private void comboBoxSearchIn_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Down)
				comboBoxSearchIn.DroppedDown = true;
		}

		private void buttonFindNext_Click(object sender, EventArgs e)
		{
			FindAndHighlight(!checkBoxSearchUp.Checked);
		}

		private void buttonReplace_Click(object sender, EventArgs e)
		{
			FindAndReplace(!checkBoxSearchUp.Checked);
		}

		private void buttonReplaceAll_Click(object sender, EventArgs e)
		{
			ReplaceAll();
		}

		private void comboBoxFindWhat_TextChanged(object sender, EventArgs e)
		{
			bool enabled = comboBoxFindWhat.Text.Length > 0;

			buttonFindNext.Enabled = enabled;
			buttonReplace.Enabled = enabled;
			buttonReplaceAll.Enabled = enabled;
		}

		private void buttonBrowseSearchIn_Click(object sender, EventArgs e)
		{
			folderBrowserDialog.SelectedPath = SearchInString;
			if (folderBrowserDialog.ShowDialog(this) == DialogResult.OK)
			{
				SearchInString = folderBrowserDialog.SelectedPath;
			}
		}

		string [] m_searchPaths;
		string m_searchPatternString;
		string m_searchFileTypesString;
		bool m_searchSubFolders;
		Regex m_searchPattern;
		Regex[] m_searchFileTypes;

		private void buttonFindAll_Click(object sender, EventArgs e)
		{
			if (comboBoxFindWhat.Text == "")
				return;

			if (comboBoxSearchIn.Text == "")
				return;

			if (comboBoxFileTypes.Text == "")
				return;

			UpdateFindHistory();
			UpdateSearchInHistory();
			UpdateFileTypesHistory();

			m_searchPaths = comboBoxSearchIn.Text.Split(new char[] { ';' });
			m_searchPatternString = comboBoxFindWhat.Text;

			RegexOptions regexOpt = checkBoxMatchCase.Checked ? RegexOptions.None : RegexOptions.IgnoreCase;
			string regexStr = checkBoxUseRegularExpressions.Checked ? comboBoxFindWhat.Text : Regex.Escape(comboBoxFindWhat.Text);
			if (checkBoxMatchWholeWord.Checked)
				regexStr = "\\b" + regexStr + "\\b";

			m_searchSubFolders = checkBoxIncludeSubFolders.Checked;
			m_searchPattern = new Regex(regexStr, regexOpt);

			m_searchFileTypesString = comboBoxFileTypes.Text;
			string[] patterns = comboBoxFileTypes.Text.Split(new char[] { ';' });
			m_searchFileTypes = new Regex[patterns.Length];
			for(int index = 0; index < patterns.Length; ++index)
			{
				string pattern = "^" + Regex.Escape(patterns[index]).Replace("\\*", ".*").Replace("\\?", ".") + "$";
				m_searchFileTypes[index] = new Regex(pattern, RegexOptions.IgnoreCase);
			}

			FindThreadStatus = FindInFilesState.InProgress;

			m_findInFilesThread = new Thread(FindInFilesThreadMain);
			m_findInFilesThread.Start();
		}

		private void buttonStopFind_Click(object sender, EventArgs e)
		{
			FindThreadStatus = FindInFilesState.Cancelling;
		}
		
		private void FindInFilesThreadMain()
		{
			mManager.OnFindInFilesStarted(this, String.Format("Searching for \"{0}\" in \"{1}\" matching \"{2}\"...", m_searchPatternString, String.Join(", ", m_searchPaths), m_searchFileTypesString));

			int count = 0;
			foreach(string path in m_searchPaths)
			{
				lock (m_lock)
				{
					if (m_findInFilesState == FindInFilesState.Cancelling)
					{
						break;
					}
				}

				if(path == "Current Project")
				{
					List<string> files = mManager.Project.GetFiles();
					FilterFiles(files);
					count += SearchFiles(files);
				}
				else if(path == "All Open Documents")
				{
					List<string> files = new List<string>();
					foreach(DocumentView view in mManager.DocumentViews)
					{
						files.Add(view.Document.FileName);
					}
					FilterFiles(files);
					count += SearchFiles(files);
				}
				else
				{
					string searchPath;
					if (!Path.IsPathRooted(path) && mManager.Project != null)
						searchPath = Path.Combine(System.Windows.Forms.Application.StartupPath, path);
					else
						searchPath = path;

					if (File.Exists(searchPath) || Directory.Exists(searchPath))
						count += SearchDirectory(searchPath);
				}
			}

			bool cancelled;
			lock (m_lock)
			{
				cancelled = (m_findInFilesState == FindInFilesState.Cancelling);
			}

			if(cancelled)
				mManager.OnFindInFilesStopped(this, String.Format("Search cancelled. Found {0} references.", count));
			else
				mManager.OnFindInFilesStopped(this, String.Format("Finished. Found {0} references.", count));

			this.Invoke(new MethodInvoker(delegate() { FindThreadStatus = FindInFilesState.Stopped; }));
		}

		private int SearchDirectory(string path)
		{
			lock (m_lock)
			{
				if (m_findInFilesState == FindInFilesState.Cancelling)
					return 0;
			}

			bool isDir = Directory.Exists(path);
			List<string> files = new List<string>();
			if (!isDir)
				files.Add(path);
			else
			{
				files.AddRange(Directory.GetFiles(path, "*"));
				FilterFiles(files);
			}

			int count = SearchFiles(files);

			if (isDir && m_searchSubFolders)
			{
				foreach (string dir in Directory.GetDirectories(path, "*"))
				{
					count += SearchDirectory(dir);
				}
			}

			return count;
		}

		private void FilterFiles(List<string> files)
		{
			// Filter them by search pattern, and remove duplicates
			string prev = null;
			for (int index = 0; index < files.Count; )
			{
				if (files[index] != prev)
				{
					prev = files[index];

					bool matched = false;
					foreach (Regex pattern in m_searchFileTypes)
					{
						if (pattern.IsMatch(Path.GetFileName(files[index])))
						{
							matched = true;
							break;
						}
					}
					if(matched)
					{
						++index;
						continue;
					}
				}
				files.RemoveAt(index);
			}
		}

		private int SearchFiles(List<string> files)
		{
			int count = 0;
			foreach (string fileName in files)
			{
				lock (m_lock)
				{
					if (m_findInFilesState == FindInFilesState.Cancelling)
						return count;
				}

				if (File.Exists(fileName))
				{
					StreamReader stream = File.OpenText(fileName);
					int line = 1;
					while (!stream.EndOfStream)
					{
						string text = stream.ReadLine();
						Match match = m_searchPattern.Match(text);
						if (match.Success)
						{
							mManager.OnFindInFilesResult(this, new FindInFilesResultEventArgs(fileName, line, match.Index, match.Index + match.Length, text));
							++count;
						}
						++line;
					}
				}
			}
			return count;
		}


	}
}

