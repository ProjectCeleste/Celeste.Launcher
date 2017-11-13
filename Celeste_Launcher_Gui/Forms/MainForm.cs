#region Using directives

using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Celeste_AOEO_Controls;
using Celeste_AOEO_Controls.MsgBox;
using Celeste_Launcher_Gui.Helpers;
using Celeste_Public_Api.GameScanner_Api;
using Open.Nat;

#endregion

namespace Celeste_Launcher_Gui.Forms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            //
            lb_Ver.Text = $@"v{Assembly.GetEntryAssembly().GetName().Version}";

            //
            panelManager1.SelectedPanel = managedPanel2;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Program.WebSocketApi?.Disconnect();
                NatDiscoverer.ReleaseAll();
            }
            catch
            {
                //
            }
        }

        private async void Btn_Play_Click(object sender, EventArgs e)
        {
            btn_Play.Enabled = false;
            var pname = Process.GetProcessesByName("spartan");
            if (pname.Length > 0)
            {
                MsgBox.ShowMessage(@"Game already running!");
                btn_Play.Enabled = true;
                return;
            }

            //QuickGameScan
            try
            {
                using (var form = new QuickGameScan())
                {
                    retry:
                    var dr = form.ShowDialog();

                    if (dr == DialogResult.Retry)
                        goto retry;

                    if (dr != DialogResult.OK)
                        Environment.Exit(0);
                }
            }
            catch (Exception ex)
            {
                MsgBox.ShowMessage(
                    $"Warning: Error during quick scan. Error message: {ex.Message}",
                    @"Project Celeste",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            //MpSettings
            try
            {
                if (Program.UserConfig.MpSettings != null)
                    if (Program.UserConfig.MpSettings.IsOnline)
                    {
                        Program.UserConfig.MpSettings.PublicIp = Program.CurrentUser.Ip;

                        if (Program.UserConfig.MpSettings.AutoPortMapping)
                        {
                            var mapPortTask = OpenNat.MapPortTask(1000, 1000);
                            try
                            {
                                await mapPortTask;
                                NatDiscoverer.TraceSource.Close();
                            }
                            catch (AggregateException ex)
                            {
                                NatDiscoverer.TraceSource.Close();

                                if (!(ex.InnerException is NatDeviceNotFoundException)) throw;

                                MsgBox.ShowMessage(
                                    "Error: Upnp device not found! Set \"Port mapping\" to manual in \"Mp Settings\" and configure your router.",
                                    @"Project Celeste",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                                btn_Play.Enabled = true;

                                return;
                            }
                        }
                    }
            }
            catch
            {
                MsgBox.ShowMessage(
                    "Error: Upnp device not found! Set \"Port mapping\" to manual in \"Mp Settings\" and configure your router.",
                    @"Project Celeste",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                btn_Play.Enabled = true;

                return;
            }
            try
            {
                //Save UserConfig
                Program.UserConfig.Save(Program.UserConfigFilePath);
            }
            catch
            {
                //
            }

            try
            {
                //Launch Game
                var path = !string.IsNullOrEmpty(Program.UserConfig.GameFilesPath)
                    ? Program.UserConfig.GameFilesPath
                    : GameScannnerApi.GetGameFilesRootPath();

                if (!path.EndsWith(Path.DirectorySeparatorChar.ToString()))
                    path += Path.DirectorySeparatorChar;

                var spartanPath = $"{path}Spartan.exe";

                if (!File.Exists(spartanPath))
                {
                    MsgBox.ShowMessage(
                        "Error: Spartan.exe not found!",
                        @"Project Celeste",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);

                    btn_Play.Enabled = true;
                    return;
                }

                string lang;
                switch (Program.UserConfig.GameLanguage)
                {
                    case GameLanguage.deDE:
                        lang = "de-DE";
                        break;
                    case GameLanguage.enUS:
                        lang = "en-US";
                        break;
                    case GameLanguage.esES:
                        lang = "es-ES";
                        break;
                    case GameLanguage.frFR:
                        lang = "fr-FR";
                        break;
                    case GameLanguage.itIT:
                        lang = "it-IT";
                        break;
                    case GameLanguage.zhCHT:
                        lang = "zh-CHT";
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                var arg = Program.UserConfig?.MpSettings == null || Program.UserConfig.MpSettings.IsOnline
                    ? $"--email \"{Program.UserConfig.LoginInfo.Email}\"  --password \"{Program.UserConfig.LoginInfo.Password}\" --ignore_rest LauncherLang={lang} LauncherLocale=1033"
                    : $"--email \"{Program.UserConfig.LoginInfo.Email}\"  --password \"{Program.UserConfig.LoginInfo.Password}\" --online-ip \"{Program.UserConfig.MpSettings.PublicIp}\" --ignore_rest LauncherLang={lang} LauncherLocale=1033";

                Process.Start(new ProcessStartInfo(spartanPath, arg) {WorkingDirectory = path});

                WindowState = FormWindowState.Minimized;
            }
            catch (Exception exception)
            {
                MsgBox.ShowMessage(
                    $"Error: {exception.Message}",
                    @"Project Celeste",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            btn_Play.Enabled = true;
        }

        private void PictureBoxButtonCustom3_Click(object sender, EventArgs e)
        {
            var btnSender = (PictureBoxButtonCustom) sender;
            var pt = new Point(btnSender.Bounds.Width + 1, btnSender.Bounds.Top);
            cMS_ProjectCelesteCom.Show(btnSender, pt);
        }

        private void PictureBoxButtonCustom1_Click(object sender, EventArgs e)
        {
            Process.Start("https://discord.gg/pkM2RAm");
        }

        private void PictureBoxButtonCustom2_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.reddit.com/r/projectceleste/");
        }

        private void PictureBoxButtonCustom4_Click(object sender, EventArgs e)
        {
            Process.Start("http://aoedb.net/");
        }

        private void PictureBoxButtonCustom5_Click(object sender, EventArgs e)
        {
            var btnSender = (PictureBoxButtonCustom) sender;
            var pt = new Point(btnSender.Bounds.Width + 1, btnSender.Bounds.Top);
            cMS_Donate.Show(btnSender, pt);
        }

        private void CustomBtn1_Click(object sender, EventArgs e)
        {
            //Login
            using (var form = new LoginForm())
            {
                var dr = form.ShowDialog();

                if (dr == DialogResult.OK)
                {
                    if (form.CurrentUser == null)
                        return;

                    Program.CurrentUser = form.CurrentUser;

                    gamerCard1.UserName = $@"{Program.CurrentUser.ProfileName}";
                    gamerCard1.Rank = $@"{Program.CurrentUser.Rank}";

                    panelManager1.SelectedPanel = managedPanel1;
                }
                else
                {
                    panelManager1.SelectedPanel = managedPanel2;
                }
            }
        }

        private void LinkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://www.xbox.com/en-us/developers/rules");
        }

        private void PictureBoxButtonCustom8_Click(object sender, EventArgs e)
        {
            var btnSender = (PictureBoxButtonCustom) sender;
            var pt = new Point(btnSender.Bounds.Width + 1, btnSender.Bounds.Top);
            cMS_Tools.Show(btnSender, pt);
        }

        private void CustomBtn2_Click(object sender, EventArgs e)
        {
            using (var form = new RegisterForm())
            {
                form.ShowDialog();
                if (form.DialogResult != DialogResult.OK)
                    return;

                //Save UserConfig
                if (Program.UserConfig == null)
                {
                    Program.UserConfig = new UserConfig
                    {
                        LoginInfo = new LoginInfo
                        {
                            Email = form.tb_Mail.Text,
                            Password = form.tb_Password.Text,
                            RememberMe = true
                        }
                    };
                }
                else
                {
                    Program.UserConfig.LoginInfo.Email = form.tb_Mail.Text;
                    Program.UserConfig.LoginInfo.Password = form.tb_Password.Text;
                    Program.UserConfig.LoginInfo.RememberMe = true;
                }
                Program.UserConfig.Save(Program.UserConfigFilePath);
            }
        }

        private void UpdaterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var pname = Process.GetProcessesByName("spartan");
            if (pname.Length > 0)
            {
                MsgBox.ShowMessage(@"Game is running, you need to close it first!");
                return;
            }

            using (var form = new UpdaterForm())
            {
                form.ShowDialog();
            }
        }

        private void ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var pname = Process.GetProcessesByName("spartan");
            if (pname.Length > 0)
            {
                MsgBox.ShowMessage(@"Game is running, you need to close it first!");
                return;
            }

            using (var form = new GameScan())
            {
                form.ShowDialog();
            }
        }

        private void PictureBoxButtonCustom9_Click(object sender, EventArgs e)
        {
            var btnSender = (PictureBoxButtonCustom) sender;
            var pt = new Point(btnSender.Bounds.Width + 1, btnSender.Bounds.Top);
            cMS_Settings.Show(btnSender, pt);
        }

        private void PictureBoxButtonCustom7_Click(object sender, EventArgs e)
        {
            var btnSender = (PictureBoxButtonCustom) sender;
            var pt = new Point(btnSender.Bounds.Width + 1, btnSender.Bounds.Top);
            cMS_Account.Show(btnSender, pt);
        }

        private void ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            using (var form = new LanguageChooser())
            {
                var dr = form.ShowDialog();

                if (dr != DialogResult.OK)
                    return;

                try
                {
                    if (Program.UserConfig != null)
                        Program.UserConfig.GameLanguage = form.SelectedLang;
                    else
                        Program.UserConfig = new UserConfig {GameLanguage = form.SelectedLang};
                    Program.UserConfig.Save(Program.UserConfigFilePath);
                }
                catch (Exception)
                {
                    //
                }
            }
        }

        private void ToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            using (var form = new MpSettingForm(Program.UserConfig.MpSettings))
            {
                form.ShowDialog();
            }
        }

        private void ToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            using (var form = new ChangePwdForm())
            {
                form.ShowDialog();
            }
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            //CleanUpFiles
            try
            {
                UpdaterForm.CleanUpFiles(Directory.GetCurrentDirectory(), "*.old");
            }
            catch (Exception ex)
            {
                MsgBox.ShowMessage(
                    $"Warning: Error during files clean-up. Error message: {ex.Message}",
                    @"Project Celeste",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            //Update Check
            try
            {
                if (!UpdaterForm.IsLatestVersion())
                {
                    using (var form =
                        new MsgBoxYesNo(
                            @"An update is avalaible. Click ""Yes"" to install it, or ""No"" to close the launcher."))
                    {
                        var dr = form.ShowDialog();
                        if (dr != DialogResult.OK)
                            Environment.Exit(0);
                    }

                    using (var form = new UpdaterForm())
                    {
                        form.ShowDialog();
                    }

                    Environment.Exit(0);
                }
            }
            catch (Exception ex)
            {
                MsgBox.ShowMessage(
                    $"Warning: Error during update check. Error message: {ex.Message}",
                    @"Project Celeste",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            //Auto Login

            if (Program.UserConfig?.LoginInfo == null)
                return;

            if (!Program.UserConfig.LoginInfo.AutoLogin)
                return;

            try
            {
                var response = await Program.WebSocketApi.DoLogin(Program.UserConfig.LoginInfo.Email,
                    Program.UserConfig.LoginInfo.Password);

                if (!response.Result)
                    return;

                //
                Program.CurrentUser = response.RemoteUser;

                gamerCard1.UserName = $@"{Program.CurrentUser.ProfileName}";
                gamerCard1.Rank = $@"{Program.CurrentUser.Rank}";

                panelManager1.SelectedPanel = managedPanel1;
            }
            catch (Exception)
            {
                //
            }
        }

        private void DonateWithPayPalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=EZ3SSAJRRUYFY");
        }

        private void MoreDonateOptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://forums.projectceleste.com/donations/");
        }

        private void HomeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://projectceleste.com");
        }

        private void ForumsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://forums.projectceleste.com");
        }

        private void LogOffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //
            try
            {
                Program.WebSocketApi?.Disconnect();
            }
            catch
            {
                //
            }
            //

            //Save UserConfig
            if (Program.UserConfig?.LoginInfo != null)
            {
                Program.UserConfig.LoginInfo.AutoLogin = false;

                try
                {
                    Program.UserConfig.Save(Program.UserConfigFilePath);
                }
                catch (Exception)
                {
                    //
                }
            }

            //
            panelManager1.SelectedPanel = managedPanel2;
        }
    }
}