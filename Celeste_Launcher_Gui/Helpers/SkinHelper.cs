#region Using directives

using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Celeste_Launcher_Gui.Forms;
using Celeste_Launcher_Gui.Properties;

#endregion

namespace Celeste_Launcher_Gui.Helpers
{
    public class SkinHelper
    {
        public static void ShowMessage(string message)
        {
            using (var frm = new MsgBox(message))
            {
                frm.ShowDialog();
            }
        }

        public static void ShowMessage(string message, string title, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            ShowMessage(message);
        }

        private static void InitFont()
        {
            //Create your private font collection object.
            if (_pfc != null) return;
            _pfc = new PrivateFontCollection();

            //Select your font from the resources.            
            var fontLength = Resources.Ashley_Crawford_CG_1.Length;

            // create a buffer to read in to
            var fontdata = Resources.Ashley_Crawford_CG_1;

            // create an unsafe memory block for the font data
            var data = Marshal.AllocCoTaskMem(fontLength);

            // copy the bytes to the unsafe memory block
            Marshal.Copy(fontdata, 0, data, fontLength);

            uint cFonts = 0;
            AddFontMemResourceEx(data, (uint) fontLength, IntPtr.Zero, ref cFonts);

            //pass the font to the font collection
            _pfc.AddMemoryFont(data, fontLength);
        }

        private static Font GetFont(float size)
        {
            //Call font initialization
            InitFont();

            //return font
            return new Font(_pfc.Families[0], size);
        }

        public static void SetFont(IEnumerable controls)
        {
            
            try
            {
                foreach (Control c in controls)
                    if (c is TextBox)
                    {
                        c.Font = new Font("Arial", c.Font.Size, FontStyle.Regular, GraphicsUnit.Point, 0);
                    }
                    else
                    {
                        c.Font = GetFont(c.Font.Size);
                        if (c.Controls.Count > 0)
                            SetFont(c.Controls);
                    }
            }
            catch (Exception)
            {
                //
            }
        }

        #region Windows Api Calls

        [DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont, IntPtr pdv,
            [In] ref uint pcFonts);

        private static PrivateFontCollection _pfc;

        // ReSharper disable InconsistentNaming
        public const int WM_NCLBUTTONDOWN = 0xA1;

        public const int HT_CAPTION = 0x2;
        // ReSharper restore InconsistentNaming

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        #endregion
    }
}