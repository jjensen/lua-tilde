
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
using System.Threading;

namespace Tilde.Framework.Controller
{
	public delegate void FileModifiedEventHandler(object sender, string fileName);
	public delegate void FileCreatedEventHandler(object sender, string fileName);
	public delegate void FileDeletedEventHandler(object sender, string fileName);
	public delegate void FileRenamedEventHandler(object sender, string fileName, string oldName);
	public delegate void FileAttributesChangedEventHandler(object sender, string fileName, FileAttributes oldAttrs, FileAttributes newAttrs);

	public class DirectoryWatcher
	{
		[Flags]
		public enum NotificationEvents
		{
			Modified = 1,
			Created = 2,
			Deleted = 4,
			Renamed = 8,
			AttributesChanged = 16
		}

		private object mLock;
		private string mBaseDirectory;
		private NotificationEvents mNotificationEvents;
		private FileSystemWatcher mFileWatcher;
		private FileSystemWatcher mFileAttributeWatcher;
		private Dictionary<string, Timer> mModifiedFiles;

		private static int mTimeout = 2000;

		public DirectoryWatcher()
		{
			mLock = new object();
			mFileWatcher = null;
			mFileAttributeWatcher = null;
			mModifiedFiles = new Dictionary<string, Timer>();
		}

		public event FileModifiedEventHandler FileModified;
		public event FileCreatedEventHandler FileCreated;
		public event FileDeletedEventHandler FileDeleted;
		public event FileRenamedEventHandler FileRenamed;
		public event FileAttributesChangedEventHandler FileAttributesChanged;

		public FileSystemWatcher FileSystemWatcher
		{
			get { return mFileWatcher; }
		}

		public FileSystemWatcher FileAttributeWatcher
		{
			get { return mFileAttributeWatcher; }
		}

		public void Watch(string baseDirectory, NotificationEvents events)
		{
			mBaseDirectory = baseDirectory;
			mNotificationEvents = events;

			if( (events & (NotificationEvents.Modified | NotificationEvents.Created | NotificationEvents.Deleted | NotificationEvents.Renamed)) != 0)
			{
				WatchFileEvents(baseDirectory, events);
			}
	
			if((events & NotificationEvents.AttributesChanged) != 0)
			{
				WatchAttributeEvents(baseDirectory);
			}
		}

		public void Cancel()
		{
			if (mFileWatcher != null)
			{
//				mFileWatcher.Dispose();		
				mFileWatcher = null;
			}

			if (mFileAttributeWatcher != null)
			{
				// Causes hang on shutdown somehow
//				mFileAttributeWatcher.Dispose();
				mFileAttributeWatcher = null;
			}
		}

		private void WatchFileEvents(string baseDirectory, NotificationEvents events)
		{
			if (mFileWatcher != null)
				mFileWatcher.Dispose();

			NotifyFilters filters = 0;
			if ((events & NotificationEvents.Modified) != 0)
				filters |= NotifyFilters.Size | NotifyFilters.LastWrite;

			if ((events & NotificationEvents.Renamed) != 0)
				filters |= NotifyFilters.FileName;

			try
			{
				mFileWatcher = new FileSystemWatcher();
				mFileWatcher.Filter = "*.*";
				mFileWatcher.IncludeSubdirectories = true;
				mFileWatcher.NotifyFilter = filters;
				mFileWatcher.Changed += new FileSystemEventHandler(mFileWatcher_Changed);
				mFileWatcher.Created += new FileSystemEventHandler(mFileWatcher_Created);
				mFileWatcher.Renamed += new RenamedEventHandler(mFileWatcher_Renamed);
				mFileWatcher.Deleted += new FileSystemEventHandler(mFileWatcher_Deleted);
				mFileWatcher.Error += new ErrorEventHandler(mFileWatcher_Error);
				mFileWatcher.InternalBufferSize = 32768;

				mFileWatcher.Path = baseDirectory;
				mFileWatcher.EnableRaisingEvents = true;
			}
			catch(Exception)
			{
				mFileWatcher = null;
			}
		}

		private void WatchAttributeEvents(string baseDirectory)
		{
			if (mFileAttributeWatcher != null)
				mFileAttributeWatcher.Dispose();

			try
			{
				mFileAttributeWatcher = new FileSystemWatcher();
				mFileAttributeWatcher.Filter = "*.*";
				mFileAttributeWatcher.IncludeSubdirectories = true;
				mFileAttributeWatcher.NotifyFilter = NotifyFilters.Attributes;
				mFileAttributeWatcher.Changed += new FileSystemEventHandler(mFileAttributeWatcher_Changed);
				mFileAttributeWatcher.Error += new ErrorEventHandler(mFileAttributeWatcher_Error);
				mFileAttributeWatcher.InternalBufferSize = 32768;

				mFileAttributeWatcher.Path = baseDirectory;
				mFileAttributeWatcher.EnableRaisingEvents = true;
			}
			catch (System.Exception)
			{
				mFileAttributeWatcher = null;
			}
		}

		void ModifiedTimer_Callback(object state)
		{
			lock (mLock)
			{
				string filename = (string)state;
				mModifiedFiles.Remove(filename);
				OnFileModified(filename);
			}
		}

		void mFileWatcher_Error(object sender, ErrorEventArgs e)
		{
			System.Windows.Forms.MessageBox.Show(e.GetException().ToString(), "FileSystemWatcher Error");
			WatchFileEvents(mBaseDirectory, mNotificationEvents);
		}

		void mFileAttributeWatcher_Error(object sender, ErrorEventArgs e)
		{
			System.Windows.Forms.MessageBox.Show(e.GetException().ToString(), "FileSystemWatcher Error");
			WatchAttributeEvents(mBaseDirectory);
		}

		void mFileWatcher_Changed(object sender, FileSystemEventArgs e)
		{
			TriggerChanged(e.FullPath);
		}

		void mFileWatcher_Created(object sender, FileSystemEventArgs e)
		{
			OnFileCreated(e.FullPath);
		}

		void mFileWatcher_Deleted(object sender, FileSystemEventArgs e)
		{
			OnFileDeleted(e.FullPath);
		}

		void mFileWatcher_Renamed(object sender, RenamedEventArgs e)
		{
			OnFileRenamed(e.OldFullPath, e.FullPath);
		}

		void mFileAttributeWatcher_Changed(object sender, FileSystemEventArgs e)
		{
			OnFileAttributesChanged(e.FullPath);
		}

		private void TriggerChanged(string fileName)
		{
			lock (mLock)
			{
				if (mModifiedFiles.ContainsKey(fileName))
				{
					// Reset the timer
					Timer timer = mModifiedFiles[fileName];
					timer.Change(mTimeout, Timeout.Infinite);
				}
				else
				{
					// Start a new timer
					Timer timer = new Timer(new TimerCallback(this.ModifiedTimer_Callback), fileName, mTimeout, Timeout.Infinite);
					mModifiedFiles.Add(fileName, timer);
				}
			}
		}

		void OnFileModified(string filename)
		{
			if (FileModified != null)
				FileModified(this, filename);
		}

		void OnFileCreated(string filename)
		{
			if (FileCreated != null)
				FileCreated(this, filename);
		}

		void OnFileDeleted(string filename)
		{
			if (FileDeleted != null)
				FileDeleted(this, filename);
		}

		void OnFileRenamed(string oldName, string newName)
		{
			if (FileRenamed != null)
				FileRenamed(this, oldName, newName);
		}

		void OnFileAttributesChanged(string filename)
		{
			if (FileAttributesChanged != null)
				FileAttributesChanged(this, filename, 0, 0);
		}
	}
}
