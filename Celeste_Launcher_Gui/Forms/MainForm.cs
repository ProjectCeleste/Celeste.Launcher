#region Using directives

using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Celeste_AOEO_Controls;
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

            //QuickGameScan
            using (var form = new QuickGameScan())
            {
                retry:
                var dr = form.ShowDialog();

                if (dr == DialogResult.Retry)
                    goto retry;

                if (dr != DialogResult.OK)
                    Environment.Exit(0);
            }

            //
            lb_Ver.Text = $@"v{Assembly.GetEntryAssembly().GetName().Version}";

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

            //AutoDisconnect
            try
            {
                Program.WebSocketClient.AgentWebSocket.Close();
            }
            catch
            {
                //
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
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
        }

        private void Linklbl_ReportBug_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/ProjectCeleste/Celeste_Server/issues");
        }

        private void LinkLbl_ProjectCelesteCom_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://projectceleste.com");
        }

        private void Linklbl_Wiki_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://ageofempiresonline.wikia.com/wiki/Age_of_Empires_Online_Wiki");
        }

        private void LinkLabel3_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://eso-community.net/");
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
                MsgBox.ShowMessage(@"Game already runing!");
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

                                MsgBox.ShowMessage(
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
                MsgBox.ShowMessage(
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
                    MsgBox.ShowMessage(
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

                WindowState = FormWindowState.Minimized;
            }
            catch (Exception exception)
            {
                MsgBox.ShowMessage(
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

        private void PictureBox1_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=EZ3SSAJRRUYFY");
        }
    }
}