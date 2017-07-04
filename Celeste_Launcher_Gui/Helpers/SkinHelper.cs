#region Using directives

using System;
using System.Collections;
using System.Collections.Generic;
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
            var fontLength = Resources.Aclonica.Length;

            // create a buffer to read in to
            var fontdata = Resources.Aclonica;

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


        /// <summary>
        ///     Configure the Skin in a form
        /// </summary>
        /// <param name="form">Form</param>
        /// <param name="lbTitle">Label control that contains the window title</param>
        /// <param name="lbClose">Label control that contains the close button</param>
        /// <param name="highlightList">List of label controls that we want to highlight when the mouse is over</param>
        public static void ConfigureSkin(Form form, Label lbTitle, Label lbClose, List<Label> highlightList)
        {
            try
            {
                SetFont(form.Controls);

                if (lbTitle != null)
                    lbTitle.MouseDown += LbTitle_MouseDown;

                if (lbClose != null)
                {
                    lbClose.MouseEnter += LbClose_MouseEnter;
                    lbClose.MouseLeave += LbClose_MouseLeave;
                    lbClose.Click += LbClose_Click;
                }

                if (highlightList == null || highlightList.Count <= 0) return;

                foreach (var lb in highlightList)
                {
                    lb.MouseEnter += LbHover_MouseEnter;
                    lb.MouseLeave += LbHover_MouseLeave;
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }

        /// <summary>
        ///     Configure the Skin in a form
        /// </summary>
        /// <param name="ctrl">Control</param>
        /// <param name="highlightList">List of label controls that we want to highlight when the mouse is over</param>
        public static void ConfigureSkin(Control ctrl, List<Label> highlightList)
        {
            try
            {
                SetFont(ctrl.Controls);

                if (highlightList == null || highlightList.Count <= 0) return;

                foreach (var lb in highlightList)
                {
                    lb.MouseEnter += LbHover_MouseEnter;
                    lb.MouseLeave += LbHover_MouseLeave;
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public static void SetFont(IEnumerable controls)
        {
            foreach (Control c in controls)
            {
                var label = c as Label;
                if (label != null)
                    label.Font = GetFont(label.Font.Size);

                var box = c as TextBox;
                if (box != null)
                    box.Font = GetFont(box.Font.Size);

                var checkBox = c as CheckBox;
                if (checkBox != null)
                    checkBox.Font = GetFont(checkBox.Font.Size);

                var radioButton = c as RadioButton;
                if (radioButton != null)
                    radioButton.Font = GetFont(radioButton.Font.Size);

                var view = c as ListView;
                if (view != null)
                    view.Font = GetFont(view.Font.Size);

                if (c.Controls.Count > 0)
                    SetFont(c.Controls);
            }
        }

        private static void LbTitle_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                var findForm = ((Label) sender).FindForm();
                if (findForm != null)
                    SendMessage(findForm.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private static void LbHover_MouseEnter(object sender, EventArgs e)
        {
            ((Label) sender).ForeColor = Color.Yellow;
        }

        private static void LbHover_MouseLeave(object sender, EventArgs e)
        {
            ((Label) sender).ForeColor = Color.White;
        }

        private static void LbClose_MouseEnter(object sender, EventArgs e)
        {
            ((Label) sender).ForeColor = Color.Yellow;
        }

        private static void LbClose_MouseLeave(object sender, EventArgs e)
        {
            ((Label) sender).ForeColor = Color.Black;
        }

        private static void LbClose_Click(object sender, EventArgs e)
        {
            var findForm = ((Label) sender).FindForm();
            findForm?.Close();
        }

        #region Windows Api Calls

        [DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont, IntPtr pdv,
            [In] ref uint pcFonts);

        private static PrivateFontCollection _pfc;

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        #endregion
    }
}