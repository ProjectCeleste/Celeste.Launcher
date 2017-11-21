#region Using directives

using System;
using System.Windows.Forms;
using Celeste_AOEO_Controls.Helpers;

#endregion

namespace Celeste_AOEO_Controls.MsgBox
{
    public partial class MsgBoxYesNo : Form
    {
        public MsgBoxYesNo(string message)
        {
            InitializeComponent();

            SkinHelper.SetFont(Controls);

            label1.Text = message;
        }

        public static void ShowMessage(string message)
        {
            using (var frm = new MsgBoxYesNo(message))
            {
                frm.ShowDialog();
            }
        }

        public static void ShowMessage(string message, string title, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            ShowMessage(message);
        }

        private void BtnSmall1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void BtnSmall2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
            Close();
        }

        private void MsgBoxYesNo_Load(object sender, EventArgs e)
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