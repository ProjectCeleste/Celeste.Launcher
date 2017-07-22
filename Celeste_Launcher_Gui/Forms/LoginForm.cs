#region Using directives

using System;
using System.Diagnostics;
using System.Dynamic;
using System.Reflection;
using System.Windows.Forms;
using Celeste_Launcher_Gui.Helpers;
using Celeste_Launcher_Gui.Properties;
using Celeste_User.Remote;

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

            //Configure Fonts
            SkinHelper.SetFont(Controls);

            //Load UserConfig
            if (Program.UserConfig?.LoginInfo?.RememberMe != true) return;

            tb_Mail.Text = Program.UserConfig.LoginInfo.Email;
            tb_Password.Text = Program.UserConfig.LoginInfo.Password;
            cb_RememberMe.Checked = Program.UserConfig.LoginInfo.RememberMe;
        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            if (!Celeste_User.Helpers.IsValideEmailAdress(tb_Mail.Text))
            {
                SkinHelper.ShowMessage(@"Invalid Email!", @"Project Celeste -- Login",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (tb_Password.Text.Length < 8 || tb_Password.Text.Length > 32)
            {
                SkinHelper.ShowMessage(@"Password minimum length is 8 char, maximum length is 32 char!",
                    @"Project Celeste -- Login",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DoLoggedIn(tb_Mail.Text, tb_Password.Text);
        }

        private void btn_Register_Click(object sender, EventArgs e)
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

            if (Program.WebSocketClient.State == WebSocketClientState.Logged ||
                Program.WebSocketClient.State == WebSocketClientState.Logging)
            {
                SkinHelper.ShowMessage(@"Already logged-in or logged-in in progress!", @"Project Celeste -- Login",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                Enabled = true;
                return;
            }

            if (Program.WebSocketClient.State != WebSocketClientState.Connected)
            {
                Program.WebSocketClient.StartConnect();

                var starttime = DateTime.UtcNow;
                while (Program.WebSocketClient.State != WebSocketClientState.Connected &&
                       Program.WebSocketClient.State != WebSocketClientState.Offline)
                {
                    Application.DoEvents();

                    if (DateTime.UtcNow.Subtract(starttime).TotalSeconds <= 20) continue;

                    SkinHelper.ShowMessage(@"Server connection timeout (> 20sec)!", @"Project Celeste -- Login",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);

                    if (Program.WebSocketClient.State != WebSocketClientState.Offline)
                        Program.WebSocketClient?.AgentWebSocket?.Close();

                    Enabled = true;
                    return;
                }
            }

            if (Program.WebSocketClient.State != WebSocketClientState.Connected)
            {
                SkinHelper.ShowMessage(@"Server Offline", @"Project Celeste -- Login",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (Program.WebSocketClient.State != WebSocketClientState.Offline)
                    Program.WebSocketClient?.AgentWebSocket?.Close();

                Enabled = true;
                return;
            }

            Program.WebSocketClient.State = WebSocketClientState.Logging;

#pragma warning disable IDE0017 // Simplifier l'initialisation des objets
            dynamic loginInfo = new ExpandoObject();
            loginInfo.Mail = email;
            loginInfo.Password = password;
            loginInfo.Version = 136;
#pragma warning restore IDE0017 // Simplifier l'initialisation des objets

            Program.WebSocketClient.AgentWebSocket?.Query<dynamic>("LOGIN", (object) loginInfo, OnLoggedIn);

            var starttime2 = DateTime.UtcNow;
            while (Program.WebSocketClient.State == WebSocketClientState.Logging)
            {
                Application.DoEvents();

                if (DateTime.UtcNow.Subtract(starttime2).TotalSeconds <= 20) continue;

                SkinHelper.ShowMessage(@"Server response timeout (> 20sec)!", @"Project Celeste -- Login",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (Program.WebSocketClient.State != WebSocketClientState.Offline)
                    Program.WebSocketClient?.AgentWebSocket?.Close();

                Enabled = true;
                return;
            }

            if (Program.WebSocketClient.State != WebSocketClientState.Logged)
            {
                Enabled = true;
                return;
            }

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

        private static void OnLoggedIn(dynamic result)
        {
            if (result["Result"].ToObject<bool>())
            {
                Program.RemoteUser = result["RemoteUser"].ToObject<RemoteUser>();
                Program.WebSocketClient.State = WebSocketClientState.Logged;
            }
            else
            {
                Program.WebSocketClient.State = WebSocketClientState.Connected;

                Program.WebSocketClient.ErrorMessage = result["Message"].ToObject<string>();
                SkinHelper.ShowMessage($@"{Program.WebSocketClient.ErrorMessage}", @"Project Celeste -- Login",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (Program.WebSocketClient.State != WebSocketClientState.Offline)
                    Program.WebSocketClient?.AgentWebSocket?.Close();
            }
        }

        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            var pname = Process.GetProcessesByName("spartan");
            if (pname.Length <= 0) return;
            SkinHelper.ShowMessage(@"You need to close the game first!");
            e.Cancel = true;
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            try
            {
                if (DwmApi.DwmIsCompositionEnabled())
                    DwmApi.DwmExtendFrameIntoClientArea(Handle, new DwmApi.MARGINS(31, 75, 31, 31));
            }
            catch (Exception)
            {
                //
            }
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://www.xbox.com/en-us/developers/rules");
        }
    }
}