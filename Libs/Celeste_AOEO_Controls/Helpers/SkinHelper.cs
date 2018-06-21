#region Using directives

using System;
using System.Runtime.InteropServices;

#endregion

namespace Celeste_AOEO_Controls.Helpers
{
    public class SkinHelper
    {
        // ReSharper disable InconsistentNaming
        public const int WM_NCLBUTTONDOWN = 0xA1;

        public const int HT_CAPTION = 0x2;
        // ReSharper restore InconsistentNaming

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
    }
}