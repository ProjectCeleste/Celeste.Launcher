#region Using directives

using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Celeste_AOEO_Controls;
using Celeste_AOEO_Controls.MsgBox;
using Celeste_Launcher_Gui.Helpers;
using Celeste_Public_Api.GameScanner_Api;
using Celeste_Public_Api.Helpers;
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
            UpdateDiagModeToolStripFromConfig();

            //
            panelManager1.SelectedPanel = managedPanel2;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                LegacyBootstrapper.WebSocketApi?.Disconnect();
                NatDiscoverer.ReleaseAll();
                LegacyBootstrapper.UserConfig?.Save(LegacyBootstrapper.UserConfigFilePath);
            }
            catch
            {
                //
            }
        }

        private void Btn_Play_Click(object sender, EventArgs e)
        {
            btn_Play.Enabled = false;
            StartGame();
            btn_Play.Enabled = true;
            WindowState = FormWindowState.Minimized;
        }

        private void PictureBoxButtonCustom3_Click(object sender, EventArgs e)
        {
            Process.Start("https://projectceleste.com");
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
            Process.Start("https://forums.projectceleste.com/donations/");
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

                    LegacyBootstrapper.CurrentUser = form.CurrentUser;

                    gamerCard1.UserName = $@"{LegacyBootstrapper.CurrentUser.ProfileName}";
                    gamerCard1.Rank = $@"{LegacyBootstrapper.CurrentUser.Rank}";

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
            ////Register
            //using (var form = new RegisterForm())
            //{
            //    form.ShowDialog();
            //    if (form.DialogResult != DialogResult.OK)
            //        return;

            //    //Save UserConfig
            //    if (LegacyBootstrapper.UserConfig == null)
            //    {
            //        LegacyBootstrapper.UserConfig = new UserConfig
            //        {
            //            LoginInfo = new LoginInfo
            //            {
            //                Email = form.tb_Mail.Text,
            //                Password = form.tb_Password.Text,
            //                RememberMe = true
            //            }
            //        };
            //    }
            //    else
            //    {
            //        LegacyBootstrapper.UserConfig.LoginInfo.Email = form.tb_Mail.Text;
            //        LegacyBootstrapper.UserConfig.LoginInfo.Password = form.tb_Password.Text;
            //        LegacyBootstrapper.UserConfig.LoginInfo.RememberMe = true;
            //    }
            //    try
            //    {
            //        LegacyBootstrapper.UserConfig.Save(LegacyBootstrapper.UserConfigFilePath);
            //    }
            //    catch (Exception)
            //    {
            //        //
            //    }
            //}

            //Login
            using (var form = new LoginForm())
            {
                var dr = form.ShowDialog();

                if (dr == DialogResult.OK)
                {
                    if (form.CurrentUser == null)
                        return;

                    LegacyBootstrapper.CurrentUser = form.CurrentUser;

                    gamerCard1.UserName = $@"{LegacyBootstrapper.CurrentUser.ProfileName}";
                    gamerCard1.Rank = $@"{LegacyBootstrapper.CurrentUser.Rank}";

                    panelManager1.SelectedPanel = managedPanel1;
                }
                else
                {
                    panelManager1.SelectedPanel = managedPanel2;
                }
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
            using (var form = new MpSettingForm(LegacyBootstrapper.UserConfig.MpSettings))
            {
                form.ShowDialog();
            }
        }

        private void PictureBoxButtonCustom7_Click(object sender, EventArgs e)
        {
            var btnSender = (PictureBoxButtonCustom) sender;
            var pt = new Point(btnSender.Bounds.Width + 1, btnSender.Bounds.Top);
            cMS_Account.Show(btnSender, pt);
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
                Misc.CleanUpFiles(Directory.GetCurrentDirectory(), "*.old");
            }
            catch (Exception ex)
            {
                MsgBox.ShowMessage(
                    $"Warning: Error during files clean-up. Error message: {ex.Message}",
                    @"Celeste Fan Project",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (!DownloadFileUtils.IsConnectedToInternet()) return;

            //Update Check
            try
            {
                if (await UpdaterForm.GetGitHubVersion() > Assembly.GetExecutingAssembly().GetName().Version)
                    using (var form =
                        new MsgBoxYesNo(
                            @"An update is avalaible. Click ""Yes"" to install it, or ""No"" to ignore it (not recommended).")
                    )
                    {
                        var dr = form.ShowDialog();
                        if (dr == DialogResult.OK)
                            using (var form2 = new UpdaterForm())
                            {
                                form2.ShowDialog();
                            }
                    }
            }
            catch (Exception ex)
            {
                MsgBox.ShowMessage(
                    $"Warning: Error during update check. Error message: {ex.Message}",
                    @"Celeste Fan Project",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            //Auto Login
            if (LegacyBootstrapper.UserConfig?.LoginInfo == null)
                return;

            if (!LegacyBootstrapper.UserConfig.LoginInfo.AutoLogin)
                return;

            panelManager1.Enabled = false;
            //try
            //{
            //    var response = await LegacyBootstrapper.WebSocketApi.DoLogin(LegacyBootstrapper.UserConfig.LoginInfo.Email,
            //        LegacyBootstrapper.UserConfig.LoginInfo.Password);

            //    if (response.Result)
            //    {
            //        LegacyBootstrapper.CurrentUser = response.User;

            //        gamerCard1.UserName = LegacyBootstrapper.CurrentUser.ProfileName;
            //        gamerCard1.Rank = $@"{LegacyBootstrapper.CurrentUser.Rank}";

            //        panelManager1.SelectedPanel = managedPanel1;
            //    }
            //}
            //catch (Exception)
            //{
            //    //
            //}
            panelManager1.Enabled = true;
        }

        private void LogOffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelManager1.Enabled = false;
            //
            try
            {
                LegacyBootstrapper.WebSocketApi?.Disconnect();
            }
            catch
            {
                //
            }
            //

            //Save UserConfig
            if (LegacyBootstrapper.UserConfig?.LoginInfo != null)
            {
                LegacyBootstrapper.UserConfig.LoginInfo.AutoLogin = false;

                try
                {
                    LegacyBootstrapper.UserConfig.Save(LegacyBootstrapper.UserConfigFilePath);
                }
                catch (Exception)
                {
                    //
                }
            }

            //
            panelManager1.SelectedPanel = managedPanel2;
            panelManager1.Enabled = true;
        }

        private void WindowsFeaturesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var osInfo = OsVersionInfo.GetOsVersionInfo();
            if (osInfo.Major < 6 || osInfo.Major == 6 && osInfo.Minor < 2)
            {
                MsgBox.ShowMessage(
                    "Only for Windows 8 and more\r\n" +
                    $"Your current OS is {osInfo.FullName}",
                    @"Celeste Fan Project",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }

            using (var form = new WindowsFeaturesForm())
            {
                form.ShowDialog();
            }
        }

        private void SteamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var form = new SteamForm())
            {
                form.ShowDialog();
            }
        }

        private void PictureBoxButtonCustom6_Click(object sender, EventArgs e)
        {
            using (var x = new FriendsForm())
            {
                x.ShowDialog();
            }
        }

        private void UpdateDiagModeToolStripFromConfig()
        {
            enableDiagnosticModeToolStripMenuItem.Text = LegacyBootstrapper.UserConfig.IsDiagnosticMode
                ? @"Disable Diagnostic Mode"
                : @"Enable Diagnostic Mode";
        }

        private void EnableDiagnosticModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LegacyBootstrapper.UserConfig.IsDiagnosticMode = !LegacyBootstrapper.UserConfig.IsDiagnosticMode;

            if (LegacyBootstrapper.UserConfig.IsDiagnosticMode)
                try
                {
                    var procdumpFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "procdump.exe");
                    if (!File.Exists(procdumpFileName))
                        using (var form =
                            new MsgBoxYesNo(
                                "ProcDump.exe need to be installed first. Click \"Yes\" to install it, or \"No\" to cancel.\r\n" +
                                "(https://docs.microsoft.com/en-us/sysinternals/downloads/procdump)"
                            )
                        )
                        {
                            var dr = form.ShowDialog();
                            if (dr == DialogResult.OK)
                                using (var form2 = new InstallProcDump())
                                {
                                    form2.ShowDialog();
                                }
                            else
                                LegacyBootstrapper.UserConfig.IsDiagnosticMode = false;
                        }
                }
                catch (Exception exception)
                {
                    LegacyBootstrapper.UserConfig.IsDiagnosticMode = false;
                    MsgBox.ShowMessage(
                        $"Warning: Failed to enable \"Diagnostic Mode\". Error message: {exception.Message}",
                        @"Celeste Fan Project",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            UpdateDiagModeToolStripFromConfig();
        }

        private void WindowsFirewallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var form = new FirewallForm())
            {
                form.ShowDialog();
            }
        }

        private void CustomBtn3_Click(object sender, EventArgs e)
        {
            customBtn3.Enabled = false;
            StartGame(true);
            customBtn3.Enabled = true;
            WindowState = FormWindowState.Minimized;
        }

        private void ToolStripMenuItem2_Click_1(object sender, EventArgs e)
        {
            using (var form = new LanguageChooser())
            {
                var dr = form.ShowDialog();

                if (dr != DialogResult.OK)
                    return;

                //try
                //{
                //    if (LegacyBootstrapper.UserConfig != null)
                //        LegacyBootstrapper.UserConfig.GameLanguage = form.SelectedLang;
                //    else
                //        LegacyBootstrapper.UserConfig = new UserConfig {GameLanguage = form.SelectedLang};
                //}
                //catch (Exception)
                //{
                //    //
                //}
            }
        }

        public static async void StartGame(bool isOffline = false)
        {
            var pname = Process.GetProcessesByName("spartan");
            if (pname.Length > 0)
            {
                MsgBox.ShowMessage(@"Game already running!");
                return;
            }

            //QuickGameScan
            if (!isOffline || DownloadFileUtils.IsConnectedToInternet())
                try
                {
                    var gameFilePath = !string.IsNullOrWhiteSpace(LegacyBootstrapper.UserConfig.GameFilesPath)
                        ? LegacyBootstrapper.UserConfig.GameFilesPath
                        : GameScannnerApi.GetGameFilesRootPath();

                    var gameScannner = new GameScannnerApi(gameFilePath, LegacyBootstrapper.UserConfig.IsSteamVersion);

                    retry:
                    if (!await gameScannner.QuickScan())
                    {
                        bool success;
                        using (var form =
                            new MsgBoxYesNo(
                                @"Error: Your game files are corrupted or outdated. Click ""Yes"" to run a ""Game Scan"" to fix your game files, or ""No"" to ignore the error (not recommended).")
                        )
                        {
                            var dr = form.ShowDialog();
                            if (dr == DialogResult.OK)
                                using (var form2 = new GameScan())
                                {
                                    form2.ShowDialog();
                                    success = false;
                                }
                            else
                                success = true;
                        }
                        if (!success)
                            goto retry;
                    }
                }
                catch (Exception ex)
                {
                    MsgBox.ShowMessage(
                        $"Warning: Error during quick scan. Error message: {ex.Message}",
                        @"Celeste Fan Project",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            //isSteam
            if (!LegacyBootstrapper.UserConfig.IsSteamVersion)
            {
                var steamApiDll = Path.Combine(LegacyBootstrapper.UserConfig.GameFilesPath, "steam_api.dll");
                if (File.Exists(steamApiDll))
                    File.Delete(steamApiDll);
            }

            //MpSettings
            if (!isOffline && LegacyBootstrapper.UserConfig.MpSettings != null)
                if (LegacyBootstrapper.UserConfig.MpSettings.ConnectionType == ConnectionType.Wan)
                {
                    LegacyBootstrapper.UserConfig.MpSettings.PublicIp = LegacyBootstrapper.CurrentUser.Ip;

                    if (LegacyBootstrapper.UserConfig.MpSettings.PortMappingType == PortMappingType.Upnp)
                        try
                        {
                            await OpenNat.MapPortTask(1000, 1000);
                        }
                        catch (Exception)
                        {
                            LegacyBootstrapper.UserConfig.MpSettings.PortMappingType = PortMappingType.NatPunch;

                            MsgBox.ShowMessage(
                                "Error: Upnp device not found! \"UPnP Port Mapping\" has been disabled.",
                                @"Celeste Fan Project",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        finally
                        {
                            NatDiscoverer.TraceSource.Close();
                        }
                }

            try
            {
                //Launch Game
                var path = !string.IsNullOrWhiteSpace(LegacyBootstrapper.UserConfig.GameFilesPath)
                    ? LegacyBootstrapper.UserConfig.GameFilesPath
                    : GameScannnerApi.GetGameFilesRootPath();

                var spartanPath = Path.Combine(path, "Spartan.exe");

                if (!File.Exists(spartanPath))
                    throw new FileNotFoundException("Spartan.exe not found!", spartanPath);

                string lang;
                switch (LegacyBootstrapper.UserConfig.GameLanguage)
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
                        throw new ArgumentOutOfRangeException(nameof(LegacyBootstrapper.UserConfig.GameLanguage),
                            LegacyBootstrapper.UserConfig.GameLanguage, null);
                }

                try
                {
                    if (LegacyBootstrapper.UserConfig.IsDiagnosticMode)
                    {
                        //
                        try
                        {
                            var killInfo = new ProcessStartInfo("cmd.exe", "/c taskkill /F /IM procdump.exe /T")
                            {
                                WorkingDirectory = path,
                                CreateNoWindow = true,
                                UseShellExecute = false,
                                RedirectStandardError = true,
                                RedirectStandardOutput = true
                            };

                            Process.Start(killInfo);
                        }
                        catch (Exception)
                        {
                            //
                        }

                        var procdumpFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "procdump.exe");
                        const int maxNumOfCrashDumps = 30;
                        if (!File.Exists(procdumpFileName))
                        {
                            LegacyBootstrapper.UserConfig.IsDiagnosticMode = false;
                            throw new FileNotFoundException(
                                "Diagonstic Mode requires procdump.exe (File not Found).\r\n" +
                                "Diagonstic Mode will be disabled.",
                                procdumpFileName);
                        }

                        // First ensure that all directories are set
                        var pathToCrashDumpFolder =
                            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                                @"Spartan\MiniDumps");

                        if (!Directory.Exists(pathToCrashDumpFolder))
                            Directory.CreateDirectory(pathToCrashDumpFolder);

                        // Check for cleanup
                        Directory.GetFiles(pathToCrashDumpFolder)
                            .OrderByDescending(File.GetLastWriteTime) // Sort by age --> old one last
                            .Skip(maxNumOfCrashDumps) // Skip max num crash dumps
                            .ToList()
                            .ForEach(File.Delete); // Remove the rest

                        var excludeExceptions = new[]
                        {
                            "E0434F4D.COM", // .NET native exception
                            "E06D7363.msc",
                            "E06D7363.PAVEEFileLoadException@@",
                            "E0434F4D.System.IO.FileNotFoundException" // .NET managed exception
                        };

                        var excludeExcpetionsCmd = string.Join(" ", excludeExceptions.Select(elem => "-fx " + elem));

                        var fullCmdArgs = "-accepteula -mm -e 1 -n 10 " + excludeExcpetionsCmd +
                                          " -g -w Spartan.exe \"" + pathToCrashDumpFolder + "\"";

                        var startInfo = new ProcessStartInfo(procdumpFileName, fullCmdArgs)
                        {
                            WorkingDirectory = path,
                            CreateNoWindow = true,
                            UseShellExecute = false,
                            RedirectStandardError = true,
                            RedirectStandardOutput = true
                        };

                        Process.Start(startInfo);
                    }
                }
                catch (Exception exception)
                {
                    MsgBox.ShowMessage(
                        $"Warning: {exception.Message}",
                        @"Celeste Fan Project",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                //SymLink CustomScn Folder
                var profileDir = Path.Combine(Environment.GetEnvironmentVariable("userprofile"));
                var path1 = Path.Combine(path, "Scenario", "CustomScn");
                var path2 = Path.Combine(profileDir, "Documents", "Spartan", "Scenario");

                if (!Directory.Exists(path2))
                    Directory.CreateDirectory(path2);

                if (Directory.Exists(path1) &&
                    (!Misc.IsSymLink(path1, Misc.SymLinkFlag.Directory) ||
                     !string.Equals(Misc.GetRealPath(path1), path2, StringComparison.OrdinalIgnoreCase)))
                {
                    Directory.Delete(path1, true);
                    Misc.CreateSymbolicLink(path1, path2, Misc.SymLinkFlag.Directory);
                }
                else
                {
                    Misc.CreateSymbolicLink(path1, path2, Misc.SymLinkFlag.Directory);
                }

                //string arg;
                //if (isOffline)
                //    arg = $"--offline --ignore_rest LauncherLang={lang} LauncherLocale=1033";
                //else if (LegacyBootstrapper.UserConfig?.MpSettings == null ||
                //         LegacyBootstrapper.UserConfig.MpSettings.ConnectionType == ConnectionType.Wan)
                //    arg = LegacyBootstrapper.UserConfig.MpSettings.PortMappingType == PortMappingType.NatPunch
                //        ? $"--email \"{LegacyBootstrapper.UserConfig.LoginInfo.Email}\" --password \"{LegacyBootstrapper.UserConfig.LoginInfo.Password}\" --ignore_rest LauncherLang={lang} LauncherLocale=1033"
                //        : $"--email \"{LegacyBootstrapper.UserConfig.LoginInfo.Email}\" --password \"{LegacyBootstrapper.UserConfig.LoginInfo.Password}\" --no-nat-punchthrough --ignore_rest LauncherLang={lang} LauncherLocale=1033";
                //else
                //    arg =
                //        $"--email \"{LegacyBootstrapper.UserConfig.LoginInfo.Email}\" --password \"{LegacyBootstrapper.UserConfig.LoginInfo.Password}\" --online-ip \"{LegacyBootstrapper.UserConfig.MpSettings.PublicIp}\" --ignore_rest LauncherLang={lang} LauncherLocale=1033";


                //Process.Start(new ProcessStartInfo(spartanPath, arg) {WorkingDirectory = path});
            }
            catch (Exception exception)
            {
                MsgBox.ShowMessage(
                    $"Error: {exception.Message}",
                    @"Celeste Fan Project",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ScenarioManagerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            using (var form = new ScnManagerForm())
            {
                form.ShowDialog();
            }
        }

        private void PlayOfflineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (customBtn3.Enabled)
                CustomBtn3_Click(sender, e);
        }
    }
}