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
                throw new Exception(MultiLanguage.GetString("strNotLoggedIn"));

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
                SkinHelper.ShowMessage(MultiLanguage.GetString("strPasswordChanged"), @"Project Celeste -- Change Password",
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
                SkinHelper.ShowMessage(MultiLanguage.GetString("strPasswordsNotMatch"),
                    @"Project Celeste -- Change Password",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            if (textBox1.Text.Length < 8)
            {
                SkinHelper.ShowMessage(MultiLanguage.GetString("strPasswordMinimunLength"), @"Project Celeste -- Change Password",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (textBox1.Text.Length > 32)
            {
                SkinHelper.ShowMessage(MultiLanguage.GetString("strPasswordMaxLength"), @"Project Celeste -- Change Password",
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
                SkinHelper.ShowMessage(MultiLanguage.GetString("strErrorTimeout"), @"Project Celeste -- Change Password",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ChangePwdForm_Load(object sender, EventArgs e)
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
    }
}