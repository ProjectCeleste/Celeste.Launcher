#region Using directives

using System;
using System.Windows.Forms;
using Celeste_AOEO_Controls.Helpers;
using Celeste_AOEO_Controls.MsgBox;

#endregion

namespace Celeste_Launcher_Gui.Forms
{
    public partial class ChangePwdForm : Form
    {
        public ChangePwdForm()
        {
            InitializeComponent();

            SkinHelperFonts.SetFont(Controls);
        }

        private async void BtnSmall1_Click(object sender, EventArgs e)
        {
            if (tb_ConfirmPassword.Text != tb_NewPassword.Text)
            {
                MsgBox.ShowMessage(@"New password value and confirm new password value don't match!",
                    @"Celeste Fan Project",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            Enabled = false;

            try
            {
                var response = await LegacyBootstrapper.WebSocketApi.DoChangePassword(tb_Password.Text, tb_ConfirmPassword.Text);

                if (response.Result)
                {
                    MsgBox.ShowMessage(@"Password changed with success.", @"Celeste Fan Project",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (LegacyBootstrapper.UserConfig?.LoginInfo != null)
                        LegacyBootstrapper.UserConfig.LoginInfo.Password = tb_ConfirmPassword.Text;

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

        private void ChangePwdForm_Load(object sender, EventArgs e)
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