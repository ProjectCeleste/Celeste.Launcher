#region Using directives

using System;
using System.Dynamic;
using System.Windows.Forms;
using Celeste_Launcher_Gui.Helpers;

#endregion

namespace Celeste_Launcher_Gui.Forms
{
    public partial class ChangePwdForm : Form
    {
        private bool _changePasswordDone;

        public ChangePwdForm()
        {
            InitializeComponent();

            //Configure Fonts
            SkinHelper.SetFont(Controls);
        }

        private void DoChangePassword(string oldPwd, string newPwd)
        {
            _changePasswordDone = false;

            if (Program.WebSocketClient.State != WebSocketClientState.Logged)
                throw new Exception("Not logged in!");

            dynamic changePwdInfo = new ExpandoObject();
            changePwdInfo.Old = oldPwd;
            changePwdInfo.New = newPwd;

            Program.WebSocketClient?.AgentWebSocket?.Query<dynamic>("CHANGEPWD", (object) changePwdInfo,
                OnChangePassword);
        }

        private void OnChangePassword(dynamic result)
        {
            if (result["Result"].ToObject<bool>())
            {
                SkinHelper.ShowMessage(@"Password changed with success.", @"Project Celeste -- Change Password",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                var str = result["Message"].ToObject<string>();
                SkinHelper.ShowMessage($@"Error: {str}", @"Project Celeste -- Change Password",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            _changePasswordDone = true;
        }

        private void btnSmall1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != textBox2.Text)
            {
                SkinHelper.ShowMessage(@"New password value and confirm new password value don't match!",
                    @"Project Celeste -- Change Password",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            if (textBox1.Text.Length < 8)
            {
                SkinHelper.ShowMessage(@"Password minimum length is 8 char!", @"Project Celeste -- Change Password",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (textBox1.Text.Length > 32)
            {
                SkinHelper.ShowMessage(@"Password maximum length is 32 char!", @"Project Celeste -- Change Password",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Enabled = false;
            DoChangePassword(tb_Password.Text, textBox2.Text);

            var starttime = DateTime.UtcNow;
            while (!_changePasswordDone && DateTime.UtcNow.Subtract(starttime).TotalSeconds < 20) //Timeout 20sec
                Application.DoEvents();

            Enabled = true;

            if (!_changePasswordDone)
                SkinHelper.ShowMessage(@"Error: Timout!", @"Project Celeste -- Change Password",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ChangePwdForm_Load(object sender, EventArgs e)
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