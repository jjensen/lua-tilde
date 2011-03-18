
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
using System.IO;
using System.Xml;
using Microsoft.Win32;

using Tilde.Framework.Model;
using Tilde.Framework.Model.ProjectHierarchy;
using Tilde.Framework.View;

using WeifenLuo.WinFormsUI.Docking;
using Tilde.Framework.Controller;
using System.Diagnostics;
using Tilde.Framework.Controls;
using System.Collections.ObjectModel;

namespace Tilde.TildeApp
{
	public partial class MainWindow : Form
	{
		private Manager mManager;
		private List<DocumentView> mDocumentViews;
		private List<IDockContent> mDockContentHistory;
		private PluginsWindow mPluginsWindow;
		private OptionsWindow mOptionsWindow;
		private FindFileInProjectWindow mFindFileInProjectWindow;
		private DocumentSwitchWindow mDocumentSwitchWindow;
		private List<ToolWindow> mToolWindows;
		private MenuStrip mCurrentDocumentMenuStrip;

		private List<string> mRecentProjects;
		
		public MainWindow(Manager manager)
		{
			mManager = manager;

			InitializeComponent();

			mDocumentViews = new List<DocumentView>();
			mToolWindows = new List<ToolWindow>();

			mManager.MainWindow = this;
			mManager.PropertyChange += new PropertyChangeEventHandler(Manager_PropertyChange);
			mManager.ProjectOpened += new ProjectOpenedEventHandler(Manager_ProjectOpened);
			mManager.ProjectClosed += new ProjectClosedEventHandler(Manager_ProjectClosed);

			mDockContentHistory = new List<IDockContent>();
			mPluginsWindow = new PluginsWindow(this);
			mOptionsWindow = new OptionsWindow(this);
			mFindFileInProjectWindow = new FindFileInProjectWindow(this);
			mDocumentSwitchWindow = new DocumentSwitchWindow(this);
			
			// Get recent projects list
			mRecentProjects = new List<string>();
			string data;
			data = (string)mManager.RegistryRoot.GetValue("RecentProjects");
			if (data != null)
			{
				foreach (string proj in data.Split(new char[] { ';' }))
				{
					if(String.Compare(System.IO.Path.GetExtension(proj), ".xml", true) == 0 ||
                       String.Compare(System.IO.Path.GetExtension(proj), ".vcproj", true) == 0)
						mRecentProjects.Add(proj);
				}
			}

			UpdateRecentProjects();

			// Set up open document dialog
			StringBuilder filter = new StringBuilder();
			foreach(Type docType in mManager.GetPluginImplementations(typeof(Document)))
			{
				DocumentClassAttribute attr = DocumentClassAttribute.ForType(docType);
				if (attr != null)
				{
					string extensions = String.Join(";", Array.ConvertAll<string, string>(attr.FileExtensions, delegate(string ext) { return "*" + ext; }));
					filter.Append(attr.Name + "(" + extensions + ")|" + extensions + "|");
				}
			}
			filter.Append("All files (*.*)|*.*");
			openDocumentDialog.Filter = filter.ToString();

			// Set up new project menu
			foreach(Type projType in mManager.GetPluginImplementations(typeof(Project)))
			{
				ProjectClassAttribute attr = ProjectClassAttribute.ForType(projType);
				ConstructorInfo constructor = projType.GetConstructor(new Type [] { typeof(IManager) } );
				if (constructor != null)
				{
					ToolStripMenuItem item = new ToolStripMenuItem(attr.Name, null, new EventHandler(NewProject_Click));
					item.Tag = projType;
					tsiFileNewProject.DropDownItems.Add(item);
				}
			}

			tsiFileNewProject.Enabled = tsiFileNewProject.DropDownItems.Count != 0;

			sourceControlToolStripMenuItem.Enabled = false;
		}

		public Manager Manager
		{
			get { return mManager; }
		}

		public DocumentView ActiveView
		{
			get { return mDockPanel.ActiveDocument as DocumentView; }
			set	{ value.Show(mDockPanel); }
		}

		public ReadOnlyCollection<DocumentView> DocumentViews
		{
			get { return mDocumentViews.AsReadOnly(); }
		}

		public List<IDockContent> DockContentHistory
		{
			get { return mDockContentHistory; }
		}

		public DockPanel DockPanel
		{
			get { return mDockPanel; }
		}

		public ToolWindow GetToolWindow(Type type)
		{
			foreach(ToolWindow window in mToolWindows)
			{
				if (type.IsInstanceOfType(window))
					return window;
			}
			return null;
		}

		public DocumentView ShowDocument(Document doc)
		{
			Type viewType = ChooseViewType(doc);
			if (viewType == null)
			{
				MessageBox.Show(this, "Tilde cannot open the file '" + doc.FileName + "' because no view class is specified for documents of type " + typeof(Document).ToString() + ".");
				return null;
			}
			else
				return ShowDocument(doc, viewType);
		}

		public DocumentView ShowDocument(Document doc, Type viewType)
		{
			DocumentView window = OpenDocument(doc, viewType, false);

			window.Show(mDockPanel);
			window.Activate();
			return window;
		}

		public void SetStatusMessage(String message, float duration)
		{
			statusMessage.Text = message;

			statusMessageTimer.Stop();
			if (duration > 0)
			{
				statusMessageTimer.Interval = (int) (duration * 1000.0f);
				statusMessageTimer.Start();
			}
		}

		public void SetProgressBar(int progress)
		{
			if (progress <= 0)
			{
				toolStripProgressBar.Visible = false;
			}
			else if (progress <= 100)
			{
				toolStripProgressBar.Style = ProgressBarStyle.Continuous;
				toolStripProgressBar.Value = progress;
				toolStripProgressBar.Visible = true;
			}
		}

		public void StartProgressBarMarquee()
		{
			toolStripProgressBar.Style = ProgressBarStyle.Marquee;
			toolStripProgressBar.Value = 0;
			toolStripProgressBar.Visible = true;
		}

		public void StopProgressBarMarquee()
		{
			toolStripProgressBar.Visible = false;
		}

		private void ClearRecentProjects()
		{
			mManager.RegistryRoot.DeleteValue("RecentProjects");
			mRecentProjects.Clear();
			UpdateRecentProjects();
		}

		private void AddToRecentProjects(string filename)
		{
			// Add to recent projects list
			if(mRecentProjects.Contains(filename))
				mRecentProjects.Remove(filename);
			mRecentProjects.Insert(0, filename);
			if(mRecentProjects.Count > 10)
				mRecentProjects.RemoveRange(10, mRecentProjects.Count - 10);
			mManager.RegistryRoot.SetValue("RecentProjects", String.Join(";", mRecentProjects.ToArray(), 0, mRecentProjects.Count));
			UpdateRecentProjects();
		}

		private void UpdateRecentProjects()
		{
			tsiFileRecentProjects.DropDownItems.Clear();
			int index = 1;
			foreach(string proj in mRecentProjects)
			{
				string label = String.Format("&{0} {1}", new Object [] { index++, proj });
				tsiFileRecentProjects.DropDownItems.Add(new ToolStripMenuItem(label, null, new EventHandler(RecentProject_Click)));
			}
			tsiFileRecentProjects.Enabled = mRecentProjects.Count > 0;
		}

		void DocumentView_FormClosing(object sender, FormClosingEventArgs e)
		{
			DocumentView docView = (DocumentView)sender;
			if (!docView.Document.Closing && !Manager.CloseDocument(docView.Document, false))
				e.Cancel = true;
		}

		void DocumentView_FormClosed(object sender, FormClosedEventArgs e)
		{
			DocumentView docView = (DocumentView)sender;
			mDocumentViews.Remove(docView);
			docView.Document.Views.Remove(docView);
		}

		private void NewProject(Type projType, bool force)
		{
			if (CloseProject(force))
				Manager.NewProject(projType);
		}

		private Type ChooseViewType(Document doc)
		{
			DocumentClassAttribute attr = DocumentClassAttribute.ForType(doc.GetType());
			if (attr == null)
				return null;
			else
				return attr.ViewType;
		}

		private DocumentView FindDocument(Document doc, Type viewType)
		{
			foreach (DocumentView window in mDocumentViews)
			{
				if (window.Document == doc && viewType.IsInstanceOfType(window))
					return window;
			}
			return null;
		}

		private DocumentView OpenDocument(Document doc, Type viewType, bool force)
		{
			DocumentView window = FindDocument(doc, viewType);
			if (window == null || force)
			{
				window = (DocumentView) Activator.CreateInstance(viewType, new object[] { mManager, doc } );

				window.FormClosing += new FormClosingEventHandler(DocumentView_FormClosing);
				window.FormClosed += new FormClosedEventHandler(DocumentView_FormClosed);
				doc.Saving += new DocumentSavingEventHandler(Document_Saving);
				doc.Saved += new DocumentSavedEventHandler(Document_Saved);
				doc.PropertyChange += new PropertyChangeEventHandler(Document_PropertyChange);
				doc.Views.Add(window);
				mDocumentViews.Add(window);
			}
			return window;
		}

		void Document_Saving(Document sender)
		{
			SetStatusMessage("Saving " + sender.FileName + "...", 0);
		}

		void Document_Saved(Document sender, bool success)
		{
			if(success)
				SetStatusMessage("Saved " + sender.FileName, 5.0f);
			else
				SetStatusMessage("FAILED saving " + sender.FileName, 5.0f);
		}

		void Document_PropertyChange(object sender, PropertyChangeEventArgs args)
		{
			if(args.Property == "ReadOnly")
			{
				tsiFileSave.Enabled = (mManager.ActiveDocument != null) && !mManager.ActiveDocument.ReadOnly;
			}
		}

		private void mDockPanel_ContentAdded(object sender, DockContentEventArgs e)
		{
			mDockContentHistory.Add(e.Content);
		}

		private void mDockPanel_ContentRemoved(object sender, DockContentEventArgs e)
		{
			mDockContentHistory.Remove(e.Content);
		}

		private void mDockPanel_ActiveContentChanged(object sender, EventArgs e)
		{
			if(mDockPanel.ActiveContent != null)
			{
				mDockContentHistory.Remove(mDockPanel.ActiveContent);
				mDockContentHistory.Insert(0, mDockPanel.ActiveContent);
			}
		}

		private void mDockPanel_ActiveDocumentChanged(object sender, EventArgs e)
		{
			if (mDockPanel.ActiveDocument == null)
				mManager.ActiveDocument = null;
			else
			{
				mCurrentDocumentMenuStrip = mDockPanel.ActiveDocument.DockHandler.Form.MainMenuStrip;
				if (mCurrentDocumentMenuStrip != null)
					ToolStripManager.Merge(mCurrentDocumentMenuStrip, this.MainMenuStrip);
				else
					ToolStripManager.RevertMerge(this.MainMenuStrip);

				if (mDockPanel.ActiveDocument is DocumentView)
				{
					DocumentView view = mDockPanel.ActiveDocument as DocumentView;
					mManager.ActiveDocument = view.Document;
				}
				else
					mManager.ActiveDocument = null;
			}

			tsiFileSave.Enabled = (mManager.ActiveDocument != null) && !mManager.ActiveDocument.ReadOnly;
			tsiFileSaveAs.Enabled = (mManager.ActiveDocument != null);
		}

		private void MainWindow_Load(object sender, EventArgs e)
		{
			if (mRecentProjects.Count > 0 && Manager.ApplicationOptions.LoadProjectOnStartup)
			{
				try
				{
					LoadProject(mRecentProjects[0]);
				}
				catch(Exception)
				{
					// Ignore errors when auto-loading the project
				}
			}
		}

		private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
		{
			e.Cancel = !CloseProject(false);
		}

		private bool CloseProject(bool force)
		{
			if (mManager != null && mManager.Project != null)
			{
				System.IO.MemoryStream stream = new System.IO.MemoryStream();
				mDockPanel.SaveAsXml(stream, System.Text.Encoding.ASCII);
				byte[] bytes = stream.ToArray();
				char[] chars = new char[bytes.Length];
				Array.Copy(bytes, chars, bytes.Length);

				XmlDocument doc = new XmlDocument();
				doc.Load(new StringReader(new String(chars)));
				mManager.Project.SetUserConfigurationXML("DockPanelState", doc.SelectSingleNode("DockPanel").OuterXml);

				if (!mManager.CloseProject(force))
					return false;
			}

			foreach (ToolWindow window in mToolWindows)
			{
				window.DockHandler.DockPanel = null;
			}

			return true;
		}

		private void tsiFileExit_Click(object sender, EventArgs e)
		{
			if (CloseProject(false))
				this.Close();
		}

		private void tsiFileSaveProject_Click(object sender, EventArgs e)
		{
			/*
			if (mManager.Project.FileName == "")
			{
				DialogResult result = saveProjectDialog.ShowDialog();
				if (result == DialogResult.OK)
				{
					mManager.Project.FileName = saveProjectDialog.FileName;
				}
				else
					return;
			}
			*/
			Manager.SaveProject();
		}
		
		private void pluginsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			mPluginsWindow.Show();
		}

		private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			mOptionsWindow.ShowDialog(this);
		}

		private void sourceControlToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SourceControlWindow window = new SourceControlWindow(Manager);
			window.ShowDialog(this);
		}

		public void CreateToolWindows()
		{
			this.CreateHandle();

			foreach (Assembly assembly in Manager.Assemblies)
			{
				foreach (Type type in assembly.GetTypes())
				{
					if (typeof(ToolWindow).IsAssignableFrom(type) && type.IsClass)
					{
						ToolWindowAttribute attr = ToolWindowAttribute.ForType(type);
						if (attr == null)
							continue;

						ToolWindow panel = Activator.CreateInstance(type, new object[] { Manager }) as ToolWindow;
						IntPtr handle = panel.Handle;	// Make sure we can call Invoke(), even if it isn't opened
						mToolWindows.Add(panel);

						ToolStripMenuItem parent;	
						if(attr.Group == "")
						{
							parent = viewToolStripMenuItem;
						}
						else
						{
							ToolStripItem [] candidates = viewToolStripMenuItem.DropDownItems.Find(attr.Group, false);
							if(candidates.Length > 0)
							{
								parent = (ToolStripMenuItem) candidates[0];
							}
							else
							{
								parent = new ToolStripMenuItem(attr.Group);
								parent.Name = attr.Group;
								viewToolStripMenuItem.DropDownItems.Add(parent);
							}
						}

						ToolStripMenuItem menu = new ToolStripMenuItem(panel.TabText);
						menu.Tag = panel;
						menu.Click += new EventHandler(ToolMenu_Click);
						parent.DropDownItems.Add(menu);
					}
				}
			}
		}

		private void OpenAllToolWindows()
		{
			foreach(ToolWindow panel in mToolWindows)
			{
				if (!IsPanelOpen(panel))
					panel.Show(mDockPanel);
			}
		}

		private bool IsPanelOpen(DockContent panel)
		{
			return panel.DockState != DockState.Hidden && panel.DockState != DockState.Unknown;
		}

		private void viewToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
		{
			CheckViewMenu(viewToolStripMenuItem);
		}

		private void CheckViewMenu(ToolStripMenuItem root)
		{
			foreach(ToolStripMenuItem item in root.DropDownItems)
			{
				if (item.Tag is ToolWindow)
					item.Checked = IsPanelOpen(item.Tag as ToolWindow);
				CheckViewMenu(item);
			}
		}

		void ToolMenu_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem menu = (ToolStripMenuItem)sender;
			ToolWindow panel = (ToolWindow)menu.Tag;

			if (IsPanelOpen(panel))
				panel.Hide();
			else
				panel.Show(mDockPanel);
		}

		// throws exceptions on failure
		private void LoadProject(string name)
		{
			// The user can cancel closing the windows if a document has not been saved
			if (!CloseProject(false))
				return;

			AddToRecentProjects(name);
			mManager.LoadProject(name);

			if (mManager.Project.GetUserConfiguration("DockPanelState") != "")
			{
				System.IO.MemoryStream stream = new System.IO.MemoryStream();
				StreamWriter writer = new StreamWriter(stream);
				writer.Write(mManager.Project.GetUserConfiguration("DockPanelState"));
				writer.Flush();
				stream.Position = 0;

				DeserializeDockContent deserialiser = new DeserializeDockContent(GetContentFromPersistString);
				mDockPanel.LoadFromXml(stream, deserialiser);
			}
			else
			{
				OpenAllToolWindows();
			}
		}

		private IDockContent GetContentFromPersistString(string persistType, string persistString)
		{
			
			Type type = System.Type.GetType(persistType);

			// Look for a tool window
			if(typeof(ToolWindow).IsAssignableFrom(type))
			{
				foreach (ToolWindow window in mToolWindows)
				{
					if (type == window.GetType())
					{
						window.ConfigureFromPersistString(persistString);
						return window;
					}
				}
			}

			// Try as a document
			else if(typeof(DocumentView).IsAssignableFrom(type))
			{
				Document doc = Manager.OpenDocument(persistString);
				if (doc == null)
					return null;

				if (type == null)
					type = ChooseViewType(doc);

				return OpenDocument(doc, type, true);	// Force one to open, because DockPanel doesn't like it when we re-use a window here
			}

			// Give up
			return null;
		}

		void Manager_ProjectOpened(object sender, Project project)
		{
			this.Text = project.ProjectName + " - Tilde";
			tsiFileSaveProject.Enabled = project.Modified;
			sourceControlToolStripMenuItem.Enabled = true;
		}

		void Manager_ProjectClosed(IManager sender)
		{
			sourceControlToolStripMenuItem.Enabled = false;
		}

		void Manager_PropertyChange(object sender, PropertyChangeEventArgs args)
		{
			if (sender is Manager)
			{
				if(args.Property == "Project" && mManager.Project != null)
				{
//					mManager.Project.Saving += new DocumentSavingEventHandler(Project_Saving);
//					mManager.Project.Saved += new DocumentSavedEventHandler(Project_Saved);
					mManager.Project.PropertyChange += new PropertyChangeEventHandler(Project_PropertyChange);
				}
			}
		}

		void Project_PropertyChange(object sender, PropertyChangeEventArgs args)
		{
			if(sender is Project)
			{
				if(args.Property == "Modified")
				{
					if((bool) args.NewValue == false)
						this.Text = ((Project) sender).ProjectName + " - Tilde";
					else
						this.Text = ((Project) sender).ProjectName + " * - Tilde";

					tsiFileSaveProject.Enabled = (bool)args.NewValue;
				}
			}
		}

		/*
		void Project_Saving(Document sender)
		{
			Document_Saving(sender);
		}

		void Project_Saved(Document sender)
		{
			Document_Saved(sender);
		}
		*/

		private void RecentProject_Click(object sender, System.EventArgs e)
		{
			ToolStripMenuItem item = (ToolStripMenuItem)sender;
			string fileName = item.Text.Substring(item.Text.IndexOf(" ") + 1);
			try
			{
				LoadProject(fileName);
			}
			catch (Exception ex)
			{
				MessageBox.Show(this, "Error loading project " + fileName + "\r\n\r\n" + ex.ToString(), "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void tsiFileOpenProject_Click(object sender, EventArgs e)
		{
			if (openProjectDialog.ShowDialog() == DialogResult.OK)
			{
				string fileName = openProjectDialog.FileName;
				try
				{
					LoadProject(fileName);
				}
				catch (Exception ex)
				{
					MessageBox.Show(this, "Error loading project " + fileName + "\r\n\r\n" + ex.ToString(), "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void NewProject_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem item = (ToolStripMenuItem)sender;

			NewProject((Type) item.Tag, false);
		}

		private void tsiFileCloseProject_Click(object sender, EventArgs e)
		{
			CloseProject(false);
		}

		private void closeAllDocumentsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			while(mDocumentViews.Count > 0)
			{
				DocumentView view = mDocumentViews[mDocumentViews.Count - 1];
				view.Close();
				if (view.Visible)
					break;
			}
		}

		private void tsiFileSave_Click(object sender, EventArgs e)
		{
			if (mManager.ActiveDocument != null)
				SaveDocument(mManager.ActiveDocument, false);
		}

		private void tsiFileSaveAs_Click(object sender, EventArgs e)
		{
			if (mManager.ActiveDocument != null)
				SaveDocument(mManager.ActiveDocument, true);
		}

		private void statusMessageTimer_Tick(object sender, EventArgs e)
		{
			statusMessage.Text = "Ready";
		}

		private void tsiFileSaveAll_Click(object sender, EventArgs e)
		{
			mManager.SaveAllDocuments();
		}

		public bool SaveDocument(Document doc, bool saveAs)
		{
			if(!doc.OnDisk || saveAs)
			{
				saveDocumentDialog.FileName = doc.FileName;

				DocumentClassAttribute attr = DocumentClassAttribute.ForType(doc.GetType());
				string extensions = String.Join(";", Array.ConvertAll<string, string>(attr.FileExtensions, delegate(string ext) { return "*" + ext; }));
				saveDocumentDialog.Filter = attr.Name + "(" + extensions + ")|" + extensions + "|All files (*.*)|*.*";
				saveDocumentDialog.DefaultExt = "*" + attr.FileExtensions[0];

				if (saveDocumentDialog.ShowDialog() == DialogResult.OK)
				{
					// If first time it's been saved then rename it in the project
					if(!saveAs)
					{
						DocumentItem documentItem = Manager.Project.FindDocument(doc.FileName);
						if (documentItem != null)
							Manager.Project.RenameDocument(documentItem, saveDocumentDialog.FileName);
					}

					doc.FileName = saveDocumentDialog.FileName;
				}
				else
					return false;
			}

			return doc.SaveDocument();
		}

		private void tsiFileOpenFile_Click(object sender, EventArgs e)
		{
			if(openDocumentDialog.ShowDialog() == DialogResult.OK)
			{
				foreach (string file in openDocumentDialog.FileNames)
				{
					Document doc = mManager.OpenDocument(file);
					if (doc != null)
						ShowDocument(doc);
				}
			}
		
		}

		private void notifyIcon_BalloonTipClicked(object sender, EventArgs e)
		{
			this.Focus();
		}

		private void notifyIcon_DoubleClick(object sender, EventArgs e)
		{
			this.Focus();
		}

		private void projectWebSiteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Process proc = new Process();
			proc.StartInfo.FileName = "http://luaforge.net/projects/tilde/";
			proc.StartInfo.UseShellExecute = true;
			proc.Start();
		}

		private void reportBugToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Process proc = new Process();
			proc.StartInfo.FileName = "http://luaforge.net/tracker/?atid=1727&group_id=421&func=browse";
			proc.StartInfo.UseShellExecute = true;
			proc.Start();
		}

		private void requestFeatureToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Process proc = new Process();
			proc.StartInfo.FileName = "http://luaforge.net/tracker/?atid=1730&group_id=421&func=browse";
			proc.StartInfo.UseShellExecute = true;
			proc.Start();
		}


		private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			AboutWindow window = new AboutWindow();
			window.ShowDialog(this);
		}

		private void findFileInProjectToolStripMenuItem_Click(object sender, EventArgs e)
		{
			mFindFileInProjectWindow.Show(this);
		}

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			//	Console.WriteLine("ProcessCmdKey(" + msg.ToString() + ", " + keyData.ToString() + ")");
			if (keyData == (Keys.Control | Keys.Tab))
			{
				mDocumentSwitchWindow.UpdateWindows();
				if (mDocumentSwitchWindow.ShowDialog(this) == DialogResult.OK && mDocumentSwitchWindow.SelectedContent != null)
				{
					mDocumentSwitchWindow.SelectedContent.Show(this.DockPanel);
					mDocumentSwitchWindow.SelectedContent.Focus();
				}

				return true;
			}
			else
			{
				Control focused = Control.FromChildHandle(Win32.GetFocus());
                if (
                        focused == null
                    || focused.FindForm() is DocumentView
                    || (
                            keyData != (Keys.Control | Keys.X)
                        && keyData != (Keys.Control | Keys.C)
                        && keyData != (Keys.Control | Keys.V)
                        && keyData != (Keys.Control | Keys.Z)
                        && keyData != (Keys.Control | Keys.Y)
                        )
                    )
                    return base.ProcessCmdKey(ref msg, keyData);
				else
					return false;
			}
		}

		private void goToNextLocationToolStripMenuItem_Click(object sender, EventArgs e)
		{
			mManager.OnGoToNextLocation();
		}

		private void goToPreviousLocationToolStripMenuItem_Click(object sender, EventArgs e)
		{
			mManager.OnGoToPreviousLocation();
		}

		private void MainWindow_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				e.Effect = DragDropEffects.Copy;
			}
		}

		private void MainWindow_DragDrop(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent("FileName"))
			{
				String[] files = (String[])e.Data.GetData(DataFormats.FileDrop);

				foreach (String file in files)
				{
					mManager.ShowDocument(file);
				}
			}
		}

		protected override void WndProc(ref Message m)
		{
			switch(m.Msg)
			{
				case Win32.WM_ACTIVATEAPP:
					bool appActive = (((int)m.WParam != 0));
					System.Diagnostics.Debug.Print("WM_ACTIVATEAPP(" + appActive.ToString() + ")");
					if (!appActive)
						Manager.FileWatcher.Pause();
					else
						Manager.FileWatcher.Resume();
					break;

			}
			base.WndProc(ref m);
		}

	}
}
