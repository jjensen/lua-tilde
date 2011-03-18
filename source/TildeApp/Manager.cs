
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
using System.Collections.ObjectModel;
using System.Text;
using System.Reflection;
using System.IO;
using System.Windows.Forms;

using Tilde.Framework.Controller;
using Tilde.Framework.Model;
using Tilde.Framework.Model.ProjectHierarchy;
using Tilde.Framework.View;
using System.Text.RegularExpressions;
using WeifenLuo.WinFormsUI.Docking;
using Tilde.Framework;
using System.Diagnostics;
using Tilde.Framework.Controls;
using Microsoft.Win32;

namespace Tilde.TildeApp
{
	public class Manager : IManager
	{
		private RegistryKey mRegistryRoot;
		private List<Assembly> mAssemblies;
		private List<string> mPluginPath;
		private PluginCollection mPlugins;
		private Project mProject;
		private List<Document> mOpenDocuments;
		private Dictionary<string, Type> mFileTypes;
		private MainWindow mMainWindow;
		private Document mActiveDocument;
		private object[] mSelection;

		private FileWatcher mFileWatcher;

		private OptionsManager mOptionsManager;
		private ApplicationOptions mApplicationOptions;

		private bool m_closing = false;

		public Manager(CommandLineArguments args)
		{
			mAssemblies = new List<Assembly>();
			mPlugins = new PluginCollection();
			mOpenDocuments = new List<Document>();
			mFileTypes = new Dictionary<string, Type>();
			mActiveDocument = null;

			mFileWatcher = new FileWatcher();
			mFileWatcher.FileModified += new FileModifiedEventHandler(FileWatcher_FileModified);
			mFileWatcher.FileAttributesChanged += new FileAttributesChangedEventHandler(FileWatcher_FileAttributesChanged);

			mOptionsManager = new OptionsManager(this);
			mApplicationOptions = new ApplicationOptions();
			mOptionsManager.Options.Add(mApplicationOptions);

			mAssemblies.Add(Assembly.GetExecutingAssembly());

			mPluginPath = new List<string>();
			string currentDirectory = System.IO.Directory.GetCurrentDirectory();
			string exeDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			mPluginPath.Add(currentDirectory);
			if(currentDirectory != exeDirectory)
				mPluginPath.Add(exeDirectory);

			foreach (string path in args.GetValues("--pluginFolder"))
				mPluginPath.Add(path);

			foreach (string path in mPluginPath)
			{
				FindPlugins(path);
			}

			mRegistryRoot = Registry.CurrentUser.OpenSubKey("Software\\Tantalus\\Tilde", true);
			if (mRegistryRoot == null)
				mRegistryRoot = Registry.CurrentUser.CreateSubKey("Software\\Tantalus\\Tilde");

			CreatePlugins();
		}

		public event PropertyChangeEventHandler PropertyChange;
		public event ProjectOpenedEventHandler ProjectOpened;
		public event ProjectClosingEventHandler ProjectClosing;
		public event ProjectClosedEventHandler ProjectClosed;
		public event ManagerDocumentOpenedEventHandler DocumentOpened;
		public event ManagerDocumentClosedEventHandler DocumentClosed;
		public event ActiveDocumentChangedEventHandler ActiveDocumentChanged;
		public event SelectionChangedEventHandler SelectionChanged;

		public event FindInFilesStartedEventHandler FindInFilesStarted;
		public event FindInFilesResultEventHandler FindInFilesResult;
		public event FindInFilesStoppedEventHandler FindInFilesStopped;

		public event GoToNextLocationEventHandler GoToNextLocation;
		public event GoToPreviousLocationEventHandler GoToPreviousLocation;

		public RegistryKey RegistryRoot
		{
			get { return mRegistryRoot; }
		}

		public List<Assembly> Assemblies
		{
			get { return mAssemblies; }
		}

		public PluginCollection Plugins
		{
			get { return mPlugins;  }
		}

		public Project Project
		{
			get { return mProject; }
		}

		public Form MainWindow
		{
			get { return mMainWindow; }
			set
			{
				mMainWindow = (MainWindow) value; InitialisePlugins();
			}
		}

		public Document ActiveDocument
		{
			get { return mActiveDocument; }
			set
			{
				if (mActiveDocument != value)
				{
					mActiveDocument = value;
					OnActiveDocumentChanged();
				}
			}
		}

		public DocumentView ActiveView
		{
			get { return mMainWindow.ActiveView; }
		}

		public DockPanel DockPanel
		{
			get { return mMainWindow.DockPanel; }
		}

		public ReadOnlyCollection<Document> OpenDocuments
		{
			get { return mOpenDocuments.AsReadOnly(); }
		}

		public FileWatcher FileWatcher
		{
			get { return mFileWatcher;  }
		}

		public OptionsManager OptionsManager
		{
			get { return mOptionsManager; }
		}

		public ApplicationOptions ApplicationOptions
		{
			get { return mApplicationOptions; }
		}

		public bool IsClosing
		{
			get { return m_closing; }
		}

		public ReadOnlyCollection<Document> Documents
		{
			get { return mOpenDocuments.AsReadOnly(); }
		}

		public ReadOnlyCollection<DocumentView> DocumentViews
		{
			get { return mMainWindow.DocumentViews; }
		}

		public object SelectedObject
		{
			get 
			{
				if (mSelection != null && mSelection.Length > 0)
					return mSelection[0];
				else
					return null;
			}
			set 
			{ 
				if(mSelection == null || mSelection.Length != 1 || mSelection[0] != value)
				{
					mSelection = value == null ? null : new object[1] { value };
					OnSelectionChanged();
				}
			}
		}

		public object [] SelectedObjects
		{
			get 
			{ 
				return mSelection; 
			}
			set 
			{
				if (mSelection != value)
				{
					mSelection = value;
					OnSelectionChanged();
				}
			}
		}

		#region Projects

		private Type FindProjectLoader(string fileName)
		{
			foreach (Type projType in GetPluginImplementations(typeof(Project)))
			{
				MethodInfo method = (MethodInfo) projType.GetMember("CanLoad")[0];
				if (method != null && method.IsStatic)
				{
					bool result = (bool)method.Invoke(null, new object[] { fileName });
					if (result)
						return projType;
				}
			}
			return null;
		}

		public bool LoadProject(string fileName)
		{
			if (mProject != null && !CloseProject(false))
				return false;

			Type projType = FindProjectLoader(fileName);

			if (projType == null)
				throw new ApplicationException("Couldn't find loader for project: " + fileName);

			mProject = (Project)Activator.CreateInstance(projType, new object[] { this, fileName });

			if(mProject.VCSType == "")
			{
				if(MessageBoxEx.Show(MainWindow, "Source control has not been configured for this project. Would you like to configure it now?", "Source control configuration", new string [] {"Yes", "No"}, MessageBoxIcon.Question, "Yes" ) == "Yes")
				{
					SourceControlWindow window = new SourceControlWindow(this);
					window.ShowDialog(MainWindow);
				}
			}

			try
			{
				// Plugins might fail here
				OnProjectOpened();
				OnPropertyChange(this, "Project", null, mProject);
			}
			catch (System.Exception e)
			{
				DestroyProject();
				throw e;
			}
			return true;
		}

		public bool NewProject(Type projType)
		{
			if (mProject != null && !CloseProject(false))
				return false;

			mProject = (Project)Activator.CreateInstance(projType, new object[] { this });

			try
			{
				// Plugins might fail here
				OnProjectOpened();
				OnPropertyChange(this, "Project", null, mProject);
			}
			catch (System.Exception e)
			{
				DestroyProject();
				throw e;
			}
			return true;
		}

		public bool CloseProject(bool force)
		{
			if (mProject == null)
				throw new NullReferenceException("Attempt to close non-existent project");

			m_closing = true;
			try
			{
				//.Let the listeners know the project is closing and give them a chance to cancel it
				if (OnProjectClosing(false) && !force)
					return false;

				if (!CloseAllDocuments(false) && !force)
				{
					// Tell the listeners the project didn't actually get closed after all
					OnProjectClosing(true);
					return false;
				}

				mProject.SaveUserConfig();

				Project oldproject = mProject;
				DestroyProject();
				OnProjectClosed();
				OnPropertyChange(this, "Project", oldproject, null);
			}
			finally
			{
				m_closing = false;
			}

			return true;
		}

		private void DestroyProject()
		{
			if (mProject != null)
			{
				mProject = null;
			}
		}

		#endregion

		#region Plugins
		public IPlugin GetPlugin(Type type)
		{
			foreach (PluginDetails plugin in mPlugins)
			{
				if (plugin.Plugin != null && type.IsAssignableFrom(plugin.Plugin.GetType()))
					return plugin.Plugin;
			}

			return null;
		}

		public List<Type> GetPluginImplementations(Type interfaceType)
		{
			List<Type> result = new List<Type>();
			foreach (PluginDetails details in mPlugins)
			{
				if (details.Assembly != null)
				{
					foreach (Type type in details.Assembly.GetTypes())
					{
						if (interfaceType.IsAssignableFrom(type) && type != interfaceType && !type.IsAbstract)
						{
							result.Add(type);
						}
					}
				}
			}
			return result;
		}

		private void FindPlugins(string folder)
		{
			foreach(string path in Directory.GetFiles(folder,  "*.plugin.dll"))
			{
				string name = Path.GetFileNameWithoutExtension(Path.GetFileNameWithoutExtension(path));
				PluginDetails details = new PluginDetails(name, path);

				if (OptionsManager.RegistryDatabase.GetBooleanOption("Application/Plugins/" + name, true))
				{
					try
					{
						LoadPluginInternal(details);
					}
					catch (System.Exception e)
					{
						DialogResult result = System.Windows.Forms.MessageBox.Show(
							"Tilde could not load the plugin:\r\n\t" + details.Path + "\r\n\r\n" + e.Message + "\r\n\r\nDo you want Tilde to attempt to load it next time?",
							"Plugin load error",
							MessageBoxButtons.YesNo,
							MessageBoxIcon.Exclamation);

						if(result == DialogResult.No)
							OptionsManager.RegistryDatabase.SetBooleanOption("Application/Plugins/" + name, false);
					}
				}
			}
		}

		public void LoadPlugin(PluginDetails plugin)
		{
		}

		/// <summary>
		/// Loads the assembly and looks for a class of type IPlugin.
		/// </summary>
		/// <param name="plugin">Plugin to load.</param>
		/// <returns>True if the assembly loaded correctly and contains a valid Tilde plugin.</returns>
		/// <remarks>Throws an exception if loading the assembly failed.</remarks>
		private bool LoadPluginInternal(PluginDetails plugin)
		{
			Assembly assembly = Assembly.LoadFile(plugin.Path);

			foreach(PluginDetails details in mPlugins)
			{
				if (details.Assembly.FullName == assembly.FullName)
					return false;
			}

			foreach (Type type in assembly.GetTypes())
			{
				if (typeof(IPlugin).IsAssignableFrom(type) && type != typeof(IPlugin))
				{
					plugin.Assembly = assembly;
					mAssemblies.Add(assembly);
					mPlugins.Add(plugin);
					return true;
				}
			}

			return false;
		}

		private void CreatePlugins()
		{
			foreach (PluginDetails details in mPlugins)
			{
				if (details.Assembly != null && details.Plugin == null)
				{
					foreach (Type type in details.Assembly.GetTypes())
					{
						if (typeof(IPlugin).IsAssignableFrom(type) && type.IsClass)
						{
							IPlugin plugin = Activator.CreateInstance(type) as IPlugin;
							details.Plugin = plugin;
						}
					}
				}
			}
		}

		private void InitialisePlugins()
		{
			foreach (PluginDetails details in mPlugins)
			{
				if (details.Plugin != null)
				{
					details.Plugin.Initialise(this);
				}
			}

			foreach (Type docType in GetPluginImplementations(typeof(Document)))
			{
				DocumentClassAttribute attr = DocumentClassAttribute.ForType(docType);
				if (attr != null)
				{
					foreach (string docExt in attr.FileExtensions)
					{
						mFileTypes[docExt] = docType;
					}
				}
			}
		}

		public int Execute(string logtype, string cmd)
		{
			return Execute(logtype, cmd, null);
		}

		public int Execute(string logtype, string cmd, StringBuilder output)
		{
			Process process = new Process();
			process.StartInfo.WorkingDirectory = Project.Documents[0].BaseDirectory;
			process.StartInfo.CreateNoWindow = true;
			process.StartInfo.ErrorDialog = false;
			process.StartInfo.UseShellExecute = false;
			process.StartInfo.RedirectStandardInput = false;
			process.StartInfo.RedirectStandardOutput = true;
			process.StartInfo.FileName = "cmd";
			process.StartInfo.Arguments = "/c " + cmd + " 2>&1";

			try
			{
				AddMessage(logtype, "Executing \"" + process.StartInfo.FileName + " " + process.StartInfo.Arguments + "\"\n");
				process.Start();
				while(!process.StandardOutput.EndOfStream)
				{
					string line = process.StandardOutput.ReadLine() + "\n";
					if (output != null)
						output.Append(line);
					AddMessage(logtype, line);
				}
				AddMessage(logtype, "\nExit code " + process.ExitCode.ToString() + "\n");
				return process.ExitCode;
			}
			catch (Exception ex)
			{
				AddMessage(logtype, ex.ToString());
				return -1;
			}
		}

		#endregion

		#region Documents


		public Document FindOpenDocument(string fileName)
		{
			System.Diagnostics.Debug.Assert(Path.IsPathRooted(fileName));

			return mOpenDocuments.Find(delegate(Document item) { return PathUtils.Compare(item.FileName, fileName) == 0; });
		}

		public Document CreateDocument(string fileName)
		{
			return CreateDocument(fileName, null, null, null);
		}

		public Document CreateDocument(string fileName, Type docType)
		{
			return CreateDocument(fileName, docType, null, null);
		}


		public Document CreateDocument(string fileName, Type docType, Stream stream)
		{
			return CreateDocument(fileName, docType, stream, null);
		}

		/// <summary>
		/// Creates a new (unsaved) Document by loading it from the specified Stream, rather than a file on disk.
		/// </summary>
		/// <param name="fileName">Absolute filename of new document.</param>
		/// <param name="docType">Type of document to create.</param>
		/// <param name="stream">Stream from which to load the document's contents.</param>
		/// <returns></returns>
		public Document CreateDocument(string fileName, Type docType, Stream stream, object[] args)
		{
			// Change it to a unique (not open) name 
			while (FindOpenDocument(fileName) != null)
			{
				string name = Path.GetFileName(fileName);
				int dotpos = name.IndexOf('.');
				string fileBase, fileExt;
				if (dotpos >= 0)
				{
					fileBase = name.Substring(0, dotpos);
					fileExt = name.Substring(dotpos, name.Length - dotpos);
				}
				else
				{
					fileBase = name;
					fileExt = "";
				}
				Regex regex = new Regex("(.*)\\[(\\d+)\\]$");
				Match match = regex.Match(fileBase);
				if(match.Success)
				{
					int val;
					if(!Int32.TryParse(match.Groups[2].Value, out val))
						val = 1;

					fileBase = match.Groups[1].Value + "[" + (val + 1).ToString() + "]";
				}
				else
				{
					fileBase = fileBase + "[2]";
				}
				fileName = Path.GetDirectoryName(fileName) + Path.DirectorySeparatorChar + fileBase + fileExt;
			}

			if(docType == null)
			{
				docType = FindDocumentType(fileName);
			}

			Document doc;
			try
			{
				doc = CreateAndLoadDocument(fileName, docType, stream, args);
			}
			catch (Exception ex)
			{
				DialogResult result = MessageBox.Show(MainWindow, "There was an error opening the document '" + fileName + "':\r\n\r\n" + ex.ToString() + "\r\n\r\nWould you like to try opening it as a text document?", "Error opening document", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
				if(result == DialogResult.Cancel)
					return null;
				else
				{
					try
					{
						stream.Seek(0, SeekOrigin.Begin);
						doc = CreateAndLoadDocument(fileName, FindDocumentType(".txt"), stream, null);
					}
					catch (Exception ex2)
					{
						MessageBox.Show(MainWindow, ex2.ToString(), "Error opening document:", MessageBoxButtons.OK, MessageBoxIcon.Error);
						return null;
					}
				}
			}

			mOpenDocuments.Add(doc);
			mFileWatcher.AddFile(fileName);
			OnDocumentOpened(doc);
			return doc;
		}

		private Document CreateAndLoadDocument(string fileName, Type docType, Stream stream, object [] args)
		{
			Document doc = CreateDocumentInstance(fileName, docType, args);
			doc.NewDocument(stream);

			mOpenDocuments.Add(doc);
			OnDocumentOpened(doc);

			return doc;
		}

		public Document OpenDocument(DocumentItem docItem)
		{
			return OpenDocument(docItem.AbsoluteFileName, docItem.DocumentType);
		}

		public Document OpenDocument(string fileName)
		{
			return OpenDocument(fileName, null, null);
		}

		public Document OpenDocument(string fileName, Type docType)
		{
			return OpenDocument(fileName, docType, null);
		}

		/// <summary>
		/// Creates a Document for a file and loads it.
		/// </summary>
		/// <param name="fileName">The name of the file to open; it must be an absolute filename.</param>
		/// <param name="docType">The type of Document to instantiate, or null to guess the document type.</param>
		/// <param name="args">Any extra arguments to pass to the Document constructor.</param>
		/// <returns></returns>
		public Document OpenDocument(string fileName, Type docType, object [] args)
		{
			System.Diagnostics.Debug.Assert(Path.IsPathRooted(fileName), "Absolute path names required");
			System.Diagnostics.Debug.Assert(args == null || docType != null, "Can't pass extra constructor arguments when guessing document type");

			// Check if it's open already
			Document doc = FindOpenDocument(fileName);
			if (doc == null || (docType != null && !docType.IsAssignableFrom(doc.GetType())))
			{
				// We need to open it then, but check if it actually exists first
				if (!System.IO.File.Exists(fileName))
					return null;
				
				// Find out its type

				// Look in the project hierarchy
				if (docType == null && mProject != null)
				{
					DocumentItem docItem = mProject.FindDocument(fileName);
					if (docItem != null)
					{
						docType = docItem.DocumentType;
					}
				}

				// Try to choose one
				if(docType == null)
				{
					docType = FindDocumentType(fileName);
				}

				if (docType == null)
					return null;

				doc = CreateDocumentInstance(fileName, docType, args);
				doc.LoadDocument();

				mOpenDocuments.Add(doc);
				mFileWatcher.AddFile(fileName);
				OnDocumentOpened(doc);
			}
			return doc;
		}

		private Document CreateDocumentInstance(string fileName, Type docType, object[] args)
		{
			object[] fullargs;
			if (args == null)
				fullargs = new object[2];
			else
			{
				fullargs = new object[2 + args.Length];
				Array.Copy(args, 0, fullargs, 2, args.Length);
			}
			fullargs[0] = this;
			fullargs[1] = fileName;

			Document doc = (Document)Activator.CreateInstance(docType, fullargs);

			doc.Saving += new DocumentSavingEventHandler(Document_Saving);
			doc.Saved += new DocumentSavedEventHandler(Document_Saved);
			doc.PropertyChange += new PropertyChangeEventHandler(Document_PropertyChange);
			doc.ExternallyModified += new DocumentExternallyModifiedEventHandler(Document_ExternallyModified);

			return doc;
		}

		// Reflect messages from Documents to our listeners
		void Document_PropertyChange(object sender, PropertyChangeEventArgs args)
		{
			if (PropertyChange != null)
				PropertyChange(sender, args);
		}

		void Document_Saving(Document sender)
		{
			mFileWatcher.EnableFile(sender.FileName, false);
		}

		void Document_Saved(Document sender, bool success)
		{
			mFileWatcher.EnableFile(sender.FileName, true);
		}

		void Document_ExternallyModified(Document doc)
		{
			DialogResult result;

			if (doc.Modified)
			{
				result = MessageBox.Show(
					MainWindow, 
					String.Format("The file\r\n\r\n{0}\r\n\r\nhas been externally modified, however there are also unsaved changes in the editor. Do you want to reload it and discard your changes?", doc.FileName), 
					"File Modification Detected", 
					MessageBoxButtons.YesNo, 
					MessageBoxIcon.Exclamation);
			}
			else if (!ApplicationOptions.AutomaticallyReloadModifiedFiles)
			{
				result = MessageBox.Show(
					MainWindow, 
					String.Format("The file\r\n\r\n{0}\r\n\r\nhas been externally modified. Do you want to reload it?", doc.FileName),
					"File Modification Detected", 
					MessageBoxButtons.YesNo, 
					MessageBoxIcon.Question);
			}
			else
			{
				result = DialogResult.Yes;
			}

			if (result == DialogResult.Yes)
			{
				doc.LoadDocument();
				SetStatusMessage("Automatically reloaded " + doc.FileName, 10.0f);
			}
		}

		void FileWatcher_FileModified(object sender, string fileName)
		{
			if (MainWindow.InvokeRequired && MainWindow.IsHandleCreated && !MainWindow.IsDisposed)
				MainWindow.BeginInvoke(new FileModifiedEventHandler(FileWatcher_FileModified), new object[] { sender, fileName });
			else
			{
				Document doc = FindOpenDocument(fileName);

				if (doc != null)
					doc.OnExternallyModified();
			}
		}

		void FileWatcher_FileAttributesChanged(object sender, string fileName, FileAttributes oldAttr, FileAttributes newAttr)
		{
			if (MainWindow.InvokeRequired && MainWindow.IsHandleCreated && !MainWindow.IsDisposed)
				MainWindow.BeginInvoke(new FileAttributesChangedEventHandler(FileWatcher_FileAttributesChanged), new object[] { sender, fileName, oldAttr, newAttr });
			else
			{
				Document doc = FindOpenDocument(fileName);

				if (doc != null && File.Exists(fileName))
				{
					try
					{
						FileAttributes attr = File.GetAttributes(fileName);
						doc.ReadOnly = (attr & FileAttributes.ReadOnly) != 0;
					}
					catch (Exception)
					{
					}
				}
			}
		}

		private bool CheckCloseDocument(Document doc)
		{
			if (doc.Modified)
			{
				if (MessageBoxEx.Show(mMainWindow, "The file\r\n\r\n" + doc.FileName + "\r\n\r\nhas been modified; do you really want to close it?", "Are you sure?", new string[] { "Close", "Cancel" }, MessageBoxIcon.Exclamation, "Cancel") == "Cancel")
					return false;
			}
			return true;
		}

		private void ForceCloseDocument(Document doc)
		{
			doc.CloseDocument();
			mOpenDocuments.Remove(doc);
			mFileWatcher.RemoveFile(doc.FileName);
			OnDocumentClosed(doc);
		}

		public bool CloseDocument(Document doc, bool force)
		{
			if (force || CheckCloseDocument(doc))
			{
				ForceCloseDocument(doc);
				return true;
			}
			else
				return false;
		}

		// Returns true if all documents closed, false otherwise (and no documents closed)
		public bool CloseAllDocuments(bool force)
		{
			// Check all documents first
			if (!force)
			{
				foreach (Document doc in mOpenDocuments)
				{
					if (!CheckCloseDocument(doc))
						return false;
				}
			}

			// If they are all allowed to be closed, then force-close them
			while (mOpenDocuments.Count > 0)
			{
				ForceCloseDocument(mOpenDocuments[mOpenDocuments.Count - 1]);
			}

			return true;
		}

		public int SaveProject()
		{
			if (Project != null)
				Project.SaveUserConfig();

			return SaveAllDocuments(typeof(ProjectDocument));
		}

		public int SaveAllDocuments()
		{
			if (Project != null)
				Project.SaveUserConfig();

			return SaveAllDocuments(typeof(Document));
		}

		public int SaveAllDocuments(Type docType)
		{
			int count = 0;
			foreach (Document doc in OpenDocuments)
			{
				if (docType.IsAssignableFrom(doc.GetType()) && doc.Modified)
				{
					if (mMainWindow.SaveDocument(doc, false))
						++count;
				}
			}

			if (count == 0)
				SetStatusMessage("No modified files", 5.0f);
			else
				SetStatusMessage(String.Format("Saved {0} file(s)", count), 5.0f);

			return count;
		}

		public Type FindFileDocumentType(string docName)
		{
			return FindDocumentType(docName);
		}

		internal Type FindDocumentType(string fileName)
		{
			Type result;
			fileName = Path.GetFileName(fileName);
			int dotIndex = -1;
			do 
			{
				dotIndex = fileName.IndexOf(".", dotIndex + 1);
				if (dotIndex < 0)
					break;

				if (mFileTypes.TryGetValue(fileName.Substring(dotIndex), out result))
				{
					return result;
				}

			} while (true);

			if (mFileTypes.TryGetValue(".*", out result))
			{
				return result;
			}
			return null;
		}

		public DocumentView ShowDocument(DocumentItem docItem)
		{
			return ShowDocument(docItem.AbsoluteFileName);
		}

		public DocumentView ShowDocument(string fileName)
		{
			Document doc = OpenDocument(fileName);
			DocumentView view = (doc == null) ? null : mMainWindow.ShowDocument(doc);
			if(view == null)
				MessageBox.Show(mMainWindow, "The document '" + fileName + "'could not be opened.", "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return view;
		}

		public DocumentView ShowDocument(Document doc)
		{
			return mMainWindow.ShowDocument(doc);
		}

		#endregion

		#region User Interface

		public ToolWindow GetToolWindow(Type type)
		{
			return mMainWindow.GetToolWindow(type);
		}

		public void AddToMenuStrip(ToolStripItemCollection source)
		{
			MergeMenu(source, mMainWindow.menuStrip.Items);
		}

		private void MergeMenu(ToolStripItemCollection source, ToolStripItemCollection dest)
		{
			while(source.Count > 0)
			{
				ToolStripMenuItem item = (ToolStripMenuItem) source[0];
				if(item.MergeAction == MergeAction.Insert)
				{
					for(int index = 0; index < dest.Count; ++index)
					{
						if(dest[index].MergeIndex > item.MergeIndex)
						{
							dest.Insert(index, item);
							break;
						}
					}
				}
				else if (item.MergeAction == MergeAction.Append)
				{
					dest.Add(item);
				}
				else if (item.MergeAction == MergeAction.MatchOnly)
				{
					ToolStripMenuItem match = null;
					foreach(ToolStripMenuItem destitem in dest)
					{
						if(destitem.Text == item.Text)
						{
							match = destitem;
							break;
						}
					}
					if(match == null)
						throw new ApplicationException("Can't merge with nonexistent menu '" + item.Text + "'");

					MergeMenu(item.DropDownItems, match.DropDownItems);
					source.Remove(item);
				}
				else
				{
					throw new ApplicationException("Unsupported menu merge action");
				}
			}
		}

		public void AddToStatusStrip(ToolStripItemCollection toolStripItemCollection)
		{
			mMainWindow.statusStrip.Items.AddRange(toolStripItemCollection);
		}

		public void AddToolStrip(ToolStrip toolStrip, DockStyle side, int row)
		{
			if(side == DockStyle.Top)
				mMainWindow.toolStripPanelTop.Join(toolStrip, row);
			else if (side == DockStyle.Left)
				mMainWindow.toolStripPanelLeft.Join(toolStrip, row);
			else if (side == DockStyle.Right)
				mMainWindow.toolStripPanelRight.Join(toolStrip, row);
			else if (side == DockStyle.Bottom)
				mMainWindow.toolStripPanelBottom.Join(toolStrip, row);
		}

		public void SetStatusMessage(String message, float duration)
		{
			mMainWindow.SetStatusMessage(message, duration);
		}

		public void SetProgressBar(int progress)
		{
			mMainWindow.SetProgressBar(progress);
		}

		public void StartProgressBarMarquee()
		{
			mMainWindow.StartProgressBarMarquee();
		}

		public void StopProgressBarMarquee()
		{
			mMainWindow.StopProgressBarMarquee();
		}

		public void FlashMainWindow()
		{
			Win32.FlashWindow(mMainWindow.Handle);
		}

		public void ShowMessages(string type)
		{
			OutputPanel outputPanel = (OutputPanel)mMainWindow.GetToolWindow(typeof(OutputPanel));
			outputPanel.Invoke(new MethodInvoker(delegate() { outputPanel.ShowLog(type); outputPanel.Show(mMainWindow.DockPanel); }));

		}

		public void AddMessage(string type, string message)
		{
			OutputPanel outputPanel = (OutputPanel) mMainWindow.GetToolWindow(typeof(OutputPanel));
			outputPanel.Invoke(new MethodInvoker(delegate() { outputPanel.AddMessage(type, message); } ));
		}
		#endregion

		private void OnPropertyChange(object sender, string property, object oldValue, object newValue)
		{
			if (PropertyChange != null)
				PropertyChange(sender, new PropertyChangeEventArgs(property, oldValue, newValue));
		}

		private void OnProjectOpened()
		{
			if (ProjectOpened != null)
				ProjectOpened(this, mProject);
		}

		private bool OnProjectClosing(bool cancelled)
		{
			if (ProjectClosing != null)
			{
				ProjectClosingEventArgs args = new ProjectClosingEventArgs(mProject, cancelled);
				ProjectClosing(this, args);
				return args.Cancel;
			}
			return false;
		}

		private void OnProjectClosed()
		{
			if (ProjectClosed != null)
				ProjectClosed(this);
		}

		private void OnDocumentOpened(Document doc)
		{
			if (DocumentOpened != null)
				DocumentOpened(this, doc);
		}

		private void OnDocumentClosed(Document doc)
		{
			if (DocumentClosed != null)
				DocumentClosed(this, doc);
		}

		private void OnActiveDocumentChanged()
		{
			if (ActiveDocumentChanged != null)
				ActiveDocumentChanged(this, mActiveDocument);
		}

		private void OnSelectionChanged()
		{
			if (SelectionChanged != null)
				SelectionChanged(this, mSelection);
		}

		public void OnFindInFilesStarted(object sender, string message)
		{
			if (FindInFilesStarted != null)
				FindInFilesStarted(sender, message);
		}

		public void OnFindInFilesResult(object sender, FindInFilesResultEventArgs args)
		{
			if (FindInFilesResult != null)
				FindInFilesResult(sender, args);
		}

		public void OnFindInFilesStopped(object sender, string message)
		{
			if (FindInFilesStopped != null)
				FindInFilesStopped(sender, message);
		}

		public void OnGoToNextLocation()
		{
			if (GoToNextLocation != null)
			{
				foreach (GoToNextLocationEventHandler del in ((MulticastDelegate)GoToNextLocation).GetInvocationList())
				{
					bool consumed = false;
					del(this, ref consumed);
					if (consumed)
						break;
				}
			}
		}

		public void OnGoToPreviousLocation()
		{
			if (GoToPreviousLocation != null)
			{
				foreach (GoToPreviousLocationEventHandler del in ((MulticastDelegate)GoToPreviousLocation).GetInvocationList())
				{
					bool consumed = false;
					del(this, ref consumed);
					if (consumed)
						break;
				}
			}
		}

	}
}
