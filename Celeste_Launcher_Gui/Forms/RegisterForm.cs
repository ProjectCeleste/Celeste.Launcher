#region Using directives

using System;
using System.Dynamic;
using System.Windows.Forms;
using Celeste_Launcher_Gui.Helpers;

#endregion

namespace Celeste_Launcher_Gui.Forms
{
    public partial class RegisterForm : Form
    {
        private bool _registerUserDone;
        private bool _registerUserFailed;

        public RegisterForm()
        {
            InitializeComponent();

            //Configure Fonts
            SkinHelper.SetFont(Controls);
        }

        private void btn_Register_Click(object sender, EventArgs e)
        {
            if (!Celeste_User.Helpers.IsValideEmailAdress(tb_Mail.Text))
            {
                SkinHelper.ShowMessage(@"Invalid Email!", @"Project Celeste -- Register",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            if (!Celeste_User.Helpers.IsValideUserName(tb_UserName.Text))
            {
                SkinHelper.ShowMessage(
                    @"Invalid User Name, only letters and digits allowed, minimum length is 3 char and maximum length is 16 char!",
                    @"Project Celeste -- Register",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (tb_ConfirmPassword.Text != tb_Password.Text)
            {
                SkinHelper.ShowMessage(@"Password value and confirm password value don't match!",
                    @"Project Celeste -- Register",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (tb_Password.Text.Length < 8 || tb_Password.Text.Length > 32)
            {
                SkinHelper.ShowMessage(@"Password minimum length is 8 char,  maximum length is 32 char!",
                    @"Project Celeste -- Register",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (tb_InviteCode.Text.Length != 128)
            {
                SkinHelper.ShowMessage(@"Invalid Invite Code!",
                    @"Project Celeste -- Register",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DoRegisterUser(tb_Mail.Text, tb_Password.Text, tb_UserName.Text, tb_InviteCode.Text);
        }

        private void DoRegisterUser(string email, string password, string username, string inviteCode)
        {
            _registerUserDone = false;

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

                    Enabled = true;
                    return;
                }
            }

            if (Program.WebSocketClient.State != WebSocketClientState.Connected)
            {
                SkinHelper.ShowMessage(@"Server Offline", @"Project Celeste -- Login",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                Enabled = true;
                return;
            }

            dynamic registerUserInfo = new ExpandoObject();
            registerUserInfo.Mail = email;
            registerUserInfo.Password = password;
            registerUserInfo.UserName = username;
            registerUserInfo.InviteCode = inviteCode;

            Program.WebSocketClient?.AgentWebSocket?.Query<dynamic>("REGISTER", (object) registerUserInfo,
                OnRegisterUser);

            var starttime2 = DateTime.UtcNow;
            while (!_registerUserDone)
            {
                Application.DoEvents();

                if (DateTime.UtcNow.Subtract(starttime2).TotalSeconds <= 20) continue;

                SkinHelper.ShowMessage(@"Server response timeout (> 20sec)!", @"Project Celeste -- Register",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (Program.WebSocketClient.State != WebSocketClientState.Offline)
                    Program.WebSocketClient?.AgentWebSocket?.Close();

                Enabled = true;
                return;
            }

            Enabled = true;

            if (_registerUserFailed)
            {
                Focus();
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void OnRegisterUser(dynamic result)
        {
            if (result["Result"].ToObject<bool>())
            {
                _registerUserFailed = false;
                SkinHelper.ShowMessage(@"Registred with success.", @"Project Celeste -- Register",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                _registerUserFailed = true;
                var str = result["Message"].ToObject<string>();
                SkinHelper.ShowMessage($@"Error: {str}", @"Project Celeste -- Register",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (Program.WebSocketClient.State != WebSocketClientState.Offline)
                Program.WebSocketClient.AgentWebSocket.Close();

            _registerUserDone = true;
        }

        private void RegisterForm_Load(object sender, EventArgs e)
        {
            if (DwmApi.DwmIsCompositionEnabled())
                DwmApi.DwmExtendFrameIntoClientArea(Handle, new DwmApi.MARGINS(16, 73, 16, 31));
        }

        private void btnSmall1_Load(object sender, EventArgs e)
        {
        }
    }
}