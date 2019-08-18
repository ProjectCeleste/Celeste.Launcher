using Celeste_AOEO_Controls.MsgBox;
using Celeste_Launcher_Gui.Helpers;
using Celeste_Public_Api.GameScanner_Api;
using Celeste_Public_Api.Helpers;
using Open.Nat;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Celeste_Launcher_Gui.Services
{
    class GameService
    {
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
                                using (var form2 = new Forms.GameScan())
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

                string arg;
                if (isOffline)
                    arg = $"--offline --ignore_rest LauncherLang={lang} LauncherLocale=1033";
                else if (LegacyBootstrapper.UserConfig?.MpSettings == null ||
                         LegacyBootstrapper.UserConfig.MpSettings.ConnectionType == ConnectionType.Wan)
                    arg = LegacyBootstrapper.UserConfig.MpSettings.PortMappingType == PortMappingType.NatPunch
                        ? $"--email \"{LegacyBootstrapper.UserConfig.LoginInfo.Email}\" --password \"{LegacyBootstrapper.UserConfig.LoginInfo.Password}\" --ignore_rest LauncherLang={lang} LauncherLocale=1033"
                        : $"--email \"{LegacyBootstrapper.UserConfig.LoginInfo.Email}\" --password \"{LegacyBootstrapper.UserConfig.LoginInfo.Password}\" --no-nat-punchthrough --ignore_rest LauncherLang={lang} LauncherLocale=1033";
                else
                    arg =
                        $"--email \"{LegacyBootstrapper.UserConfig.LoginInfo.Email}\" --password \"{LegacyBootstrapper.UserConfig.LoginInfo.Password}\" --online-ip \"{LegacyBootstrapper.UserConfig.MpSettings.PublicIp}\" --ignore_rest LauncherLang={lang} LauncherLocale=1033";


                Process.Start(new ProcessStartInfo(spartanPath, arg) { WorkingDirectory = path });
            }
            catch (Exception exception)
            {
                MsgBox.ShowMessage(
                    $"Error: {exception.Message}",
                    @"Celeste Fan Project",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
