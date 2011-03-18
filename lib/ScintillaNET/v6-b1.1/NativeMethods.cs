using System.Runtime.InteropServices;
using System;

namespace Scintilla
{
    public enum BeepType
    {
        Default = -1,
        Ok = 0x00000000,
        Error = 0x00000010,
        Question = 0x00000020,
        Warning = 0x00000030,
        Information = 0x00000040,
    }

    internal class NativeMethods
    {
        private NativeMethods()
        {
        }
        
        [DllImport("kernel32")]
        internal extern static IntPtr LoadLibrary(string lpLibFileName);

        internal const int WM_NOTIFY = 0x004e;

        /*

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool MessageBeep(BeepType type);

        [DllImport("kernel32.dll")]
        internal static extern bool Beep(int frequency, int time);


        [DllImport("kernel32", EntryPoint = "SendMessage")]
        internal static extern int SendMessageStr(
            IntPtr hWnd,
            int message,
            int data,
            string s);

        [DllImport("user32.dll")]
        internal static extern IntPtr SetFocus(IntPtr hwnd);
         */
    }
}

