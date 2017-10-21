#region Using directives

using System;
using System.Dynamic;
using System.Windows.Forms;
using Celeste_AOEO_Controls;
using Celeste_Launcher_Gui.Helpers;

#endregion

namespace Celeste_Launcher_Gui.Forms
{
    public enum ChangePwdState
    {
        TimedOut = -2,
        Failed = -1,
        Idle = 0,
        InProgress = 1,
        Success = 2
    }

    public partial class ChangePwdForm : Form
    {
        private ChangePwdState _changePwdState = ChangePwdState.Idle;

        public ChangePwdForm()
        {
            InitializeComponent();
        }

        private void DoChangePassword(string oldPwd, string newPwd)
        {
            Enabled = false;
            try
            {
                Program.WebSocketClient.StartConnect(true, Program.UserConfig.LoginInfo.Email,
                    Program.UserConfig.LoginInfo.Password);

#pragma warning disable IDE0017 // Simplifier l'initialisation des objets
                dynamic changePwdInfo = new ExpandoObject();
                changePwdInfo.Old = oldPwd;
                changePwdInfo.New = newPwd;
#pragma warning restore IDE0017 // Simplifier l'initialisation des objets

                _changePwdState = ChangePwdState.InProgress;

                Program.WebSocketClient.AgentWebSocket.Query<dynamic>("CHANGEPWD", (object) changePwdInfo,
                    OnChangePassword);

                var starttime = DateTime.UtcNow;

                while (_changePwdState == ChangePwdState.InProgress &&
                       Program.WebSocketClient.State == WebSocketClientState.Connected)
                {
                    Application.DoEvents();
                    var diff = DateTime.UtcNow.Subtract(starttime).TotalSeconds;
                    if (diff <= WebSocketClient.TimeOut) continue;

                    _changePwdState = ChangePwdState.TimedOut;

                    if (Program.WebSocketClient.State != WebSocketClientState.Offline)
                        Program.WebSocketClient.AgentWebSocket.Close();

                    throw new Exception($"Server response timeout (total time = {diff})!");
                }
            }
            catch (Exception e)
            {
                CustomMsgBox.ShowMessage($"Error: {e.Message}", @"Project Celeste -- Change Password",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Enabled = true;
        }

        private void OnChangePassword(dynamic result)
        {
            if (result["Result"].ToObject<bool>())
            {
                _changePwdState = ChangePwdState.Success;
                CustomMsgBox.ShowMessage(@"Password changed with success.", @"Project Celeste -- Change Password",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                _changePwdState = ChangePwdState.Failed;
                var str = result["Message"].ToObject<string>();
                CustomMsgBox.ShowMessage($@"Error: {str}", @"Project Celeste -- Change Password",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSmall1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != textBox2.Text)
            {
                CustomMsgBox.ShowMessage(@"New password value and confirm new password value don't match!",
                    @"Project Celeste -- Change Password",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            if (textBox1.Text.Length < 8)
            {
                CustomMsgBox.ShowMessage(@"Password minimum length is 8 char!", @"Project Celeste -- Change Password",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (textBox1.Text.Length > 32)
            {
                CustomMsgBox.ShowMessage(@"Password maximum length is 32 char!", @"Project Celeste -- Change Password",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!Celeste_User.Helpers.IsValidePassword(textBox1.Text))
            {
                CustomMsgBox.ShowMessage("Invalid password, character ' and \" are not allowed!",
                    @"Project Celeste -- Change Password",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DoChangePassword(tb_Password.Text, textBox2.Text);
        }
    }
}