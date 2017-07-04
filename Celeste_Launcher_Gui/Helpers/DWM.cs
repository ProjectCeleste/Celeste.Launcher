#region Using directives

using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

#endregion

namespace Celeste_Launcher_Gui.Helpers
{
    public class UnsafeNativeMethods
    {
        ////the character set needs to be Unicode
        [DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
        internal static extern int SetWindowTheme(IntPtr hWnd, string pszSubAppName, string pszSubIdList);
    }

    public class DwmApi
    {
        public static int ApplyTheme(Control control)
        {
            return ApplyTheme2(control, "Explorer");
        }

        public static int ApplyTheme2(Control control, string theme)
        {
            try
            {
                if (control != null)
                    if (control.IsHandleCreated)
                        return UnsafeNativeMethods.SetWindowTheme(control.Handle, theme, null);
            }
            catch (DllNotFoundException)
            {
                return -1;
            }
            return -1;
        }


        [DllImport("dwmapi.dll", PreserveSig = false)]
        public static extern void DwmEnableBlurBehindWindow(IntPtr hWnd, DWM_BLURBEHIND pBlurBehind);

        [DllImport("dwmapi.dll", PreserveSig = false)]
        public static extern void DwmExtendFrameIntoClientArea(IntPtr hWnd, MARGINS pMargins);

        [DllImport("dwmapi.dll", PreserveSig = false)]
        public static extern bool DwmIsCompositionEnabled();

        [DllImport("dwmapi.dll", PreserveSig = false)]
        public static extern void DwmEnableComposition(bool bEnable);

        [DllImport("dwmapi.dll", PreserveSig = false)]
        public static extern void DwmGetColorizationColor(ref int pcrColorization,
            [MarshalAs(UnmanagedType.Bool)] ref bool pfOpaqueBlend);

        [DllImport("dwmapi.dll", PreserveSig = false)]
        public static extern IntPtr DwmRegisterThumbnail(IntPtr dest, IntPtr source);

        [DllImport("dwmapi.dll", PreserveSig = false)]
        public static extern void DwmUnregisterThumbnail(IntPtr hThumbnail);

        [DllImport("dwmapi.dll", PreserveSig = false)]
        public static extern void DwmUpdateThumbnailProperties(IntPtr hThumbnail, DWM_THUMBNAIL_PROPERTIES props);

        //<DllImport("dwmapi.dll", PreserveSig:=False)> _
        // Public Shared Sub DwmQueryThumbnailSourceSize(ByVal hThumbnail As IntPtr, ByRef size As size)
        // End Sub

        [StructLayout(LayoutKind.Sequential)]
        public class DWM_THUMBNAIL_PROPERTIES
        {
            public const uint DWM_TNP_RECTDESTINATION = 0x1;
            public const uint DWM_TNP_RECTSOURCE = 0x2;
            public const uint DWM_TNP_OPACITY = 0x4;
            public const uint DWM_TNP_VISIBLE = 0x8;
            public const uint DWM_TNP_SOURCECLIENTAREAONLY = 0x10;
            public uint dwFlags;

            [MarshalAs(UnmanagedType.Bool)] public bool fSourceClientAreaOnly;

            [MarshalAs(UnmanagedType.Bool)] public bool fVisible;

            public byte opacity;
            public RECT rcDestination;
            public RECT rcSource;
        }

        [StructLayout(LayoutKind.Sequential)]
        public class MARGINS
        {
            public int cxLeftWidth;
            public int cxRightWidth;

            public int cyBottomHeight;
            public int cyTopHeight;

            public MARGINS(int left, int top, int right, int bottom)
            {
                cxLeftWidth = left;
                cyTopHeight = bottom;
                cxRightWidth = right;
                cyBottomHeight = top;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public class DWM_BLURBEHIND
        {
            public const uint DWM_BB_ENABLE = 0x1;
            public const uint DWM_BB_BLURREGION = 0x2;
            public const uint DWM_BB_TRANSITIONONMAXIMIZED = 0x4;
            public uint dwFlags;

            [MarshalAs(UnmanagedType.Bool)] public bool fEnable;

            [MarshalAs(UnmanagedType.Bool)] public bool fTransitionOnMaximized;

            public IntPtr hRegionBlur;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int left;
            public int top;
            public int right;

            public int bottom;

            public RECT(int left, int top, int right, int bottom)
            {
                this.left = left;
                this.top = top;
                this.right = right;
                this.bottom = bottom;
            }
        }
    }
}