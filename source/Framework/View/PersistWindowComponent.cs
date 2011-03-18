
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
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Diagnostics;
using System.Windows.Forms;


namespace Tilde.Framework.View
{
	public class PersistWindowComponent : System.ComponentModel.Component
	{
		#region Declarations
		// event info that allows form to persist extra window state data
		public delegate void WindowStateDelegate(object sender, WindowStateInfo WindowInfo);
		public event WindowStateDelegate LoadStateEvent;
		public event WindowStateDelegate SaveStateEvent;

		private Form mParentForm;
		private WindowStateInfo mWindowInfo = new WindowStateInfo();
		private PersistMethods mPersistMethod = PersistMethods.Registry;
		private string mRegistryPath = "";
		private string mRegistryKey = "";
		public enum PersistMethods { Registry, Custom }

		private System.ComponentModel.Container components = null;
		#endregion

		#region Public Properties

		[DefaultValue(PersistMethods.Registry)]
		[Description("Method of persisting window state information."), 
		Category("Persist Configuration")]
		public PersistMethods PersistMethod
		{
			get { return this.mPersistMethod; }
			set { this.mPersistMethod = value; }
		}
		
		[DefaultValue("")]
		[Description("Key for saving info to registry."), 
		Category("Persist Configuration")]
		public string RegistryKey
		{
			get { return this.mRegistryKey; }
			set { this.mRegistryKey=value; }
		}
		
		
		[Browsable(false)]
		public Form Form
		{
			get
			{
				if (this.mParentForm == null)
				{
					if (this.Site.DesignMode)
					{
						IDesignerHost dh = (IDesignerHost)this.GetService(typeof(IDesignerHost));

						if (dh != null)
						{
							Object obj = dh.RootComponent;
							if (obj != null)
							{
								this.mParentForm = (Form)obj;
							}
						}
					}
				}

				return this.mParentForm;
			}

			set
			{
				if (this.mParentForm != null)
					return;

				if (value != null)
				{
					this.mParentForm = value;
					
					// subscribe to parent form's events
					mParentForm.Closing += new System.ComponentModel.CancelEventHandler(OnClosing);
					mParentForm.Resize += new System.EventHandler(OnResize);
					mParentForm.Move += new System.EventHandler(OnMove);
					mParentForm.Load += new System.EventHandler(OnLoad);

					// get initial width and height in case form is never resized
					mWindowInfo.Width = mParentForm.Width;
					mWindowInfo.Height = mParentForm.Height;

					mRegistryPath = "Software\\" + this.mParentForm.CompanyName + "\\" + System.Reflection.Assembly.GetEntryAssembly().GetName().Name + "\\WindowPositions";
				}
			}
		}

		
		#endregion

		#region Public Methods

		public void SetWindowInfo(WindowStateInfo WindowInfo)
		{
			this.mWindowInfo = WindowInfo;

			mParentForm.Location = new System.Drawing.Point(mWindowInfo.Left, mWindowInfo.Top);
			mParentForm.Size = new System.Drawing.Size(mWindowInfo.Width, mWindowInfo.Height);
			mParentForm.WindowState = mWindowInfo.WindowState;
		}


		#endregion

		#region Private Form Event Handlers

		private void OnResize(object sender, System.EventArgs e)
		{
			// save width and height
			if(mParentForm.WindowState == FormWindowState.Normal)
			{
				mWindowInfo.Width = mParentForm.Width;
				mWindowInfo.Height = mParentForm.Height;
			}
		}

		
		private void OnMove(object sender, System.EventArgs e)
		{
			// save position
			if(mParentForm.WindowState == FormWindowState.Normal)
			{
				mWindowInfo.Left = mParentForm.Left;
				mWindowInfo.Top= mParentForm.Top;
			}
			// save state
			mWindowInfo.WindowState = mParentForm.WindowState;
		}


		private void OnClosing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			// check if we are allowed to save the state as minimized (not normally)
			if(! mWindowInfo.AllowSaveMinimised)
			{
				if(mWindowInfo.WindowState == FormWindowState.Minimized)
					mWindowInfo.WindowState= FormWindowState.Normal;
			}

			switch (this.mPersistMethod)
			{
				case PersistMethods.Registry:
					if (this.mRegistryPath == "" || this.mRegistryKey == "")
						throw new Exception("Registry File path is empty");

					this.SaveInfoToRegistry();
					break;
				case PersistMethods.Custom:
					// fire SaveState event
					if(SaveStateEvent != null)
						SaveStateEvent(this, this.mWindowInfo);

					break;
			}
		}

		
		private void OnLoad(object sender, System.EventArgs e)
		{
			switch (this.mPersistMethod)
			{
				case PersistMethods.Registry:
					if (this.mRegistryPath == "" || this.mRegistryKey == "")
						throw new Exception("Registry File path is empty");

					this.LoadInfoFromRegistry();
					break;
				case PersistMethods.Custom:
					// fire LoadState event
					if(LoadStateEvent != null)
						LoadStateEvent(this, this.mWindowInfo);

					break;
			}
		}

	
		#endregion

		#region Private Support Functions

		private void LoadInfoFromRegistry()
		{
			// attempt to read state from registry
			Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(this.mRegistryPath);

			if(key != null)
			{
				string data = (string)key.GetValue(mRegistryKey);
				if (data != null)
				{
					string[] windowpos = data.Split(new char[] { ',' });
					FormWindowState windowState = (FormWindowState)Enum.Parse(typeof(FormWindowState), windowpos[0]);
					int left = (int)Decimal.Parse(windowpos[1]);
					int top = (int)Decimal.Parse(windowpos[2]);
					int width = (int)Decimal.Parse(windowpos[3]);
					int height = (int)Decimal.Parse(windowpos[4]);

					mParentForm.Location = new System.Drawing.Point(left, top);
					mParentForm.Size = new System.Drawing.Size(width, height);
					mParentForm.WindowState = windowState;

					this.mWindowInfo.Left = mParentForm.Left;
					this.mWindowInfo.Top = mParentForm.Top;
					this.mWindowInfo.Height = mParentForm.Height;
					this.mWindowInfo.Width = mParentForm.Width;
					this.mWindowInfo.WindowState = mParentForm.WindowState;
				}
			}
		}

		
		private void SaveInfoToRegistry()
		{
			// save position, size and state
			Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(this.mRegistryPath);

			key.SetValue(mRegistryKey, String.Join(",", new String[] 
			{ 
				mWindowInfo.WindowState.ToString(),
				mWindowInfo.Left.ToString(),
				mWindowInfo.Top.ToString(),
				mWindowInfo.Width.ToString(),
				mWindowInfo.Height.ToString()
			}));
		}


		#endregion

		#region Creator

		public PersistWindowComponent(System.ComponentModel.IContainer container)
		{
			container.Add(this);
			InitializeComponent();
		}

		public PersistWindowComponent()
		{
			InitializeComponent();
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}


		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
		}
		#endregion

		#endregion
	}

	#region WindowStateInfo

	public class WindowStateInfo
	{
		public Int32 Top;
		public Int32 Left;
		public Int32 Height;
		public Int32 Width;
		public FormWindowState WindowState;
		public bool AllowSaveMinimised = false;
	}

	#endregion
}