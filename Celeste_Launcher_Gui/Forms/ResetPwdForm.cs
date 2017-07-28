#region Using directives

using System;
using System.Dynamic;
using System.Reflection;
using System.Windows.Forms;
using Celeste_Launcher_Gui.Helpers;

#endregion

namespace Celeste_Launcher_Gui.Forms
{
    public partial class ResetPwdForm : Form
    {
        private bool _resetPwdDone;
        private bool _resetPwdFailed = true;
        private bool _verifyUserDone;
        private bool _verifyUserFailed = true;

        public ResetPwdForm()
        {
            InitializeComponent();

            //Configure Fonts
            SkinHelper.SetFont(Controls);
        }

        private void Btn_Verify_Click(object sender, EventArgs e)
        {
            if (!Celeste_User.Helpers.IsValideEmailAdress(tb_Mail.Text))
            {
                SkinHelper.ShowMessage(@"Invalid Email!", @"Project Celeste -- Reset Password",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            DoVerifyUser(tb_Mail.Text);
        }

        private void DoVerifyUser(string email)
        {
            Enabled = false;

            if (Program.WebSocketClient.State == WebSocketClientState.Logged ||
                Program.WebSocketClient.State == WebSocketClientState.Logging)
            {
                SkinHelper.ShowMessage(@"Already logged-in or logged-in in progress!",
                    @"Project Celeste -- Reset Password",
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

                    SkinHelper.ShowMessage(@"Server connection timeout (> 20sec)!",
                        @"Project Celeste -- Reset Password",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);

                    Enabled = true;
                    return;
                }
            }

            if (Program.WebSocketClient.State != WebSocketClientState.Connected)
            {
                SkinHelper.ShowMessage(@"Server Offline", @"Project Celeste -- Reset Password",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                Enabled = true;
                return;
            }
            _verifyUserDone = false;
            _verifyUserFailed = true;
#pragma warning disable IDE0017 // Simplifier l'initialisation des objets
            dynamic forgotPwdInfo = new ExpandoObject();
            forgotPwdInfo.Version = Assembly.GetEntryAssembly().GetName().Version;
            forgotPwdInfo.EMail = email;
#pragma warning restore IDE0017 // Simplifier l'initialisation des objets

            Program.WebSocketClient?.AgentWebSocket?.Query<dynamic>("FORGOTPWD", (object) forgotPwdInfo,
                OnVerifyUser);

            var starttime2 = DateTime.UtcNow;
            while (!_verifyUserDone)
            {
                Application.DoEvents();

                if (DateTime.UtcNow.Subtract(starttime2).TotalSeconds <= 20) continue;

                SkinHelper.ShowMessage(@"Server response timeout (> 20sec)!", @"Project Celeste -- Reset Password",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (Program.WebSocketClient.State != WebSocketClientState.Offline)
                    Program.WebSocketClient?.AgentWebSocket?.Close();

                Enabled = true;
                return;
            }

            Enabled = true;

            if (_verifyUserFailed) return;

            p_Verify.Enabled = false;
            p_ResetPassword.Enabled = true;
        }

        private void OnVerifyUser(dynamic result)
        {
            if (result["Result"].ToObject<bool>())
            {
                _verifyUserFailed = false;
                var str = result["Message"].ToObject<string>();
                SkinHelper.ShowMessage($@"{str}", @"Project Celeste -- Reset Password",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                _verifyUserFailed = true;
                var str = result["Message"].ToObject<string>();
                SkinHelper.ShowMessage($@"Error: {str}", @"Project Celeste -- Reset Password",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (Program.WebSocketClient.State != WebSocketClientState.Offline)
                Program.WebSocketClient.AgentWebSocket.Close();

            _verifyUserDone = true;
        }


        private void Btn_ResetPassword_Click(object sender, EventArgs e)
        {
            if (!Celeste_User.Helpers.IsValideEmailAdress(tb_Mail.Text))
            {
                SkinHelper.ShowMessage(@"Invalid Email!", @"Project Celeste -- Reset Password",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            if (tb_InviteCode.Text.Length != 32)
            {
                SkinHelper.ShowMessage(@"Invalid Verify Key!",
                    @"Project Celeste -- Reset Password",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DoResetPassword(tb_Mail.Text, tb_InviteCode.Text);
        }

        private void DoResetPassword(string email, string verifKey)
        {
            Enabled = false;

            if (Program.WebSocketClient.State == WebSocketClientState.Logged ||
                Program.WebSocketClient.State == WebSocketClientState.Logging)
            {
                SkinHelper.ShowMessage(@"Already logged-in or logged-in in progress!",
                    @"Project Celeste -- Reset Password",
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

                    SkinHelper.ShowMessage(@"Server connection timeout (> 20sec)!",
                        @"Project Celeste -- Reset Password",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);

                    Enabled = true;
                    return;
                }
            }

            if (Program.WebSocketClient.State != WebSocketClientState.Connected)
            {
                SkinHelper.ShowMessage(@"Server Offline", @"Project Celeste -- Reset Password",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                Enabled = true;
                return;
            }
            _resetPwdDone = false;
            _resetPwdFailed = true;
#pragma warning disable IDE0017 // Simplifier l'initialisation des objets
            dynamic resetPwdInfo = new ExpandoObject();
            resetPwdInfo.Version = Assembly.GetEntryAssembly().GetName().Version;
            resetPwdInfo.EMail = email;
            resetPwdInfo.VerifyKey = verifKey;
#pragma warning restore IDE0017 // Simplifier l'initialisation des objets

            Program.WebSocketClient?.AgentWebSocket?.Query<dynamic>("RESETPWD", (object) resetPwdInfo,
                OnResetPassword);

            var starttime2 = DateTime.UtcNow;
            while (!_resetPwdDone)
            {
                Application.DoEvents();

                if (DateTime.UtcNow.Subtract(starttime2).TotalSeconds <= 20) continue;

                SkinHelper.ShowMessage(@"Server response timeout (> 20sec)!", @"Project Celeste -- Reset Password",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (Program.WebSocketClient.State != WebSocketClientState.Offline)
                    Program.WebSocketClient?.AgentWebSocket?.Close();

                Enabled = true;
                return;
            }

            Enabled = true;

            if (_resetPwdFailed) return;

            DialogResult = DialogResult.OK;
            Close();
        }

        private void OnResetPassword(dynamic result)
        {
            if (result["Result"].ToObject<bool>())
            {
                _resetPwdFailed = false;
                var str = result["Message"].ToObject<string>();
                SkinHelper.ShowMessage($@"{str}", @"Project Celeste -- Reset Password",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                _resetPwdFailed = true;
                var str = result["Message"].ToObject<string>();
                SkinHelper.ShowMessage($@"Error: {str}", @"Project Celeste -- Reset Password",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (Program.WebSocketClient.State != WebSocketClientState.Offline)
                Program.WebSocketClient.AgentWebSocket.Close();

            _resetPwdDone = true;
        }

        private void ResetPasswordForm_Load(object sender, EventArgs e)
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