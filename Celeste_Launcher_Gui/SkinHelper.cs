using Celeste_Launcher_Gui.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Celeste_Launcher_Gui
{
    public class SkinHelper
    {
        #region Windows Api Calls

        [DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont, IntPtr pdv, [In] ref uint pcFonts);

        private static PrivateFontCollection _pfc;

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        #endregion


        public static void ShowMessage(string message)
        {
            var frm = new MsgBox("PROJECT CELESTE", message);
            frm.ShowDialog();
        }

        public static void ShowMessage(string message, string title)
        {
            var frm = new MsgBox(title, message);
            frm.ShowDialog();
        }

        public static void ShowMessage(string message, string title, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            var frm = new MsgBox(title, message);
            frm.ShowDialog();
        }

        private static void InitFont()
        {
            //Create your private font collection object.
            if (_pfc != null) return;
            _pfc = new PrivateFontCollection();
            
            //Select your font from the resources.            
            int fontLength = Properties.Resources.Aclonica.Length;

            // create a buffer to read in to
            byte[] fontdata = Properties.Resources.Aclonica;

            // create an unsafe memory block for the font data
            System.IntPtr data = Marshal.AllocCoTaskMem(fontLength);

            // copy the bytes to the unsafe memory block
            Marshal.Copy(fontdata, 0, data, fontLength);

            uint cFonts = 0;
            AddFontMemResourceEx(data, (uint)fontLength, IntPtr.Zero, ref cFonts);

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


        /// <summary>
        /// Configure the Skin in a form
        /// </summary>
        /// <param name="form">Form</param>
        /// <param name="lbTitle">Label control that contains the window title</param>
        /// <param name="lbClose">Label control that contains the close button</param>
        /// <param name="highlightList">List of label controls that we want to highlight when the mouse is over</param>
        public static void ConfigureSkin(System.Windows.Forms.Form form, System.Windows.Forms.Label lbTitle, System.Windows.Forms.Label lbClose, List<System.Windows.Forms.Label> highlightList)
        {
            try
            {
                SetFont(form.Controls);

                lbTitle.MouseDown += LbTitle_MouseDown;
                lbClose.MouseEnter += LbClose_MouseEnter;
                lbClose.MouseLeave += LbClose_MouseLeave;
                lbClose.Click += LbClose_Click;

                foreach (var lb in highlightList)
                {
                    lb.MouseEnter += LbHover_MouseEnter;
                    lb.MouseLeave += LbHover_MouseLeave;
                }
            }
            catch { }
        }

        private static void SetFont(System.Windows.Forms.Control.ControlCollection controls)
        {
            foreach (System.Windows.Forms.Control c in controls)
            {
                if (c is System.Windows.Forms.Label)
                    ((System.Windows.Forms.Label)c).Font = GetFont(((System.Windows.Forms.Label)c).Font.Size);

                if (c is System.Windows.Forms.TextBox)
                    ((System.Windows.Forms.TextBox)c).Font = GetFont(((System.Windows.Forms.TextBox)c).Font.Size);

                if (c is System.Windows.Forms.CheckBox)
                    ((System.Windows.Forms.CheckBox)c).Font = GetFont(((System.Windows.Forms.CheckBox)c).Font.Size);

                if (c is ListView)
                    ((System.Windows.Forms.ListView)c).Font = GetFont(((System.Windows.Forms.ListView)c).Font.Size);

                if (c.Controls.Count > 0)
                    SetFont(c.Controls);
            }
        }

        private static void LbTitle_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(((System.Windows.Forms.Label)sender).FindForm().Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private static void LbHover_MouseEnter(object sender, EventArgs e)
        {
            ((System.Windows.Forms.Label)sender).ForeColor = System.Drawing.Color.Yellow;
        }

        private static void LbHover_MouseLeave(object sender, EventArgs e)
        {
            ((System.Windows.Forms.Label)sender).ForeColor = System.Drawing.Color.White;
        }

        private static void LbClose_MouseEnter(object sender, EventArgs e)
        {
            ((System.Windows.Forms.Label)sender).ForeColor = System.Drawing.Color.Yellow;
        }

        private static void LbClose_MouseLeave(object sender, EventArgs e)
        {
            ((System.Windows.Forms.Label)sender).ForeColor = System.Drawing.Color.Black;
        }

        private static void LbClose_Click(object sender, EventArgs e)
        {
            ((System.Windows.Forms.Label)sender).FindForm().Close();
        }

    }
}
