using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;

namespace WeGamePlus
{
    public class Win32Native
    {
        public const uint WS_EX_LAYERED = 0x80000;
        public const int WS_EX_TRANSPARENT = 0x20;
        public const int GWL_STYLE = -16;
        public const int GWL_HWNDPARENT = -8;
        public const int GWL_EXSTYLE = -20;
        public const int WS_CHILD = 0x40000000;
        public const int LWA_ALPHA = 3;
        public const int SWP_NOMOVE = 2;
        public const int SWP_NOSIZE = 1;
        public const int SWP_NOOWNERZORDER = 0x200;
        public const int SWP_NOCOPYBITS = 0x100;
        public const uint WS_POPUP = 0x80000000;
        public const int HWND_BOTTOM = -1;
        public const int SWP_SHOWWINDOW = 0x40;
        public const int SWP_FRAMECHANGED = 0x20;
        public const int SWP_NOZORDER = 4;
        public const int WM_SETREDRAW = 11;
        public const int WM_CLOSE = 11;
        public const int WM_SYSCOMMAND = 0x112;
        public const int SC_MINIMIZE = 0xf020;
        public const int SW_SHOW = 5;
        public const int SW_HIDE = 0;
        public const int SC_CLOSE = 0xf060;

        public static void ClearMemory(IntPtr intp)
        {
            EmptyWorkingSet(intp);
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        [DllImport("psapi.dll")]
        private static extern int EmptyWorkingSet(IntPtr hwProc);
        [DllImport("User32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("User32.dll")]
        public static extern IntPtr GetDC(IntPtr hWnd);
        [DllImport("user32.dll", CharSet=CharSet.Auto, ExactSpelling=true)]
        public static extern IntPtr GetForegroundWindow();
        [DllImport("gdi32.dll")]
        public static extern uint GetPixel(IntPtr hdc, int x, int y);
        public static uint GetPixelColor(Point pt, IntPtr t)
        {
            IntPtr dC = GetDC(t);
            ReleaseDC(IntPtr.Zero, dC);
            return GetPixel(dC, pt.X, pt.Y);
        }

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);
        [DllImport("User32.dll", CharSet=CharSet.Auto)]
        public static extern int GetWindowThreadProcessId(IntPtr hwnd, out int ID);
        [DllImport("user32.dll", CharSet=CharSet.Auto)]
        public static extern int MoveWindow(IntPtr hWnd, int x, int y, int nWidth, int nHeight, bool BRePaint);
        [DllImport("user32", CharSet=CharSet.Ansi, SetLastError=true, ExactSpelling=true)]
        public static extern int ReleaseDC(IntPtr hwnd, IntPtr hdc);
        [DllImport("user32", CharSet=CharSet.Ansi, SetLastError=true, ExactSpelling=true)]
        public static extern int ScreenToClient(IntPtr hwnd, ref POINT lppoint);
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int wMsg, bool wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern IntPtr SetFocus(IntPtr hWnd);
        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll")]
        public static extern int SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
        [DllImport("kernel32.dll")]
        public static extern int SetProcessWorkingSetSize(IntPtr process, int minSize, int maxSize);
        [DllImport("user32.dll", CharSet=CharSet.Auto)]
        public static extern int SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int y, int Width, int Height, int flags);
        [DllImport("user32.dll", CharSet=CharSet.Auto)]
        public static extern int ShowWindow(IntPtr hwnd, int nCmdShow);
        [DllImport("user32.dll")]
        public static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);
        [DllImport("user32.dll")]
        public static extern bool TerminateProcess(IntPtr hProcess, uint uExitCode);
        [DllImport("user32")]
        public static extern IntPtr WindowFromPoint(Point Point);

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            private int x;
            private int y;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct tagRECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct tagWINDOWINFO
        {
            public uint cbSize;
            public Win32Native.tagRECT rcWindow;
            public Win32Native.tagRECT rcClient;
            public uint dwStyle;
            public uint dwExStyle;
            public uint dwWindowStatus;
            public uint cxWindowBorders;
            public uint cyWindowBorders;
            public ushort atomWindowType;
            public ushort wCreatorVersion;
        }
    }
}

