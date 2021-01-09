using System;
using System.Runtime.InteropServices;

namespace Celeste_Launcher_Gui.Win32
{
    public static class WindowsInterop
    {
        public static void SetWindowOwner(IntPtr owner, IntPtr origin)
        {
            SetWindowLong(new HandleRef(null, owner), -8, origin);
        }

        [DllImport("PresentationNative_v0400.dll", CharSet = CharSet.Auto, EntryPoint = "SetWindowLongWrapper")]
        public static extern int SetWindowLong(HandleRef hWnd, int nIndex, int dwNewLong);


        [DllImport("PresentationNative_v0400.dll", CharSet = CharSet.Auto, EntryPoint = "SetWindowLongPtrWrapper")]
        public static extern IntPtr SetWindowLongPtr(HandleRef hWnd, int nIndex, IntPtr dwNewLong);


        private static IntPtr SetWindowLong(HandleRef hWnd, int nIndex, IntPtr dwNewLong)
        {
            if (IntPtr.Size == 4)
            {
                int value = SetWindowLong(hWnd, nIndex, IntPtrToInt32(dwNewLong));
                return new IntPtr(value);
            }
            else
            {
                return SetWindowLongPtr(hWnd, nIndex, dwNewLong);
            }
        }

        private static int IntPtrToInt32(IntPtr intPtr)
        {
            return (int)intPtr.ToInt64();
        }
    }
}
