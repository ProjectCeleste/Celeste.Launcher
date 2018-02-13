#region Using directives

using System;
using System.Windows.Forms;
using Celeste_AOEO_Controls.Helpers;
using Celeste_AOEO_Controls.MsgBox;
using Celeste_Public_Api.WebSocket_Api.WebSocket.CommandInfo.Member;

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

        public User CurrentUser { get; private set; }

        private async void Btn_Login_Click(object sender, EventArgs e)
        {
            Enabled = false;
            try
            {
                var response = await Program.WebSocketApi.DoLogin(tb_Mail.Text, tb_Password.Text);

                if (response.Result)
                {
                    //
                    CurrentUser = response.User;

                    //Save UserConfig
                    if (Program.UserConfig == null)
                    {
                        Program.UserConfig = new UserConfig
                        {
                            LoginInfo = new LoginInfo
                            {
                                Email = tb_Mail.Text,
                                Password = tb_Password.Text,
                                RememberMe = cb_RememberMe.Checked,
                                AutoLogin = cB_AutoLogin.Checked
                            }
                        };
                    }
                    else
                    {
                        Program.UserConfig.LoginInfo.Email = tb_Mail.Text;
                        Program.UserConfig.LoginInfo.Password = tb_Password.Text;
                        Program.UserConfig.LoginInfo.RememberMe = cb_RememberMe.Checked;
                        Program.UserConfig.LoginInfo.AutoLogin = cB_AutoLogin.Checked;
                    }

                    try
                    {
                        Program.UserConfig.Save(Program.UserConfigFilePath);
                    }
                    catch (Exception)
                    {
                        //
                    }

                    //
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

        private void Cb_RememberMe_CheckedChanged(object sender, EventArgs e)
        {
            cB_AutoLogin.Enabled = cb_RememberMe.Checked;
            if (!cb_RememberMe.Checked)
                cB_AutoLogin.Checked = false;
        }
    }
}