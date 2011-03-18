
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

using Timer = System.Threading.Timer;

namespace Tilde.Framework.Controller
{
	/// <summary>
	/// Provides a service where nominated files are regularly polled to check if they have been
	/// externally modified. 
	/// </summary>
	/// <remarks>
	/// Events are fired when a file's modified time or attributes have changed; however note that 
	/// these will be running in a worker thread, not the main thread! The user can flag files to be
	/// temporarily ignored, such as when the main application is making changes. The service as a
	/// whole can also be paused and resumed, such as when the application loses focus.
	/// </remarks>
	public class FileWatcher
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

		class WatchedFile
		{
			public string FileName;
			public DateTime LastWriteTime;
			public FileAttributes Attributes;
		}

		private object mLock;
		private List<WatchedFile> mWatchedFiles;
		private List<WatchedFile> mIgnoredFiles;
		private Timer mUpdateTimer;
		private bool mPaused;
		private DateTime mPauseTime;

		private static int mUpdatePeriod = 2000;

		public FileWatcher()
		{
			mLock = new object();
			mWatchedFiles = new List<WatchedFile>();
			mIgnoredFiles = new List<WatchedFile>();
			mUpdateTimer = new Timer(new TimerCallback(this.UpdateTimer_Callback));
			mPaused = false;
		}

		public event FileModifiedEventHandler FileModified;
		public event FileDeletedEventHandler FileDeleted;
		public event FileAttributesChangedEventHandler FileAttributesChanged;

		public void AddFile(string fileName)
		{
			if (!File.Exists(fileName))
				return;

			WatchedFile fileInfo = new WatchedFile();
			try
			{
				fileInfo.FileName = fileName;
				fileInfo.LastWriteTime = File.GetLastWriteTime(fileName);
				fileInfo.Attributes = File.GetAttributes(fileName);
			}
			catch(Exception)
			{
				return;
			}

			lock (mLock)
			{
				mWatchedFiles.Add(fileInfo);
				UpdateTimer();
			}
		}

		public void RemoveFile(string fileName)
		{
			lock (mLock)
			{
				mWatchedFiles.RemoveAll(delegate(WatchedFile file) { return PathUtils.Compare(file.FileName, fileName) == 0; });
				mIgnoredFiles.RemoveAll(delegate(WatchedFile file) { return PathUtils.Compare(file.FileName, fileName) == 0; });
				
				UpdateTimer();
			}
		}

		/// <summary>
		/// Enables or disables a file from being watched (such as when the client code is about to save it).
		/// </summary>
		/// <param name="fileName">Files' absolute path name.</param>
		/// <param name="enabled">Whether to enable or disable watching.</param>
		/// <remarks>Enabling a file that is not monitored causes it to be added to the watch list.</remarks>
		public void EnableFile(string fileName, bool enabled)
		{
			lock (mLock)
			{
				if (!enabled)
				{
					WatchedFile fileInfo = mWatchedFiles.Find(delegate(WatchedFile file) { return PathUtils.Compare(file.FileName, fileName) == 0; });
					if (fileInfo != null)
					{
						mWatchedFiles.Remove(fileInfo);
						mIgnoredFiles.Add(fileInfo);
						UpdateTimer();
					}
				}
				else
				{
					WatchedFile fileInfo = mIgnoredFiles.Find(delegate(WatchedFile file) { return PathUtils.Compare(file.FileName, fileName) == 0; });
					if (fileInfo == null)
						AddFile(fileName);
					else
					{
						mIgnoredFiles.Remove(fileInfo);
						mWatchedFiles.Add(fileInfo);
						fileInfo.Attributes = File.GetAttributes(fileInfo.FileName);
						fileInfo.LastWriteTime = File.GetLastWriteTime(fileInfo.FileName);
						UpdateTimer();
					}
				}
			}
		}

		public bool Paused
		{
			get { lock(mLock) { return mPaused; } }
		}

		public void Pause()
		{
			lock (mLock)
			{
				if (!mPaused)
				{
					mPaused = true;
					mPauseTime = DateTime.Now;
					UpdateTimer();
				}
			}
		}

		/// <summary>
		/// Resumes the polling of watched files.
		/// </summary>
		/// <remarks>
		/// If it has been longer than the polling interval since the FileWatcher was paused,
		/// all watched files are immediately polled for changes and the results dispatched immediately
		/// in the callers' thread (before the function returns).
		/// </remarks>
		public void Resume()
		{
			lock (mLock)
			{
				if (mPaused)
				{
					if (DateTime.Now.Subtract(mPauseTime).TotalMilliseconds > mUpdatePeriod)
					{
						// Go through all the watched files and check them
						for (int index = 0; index < mWatchedFiles.Count; )
						{
							WatchedFile file = mWatchedFiles[index];

							if (!CheckFile(file))
							{
								// Remove from queue
								mWatchedFiles.RemoveAt(index);
							}
							else
							{
								// Leave on queue
								++index;
							}
						}
					}

					mPaused = false;
					UpdateTimer();
				}
			}
		}

		private void UpdateTimer()
		{
			lock (mLock)
			{
				if (mWatchedFiles.Count == 0 || mPaused)
					mUpdateTimer.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
				else
					mUpdateTimer.Change(0, mUpdatePeriod / mWatchedFiles.Count);
			}
		}

		void UpdateTimer_Callback(object state)
		{
			lock (mLock)
			{
				if (!mPaused && mWatchedFiles.Count > 0)
				{
					WatchedFile file = mWatchedFiles[0];

					if (!CheckFile(file))
					{
						// Remove from queue
						mWatchedFiles.RemoveAt(0);
					}
					else
					{
						// Put on back of queue
						mWatchedFiles.RemoveAt(0);
						mWatchedFiles.Add(file);
					}
				}
			}
		}

		bool CheckFile(WatchedFile file)
		{
			if (!File.Exists(file.FileName))
			{
				OnFileDeleted(file.FileName);
				return false;
			}
			else
			{
				DateTime fileWriteTime = File.GetLastWriteTime(file.FileName);
				FileAttributes fileAttributes = File.GetAttributes(file.FileName);

				if (fileWriteTime.CompareTo(file.LastWriteTime) > 0)
				{
					OnFileModified(file.FileName);
				}

				if (fileAttributes != file.Attributes)
				{
					OnFileAttributesChanged(file.FileName, file.Attributes, fileAttributes);
				}

				file.Attributes = fileAttributes;
				file.LastWriteTime = fileWriteTime;
				return true;
			}
		}

		void OnFileModified(string filename)
		{
			if (FileModified != null)
				FileModified(this, filename);
		}

		void OnFileDeleted(string filename)
		{
			if (FileDeleted != null)
				FileDeleted(this, filename);
		}

		void OnFileAttributesChanged(string filename, FileAttributes oldAttr, FileAttributes newAttr)
		{
			if (FileAttributesChanged != null)
				FileAttributesChanged(this, filename, oldAttr, newAttr);
		}
	}
}
