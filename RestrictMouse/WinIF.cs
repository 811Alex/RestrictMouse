using System;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace RestrictMouse
{
    class WinIF
    {
        private const int DWMWA_EXTENDED_FRAME_BOUNDS = 9;

        static public IntPtr FindWindow(string windowName)
        {
            return FindWindow(IntPtr.Zero, windowName);
        }

        static public int getWinBounds(IntPtr target, out RECT targetRect)
        {
            return DwmGetWindowAttribute(target, DWMWA_EXTENDED_FRAME_BOUNDS, out targetRect, Marshal.SizeOf(typeof(RECT))) == 0 ? 0 : Marshal.GetLastWin32Error();
        }

        static public Process getProcessByHandle(IntPtr handle)
        {
            uint pid;
            GetWindowThreadProcessId(handle, out pid);
            return Process.GetProcessById((int) pid);
        }

        // --- UNMANAGED --- //

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        [DllImport("dwmapi.dll", SetLastError = true)]
        private static extern int DwmGetWindowAttribute(IntPtr hwnd, int dwAttribute, out RECT pvAttribute, int cbAttribute);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr FindWindow(IntPtr ZeroOnly, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetWindowThreadProcessId(IntPtr hWnd, out uint ProcessId);

        // --- SUBCLASSES --- //

        public class SystemEvent
        {
            public delegate void SystemEventEventHandler(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime);
            public event SystemEventEventHandler SystemEventHandler;

            private const uint WINEVENT_OUTOFCONTEXT = 0;
            private delegate void WinEventDelegate(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime);
            private WinEventDelegate m_delegate = null;

            private IntPtr m_foregroundHwnd = IntPtr.Zero;
            public SystemEvent(uint SysEvent)
            {
                m_delegate = new WinEventDelegate(WinEventProc);
                try
                {
                    SetWinEventHook(SysEvent, SysEvent, IntPtr.Zero, m_delegate, Convert.ToUInt32(0), Convert.ToUInt32(0), WINEVENT_OUTOFCONTEXT);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine(ex.ToString());
                }
            }

            public void WinEventProc(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime)
            {
                if ((((SystemEventHandler != null)) && (SystemEventHandler.GetInvocationList().Length > 0)))
                {
                    m_foregroundHwnd = hwnd;
                    SystemEventHandler?.Invoke(hWinEventHook, eventType, hwnd, idObject, idChild, dwEventThread, dwmsEventTime);
                }
            }

            public IntPtr Hwnd
            {
                get { return m_foregroundHwnd; }
            }

            // --- UNMANAGED --- //

            [DllImport("user32.dll", SetLastError = true)]
            private static extern IntPtr SetWinEventHook(uint eventMin, uint eventMax, IntPtr hmodWinEventProc, WinEventDelegate lpfnWinEventProc, uint idProcess, uint idThread, uint dwFlags);

            // --- SUBCLASSES --- //

            public class WinFocusChange : SystemEvent
            {
                private const uint EVENT_OBJECT_FOCUS = 32773;

                public WinFocusChange() : base(EVENT_OBJECT_FOCUS) { }
            }
        }
    }
}
