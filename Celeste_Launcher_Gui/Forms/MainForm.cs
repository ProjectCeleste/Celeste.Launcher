#region Using directives

using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Celeste_Launcher_Gui.Helpers;
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

            //Configure Fonts
            SkinHelper.SetFont(Controls);

            //Game Lang
            if (Program.UserConfig != null)
                comboBox2.SelectedIndex = (int) Program.UserConfig.GameLanguage;
            else
                comboBox2.SelectedIndex = (int) GameLanguage.enUS;

            //Login
            using (var form = new LoginForm())
            {
                var dr = form.ShowDialog();

                if (dr != DialogResult.OK)
                {
                    try
                    {
                        Program.WebSocketClient.AgentWebSocket.Close();
                        NatDiscoverer.ReleaseAll();
                    }
                    catch
                    {
                        //
                    }
                    Environment.Exit(0);
                }
            }

            //User Info
            if (Program.WebSocketClient.UserInformation == null) return;

            lbl_Mail.Text += $@" {Program.WebSocketClient.UserInformation.Mail}";
            lbl_UserName.Text += $@" {Program.WebSocketClient.UserInformation.ProfileName}";
            lbl_Rank.Text += $@" {Program.WebSocketClient.UserInformation.Rank}";
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //_timer.Stop();
            Program.WebSocketClient.AgentWebSocket.Close();
            try
            {
                Program.WebSocketClient.AgentWebSocket.Close();
                NatDiscoverer.ReleaseAll();
            }
            catch
            {
                //
            }
        }

        private void Linklbl_ReportBug_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/ProjectCeleste/Celeste_Server/issues");
        }

        private void LinkLbl_ProjectCelesteCom_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://projectceleste.com");
        }

        private void Linklbl_Wiki_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://ageofempiresonline.wikia.com/wiki/Age_of_Empires_Online_Wiki");
        }

        private void LinkLabel3_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://eso-community.net/");
        }

        private void LinkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (var form = new UpgradeForm())
            {
                Hide();
                form.ShowDialog();
                Show();
            }
        }

        private void LinkLbl_ChangePwd_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (var form = new ChangePwdForm())
            {
                Hide();
                form.ShowDialog();
                Show();
            }
        }

        private void Pb_Avatar_Click(object sender, EventArgs e)
        {
            //TODO
        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //TODO
        }

        private void LinkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //TODO
        }

        private async void Btn_Play_Click(object sender, EventArgs e)
        {
            btnSmall1.Enabled = false;
            var pname = Process.GetProcessesByName("spartan");
            if (pname.Length > 0)
            {
                SkinHelper.ShowMessage(@"Game already runing!");
                btnSmall1.Enabled = true;
                return;
            }

            //MpSettings
            try
            {
                if (Program.UserConfig.MpSettings != null)
                    if (Program.UserConfig.MpSettings.IsOnline)
                    {
                        Program.UserConfig.MpSettings.PublicIp = Program.WebSocketClient.UserInformation.Ip;

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

                                SkinHelper.ShowMessage(
                                    "Error: Upnp device not found! Set \"Port mapping\" to manual in \"Mp Settings\" and configure your router.",
                                    @"Project Celeste",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                btnSmall1.Enabled = true;
                                return;
                            }
                        }
                    }
            }
            catch
            {
                SkinHelper.ShowMessage(
                    "Error: Upnp device not found! Set \"Port mapping\" to manual in \"Mp Settings\" and configure your router.",
                    @"Project Celeste",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSmall1.Enabled = true;
                return;
            }

            try
            {
                //Save UserConfig
                Program.UserConfig.GameLanguage = (GameLanguage) comboBox2.SelectedIndex;
                Program.UserConfig.Save(Program.UserConfigFilePath);
            }
            catch
            {
                //
            }

            try
            {
                //Launch Game
                var path = $"{AppDomain.CurrentDomain.BaseDirectory}Spartan.exe";

                if (!File.Exists(path))
                {
                    SkinHelper.ShowMessage(
                        "Error: Spartan.exe not found!",
                        @"Project Celeste",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnSmall1.Enabled = true;
                    return;
                }

                var arg = Program.UserConfig?.MpSettings == null || Program.UserConfig.MpSettings.IsOnline
                    ? $"--email \"{Program.UserConfig.LoginInfo.Email}\"  --password \"{Program.UserConfig.LoginInfo.Password}\" --ignore_rest LauncherLang={comboBox2.Text} LauncherLocale=1033"
                    : $"--email \"{Program.UserConfig.LoginInfo.Email}\"  --password \"{Program.UserConfig.LoginInfo.Password}\" --online-ip \"{Program.UserConfig.MpSettings.PublicIp}\" --ignore_rest LauncherLang={comboBox2.Text} LauncherLocale=1033";

                Process.Start(path, arg);

                Close();
            }
            catch (Exception exception)
            {
                SkinHelper.ShowMessage(
                    $"Error: {exception.Message}",
                    @"Project Celeste",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            btnSmall1.Enabled = true;
        }

        private void Label6_Click(object sender, EventArgs e)
        {
            using (var form = new MpSettingForm(Program.UserConfig.MpSettings))
            {
                Hide();
                form.ShowDialog();
                Show();
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
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

        private void LinkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://aoedb.net/");
        }

        private void LinkLabel7_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://discord.gg/pkM2RAm");
        }

        private void BtnSmall1_Load(object sender, EventArgs e)
        {
        }

        private void LinkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://www.reddit.com/r/projectceleste/");
        }
    }
}