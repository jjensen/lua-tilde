
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
using System.Runtime;
using System.Runtime.InteropServices;

using HWND = System.Runtime.InteropServices.HandleRef;
using System.Text;

namespace Tilde.Framework.Controls
{
	/// <summary>
	/// Summary description for Win32.
	/// </summary>
	public class Win32
	{
		private Win32()
		{
		}


		public const int FALSE = 0;
		public const int TRUE = 1;


		public const int ES_READONLY	= 0x0800;
		public const int ES_MULTILINE	= 0x0004;

		public const int WS_VISIBLE		= 0x10000000;
		public const int WS_CHILD		= 0x40000000;

		public const int EM_SETEVENTMASK  =		  (WM_USER + 69);
		public const int EM_GETSCROLLPOS  =       (WM_USER + 221);
		public const int EM_SETSCROLLPOS  =       (WM_USER + 222);

		public const int SB_HORZ = 0;
		public const int SB_VERT = 1;

		public const int VK_CONTROL = 0x11;
		public const int VK_UP = 0x26;
		public const int VK_DOWN = 0x28;
		public const int VK_NUMLOCK = 0x90;

		public const short KS_ON = 0x01;
		public const short KS_KEYDOWN = 0x80;

		public const int SW_PARENTCLOSING	= 1;
		public const int SW_OTHERZOOM		= 2;
		public const int SW_PARENTOPENING	= 3;
		public const int SW_OTHERUNZOOM		= 4;

		// --------------------------------------------------
		#region CommCtrl.h
		// --------------------------------------------------
		public const int LVM_GETEXTENDEDLISTVIEWSTYLE = (0x1000 + 55);
		public const int LVM_SETEXTENDEDLISTVIEWSTYLE	= (0x1000 + 54);
		public const int LVS_EX_DOUBLEBUFFER			= 0x00010000;
		public const int LVS_EX_BORDERSELECT			= 0x00008000;
		#endregion

		// --------------------------------------------------
		#region winuser.h
		// --------------------------------------------------

		// WM_ACTIVATE state values
		public const int WM_SETREDRAW = 0x000B;
		public const int WM_PAINT = 0x000F;
		public const int WM_SHOWWINDOW = 0x0018;
		public const int WM_ACTIVATEAPP = 0x001C;
		public const int WM_SETFONT = 0x0030;
		public const int WM_GETFONT = 0x0031;
		public const int WM_KEYDOWN = 0x0100;
		public const int WM_KEYUP = 0x0101;
		public const int WM_CHAR = 0x0102;
		public const int WM_USER = 0x0400;
		public const int WM_APP = 0x8000;

		// Window field offsets for GetWindowLong()
		public const int GWL_WNDPROC			= -4;
		public const int GWL_HINSTANCE			= -6;
		public const int GWL_HWNDPARENT			= -8;
		public const int GWL_STYLE				= -16;
		public const int GWL_EXSTYLE			= -20;
		public const int GWL_USERDATA			= -21;
		public const int GWL_ID					= -12;

		// Dialog Box Command IDs
		public const int IDHELP = 9;

		// SetWindowsHook() codes
		public const int WH_CBT = 5;

		// CBT Hook Codes
		public const int HCBT_CREATEWND = 3;
		public const int HCBT_DESTROYWND = 4;
		public const int HCBT_ACTIVATE = 5;

		// MessageBox() Flags
		public const int MB_OK					= 0x00000000;
		public const int MB_OKCANCEL			= 0x00000001;
		public const int MB_ABORTRETRYIGNORE	= 0x00000002;
		public const int MB_YESNOCANCEL			= 0x00000003;
		public const int MB_YESNO				= 0x00000004;
		public const int MB_RETRYCANCEL			= 0x00000005;
		public const int MB_HELP				= 0x00004000;

		public const int MB_DEFBUTTON1			= 0x00000000;
		public const int MB_DEFBUTTON2			= 0x00000100;
		public const int MB_DEFBUTTON3			= 0x00000200;
		public const int MB_DEFBUTTON4			= 0x00000300;


		// Scroll bar messages
		public const uint SIF_RANGE = 0x0001;
		public const uint SIF_PAGE = 0x0002;
		public const uint SIF_POS = 0x0004;
		public const uint SIF_DISABLENOSCROLL = 0x0008;
		public const uint SIF_TRACKPOS = 0x0010;
		public const uint SIF_ALL = (SIF_RANGE | SIF_PAGE | SIF_POS | SIF_TRACKPOS);

		#endregion


		[StructLayout(LayoutKind.Sequential)]
		public struct POINT 
		{
			public int x;
			public int y;

			//public POINT()
			//{
			//}

			public POINT(int x, int y)
			{
				this.x = x;
				this.y = y;
			} 	
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct RECT
		{
			public int left;
			public int top;
			public int right;
			public int bottom;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct SCROLLINFO
		{
			public uint	cbSize;
			public uint	fMask;
			public int	nMin;
			public int	nMax;
			public uint	nPage;
			public int	nPos;
			public int	nTrackPos;

			public SCROLLINFO(uint mask)
			{
				this.cbSize = (uint) System.Runtime.InteropServices.Marshal.SizeOf(typeof(SCROLLINFO));
				this.fMask = mask;
				this.nMin = 0;
				this.nMax = 0;
				this.nPage = 0;
				this.nPos = 0;
				this.nTrackPos = 0;
			}
		}

		[DllImport("user32.dll")]
		public static extern IntPtr GetFocus();

        [DllImport("user32", CharSet=CharSet.Auto)] 
        public static extern IntPtr SendMessage(HWND hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(HWND hWnd, uint Msg, int wParam, int lParam);

		[DllImport("user32", CharSet=CharSet.Auto)] 
        public static extern int PostMessage(HWND hwnd, uint wMsg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32", CharSet = CharSet.Auto)]
        public static extern bool ShowWindow(HWND hwnd, int cmd);

		[DllImport("user32", CharSet=CharSet.Auto)] 
        public static extern short GetKeyState(int nVirtKey);

        [DllImport("user32", CharSet = CharSet.Auto)]
        public static extern int LockWindowUpdate(HWND hwnd);

		[DllImport("user32", CharSet=CharSet.Auto)] 
        public static extern bool GetScrollRange(HWND hWnd, int nBar, out int lpMinPos, out int lpMaxPos);

        [DllImport("user32", CharSet = CharSet.Auto)]
        public static extern int GetScrollPos(HWND hWnd, int nBar);

        [DllImport("user32", CharSet = CharSet.Auto)]
        public static extern int GetScrollInfo(HWND hWnd, int nBar, ref SCROLLINFO lpsi);

        [DllImport("user32", CharSet = CharSet.Auto)]
        public static extern bool SetScrollPos(HWND hWnd, int nBar, int nPos, int bRedraw);

		public const int CTRL_C_EVENT = 0;
		public const int CTRL_BREAK_EVENT = 1;

		[DllImport("kernel32")] 
		public static extern bool GenerateConsoleCtrlEvent(int dwCtrlEvent, int dwProcessGroupId);

        public static unsafe void RichTextBox_GetScrollPos(HWND handle, out int x, out int y)
		{
			Win32.POINT point = new Win32.POINT();

			Win32.SendMessage(handle, Win32.EM_GETSCROLLPOS, new IntPtr(0), new IntPtr(&point));
			x = point.x;
			y = point.y;
		}

        public static unsafe void RichTextBox_SetScrollPos(HWND handle, int x, int y)
		{
			Win32.POINT point = new Win32.POINT(x, y);

			Win32.SendMessage(handle, Win32.EM_SETSCROLLPOS, new IntPtr(0), new IntPtr(&point));
		}

		public const UInt32 FLASHW_STOP = 0;		//Stop flashing. The system restores the window to its original state.
		public const UInt32 FLASHW_CAPTION = 1;		//Flash the window caption.
		public const UInt32 FLASHW_TRAY = 2;		//Flash the taskbar button.
		public const UInt32 FLASHW_ALL = 3;			//Flash both the window caption and taskbar button.
		public const UInt32 FLASHW_TIMER = 4;		//Flash continuously, until the FLASHW_STOP flag is set.
		public const UInt32 FLASHW_TIMERNOFG = 12;	//Flash continuously until the window comes to the foreground.

		[StructLayout(LayoutKind.Sequential)]
		public struct FLASHWINFO
		{
			public UInt32 cbSize;
			public IntPtr hwnd;
			public UInt32 dwFlags;
			public UInt32 uCount;
			public UInt32 dwTimeout;
		}

		[DllImport("user32.dll")]
		static extern Int32 FlashWindowEx(ref FLASHWINFO pwfi);

		public static bool FlashWindow(IntPtr hwnd)
		{
			FLASHWINFO info = new FLASHWINFO();
			info.cbSize = Convert.ToUInt32(Marshal.SizeOf(info));
			info.hwnd = hwnd;
			info.dwFlags = FLASHW_ALL | FLASHW_TIMERNOFG;
			info.uCount = UInt32.MaxValue;
			info.dwTimeout = 0;

			return (FlashWindowEx(ref info) == 0);
		}

		public static bool FlashWindow(IntPtr hwnd, UInt32 flags, UInt32 count, UInt32 timeout)
		{
			FLASHWINFO info = new FLASHWINFO();
			info.cbSize = Convert.ToUInt32(Marshal.SizeOf(info));
			info.hwnd = hwnd;
			info.dwFlags = flags;
			info.uCount = count;
			info.dwTimeout = timeout;

			return (FlashWindowEx(ref info) == 0);
		}

		[DllImport("user32.dll")]
		public static extern IntPtr SetFocus(IntPtr hWnd);

		[DllImport("user32.dll")]
		public static extern IntPtr GetForegroundWindow();

		[DllImport("user32.dll")]
		public static extern IntPtr GetDlgItem(IntPtr hwnd, int id);

		[DllImport("user32.dll")]
		public static extern bool SetWindowText(IntPtr hwnd, string text);

		[DllImport("user32.dll")]
		public static extern int SetWindowLong(IntPtr hwnd, int index, int newValue);

		[DllImport("shell32.dll")]
		public static extern IntPtr ExtractIcon(IntPtr hInst, string lpszExeFileName, int nIconIndex);

		[DllImport("user32.dll")]
		public static extern void DestroyWindow(HWND hwnd);

		[DllImport("user32.dll", EntryPoint = "MessageBox")]
		public static extern int MessageBox(IntPtr hwnd, string text, string caption, int options);

		public delegate int HookProc(int code, IntPtr wParam, IntPtr lParam);

		[DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
		public static extern IntPtr SetWindowsHookEx(int code, HookProc func, IntPtr hInstance, int threadID);

		[DllImport("user32.dll")]
		public static extern int UnhookWindowsHookEx(IntPtr hhook);

		[DllImport("user32.dll")]
		public static extern int CallNextHookEx(IntPtr hhook, int code, IntPtr wParam, IntPtr lParam);

		[DllImport("user32.dll")]
		public static extern int GetClassName(IntPtr hwnd, StringBuilder lpClassName, int nMaxCount);
	}
}
