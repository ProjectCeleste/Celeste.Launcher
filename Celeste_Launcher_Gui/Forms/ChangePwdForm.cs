#region Using directives

using System;
using System.Dynamic;
using System.Windows.Forms;
using Celeste_AOEO_Controls.Helpers;
using Celeste_AOEO_Controls.MsgBox;
using Celeste_Launcher_Gui.Helpers;
using Celeste_Public_Api.Helpers;

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

            SkinHelper.SetFont(Controls);
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
                MsgBox.ShowMessage($"Error: {e.Message}", @"Project Celeste -- Change Password",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Enabled = true;
        }

        private void OnChangePassword(dynamic result)
        {
            if (result["Result"].ToObject<bool>())
            {
                _changePwdState = ChangePwdState.Success;
                MsgBox.ShowMessage(@"Password changed with success.", @"Celeste Fan Project",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                _changePwdState = ChangePwdState.Failed;
                var str = result["Message"].ToObject<string>();
                MsgBox.ShowMessage($@"Error: {str}", @"Celeste Fan Project",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSmall1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != textBox2.Text)
            {
                MsgBox.ShowMessage(@"New password value and confirm new password value don't match!",
                    @"Celeste Fan Project",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            if (textBox1.Text.Length < 8)
            {
                MsgBox.ShowMessage(@"Password minimum length is 8 char!", @"Celeste Fan Project",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (textBox1.Text.Length > 32)
            {
                MsgBox.ShowMessage(@"Password maximum length is 32 char!", @"Celeste Fan Project",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!Misc.IsValidePassword(textBox1.Text))
            {
                MsgBox.ShowMessage("Invalid password, character ' and \" are not allowed!",
                    @"Celeste Fan Project",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DoChangePassword(tb_Password.Text, textBox2.Text);
        }

        private void PictureBoxButtonCustom1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void ChangePwdForm_Load(object sender, EventArgs e)
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