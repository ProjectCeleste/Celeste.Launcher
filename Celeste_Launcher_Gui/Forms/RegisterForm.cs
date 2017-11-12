#region Using directives

using System;
using System.Dynamic;
using System.Reflection;
using System.Windows.Forms;
using Celeste_AOEO_Controls.MsgBox;
using Celeste_Launcher_Gui.Helpers;
using Celeste_Public_Api.Helpers;

#endregion

namespace Celeste_Launcher_Gui.Forms
{
    public enum RegisterUserState
    {
        TimedOut = -2,
        Failed = -1,
        Idle = 0,
        InProgress = 1,
        Success = 2
    }

    public partial class RegisterForm : Form
    {
        private DateTime _lastVerifyTime = DateTime.UtcNow.AddMinutes(-1);
        private RegisterUserState _registerUserState = RegisterUserState.Idle;
        private RegisterUserState _validMailState = RegisterUserState.Idle;

        public RegisterForm()
        {
            InitializeComponent();
        }

        private void Btn_Verify_Click(object sender, EventArgs e)
        {
            if (!Misc.IsValideEmailAdress(tb_Mail.Text))
            {
                MsgBox.ShowMessage(@"Invalid Email!", @"Project Celeste -- Register",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            var lastSendTime = (DateTime.UtcNow - _lastVerifyTime).TotalSeconds;
            if (lastSendTime <= 45)
            {
                MsgBox.ShowMessage(
                    $"You need to wait at least 45 seconds before asking to resend an confirmation key! Last request was {lastSendTime} seconds ago.",
                    @"Project Celeste -- Register",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }
            _lastVerifyTime = DateTime.UtcNow;

            DoVerifyUser(tb_Mail.Text);
        }

        private void DoVerifyUser(string email)
        {
            Enabled = false;

            try
            {
                Program.WebSocketClient.StartConnect(false);

#pragma warning disable IDE0017 // Simplifier l'initialisation des objets
                dynamic validMailInfo = new ExpandoObject();
                validMailInfo.Version = Assembly.GetEntryAssembly().GetName().Version;
                validMailInfo.EMail = email;
#pragma warning restore IDE0017 // Simplifier l'initialisation des objets

                _validMailState = RegisterUserState.InProgress;

                Program.WebSocketClient.AgentWebSocket.Query<dynamic>("VALIDMAIL", (object) validMailInfo,
                    OnVerifyUser);

                var starttime = DateTime.UtcNow;
                while (_validMailState == RegisterUserState.InProgress &&
                       Program.WebSocketClient.State != WebSocketClientState.Offline)
                {
                    Application.DoEvents();
                    var diff = DateTime.UtcNow.Subtract(starttime).TotalSeconds;
                    if (diff <= WebSocketClient.TimeOut) continue;

                    if (Program.WebSocketClient.State != WebSocketClientState.Offline)
                        Program.WebSocketClient.AgentWebSocket.Close();

                    _validMailState = RegisterUserState.TimedOut;

                    throw new Exception($"DoVerifyUser() Server connection timeout (total send time = {diff})!");
                }

                if (_validMailState == RegisterUserState.Success)
                {
                    p_Verify.Enabled = false;
                    p_Register.Enabled = true;
                }
            }
            catch (Exception e)
            {
                MsgBox.ShowMessage($"Error: {e.Message}", @"Project Celeste -- Register",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Enabled = true;
        }

        private void OnVerifyUser(dynamic result)
        {
            if (result["Result"].ToObject<bool>())
            {
                _validMailState = RegisterUserState.Success;
                var str = result["Message"].ToObject<string>();
                MsgBox.ShowMessage($@"{str}", @"Project Celeste -- Register",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                _validMailState = RegisterUserState.Failed;
                var str = result["Message"].ToObject<string>();
                MsgBox.ShowMessage($@"Error: {str}", @"Project Celeste -- Register",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Btn_Register_Click(object sender, EventArgs e)
        {
            if (!Misc.IsValideEmailAdress(tb_Mail.Text))
            {
                MsgBox.ShowMessage(@"Invalid Email!", @"Project Celeste -- Register",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            if (!Misc.IsValideUserName(tb_UserName.Text))
            {
                MsgBox.ShowMessage(
                    @"Invalid User Name, only letters and digits allowed, minimum length is 3 char and maximum length is 15 char!",
                    @"Project Celeste -- Register",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (tb_ConfirmPassword.Text != tb_Password.Text)
            {
                MsgBox.ShowMessage(@"Password value and confirm password value don't match!",
                    @"Project Celeste -- Register",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (tb_Password.Text.Length < 8 || tb_Password.Text.Length > 32)
            {
                MsgBox.ShowMessage(@"Password minimum length is 8 char,  maximum length is 32 char!",
                    @"Project Celeste -- Register",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!Misc.IsValidePassword(tb_Password.Text))
            {
                MsgBox.ShowMessage("Invalid password, character ' and \" are not allowed!",
                    @"Project Celeste -- Register",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (tb_InviteCode.Text.Length != 32)
            {
                MsgBox.ShowMessage(@"Invalid Verify Key!",
                    @"Project Celeste -- Register",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DoRegisterUser(tb_Mail.Text, tb_Password.Text, tb_UserName.Text, tb_InviteCode.Text);
        }

        private void DoRegisterUser(string email, string password, string username, string verifyKey)
        {
            try
            {
                Program.WebSocketClient.StartConnect(false);

#pragma warning disable IDE0017 // Simplifier l'initialisation des objets
                dynamic registerUserInfo = new ExpandoObject();
                registerUserInfo.Version = Assembly.GetEntryAssembly().GetName().Version;
                registerUserInfo.Mail = email;
                registerUserInfo.VerifyKey = verifyKey;
                registerUserInfo.Password = password;
                registerUserInfo.UserName = username;
#pragma warning restore IDE0017 // Simplifier l'initialisation des objets

                _registerUserState = RegisterUserState.InProgress;

                Program.WebSocketClient.AgentWebSocket.Query<dynamic>("REGISTER", (object) registerUserInfo,
                    OnRegisterUser);

                var starttime = DateTime.UtcNow;
                while (_registerUserState == RegisterUserState.InProgress &&
                       Program.WebSocketClient.State != WebSocketClientState.Offline)
                {
                    Application.DoEvents();
                    var diff = DateTime.UtcNow.Subtract(starttime).TotalSeconds;
                    if (diff <= WebSocketClient.TimeOut) continue;

                    if (Program.WebSocketClient.State != WebSocketClientState.Offline)
                        Program.WebSocketClient.AgentWebSocket.Close();

                    _registerUserState = RegisterUserState.TimedOut;

                    throw new Exception($"DoRegisterUser() Server connection timeout (total send time = {diff})!");
                }

                if (_registerUserState == RegisterUserState.Success)
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
            catch (Exception e)
            {
                MsgBox.ShowMessage($"Error: {e.Message}", @"Project Celeste -- Register",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Enabled = true;
        }

        private void OnRegisterUser(dynamic result)
        {
            if (result["Result"].ToObject<bool>())
            {
                _registerUserState = RegisterUserState.Success;
                MsgBox.ShowMessage(@"Registred with success.", @"Project Celeste -- Register",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                _registerUserState = RegisterUserState.Failed;
                var str = result["Message"].ToObject<string>();
                MsgBox.ShowMessage($@"Error: {str}", @"Project Celeste -- Register",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}