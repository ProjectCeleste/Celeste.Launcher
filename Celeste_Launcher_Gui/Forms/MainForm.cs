#region Using directives

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Reflection;
using System.Timers;
using System.Windows.Forms;
using Celeste_Launcher_Gui.Helpers;
using Celeste_User.Remote;
using Open.Nat;
using Timer = System.Timers.Timer;

#endregion

namespace Celeste_Launcher_Gui.Forms
{
    public partial class MainForm : Form
    {
        private static Timer _timer;
        private static bool _loginPassed;

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

            //OnPropertyChanged
            Program.WebSocketClient.PropertyChanged += OnPropertyChanged;

            //Login
            if (Program.WebSocketClient.State != WebSocketClientState.Logging ||
                Program.WebSocketClient.State != WebSocketClientState.Logged)
                using (var form = new LoginForm())
                {
                    var dr = form.ShowDialog();

                    if (dr != DialogResult.OK)
                    {
                        Program.WebSocketClient.AgentWebSocket.Close();
                        Environment.Exit(0);
                    }
                }

            //User Info
            if (Program.RemoteUser != null)
                ExecuteUserInfoResultCommand(Program.RemoteUser);

            //Auto-Refresh User Info
            if (_timer != null) return;

            _timer = new Timer(1000 * 60); //60Sec
            _timer.Elapsed += DoUserInfo;
            _timer.AutoReset = true;
            _timer.Enabled = true;
            _timer.Enabled = true;
            _timer.Start();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _timer.Stop();
            Program.WebSocketClient.AgentWebSocket.Close();
            try
            {
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

        private void LinkLbl_aoeo4evernet_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://aoeo4ever.net");
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

        private static void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (e.PropertyName)
            {
                case "State":
                {
                    switch (Program.WebSocketClient.State)
                    {
                        case WebSocketClientState.Offline:
                        {
                            if (_timer != null && _timer.Enabled)
                            {
                                _timer.Enabled = false;
                                _timer.Stop();
                            }
                            if (_loginPassed)
                            {
                                SkinHelper.ShowMessage(@"You have been disconnected from the server!",
                                    @"Project Celeste",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Application.Exit();
                            }
                            break;
                        }
                        case WebSocketClientState.Connecting:
                            break;
                        case WebSocketClientState.Logging:
                            break;
                        case WebSocketClientState.Logged:
                        {
                            if (_timer != null && !_timer.Enabled)
                            {
                                _timer.Enabled = true;
                                _timer.Start();
                            }
                            _loginPassed = true;
                            break;
                        }
                        case WebSocketClientState.Connected:
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(WebSocketClient.State),
                                Program.WebSocketClient.State,
                                @"OnPropertyChanged()");
                    }
                    break;
                }
            }
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
                        Program.UserConfig.MpSettings.PublicIp = Program.RemoteUser.Ip;

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

            //Save UserConfig
            Program.UserConfig.GameLanguage = (GameLanguage) comboBox2.SelectedIndex;
            Program.UserConfig.Save(Program.UserConfigFilePath);

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

            btnSmall1.Enabled = true;

            var arg = Program.UserConfig.MpSettings.IsOnline
                ? $"--email \"{Program.UserConfig.LoginInfo.Email}\"  --password \"{Program.UserConfig.LoginInfo.Password}\" --ignore_rest LauncherLang={comboBox2.Text} LauncherLocale=1033"
                : $"--email \"{Program.UserConfig.LoginInfo.Email}\"  --password \"{Program.UserConfig.LoginInfo.Password}\" --online-ip \"{Program.UserConfig.MpSettings.PublicIp}\" --ignore_rest LauncherLang={comboBox2.Text} LauncherLocale=1033";

            Process.Start(path, arg);
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



        #region "User Info"

        private UserInfoResult _userInfoResult;

        private delegate void UserInfoResult(RemoteUser remoteUser);

        private void DoUserInfo(object source, ElapsedEventArgs e)
        {
            DoUserInfo();
        }

        private void DoUserInfo()
        {
            if (Program.WebSocketClient.State != WebSocketClientState.Logged)
                return;

            dynamic getUserInfo = new ExpandoObject();
            getUserInfo.UserName = "";

            Program.WebSocketClient.AgentWebSocket?.Query<dynamic>("GETUSERINFO", (object) getUserInfo, OnUserInfo);
        }

        private void OnUserInfo(dynamic result)
        {
            if (result["Result"].ToObject<bool>())
            {
                Program.RemoteUser = result["RemoteUser"].ToObject<RemoteUser>();

                if (_userInfoResult == null)
                    _userInfoResult = ExecuteUserInfoResultCommand;
                try
                {
                    Invoke(_userInfoResult, Program.RemoteUser);
                }
                catch (Exception)
                {
                    //
                }
            }
            else
            {
                var str = result["Message"].ToObject<string>();
                SkinHelper.ShowMessage($@"OnUserInfo(): {str}", @"Project Celeste",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExecuteUserInfoResultCommand(RemoteUser remoteUser)
        {
            lbl_Mail.Text = $@"Email: {remoteUser.Mail}";
            lbl_UserName.Text = $@"User Name: {remoteUser.ProfileName}";
            lbl_Rank.Text = $@"Rank: {remoteUser.Rank}";
        }

        #endregion
    }
}