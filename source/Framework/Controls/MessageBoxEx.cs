
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
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace Tilde.Framework.Controls
{
	public class MessageBoxEx
	{
		protected IntPtr mHandle = IntPtr.Zero;
		protected bool mInitialised = false;
		protected string[] mButtons;
		protected Dictionary<DialogResult, string> mButtonMap;

		protected static int [] mDefaultFlags = new int[] { Win32.MB_DEFBUTTON1, Win32.MB_DEFBUTTON2, Win32.MB_DEFBUTTON3, Win32.MB_DEFBUTTON4 };

		public MessageBoxEx()
		{
		}

		public static string Show(IWin32Window window, string text, string title, string[] buttons, MessageBoxIcon icon, string defaultButton)
		{
			MessageBoxEx instance = new MessageBoxEx();
			return instance.ShowImpl(window, text, title, buttons, icon, defaultButton);
		}

		internal string ShowImpl(IWin32Window window, string text, string title, string [] buttons, MessageBoxIcon icon, string defaultButton)
		{
			System.Diagnostics.Debug.Assert(buttons.Length <= 4 && buttons.Length >= 1);

			mButtons = buttons;
			mButtonMap = new Dictionary<DialogResult, string>();

			int buttonstype;
			switch (buttons.Length)
			{
				case 1:
					buttonstype = Win32.MB_OK;
					mButtonMap[DialogResult.OK] = buttons[0];
					break;

				case 2:
					if (buttons[1] == "Cancel")
					{
						buttonstype = Win32.MB_OKCANCEL;
						mButtonMap[DialogResult.OK] = buttons[0];
						mButtonMap[DialogResult.Cancel] = buttons[1];
					}
					else
					{
						buttonstype = Win32.MB_YESNO;
						mButtonMap[DialogResult.Yes] = buttons[0];
						mButtonMap[DialogResult.No] = buttons[1];
					}
					break;

				case 3:
					if (buttons[2] == "Cancel")
					{
						buttonstype = Win32.MB_YESNOCANCEL;
						mButtonMap[DialogResult.Yes] = buttons[0];
						mButtonMap[DialogResult.No] = buttons[1];
						mButtonMap[DialogResult.Cancel] = buttons[2];
					}
					else
					{
						buttonstype = Win32.MB_ABORTRETRYIGNORE;
						mButtonMap[DialogResult.Abort] = buttons[0];
						mButtonMap[DialogResult.Retry] = buttons[1];
						mButtonMap[DialogResult.Ignore] = buttons[2];
					}
					break;

				case 4:
					if (buttons[3] == "Cancel")
					{
						buttonstype = Win32.MB_ABORTRETRYIGNORE + Win32.MB_HELP;
						mButtonMap[DialogResult.Abort] = buttons[0];
						mButtonMap[DialogResult.Retry] = buttons[1];
						mButtonMap[DialogResult.Ignore] = buttons[2];
						mButtonMap[DialogResult.Cancel] = buttons[3];
					}
					else
					{
						buttonstype = Win32.MB_ABORTRETRYIGNORE + Win32.MB_HELP;
						mButtonMap[DialogResult.Abort] = buttons[0];
						mButtonMap[DialogResult.Retry] = buttons[1];
						mButtonMap[DialogResult.Ignore] = buttons[2];
						mButtonMap[DialogResult.OK] = buttons[3];
					}
					break;

				default:
					throw new ApplicationException("MessageBoxEx can only present 4 buttons");
			}

			int defaultFlag = 0;
			for(int index = 0; index < buttons.Length; ++index)
			{
				if (buttons[index] == defaultButton)
					defaultFlag = mDefaultFlags[index];
			}

			DialogResult result = DialogResult.None;

			// Make sure the hook gets uninstalled in the event of an exception
			using(WindowsHook mWindowsHook = new WindowsHook())
			{
				mWindowsHook.WindowCreated += new WindowsHook.WindowsEventHandler(WndCreated);
				mWindowsHook.WindowDestroyed += new WindowsHook.WindowsEventHandler(WndDestroyed);
				mWindowsHook.WindowActivated += new WindowsHook.WindowsEventHandler(WndActivated);
				mWindowsHook.Install();

				// This call to Win32.MessageBox blocks until the dialog has been closed
				result = (DialogResult) Win32.MessageBox(window.Handle, text, title, buttonstype + (int)icon + defaultFlag);
			}

			string btnresult;
			if(mButtonMap.TryGetValue(result, out btnresult))
				return btnresult;
			else
				throw new ApplicationException("Unexpected return from Win32.MessageBox() call: " + result.ToString());
		}

		private void WndCreated(object sender, IntPtr hwnd, string className)
		{
			if (className == WindowsHook.DialogClassName)
			{
				mInitialised = false;
				mHandle = hwnd;
			}
		}

		private void WndDestroyed(object sender, IntPtr hwnd, string className)
		{
			if (hwnd == mHandle)
			{
				mInitialised = false;
				mHandle = IntPtr.Zero;
			}
		}

		private void WndActivated(object sender, IntPtr hwnd, string className)
		{
			if (mHandle != hwnd || mInitialised)
				return;

			if(mButtons.Length == 4)
				Win32.SetWindowLong(Win32.GetDlgItem(mHandle, Win32.IDHELP), Win32.GWL_ID, mButtons[3] == "Cancel" ? (int)DialogResult.Cancel : (int)DialogResult.OK);

			foreach(KeyValuePair<DialogResult, string> pair in mButtonMap)
			{
				Win32.SetWindowText(Win32.GetDlgItem(mHandle, (int)pair.Key), pair.Value);
			}

			mInitialised = true;
		}
	}
}

