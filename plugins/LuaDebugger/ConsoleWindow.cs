
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
using Tilde.CorePlugins.TextEditor;
using System.Xml;
using System.IO;
using System.Xml.Schema;
using System.Text.RegularExpressions;

namespace Tilde.LuaDebugger
{
	[ToolWindowAttribute(Group = "Debug")]
	public partial class ConsoleWindow : Tilde.Framework.View.ToolWindow
	{
		IManager m_manager;
		LuaPlugin m_plugin;
		DebugManager m_debugger;
		bool m_waitingOnResult = false;
		List<string> m_history = new List<string>();
		int m_historyPosition = 0;
		DirectoryWatcher m_snippetsWatcher;

		AutocompletePopup m_autocompletePopup;
		int m_autocompleteSequenceID = 0;
		int m_autocompleteStartPos;
		int m_autocompleteEndPos;
		string m_autocompleteOperator;

		private Regex m_wordRegex = new Regex(@"^\w+$");

		public ConsoleWindow(IManager manager)
		{
			InitializeComponent();

			m_manager = manager;
			m_plugin = (LuaPlugin)m_manager.GetPlugin(typeof(LuaPlugin));
			m_debugger = m_plugin.Debugger;

			m_manager.ProjectOpened += new ProjectOpenedEventHandler(Manager_ProjectOpened);
			m_manager.ProjectClosing += new ProjectClosingEventHandler(Manager_ProjectClosing);

			m_debugger.DebuggerConnected += new DebuggerConnectedEventHandler(Debugger_DebuggerConnected);
			m_debugger.DebuggerDisconnected += new DebuggerDisconnectedEventHandler(Debugger_DebuggerDisconnected);

			m_plugin.Options.OptionsChanged += new OptionsChangedDelegate(Options_OptionsChanged);

			m_autocompletePopup = new AutocompletePopup(m_debugger, this);
			m_autocompletePopup.Selection += new AutocompleteSelectionEventHandler(m_autocompletePopup_Selection);

			SetWaitingForResult(false);

			ConfigureScintillaControl(inputBox);

			ClearBrowser();
		}

		private void ConsoleWindow_Load(object sender, EventArgs e)
		{

		}


		void ClearBrowser()
		{
			// The Navigate() and Write() are needed to get around a bug in .NET 2.0 apparently (!) 
			// http://geekswithblogs.net/paulwhitblog/archive/2005/12/12/62961.aspx
			outputBrowser.Navigate("about:blank");
			outputBrowser.Document.Write("");
			outputBrowser.DocumentText = Tilde.LuaDebugger.Properties.Resources.ConsoleWindowTemplate;
		}

		void SetWaitingForResult(bool waiting)
		{
			m_waitingOnResult = waiting;
			progressBarWaiting.Style = waiting ? ProgressBarStyle.Marquee : ProgressBarStyle.Continuous;
		}

		void Manager_ProjectOpened(IManager sender, Tilde.Framework.Model.Project project)
		{
			XmlElement historyRoot = project.GetUserConfigurationXML("LuaConsoleHistory");
			if(historyRoot != null)
			{
				m_history.Clear();
				foreach(XmlElement node in historyRoot.ChildNodes)
				{
					m_history.Add(node.InnerText);
				}
				m_historyPosition = m_history.Count;
				inputBox.EmptyUndoBuffer();
				inputBox.Text = "";
			}
		}

		void Manager_ProjectClosing(IManager sender, ProjectClosingEventArgs args)
		{
			XmlElement historyRoot = args.Project.CreateUserConfigurationXML("LuaConsoleHistory");
			for (int index = Math.Max(0, m_history.Count - m_plugin.Options.ConsoleHistorySize); index < m_history.Count; ++index )
			{
				string item = m_history[index];
				XmlElement node = historyRoot.OwnerDocument.CreateElement("History");
				node.InnerText = item;
				historyRoot.AppendChild(node);
			}
		}

		void SnippetsWatcher_FileCreated(object sender, string fileName)
		{
			if(this.IsHandleCreated && !this.IsDisposed)
				this.BeginInvoke(new MethodInvoker(delegate() { GenerateSnippetsMenu(); }));
		}

		void SnippetsWatcher_FileModified(object sender, string fileName)
		{
			if (this.IsHandleCreated && !this.IsDisposed)
				this.BeginInvoke(new MethodInvoker(delegate() { GenerateSnippetsMenu(); }));
		}

		void Debugger_DebuggerConnected(DebugManager sender, Target target)
		{
			SetWaitingForResult(false);
			target.SnippetResult += new SnippetResultEventHandler(target_SnippetResult);
			target.AutocompleteOptions += new AutocompleteOptionsEventHandler(target_AutocompleteOptions);
		}

		void Debugger_DebuggerDisconnected(DebugManager sender)
		{
			SetWaitingForResult(false);
		}

		void Options_OptionsChanged(IOptions sender, string option)
		{
			if (option == "SnippetsFolder")
				UpdateSnippetsFolder();
		}

		void target_SnippetResult(Target sender, SnippetResultEventArgs args)
		{
			if(m_waitingOnResult)
			{
				HtmlElement[] element = new HtmlElement[3];

				element[0] = outputBrowser.Document.CreateElement("p");
				element[0].SetAttribute("className", "output");
				element[0].InnerText = args.Output;

				element[1] = outputBrowser.Document.CreateElement("p");
				element[1].SetAttribute("className", args.Success ? "result" : "error");
				element[1].InnerText = args.Result;

				element[2] = outputBrowser.Document.CreateElement("hr");

				outputBrowser.Document.Body.AppendChild(element[0]);
				outputBrowser.Document.Body.AppendChild(element[1]);
				outputBrowser.Document.Body.AppendChild(element[2]);

				element[2].ScrollIntoView(false);

				SetWaitingForResult(false);
			}
		}

		private void AutocompleteOption_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem item = (ToolStripMenuItem)sender;
			LuaValue value = (LuaValue)item.Tag;

			string str = m_debugger.GetValueString(value);

			string text;
			if (value.Type == LuaValueType.NUMBER)
				text = String.Format("[{0}]", str);
			else if (m_wordRegex.IsMatch(str))
				text = String.Format(".{0}", str);
			else
				text = String.Format("[\"{0}\"]", str);

			inputBox.SetSelection(inputBox.CurrentPos - 1, inputBox.CurrentPos);
			inputBox.ReplaceSelection(text);
		}

		private void ConfigureScintillaControl(Scintilla.ScintillaControl control)
		{
			control.Configuration = new Scintilla.Configuration.ScintillaConfig(TextPlugin.ScintillaProperties, "*.lua");
			control.ConfigurationLanguage = "lua";

			control.MarginWidthN(0, 0);
			control.MarginWidthN(1, 0);
			control.MarginWidthN(2, 0);
			control.MarginWidthN(3, 0);

			control.UseMonospaceFont(TextPlugin.ScintillaProperties.GetByKey("font.monospace"));
			control.TabWidth = 4;
		}

		private void inputBox_KeyPress(object sender, KeyPressEventArgs e)
		{
		}

		private string ExportToHTML(Scintilla.ScintillaControl sc)
		{
			StringBuilder result = new StringBuilder();

			result.Append("<span>");


			int tabSize = 4;
			int styleCurrent = sc.StyleAt(0);
			result.Append(String.Format("<span class=\"S{0:d}\">", styleCurrent));
			bool inStyleSpan = true;

			int lengthDoc = sc.Length;
			int column = 0;
			for (int i = 0; i < lengthDoc; ++i)
			{
				char ch = sc.CharAt(i);
				int style = sc.StyleAt(i);

				if (style != styleCurrent)
				{
					if (inStyleSpan)
					{
						result.Append("</span>");
						inStyleSpan = false;
					}
					if (ch != '\r' && ch != '\n')
					{	// No need of a span for the EOL
						result.Append(String.Format("<span class=\"S{0:d}\">", style));
						inStyleSpan = true;
						styleCurrent = style;
					}
				}
				if (ch == ' ') 
				{
					char prevCh = '\0';
					if (column == 0) 
					{	// At start of line, must put a &nbsp; because regular space will be collapsed
						prevCh = ' ';
					}
					while (i < lengthDoc && sc.CharAt(i) == ' ') 
					{
						if (prevCh != ' ') {
							result.Append(' ');
						} else {
							result.Append("&nbsp;");
						}
						prevCh = sc.CharAt(i);
						i++;
						column++;
					}
					i--; // the last incrementation will be done by the for loop
				} 
				else if (ch == '\t') 
				{
					int ts = tabSize - (column % tabSize);
					for (int itab = 0; itab < ts; itab++) 
					{
						if (itab % 2 == 1) {
							result.Append(' ');
						} else {
							result.Append("&nbsp;");
						}
					}
					column += ts;
				} 
				else if (ch == '\r' || ch == '\n') 
				{
					if (inStyleSpan) 
					{
						result.Append("</span>");
						inStyleSpan = false;
					}
					if (ch == '\r' && sc.CharAt(i + 1) == '\n') 
					{
						i++;	// CR+LF line ending, skip the "extra" EOL char
					}
					column = 0;
					result.Append("<br />");

					styleCurrent = sc.StyleAt(i + 1);
					result.Append('\n');

					if (sc.CharAt(i + 1) != '\r' && sc.CharAt(i + 1) != '\n')
					{
						// We know it's the correct next style,
						// but no (empty) span for an empty line
						result.Append(String.Format("<span class=\"S{0:d}\">", styleCurrent));
						inStyleSpan = true;
					}
				} 
				else 
				{
					switch (ch) 
					{
					case '<':
						result.Append("&lt;");
						break;
					case '>':
						result.Append("&gt;");
						break;
					case '&':
						result.Append("&amp;");
						break;
					default:
						result.Append(ch);
						break;
					}
					column++;
				}
			}

			if(inStyleSpan)
				result.Append("</span>");

			result.Append("</span>");

			return result.ToString();
		}

		private void inputBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Up && e.Control)
			{
				if(m_historyPosition > 0)
				{
					m_historyPosition--;
					inputBox.Text = m_history[m_historyPosition];
					inputBox.EmptyUndoBuffer();
					inputBox.GotoPos(inputBox.TextLength);
				}
				e.SuppressKeyPress = true;
			}
			else if (e.KeyCode == Keys.Down && e.Control)
			{
				if (m_historyPosition < m_history.Count - 1)
				{
					m_historyPosition++;
					inputBox.Text = m_history[m_historyPosition];
					inputBox.EmptyUndoBuffer();
					inputBox.GotoPos(inputBox.TextLength);
				}
				else if (m_historyPosition == m_history.Count - 1)
				{
					m_historyPosition++;
					inputBox.Text = "";
					inputBox.EmptyUndoBuffer();
				}
				e.SuppressKeyPress = true;
			}
			else if (e.KeyCode == Keys.Space && e.Control)
			{
				if (m_debugger.ConnectedTarget != null)
					BeginAutocomplete();
				e.SuppressKeyPress = true;
			}
			else if (e.KeyCode == Keys.Enter && e.Control)
			{
				if(m_waitingOnResult || m_debugger.ConnectionStatus != ConnectionStatus.Connected)
				{
					System.Media.SystemSounds.Asterisk.Play();
				}
				else
				{
					HtmlElement element = outputBrowser.Document.CreateElement("span");
					element.InnerHtml = ExportToHTML(inputBox) + "<br/>";
					outputBrowser.Document.Body.AppendChild(element);
					element.ScrollIntoView(false);

					if(m_debugger.CurrentStackFrame == null)
						m_debugger.ConnectedTarget.RunSnippet(inputBox.Text, m_debugger.MainThread, 0);
					else
						m_debugger.ConnectedTarget.RunSnippet(inputBox.Text, m_debugger.CurrentThread, m_debugger.CurrentStackFrame.Depth);

					if (m_history.Count == 0 || m_history[m_history.Count - 1] != inputBox.Text)
						m_history.Add(inputBox.Text);
					m_historyPosition = m_history.Count;

					inputBox.ClearAll();
					inputBox.EmptyUndoBuffer();
					SetWaitingForResult(true);
				}
				e.SuppressKeyPress = true;
			}
		}

		private void inputBox_Modified(object sender, Scintilla.ModifiedEventArgs e)
		{

		}

		private void UpdateSnippetsFolder()
		{
			GenerateSnippetsMenu();

			if (Directory.Exists(m_plugin.Options.SnippetsFolder))
			{
				m_snippetsWatcher = new DirectoryWatcher();
				m_snippetsWatcher.Watch(m_plugin.Options.SnippetsFolder, DirectoryWatcher.NotificationEvents.Created | DirectoryWatcher.NotificationEvents.Deleted | DirectoryWatcher.NotificationEvents.Modified | DirectoryWatcher.NotificationEvents.Renamed);
				m_snippetsWatcher.FileModified += new FileModifiedEventHandler(SnippetsWatcher_FileModified);
				m_snippetsWatcher.FileCreated += new FileCreatedEventHandler(SnippetsWatcher_FileCreated);
			}

			saveSnippetDialog.InitialDirectory = m_plugin.Options.SnippetsFolder;
		}

		private void GenerateSnippetsMenu()
		{
			try
			{
				snippetsMenuButton.DropDown.Items.Clear();

				if (Directory.Exists(m_plugin.Options.SnippetsFolder))
				{
					GenerateSnippetsMenu(m_plugin.Options.SnippetsFolder, snippetsMenuButton.DropDown);
				}
			}
			catch (System.Exception e)
			{
				MessageBox.Show("There was an error creating the snippets menu: " + e.ToString(), "Error creating snippets menu");
			}
		}

		private void GenerateSnippetsMenu(string path, ToolStripDropDown menu)
		{
			List<ToolStripMenuItem> items = new List<ToolStripMenuItem>();

			foreach (string dir in Directory.GetDirectories(path))
			{
				ToolStripMenuItem item = new ToolStripMenuItem();
				item.Text = Path.GetFileName(dir);
				item.Image = Properties.Resources.FolderClosed_16x16;
				GenerateSnippetsMenu(dir, item.DropDown);
				items.Add(item);
			}
			items.Sort(delegate(ToolStripMenuItem lhs, ToolStripMenuItem rhs)
				{
					return String.Compare(lhs.Text, rhs.Text, true);
				});
			menu.Items.AddRange(items.ToArray());
			items.Clear();

			foreach (string file in Directory.GetFiles(path, "*.lua"))
			{
				string description = null;

				try
				{
					TextReader reader = new StreamReader(file);
					string line = reader.ReadLine();
					if (line != null && line.Trim().StartsWith("--"))
						description = line.Substring(2).Trim();
					reader.Close();
					reader.Dispose();
				}
				catch (System.Exception)
				{
					description = "Error previewing file!";
				}

				ToolStripMenuItem item = new ToolStripMenuItem();
				item.Text = Path.GetFileNameWithoutExtension(file);
				item.Tag = file;
				item.ToolTipText = description;
				item.Click += new EventHandler(SnippetItem_Click);
				items.Add(item);
			}

			items.Sort(delegate(ToolStripMenuItem lhs, ToolStripMenuItem rhs)
				{
					return String.Compare(lhs.Text, rhs.Text, true);
				});
			menu.Items.AddRange(items.ToArray());
		}
		
		private void SaveSnippet()
		{
			if (inputBox.Text.Length > 0 && saveSnippetDialog.ShowDialog() == DialogResult.OK)
			{
				while (true)
				{
					try
					{
						TextWriter writer = new StreamWriter(saveSnippetDialog.FileName);
						writer.Write(inputBox.Text);
						writer.Close();
						writer.Dispose();
						break;
					}
					catch (Exception ex)
					{
						if (MessageBox.Show(this, ex.ToString(), "Error saving snippet", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry)
							continue;
						else
							break;
					}
				}
			}

		}

		private void BeginAutocomplete()
		{
			// Find the beginning of the expression
			int pos = inputBox.CurrentPos;
			int line = inputBox.LineFromPosition(pos);
			int linePos = pos - inputBox.PositionFromLine(line);
			string lineText = inputBox.Line[line];

			linePos = Math.Min(lineText.Length, linePos);

			string opText;
			string suffixText;
			string exprText = GetAutocompleteExpression(lineText.Substring(0, linePos), out opText, out suffixText);

			if (exprText != null)
			{
				Point popupPos = inputBox.PointToScreen(new Point(inputBox.PointXFromPosition(pos), inputBox.PointYFromPosition(pos)));
				m_autocompletePopup.Show(exprText, opText, suffixText, popupPos);

				m_autocompleteStartPos = pos - opText.Length - suffixText.Length;
				m_autocompleteEndPos = pos;
				m_autocompleteOperator = opText;
				++m_autocompleteSequenceID;

				inputBox.SetSelection(m_autocompleteStartPos, m_autocompleteEndPos);

				if (m_debugger.CurrentStackFrame == null)
					m_debugger.ConnectedTarget.RetrieveAutocompleteOptions(m_autocompleteSequenceID, exprText, m_debugger.MainThread, 0);
				else
					m_debugger.ConnectedTarget.RetrieveAutocompleteOptions(m_autocompleteSequenceID, exprText, m_debugger.CurrentThread, m_debugger.CurrentStackFrame.Depth);
			}
		}

		/* Accept expressions of reduced lua grammar:
		  	var ::= Name
			var ::= var '[' exp ']'
			var ::= var '.' Name
			exp	::= Number | String
		*/
		private string GetAutocompleteExpression(string line, out string op, out string suffix)
		{
			string start = @"\w+";
			string varname = @"\.\s*\w+";
			string whitespace = @"\s*";
			string strindexer = @"\[\s*""[^""]*\s*""\s*\]";	// Matches ["foo"] or [   "bsdfhgjh #^#^*&#@^*"  ]
			string numindexer = @"\[\s*\d+(\.\d*)?\]";		// Matches [10] or [1.33555] or [  0. ]
			string varend = @"((?<operator>[.:])\s*(?<suffix>\w*))?";
			Regex regex = new Regex("(?<expr>" + start + "(" + whitespace + "|" + varname + "|" + strindexer + "|" + numindexer + ")*)" + varend + "$");
			Match match = regex.Match(line);
			if (match.Success)
			{
				op = match.Groups["operator"].Value;
				suffix = match.Groups["suffix"].Value;
				return match.Groups["expr"].Value;
			}
			else
			{
				op = "";
				suffix = "";
				return null;
			}
		}

		void target_AutocompleteOptions(Target sender, AutocompleteOptionsEventArgs args)
		{
			if (m_autocompletePopup.Visible && args.SequenceID == m_autocompleteSequenceID)
			{
				if (args.Message != null && args.Message.Length > 0)
				{
					m_autocompletePopup.SetMessage(args.Message);
				}
				else if(args.Options != null)
				{
					m_autocompletePopup.SetOptions(args.Options);
				}
			}
		}

		void m_autocompletePopup_Selection(object sender, AutocompleteResult value)
		{
			string str = m_debugger.GetValueString(value.m_key);

			string text;
			if (value.m_key.Type == LuaValueType.NUMBER)
				text = String.Format("[{0}]", str);
			else if (m_wordRegex.IsMatch(str))
				text = String.Format("{0}{1}", m_autocompleteOperator, str);
			else
				text = String.Format("[\"{0}\"]", str);

			inputBox.ReplaceSelection(text);
		}

		void SnippetItem_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem item = (ToolStripMenuItem) sender;
			string fileName = (string) item.Tag;

			TextReader reader = new StreamReader(fileName);
			string snippet = reader.ReadToEnd();
			reader.Close();

			inputBox.InsertText(inputBox.SelectionStart, snippet);
		}
		
		private void saveSnippetButton_Click(object sender, EventArgs e)
		{
			SaveSnippet();
		}

		private void saveSnippetToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SaveSnippet();
		}

		private void clearInputToolStripMenuItem_Click(object sender, EventArgs e)
		{
			inputBox.ClearAll();
		}

		private void clearBrowserToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			ClearBrowser();
		}

	}
}

