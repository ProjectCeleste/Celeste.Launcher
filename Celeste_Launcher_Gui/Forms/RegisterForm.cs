#region Using directives

using System;
using System.Dynamic;
using System.Reflection;
using System.Windows.Forms;
using Celeste_Launcher_Gui.Helpers;

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
        private RegisterUserState _validMailState = RegisterUserState.Idle;
        private RegisterUserState _registerUserState = RegisterUserState.Idle;

        public RegisterForm()
        {
            InitializeComponent();

            //Configure Fonts
            SkinHelper.SetFont(Controls);
        }

        private void Btn_Verify_Click(object sender, EventArgs e)
        {
            if (!Celeste_User.Helpers.IsValideEmailAdress(tb_Mail.Text))
            {
                SkinHelper.ShowMessage(@"Invalid Email!", @"Project Celeste -- Register",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }
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
                while (_validMailState == RegisterUserState.InProgress && Program.WebSocketClient.State != WebSocketClientState.Offline)
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
                SkinHelper.ShowMessage($"Error: {e.Message}", @"Project Celeste -- Register",
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
                SkinHelper.ShowMessage($@"{str}", @"Project Celeste -- Register",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                _validMailState = RegisterUserState.Failed;
                var str = result["Message"].ToObject<string>();
                SkinHelper.ShowMessage($@"Error: {str}", @"Project Celeste -- Register",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Btn_Register_Click(object sender, EventArgs e)
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
                    @"Invalid User Name, only letters and digits allowed, minimum length is 3 char and maximum length is 15 char!",
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

            if (tb_InviteCode.Text.Length != 32)
            {
                SkinHelper.ShowMessage(@"Invalid Verify Key!",
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
                while (_registerUserState == RegisterUserState.InProgress && Program.WebSocketClient.State != WebSocketClientState.Offline)
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
                SkinHelper.ShowMessage($"Error: {e.Message}", @"Project Celeste -- Register",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            Enabled = true;
        }

        private void OnRegisterUser(dynamic result)
        {
            if (result["Result"].ToObject<bool>())
            {
                _registerUserState = RegisterUserState.Success;
                SkinHelper.ShowMessage(@"Registred with success.", @"Project Celeste -- Register",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                _registerUserState = RegisterUserState.Failed;
                var str = result["Message"].ToObject<string>();
                SkinHelper.ShowMessage($@"Error: {str}", @"Project Celeste -- Register",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RegisterForm_Load(object sender, EventArgs e)
        {
            try
            {
                if (DwmApi.DwmIsCompositionEnabled())
                    DwmApi.DwmExtendFrameIntoClientArea(Handle, new DwmApi.MARGINS(31, 75, 31, 26));
            }
            catch (Exception)
            {
                //
            }
        }
    }
}