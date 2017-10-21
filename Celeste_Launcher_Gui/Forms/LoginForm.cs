#region Using directives

using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;
using Celeste_AOEO_Controls;

#endregion

namespace Celeste_Launcher_Gui.Forms
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();

            //
            lb_Ver.Text = $@"v{Assembly.GetEntryAssembly().GetName().Version}";

            //Load UserConfig
            if (Program.UserConfig?.LoginInfo?.RememberMe != true) return;

            tb_Mail.Text = Program.UserConfig.LoginInfo.Email;
            tb_Password.Text = Program.UserConfig.LoginInfo.Password;
            cb_RememberMe.Checked = Program.UserConfig.LoginInfo.RememberMe;
        }

        private void Btn_Login_Click(object sender, EventArgs e)
        {
            if (!Celeste_User.Helpers.IsValideEmailAdress(tb_Mail.Text))
            {
                CustomMsgBox.ShowMessage(@"Invalid Email!", @"Project Celeste -- Login",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (tb_Password.Text.Length < 8 || tb_Password.Text.Length > 32)
            {
                CustomMsgBox.ShowMessage(@"Password minimum length is 8 char, maximum length is 32 char!",
                    @"Project Celeste -- Login",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DoLoggedIn(tb_Mail.Text, tb_Password.Text);
        }

        private void Btn_Register_Click(object sender, EventArgs e)
        {
            using (var form = new RegisterForm())
            {
                Hide();
                form.ShowDialog();
                if (form.DialogResult == DialogResult.OK)
                {
                    tb_Mail.Text = form.tb_Mail.Text;
                    tb_Password.Text = form.tb_Password.Text;
                    cb_RememberMe.Checked = true;

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
                }
                Show();
            }
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
                CustomMsgBox.ShowMessage($"Error: {e.Message}", @"Project Celeste -- Login",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Enabled = true;
        }

        private void LinkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://www.xbox.com/en-us/developers/rules");
        }

        private void LinkLbl_ForgotPwd_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (var form = new ResetPwdForm())
            {
                Hide();
                form.ShowDialog();
                if (form.DialogResult == DialogResult.OK)
                {
                    tb_Mail.Text = form.tb_Mail.Text;
                    tb_Password.Text = "";
                    cb_RememberMe.Checked = true;
                }
                Show();
            }
        }
    }
}