#region Using directives

using System;
using System.Windows.Forms;
using Celeste_AOEO_Controls.Helpers;
using Celeste_AOEO_Controls.MsgBox;

#endregion

namespace Celeste_Launcher_Gui.Forms
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();

            SkinHelperFonts.SetFont(Controls);
        }

        private async void Btn_Verify_Click(object sender, EventArgs e)
        {
            Enabled = false;

            try
            {
                var response = await LegacyBootstrapper.WebSocketApi.DoValidMail(tb_Mail.Text);

                if (response.Result)
                {
                    p_Verify.Enabled = false;
                    p_Register.Enabled = true;

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

        private async void Btn_Register_Click(object sender, EventArgs e)
        {
            if (tb_ConfirmPassword.Text != tb_Password.Text)
            {
                MsgBox.ShowMessage(@"Password value and confirm password value don't match!",
                    @"Project Celeste -- Register",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Enabled = false;

            try
            {
                var response = await LegacyBootstrapper.WebSocketApi.DoRegister(tb_Mail.Text, tb_InviteCode.Text, tb_UserName.Text,
                    tb_Password.Text);

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

        private void RegisterForm_Load(object sender, EventArgs e)
        {
            try
            {
                if (DwmApi.DwmIsCompositionEnabled())
                    DwmApi.DwmExtendFrameIntoClientArea(Handle, new DwmApi.MARGINS(33, 10, 33, 10));
            }
            catch (Exception)
            {
                //
            }
        }
    }
}