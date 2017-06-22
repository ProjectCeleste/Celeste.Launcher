using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Celeste_Launcher_Gui
{
    //public class UnsafeNativeMethods
    //{
    //    [DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
    //    internal static extern int SetWindowTheme(IntPtr hWnd, String pszSubAppName, String pszSubIdList);
    //}

    public class DwmApi
    {
        [DllImport("dwmapi.dll", PreserveSig = false)]
        public static extern void DwmExtendFrameIntoClientArea(IntPtr hWnd, MARGINS pMargins);


        [DllImport("dwmapi.dll", PreserveSig = false)]
        public static extern bool DwmIsCompositionEnabled();

        [StructLayout(LayoutKind.Sequential)]
        public class MARGINS
        {
            public int cxLeftWidth;
            public int cxRightWidth;
            public int cyTopHeight;
            public int cyBottomHeight;

            public MARGINS(int left, int top, int right, int bottom)
            {
                cxLeftWidth = left;
                cyTopHeight = top;
                cxRightWidth = right;
                cyBottomHeight = bottom;
            }
        }
        
        //public static int ApplyTheme(Control control)
        //{
        //    return ApplyTheme2(control, "Explorer");
        //}

        //public static int ApplyTheme2(Control control, string theme)
        //{
        //    try
        //    {
        //        if (control != null && control.IsHandleCreated)
        //            return UnsafeNativeMethods.SetWindowTheme(control.Handle, theme, null);
        //    }
        //    catch(DllNotFoundException)
        //    {
        //        return -1;
        //    }
        //    return -1;
        //}



    }
}
