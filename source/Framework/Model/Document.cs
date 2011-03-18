
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

using Tilde.Framework.View;
using Tilde.Framework.Controller;
using Tilde.Framework.Model.ProjectHierarchy;

namespace Tilde.Framework.Model
{
	public delegate void DocumentClosedEventHandler(Document sender);
	public delegate void DocumentSavingEventHandler(Document sender);
	public delegate void DocumentSavedEventHandler(Document sender, bool success);
	public delegate void DocumentLoadingEventHandler(Document sender);
	public delegate void DocumentLoadedEventHandler(Document sender);
	public delegate void DocumentExternallyModifiedEventHandler(Document sender);

	public abstract class Document
	{
		private IManager mManager;
		private String mFileName;
		private DateTime mLastSaveTime;
		private bool mOnDisk;
		private bool mReadOnly;
		private bool mModified;
		private bool mClosing;

		private List<DocumentView> mViews;

		public Document(IManager manager, String filename)
		{
			System.Diagnostics.Debug.Assert(Path.IsPathRooted(filename));

			mManager = manager;
			mFileName = filename;
			mLastSaveTime = DateTime.MinValue;
			mOnDisk = false;
			mModified = false;
			mReadOnly = false;
			mClosing = false;
			mViews = new List<DocumentView>();
		}

		public IManager Manager
		{
			get { return mManager; }
		}

		/// <summary>
		/// The full file name (including absolute path) of the document.
		/// </summary>
		public String FileName
		{
			get { return mFileName; }
			set
			{
				if (mFileName != value)
				{
					System.Diagnostics.Debug.Assert(Path.IsPathRooted(value));

					string oldvalue = mFileName;
					mFileName = value;
					OnPropertyChange("FileName", oldvalue, value);
				}
			}
		}

		public DateTime LastSaveTime
		{
			get { return mLastSaveTime; }
		}

		/// <summary>
		/// Has the document been saved/loaded from an actual file yet?
		/// </summary>
		public bool OnDisk
		{
			get { return mOnDisk; }
			set 
			{ 
				if (mOnDisk != value) 
				{
					bool oldvalue = mOnDisk;
					mOnDisk = value;
					OnPropertyChange("OnDisk", oldvalue, value); 
				}  
			}
		}

		public bool ReadOnly
		{
			get { return mReadOnly; }
			set 
			{ 
				if (mReadOnly != value) 
				{
					bool oldvalue = mReadOnly;
					mReadOnly = value; 
					OnPropertyChange("ReadOnly", oldvalue, value); 
				} 
			}
		}

		public bool Modified
		{
			get { return mModified; }
			set 
			{ 
				if (mModified != value) 
				{
					bool oldvalue = mModified;
					mModified = value;
					OnPropertyChange("Modified", oldvalue, value);
				} 
			}
		}

		public bool Closing
		{
			get { return mClosing; }
		}

		public List<DocumentView> Views
		{
			get { return mViews; }
		}

		public void CloseDocument()
		{
			mClosing = true;
			OnClosed();
		}

		public bool NewDocument(Stream stream)
		{
			if(New(stream))
			{
				OnDisk = false;
				ReadOnly = false;
				Modified = true;
				return true;
			}
			return false;
		}

		public bool LoadDocument()
		{
			OnLoading();

			if(Load())
			{
				OnDisk = true;
				Modified = false;
				ReadOnly = (System.IO.File.GetAttributes(FileName) & FileAttributes.ReadOnly) != 0;
				OnLoaded();
				return true;
			}
			return false;
		}

		public bool SaveDocument()
		{
			if (ReadOnly && !Checkout())
				return false;

			OnSaving();

			bool result = false;
			while (true)
			{
				try
				{
					result = Save();
				}
				catch (Exception ex)
				{
					DialogResult option = MessageBox.Show("An error occurred while saving '" + this.FileName + "':\r\n\r\n" + ex.ToString(), "Error saving document", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
					if (option == DialogResult.Retry)
						continue;
				}
				break;
			}

			if (result)
			{
				OnDisk = true;
				Modified = false;
				mLastSaveTime = DateTime.Now;
			}

			OnSaved(result);

			return result;
		}

		public bool Checkout()
		{
			IVersionController vcs = Manager.Project.VCS;
			if (vcs != null && vcs.IsVersionControlled(this.FileName))
			{
				if (MessageBox.Show(Manager.MainWindow, "The file '" + this.FileName + "' is under revision control but not checked out; do you want to check it out?", "Read-only file", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					if (vcs.Checkout(this.FileName))
					{
						this.ReadOnly = false;
						return true;
					}
				}
			}
			else
			{
				if(MessageBox.Show(Manager.MainWindow, "The file is read-only and cannot be edited. Would you like to remove the read-only attribute from it?", "Read-only file", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					try
					{
						File.SetAttributes(this.FileName, File.GetAttributes(this.FileName) & ~FileAttributes.ReadOnly);
						this.ReadOnly = false;
						return true;
					}
					catch(Exception ex)
					{
						MessageBox.Show(Manager.MainWindow, "Failed making " + this.FileName + " writable:\r\n\r\n" + ex.ToString(), "Read-only file", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
			}

			return false;
		}

		public event PropertyChangeEventHandler PropertyChange;
		public event DocumentClosedEventHandler Closed;
		public event DocumentSavingEventHandler Saving;
		public event DocumentSavedEventHandler Saved;
		public event DocumentLoadingEventHandler Loading;
		public event DocumentLoadedEventHandler Loaded;
		public event DocumentExternallyModifiedEventHandler ExternallyModified;

		protected abstract bool New(Stream stream);
		protected abstract bool Load();
		protected abstract bool Save();

		protected void OnPropertyChange(string property, object oldValue, object newValue)
		{
			if (PropertyChange != null)
				PropertyChange(this, new PropertyChangeEventArgs(property, oldValue, newValue));
		}

		protected void OnClosed()
		{
			if (Closed != null)
				Closed(this);
		}

		protected void OnSaved(bool success)
		{
			if (Saved != null)
				Saved(this, success);
		}

		protected void OnSaving()
		{
			if (Saving != null)
				Saving(this);
		}

		protected void OnLoading()
		{
			if (Loading != null)
				Loading(this);
		}

		protected void OnLoaded()
		{
			if (Loaded != null)
				Loaded(this);
		}

		public virtual void OnExternallyModified()
		{
			if (ExternallyModified != null)
				ExternallyModified(this);
		}
	}
}
