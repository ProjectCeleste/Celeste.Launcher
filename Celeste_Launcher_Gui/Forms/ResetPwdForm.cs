#region Using directives

using System;
using System.Windows.Forms;
using Celeste_AOEO_Controls.Helpers;
using Celeste_AOEO_Controls.MsgBox;

#endregion

namespace Celeste_Launcher_Gui.Forms
{
    public partial class ResetPwdForm : Form
    {
        public ResetPwdForm()
        {
            InitializeComponent();

            SkinHelper.SetFont(Controls);
        }

        private async void Btn_Verify_Click(object sender, EventArgs e)
        {
            Enabled = false;

            try
            {
                var response = await Program.WebSocketApi.DoForgotPwd(tb_Mail.Text);

                if (response.Result)
                {
                    p_Verify.Enabled = false;
                    p_ResetPassword.Enabled = true;

                    MsgBox.ShowMessage($@"{response.Message}", @"Celeste Fan Project",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MsgBox.ShowMessage($@"Error: {response.Message}", @"Celeste Fan Project",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MsgBox.ShowMessage($"Error: {ex.Message}", @"Celeste Fan Project",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Enabled = true;
        }

        private async void Btn_ResetPassword_Click(object sender, EventArgs e)
        {
            Enabled = false;

            try
            {
                var response = await Program.WebSocketApi.DoResetPwd(tb_Mail.Text, tb_InviteCode.Text);

                if (response.Result)
                {
                    MsgBox.ShowMessage($@"{response.Message}", @"Celeste Fan Project",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);

                    DialogResult = DialogResult.OK;
                    Close();
                    return;
                }
                MsgBox.ShowMessage($@"Error: {response.Message}", @"Celeste Fan Project",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MsgBox.ShowMessage($"Error: {ex.Message}", @"Celeste Fan Project",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Enabled = true;
        }

        private void PictureBoxButtonCustom1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void ResetPwdForm_Load(object sender, EventArgs e)
        {
            try
            {
                if (DwmApi.DwmIsCompositionEnabled())
                    DwmApi.DwmExtendFrameIntoClientArea(Handle, new DwmApi.MARGINS(10, 10, 10, 10));
            }
            catch (Exception)
            {
                //
            }
        }
    }
}