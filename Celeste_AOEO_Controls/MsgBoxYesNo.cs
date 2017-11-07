#region Using directives

using System;
using System.Windows.Forms;

#endregion

namespace Celeste_AOEO_Controls
{
    public partial class MsgBoxYesNo : Form
    {
        public MsgBoxYesNo(string message)
        {
            InitializeComponent();
            label1.Text = message;

            //Configure Fonts
            SkinHelper.SetFont(Controls);
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
    }
}