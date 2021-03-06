// Name: MSG.cs
// Author: Stephen Toub

using System;
using System.Runtime.InteropServices;

namespace PreviewHandler.COMInterop
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct MSG
    {
        public IntPtr hwnd;
        public int message;
        public IntPtr wParam;
        public IntPtr lParam;
        public int time;
        public int pt_x;
        public int pt_y;
    }
}
