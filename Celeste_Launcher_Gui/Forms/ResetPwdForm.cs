#region Using directives

using System;
using System.Dynamic;
using System.Reflection;
using System.Windows.Forms;
using Celeste_Launcher_Gui.Helpers;

#endregion

namespace Celeste_Launcher_Gui.Forms
{
    public enum ResetPwdState
    {
        TimedOut = -2,
        Failed = -1,
        Idle = 0,
        InProgress = 1,
        Success = 2
    }

    public partial class ResetPwdForm : Form
    {
        private DateTime _lastVerifyTime = DateTime.UtcNow.AddMinutes(-1);
        private ResetPwdState _resetPwdState = ResetPwdState.Idle;
        private ResetPwdState _verifyUserState = ResetPwdState.Idle;

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

            var lastSendTime = (DateTime.UtcNow - _lastVerifyTime).TotalSeconds;
            if (lastSendTime <= 45)
            {
                SkinHelper.ShowMessage(
                    $"You need to wait at least 45 seconds before asking to resend an confirmation key! Last request was {lastSendTime} seconds ago.",
                    @"Project Celeste -- Reset Password",
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
                Program.WebSocketClient.StartConnect(true, Program.UserConfig.LoginInfo.Email,
                    Program.UserConfig.LoginInfo.Password);

#pragma warning disable IDE0017 // Simplifier l'initialisation des objets
                dynamic forgotPwdInfo = new ExpandoObject();
                forgotPwdInfo.Version = Assembly.GetEntryAssembly().GetName().Version;
                forgotPwdInfo.EMail = email;
#pragma warning restore IDE0017 // Simplifier l'initialisation des objets

                _verifyUserState = ResetPwdState.InProgress;

                Program.WebSocketClient.AgentWebSocket.Query<dynamic>("FORGOTPWD", (object) forgotPwdInfo,
                    OnVerifyUser);

                var starttime = DateTime.UtcNow;
                while (_verifyUserState == ResetPwdState.InProgress &&
                       Program.WebSocketClient.State == WebSocketClientState.Connected)
                {
                    Application.DoEvents();
                    var diff = DateTime.UtcNow.Subtract(starttime).TotalSeconds;
                    if (diff <= WebSocketClient.TimeOut) continue;

                    _verifyUserState = ResetPwdState.TimedOut;

                    if (Program.WebSocketClient.State != WebSocketClientState.Offline)
                        Program.WebSocketClient.AgentWebSocket.Close();

                    throw new Exception($"Server response timeout (total time = {diff})!");
                }

                if (_verifyUserState != ResetPwdState.Success) return;

                p_Verify.Enabled = false;
                p_ResetPassword.Enabled = true;
            }
            catch (Exception e)
            {
                SkinHelper.ShowMessage($"Error: {e.Message}", @"Project Celeste -- Reset Password",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Enabled = true;
        }

        private void OnVerifyUser(dynamic result)
        {
            if (result["Result"].ToObject<bool>())
            {
                _verifyUserState = ResetPwdState.Success;
                var str = result["Message"].ToObject<string>();
                SkinHelper.ShowMessage($@"{str}", @"Project Celeste -- Reset Password",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                _verifyUserState = ResetPwdState.Failed;
                var str = result["Message"].ToObject<string>();
                SkinHelper.ShowMessage($@"Error: {str}", @"Project Celeste -- Reset Password",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

            try
            {
                Program.WebSocketClient.StartConnect(false);

#pragma warning disable IDE0017 // Simplifier l'initialisation des objets
                dynamic resetPwdInfo = new ExpandoObject();
                resetPwdInfo.Version = Assembly.GetEntryAssembly().GetName().Version;
                resetPwdInfo.EMail = email;
                resetPwdInfo.VerifyKey = verifKey;
#pragma warning restore IDE0017 // Simplifier l'initialisation des objets

                _resetPwdState = ResetPwdState.InProgress;

                Program.WebSocketClient.AgentWebSocket.Query<dynamic>("RESETPWD", (object) resetPwdInfo,
                    OnResetPassword);

                var starttime = DateTime.UtcNow;
                while (_resetPwdState == ResetPwdState.InProgress &&
                       Program.WebSocketClient.State == WebSocketClientState.Connected)
                {
                    Application.DoEvents();
                    var diff = DateTime.UtcNow.Subtract(starttime).TotalSeconds;
                    if (diff <= WebSocketClient.TimeOut) continue;

                    _resetPwdState = ResetPwdState.TimedOut;

                    if (Program.WebSocketClient.State != WebSocketClientState.Offline)
                        Program.WebSocketClient.AgentWebSocket.Close();

                    throw new Exception($"Server response timeout (total time = {diff})!");
                }

                if (_resetPwdState != ResetPwdState.Success) return;

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception e)
            {
                SkinHelper.ShowMessage($"Error: {e.Message}", @"Project Celeste -- Reset Password",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Enabled = true;
        }

        private void OnResetPassword(dynamic result)
        {
            if (result["Result"].ToObject<bool>())
            {
                _resetPwdState = ResetPwdState.Success;
                var str = result["Message"].ToObject<string>();
                SkinHelper.ShowMessage($@"{str}", @"Project Celeste -- Reset Password",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                _resetPwdState = ResetPwdState.Failed;
                var str = result["Message"].ToObject<string>();
                SkinHelper.ShowMessage($@"Error: {str}", @"Project Celeste -- Reset Password",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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