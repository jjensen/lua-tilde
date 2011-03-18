
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
using System.Windows.Forms;
using System.IO;

using Tilde.Framework.Model;
using Tilde.Framework.Model.ProjectHierarchy;
using Tilde.Framework.View;
using WeifenLuo.WinFormsUI.Docking;
using System.Collections.ObjectModel;
using Microsoft.Win32;

namespace Tilde.Framework.Controller
{
	public class ProjectClosingEventArgs
	{
		private Project m_project;
		private bool m_cancelled;
		private bool m_cancel;
		
		public ProjectClosingEventArgs(Project project, bool cancelled)
		{
			m_project = project;
			m_cancelled = cancelled;
			m_cancel = false;
		}

		public Project Project
		{
			get { return m_project; }
		}

		public bool Cancelled
		{
			get { return m_cancelled; }
		}

		public bool Cancel
		{
			get { return m_cancel; }
			set { m_cancel = value; }
		}
	}

	public class FindInFilesResultEventArgs
	{
		string m_file;
		int m_line;
		int m_startChar;
		int m_endChar;
		string m_message;

		public FindInFilesResultEventArgs(string file, int line, string message)
		{
			m_file = file;
			m_line = line;
			m_startChar = -1;
			m_endChar = -1;
			m_message = message;
		}

		public FindInFilesResultEventArgs(string file, int line, int startChar, int endChar, string message)
		{
			m_file = file;
			m_line = line;
			m_startChar = startChar;
			m_endChar = endChar;
			m_message = message;
		}

		public string File
		{
			get { return m_file; }
		}

		public int Line
		{
			get { return m_line; }
		}

		public int StartChar
		{
			get { return m_startChar; }
		}

		public int EndChar
		{
			get { return m_endChar; }
		}

		public string Message
		{
			get { return m_message; }
		}
	}

	public delegate void ProjectOpenedEventHandler(IManager sender, Project project);
	public delegate void ProjectClosingEventHandler(IManager sender, ProjectClosingEventArgs args);
	public delegate void ProjectClosedEventHandler(IManager sender);
	public delegate void ManagerDocumentOpenedEventHandler(IManager sender, Document document);
	public delegate void ManagerDocumentClosedEventHandler(IManager sender, Document document);
	public delegate void ActiveDocumentChangedEventHandler(IManager sender, Document view);
	public delegate void SelectionChangedEventHandler(IManager sender, object [] selection);

	public delegate void FindInFilesStartedEventHandler(object sender, string message);
	public delegate void FindInFilesResultEventHandler(object sender, FindInFilesResultEventArgs args);
	public delegate void FindInFilesStoppedEventHandler(object sender, string message);

	public delegate void GoToNextLocationEventHandler(IManager sender, ref bool consumed);
	public delegate void GoToPreviousLocationEventHandler(IManager sender, ref bool consumed);

	public interface IManager
	{
		event PropertyChangeEventHandler PropertyChange;
		event ProjectOpenedEventHandler ProjectOpened;
		event ProjectClosingEventHandler ProjectClosing;
		event ProjectClosedEventHandler ProjectClosed;
		event ManagerDocumentOpenedEventHandler DocumentOpened;
		event ManagerDocumentClosedEventHandler DocumentClosed;
		event ActiveDocumentChangedEventHandler ActiveDocumentChanged;
		event SelectionChangedEventHandler SelectionChanged;

		event FindInFilesStartedEventHandler FindInFilesStarted;
		event FindInFilesResultEventHandler FindInFilesResult;
		event FindInFilesStoppedEventHandler FindInFilesStopped;

		event GoToNextLocationEventHandler GoToNextLocation;
		event GoToPreviousLocationEventHandler GoToPreviousLocation;

		RegistryKey RegistryRoot
		{
			get;
		}

		PluginCollection Plugins
		{
			get;
		}

		Project Project
		{
			get;
		}

		Form MainWindow
		{
			get;
			set;
		}

		Document ActiveDocument
		{
			get;
			set;
		}

		DocumentView ActiveView
		{
			get;
		}

		DockPanel DockPanel
		{
			get;
		}

		FileWatcher FileWatcher
		{
			get;
		}

		OptionsManager OptionsManager
		{
			get;
		}

		ApplicationOptions ApplicationOptions
		{
			get;
		}

		bool IsClosing
		{
			get;
		}

		ReadOnlyCollection<Document> Documents
		{
			get;
		}

		ReadOnlyCollection<DocumentView> DocumentViews
		{
			get;
		}

		object SelectedObject
		{
			get;
			set;
		}

		object[] SelectedObjects
		{
			get;
			set;
		}

		void AddToMenuStrip(ToolStripItemCollection toolStripItemCollection);
		void AddToStatusStrip(ToolStripItemCollection toolStripItemCollection);
		void AddToolStrip(ToolStrip toolStrip, DockStyle side, int row);

		void SetStatusMessage(String message, float duration);
		void SetProgressBar(int progress);
		void StartProgressBarMarquee();
		void StopProgressBarMarquee();
		void FlashMainWindow();

		bool LoadProject(string fileName);
		bool NewProject(Type projType);
		bool CloseProject(bool force);

		Document CreateDocument(string fileName);
		Document CreateDocument(string fileName, Type docType);
		Document CreateDocument(string fileName, Type docType, Stream stream);
		Document CreateDocument(string fileName, Type docType, Stream stream, object[] args);
		Document OpenDocument(DocumentItem docItem);
		Document OpenDocument(string fileName);
		Document OpenDocument(string fileName, Type docType);
		Document OpenDocument(string fileName, Type docType, object[] args);
		Document FindOpenDocument(string fileName);
		DocumentView ShowDocument(DocumentItem docItem);
		DocumentView ShowDocument(string fileName);
		DocumentView ShowDocument(Document doc);
		bool CloseDocument(Document doc, bool force);
		bool CloseAllDocuments(bool force);
		int SaveAllDocuments();

		Type FindFileDocumentType(string docName);

		void ShowMessages(string type);
		void AddMessage(string type, string message);

		IPlugin GetPlugin(Type type);
		ToolWindow GetToolWindow(Type type);
		List<Type> GetPluginImplementations(Type interfaceType);

		int Execute(string logtype, string args);
		int Execute(string logtype, string args, StringBuilder output);

		void OnFindInFilesStarted(object sender, string message);
		void OnFindInFilesResult(object sender, FindInFilesResultEventArgs args);
		void OnFindInFilesStopped(object sender, string message);

		void OnGoToNextLocation();
		void OnGoToPreviousLocation();


	}
}
