#region Using directives

using System;
using System.Windows.Forms;
using Celeste_AOEO_Controls.Helpers;

#endregion

namespace Celeste_AOEO_Controls.MsgBox
{
    public partial class MsgBox : Form
    {
        public MsgBox(string message)
        {
            InitializeComponent();

            SkinHelperFonts.SetFont(Controls);

            label1.Text = message;
        }

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

        private void Lb_OK_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MsgBox_Load(object sender, EventArgs e)
        {
            try
            {
                if (DwmApi.DwmIsCompositionEnabled())
                    DwmApi.DwmExtendFrameIntoClientArea(Handle, new DwmApi.MARGINS(8, 8, 8, 8));
            }
            catch (Exception)
            {
                //
            }
        }
    }
}