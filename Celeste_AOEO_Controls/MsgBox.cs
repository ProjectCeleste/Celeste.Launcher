#region Using directives

using System;
using System.Windows.Forms;

#endregion

namespace Celeste_AOEO_Controls
{
    public partial class CustomMsgBox : Form
    {
        public CustomMsgBox(string message)
        {
            InitializeComponent();
            label1.Text = message;

            //Configure Fonts
            SkinHelper.SetFont(Controls);
        }

        public static void ShowMessage(string message)
        {
            using (var frm = new CustomMsgBox(message))
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
                    DwmApi.DwmExtendFrameIntoClientArea(Handle, new DwmApi.MARGINS(31, 75, 31, 26));
            }
            catch (Exception)
            {
                //
            }
        }
    }
}