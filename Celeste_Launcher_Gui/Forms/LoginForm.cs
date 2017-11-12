#region Using directives

using System;
using System.Windows.Forms;
using Celeste_AOEO_Controls.Helpers;
using Celeste_AOEO_Controls.MsgBox;
using Celeste_Public_Api.Helpers;

#endregion

namespace Celeste_Launcher_Gui.Forms
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();

            SkinHelper.SetFont(Controls);

            //Load UserConfig
            if (Program.UserConfig?.LoginInfo?.RememberMe != true) return;

            tb_Mail.Text = Program.UserConfig.LoginInfo.Email;
            tb_Password.Text = Program.UserConfig.LoginInfo.Password;
            cb_RememberMe.Checked = Program.UserConfig.LoginInfo.RememberMe;
        }

        private void Btn_Login_Click(object sender, EventArgs e)
        {
            if (!Misc.IsValideEmailAdress(tb_Mail.Text))
            {
                MsgBox.ShowMessage(@"Invalid Email!", @"Project Celeste -- Login",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (tb_Password.Text.Length < 8 || tb_Password.Text.Length > 32)
            {
                MsgBox.ShowMessage(@"Password minimum length is 8 char, maximum length is 32 char!",
                    @"Project Celeste -- Login",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DoLoggedIn(tb_Mail.Text, tb_Password.Text);
        }

        public void DoLoggedIn(string email, string password)
        {
            Enabled = false;
            try
            {
                Program.WebSocketClient.StartConnect(true, email, password);

                //Save UserConfig
                if (Program.UserConfig == null)
                {
                    Program.UserConfig = new UserConfig
                    {
                        LoginInfo = new LoginInfo
                        {
                            Email = tb_Mail.Text,
                            Password = tb_Password.Text,
                            RememberMe = cb_RememberMe.Checked
                        }
                    };
                }
                else
                {
                    Program.UserConfig.LoginInfo.Email = tb_Mail.Text;
                    Program.UserConfig.LoginInfo.Password = tb_Password.Text;
                    Program.UserConfig.LoginInfo.RememberMe = cb_RememberMe.Checked;
                }

                Program.UserConfig.Save(Program.UserConfigFilePath);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception e)
            {
                MsgBox.ShowMessage($"Error: {e.Message}", @"Project Celeste -- Login",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Enabled = true;
        }

        private void UpdaterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (var form = new UpdaterForm())
                {
                    form.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MsgBox.ShowMessage(
                    $"Warning: Error with updater. Error message: {ex.Message}",
                    @"Project Celeste",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                using (var form = new GameScan())
                {
                    form.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MsgBox.ShowMessage(
                    $"Warning: Error with Game Scan. Error message: {ex.Message}",
                    @"Project Celeste",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LinkLbl_ForgotPwd_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (var form = new ResetPwdForm())
            {
                form.ShowDialog();

                if (form.DialogResult != DialogResult.OK)
                    return;

                tb_Mail.Text = form.tb_Mail.Text;
                tb_Password.Text = "";
            }
        }

        private void PictureBoxButtonCustom1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void LoginForm_Load(object sender, EventArgs e)
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