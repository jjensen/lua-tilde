
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
using System.Runtime.InteropServices;
using System.Threading;
using System.Text;

namespace Tilde.Framework.Controls
{
	public class WindowsHook : IDisposable
	{
		protected IntPtr mHook = IntPtr.Zero;

		public delegate void WindowsEventHandler(object sender, IntPtr hwnd, string className);

		public event WindowsEventHandler WindowCreated;
		public event WindowsEventHandler WindowDestroyed;
		public event WindowsEventHandler WindowActivated;

		public const string DialogClassName = "#32770";

		public WindowsHook()
		{
		}

		public void Install()
		{
			if (mHook != IntPtr.Zero)
				throw new ApplicationException("Attempt to install a WindowsHook twice");

			mHook = Win32.SetWindowsHookEx(
				Win32.WH_CBT,
				new Win32.HookProc(this.CoreHookProc),
				IntPtr.Zero,
				AppDomain.GetCurrentThreadId());		// If this is changed according to compiler message, it doesn't work :(
		}

		public void Uninstall()
		{
			if (mHook != IntPtr.Zero)
			{
				Win32.UnhookWindowsHookEx(mHook);
				mHook = IntPtr.Zero;
			}
		}

		protected int CoreHookProc(int code, IntPtr wParam, IntPtr lParam)
		{
			if (code >= 0)
			{
				IntPtr hwnd = wParam;

				StringBuilder sb1 = new StringBuilder();
				sb1.Capacity = 40;
				Win32.GetClassName(hwnd, sb1, 40);
				string className = sb1.ToString();

				switch (code)
				{
					case Win32.HCBT_CREATEWND:
						OnWindowCreated(hwnd, className);
						break;
					case Win32.HCBT_DESTROYWND:
						OnWindowDestroyed(hwnd, className);
						break;
					case Win32.HCBT_ACTIVATE:
						OnWindowActivated(hwnd, className);
						break;
				}
			}

			// Yield to the next hook in the chain
			return Win32.CallNextHookEx(mHook, code, wParam, lParam);
		}

		private void OnWindowCreated(IntPtr hwnd, string className)
		{
			if (WindowCreated != null)
				WindowCreated(this, hwnd, className);
		}

		private void OnWindowDestroyed(IntPtr hwnd, string className)
		{
			if (WindowCreated != null)
				WindowDestroyed(this, hwnd, className);
		}

		private void OnWindowActivated(IntPtr hwnd, string className)
		{
			if (WindowCreated != null)
				WindowActivated(this, hwnd, className);
		}

		#region IDisposable Members

		public void Dispose()
		{
			Uninstall();
		}

		#endregion
	}
}
