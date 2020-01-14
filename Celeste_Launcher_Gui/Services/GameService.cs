#region Using directives

using Celeste_Launcher_Gui.Helpers;
using Celeste_Launcher_Gui.Properties;
using Celeste_Launcher_Gui.Windows;
using Open.Nat;
using ProjectCeleste.GameFiles.GameScanner;
using ProjectCeleste.Launcher.PublicApi.Helpers;
using ProjectCeleste.Launcher.PublicApi.Logging;
using Serilog;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security;
using System.Threading.Tasks;

#endregion Using directives

namespace Celeste_Launcher_Gui.Services
{
    internal static class GameService
    {
        // TODO: Find a better way to do this (for example using an auth token)
        private static string _currentEmail;

        private static SecureString _currentPassword;
        private static readonly ILogger Logger = LoggerFactory.GetLogger();

        internal static void SetCredentials(string email, SecureString password)
        {
            _currentEmail = email;
            _currentPassword = password;
        }

        public static async Task StartGame(bool isOffline = false)
        {
            Logger.Information("Preparing to start game, is offline: {@isOffline}", isOffline);

            var pname = Process.GetProcessesByName("spartan");
            if (pname.Length > 0)
            {
                Logger.Information("Game is already started with PID {@PID}", pname.Select(t => t.Id));
                GenericMessageDialog.Show(Resources.GameAlreadyRunningError, DialogIcon.Warning);
                return;
            }

            //QuickGameScan
            if (!isOffline || InternetUtils.IsConnectedToInternet())
            {
                Logger.Information("User is online, will perform game scan");
                try
                {
                    var gameFilePath = !string.IsNullOrWhiteSpace(LegacyBootstrapper.UserConfig.GameFilesPath)
                        ? LegacyBootstrapper.UserConfig.GameFilesPath
                        : GameScannnerManager.GetGameFilesRootPath();

                    Logger.Information("Preparing games canner api");
                    var gameScanner = new GameScannnerManager(gameFilePath, false,
                        LegacyBootstrapper.UserConfig.IsSteamVersion);
                    await gameScanner.InitializeFromCelesteManifest();

                    var success = false;

                    while (!success)
                    {
                        Logger.Information("Starting quick game scan");
                        if (!await gameScanner.Scan())
                        {
                            Logger.Information("Game scanner did not approve game files");

                            var dialogResult = GenericMessageDialog.Show(
                                Resources.GameScannerDidNotPassQuickScan,
                                DialogIcon.None,
                                DialogOptions.YesNo);

                            if (dialogResult != null && dialogResult.Value)
                            {
                                var scanner = new GamePathSelectionWindow();
                                scanner.ShowDialog();
                            }
                            else
                            {
                                success = true;
                            }
                        }
                        else
                        {
                            Logger.Information("Game files passed file scanner");
                            success = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error(ex, ex.Message);
                    GenericMessageDialog.Show(Resources.GameScannerScanError, DialogIcon.Warning);
                }
            }

            //isSteam
            if (!LegacyBootstrapper.UserConfig.IsSteamVersion)
            {
                var steamApiDll = Path.Combine(LegacyBootstrapper.UserConfig.GameFilesPath, "steam_api.dll");
                if (File.Exists(steamApiDll))
                    File.Delete(steamApiDll);
            }

            //MpSettings
            if (!isOffline && LegacyBootstrapper.UserConfig.MpSettings?.ConnectionType == ConnectionType.Wan)
            {
                LegacyBootstrapper.UserConfig.MpSettings.PublicIp = LegacyBootstrapper.CurrentUser.Ip;

                if (LegacyBootstrapper.UserConfig.MpSettings.PortMappingType == PortMappingType.Upnp)
                    try
                    {
                        await OpenNat.MapPortTask(1000, 1000);
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(ex, ex.Message);
                        LegacyBootstrapper.UserConfig.MpSettings.PortMappingType = PortMappingType.NatPunch;

                        GenericMessageDialog.Show(Resources.UPnPDisabledError, DialogIcon.Error);
                    }
                    finally
                    {
                        NatDiscoverer.TraceSource.Close();
                    }
            }

            try
            {
                //Launch Game
                var gamePath = !string.IsNullOrWhiteSpace(LegacyBootstrapper.UserConfig.GameFilesPath)
                    ? LegacyBootstrapper.UserConfig.GameFilesPath
                    : GameScannnerManager.GetGameFilesRootPath();

                var spartanPath = Path.Combine(gamePath, "Spartan.exe");

                if (!File.Exists(spartanPath))
                {
                    GenericMessageDialog.Show(Resources.GameScannerSpartanNotFound, DialogIcon.Error);
                    return;
                }

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
                        Logger.Information("Diagnostics mode is enabled");
                        //
                        try
                        {
                            var killInfo = new ProcessStartInfo("cmd.exe", "/c taskkill /F /IM procdump.exe /T")
                            {
                                WorkingDirectory = gamePath,
                                CreateNoWindow = true,
                                UseShellExecute = false,
                                RedirectStandardError = true,
                                RedirectStandardOutput = true
                            };

                            Logger.Information("Starting prcoess {@Proc} with args {@Procargs}", killInfo.FileName,
                                killInfo.Arguments);
                            Process.Start(killInfo);
                        }
                        catch (Exception ex)
                        {
                            Logger.Error(ex, ex.Message);
                        }

                        var procdumpFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "procdump.exe");
                        const int maxNumOfCrashDumps = 30;
                        if (!File.Exists(procdumpFileName))
                        {
                            Logger.Information("Could not find procdump.exe");
                            LegacyBootstrapper.UserConfig.IsDiagnosticMode = false;
                            throw new FileNotFoundException(
                                Resources.DiagnosticsModeMissingProcdump,
                                procdumpFileName);
                        }

                        // First ensure that all directories are set
                        var pathToCrashDumpFolder =
                            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                                @"Spartan\MiniDumps");

                        Logger.Information("CrashDumpFolder is set to {@CrashDumpFolder}", pathToCrashDumpFolder);

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
                            WorkingDirectory = gamePath,
                            CreateNoWindow = true,
                            UseShellExecute = false,
                            RedirectStandardError = true,
                            RedirectStandardOutput = true
                        };

                        Logger.Information("Starting prcoess {@Proc} with args {@Procargs}", startInfo.FileName,
                            startInfo.Arguments);

                        Process.Start(startInfo);
                    }
                }
                catch (Exception exception)
                {
                    Logger.Error(exception, exception.Message);
                    GenericMessageDialog.Show(Resources.DiagnosticsModeError, DialogIcon.Warning);
                }

                //SymLink CustomScn Folder
                var profileDir = Path.Combine(Environment.GetEnvironmentVariable("userprofile"));
                var customScnGamePath = Path.Combine(gamePath, "Scenario", "CustomScn");
                var scenarioUserPath = Path.Combine(profileDir, "Documents", "Spartan", "Scenario");

                Logger.Information("CustomScn directory: {@customScnPath}", customScnGamePath);
                Logger.Information("Scenario directory: {@scenarioPath}", scenarioUserPath);

                if (!Directory.Exists(scenarioUserPath))
                    Directory.CreateDirectory(scenarioUserPath);

                if (Directory.Exists(customScnGamePath) &&
                    (!Files.IsSymLink(customScnGamePath, Files.SymLinkFlag.Directory) ||
                     !string.Equals(Files.GetRealPath(customScnGamePath), scenarioUserPath,
                         StringComparison.OrdinalIgnoreCase)))
                {
                    Directory.Delete(customScnGamePath, true);
                    Files.CreateSymbolicLink_(customScnGamePath, scenarioUserPath, Files.SymLinkFlag.Directory);
                }
                else
                {
                    Files.CreateSymbolicLink_(customScnGamePath, scenarioUserPath, Files.SymLinkFlag.Directory);
                }

                string arg;
                if (isOffline)
                    arg = $"--offline --ignore_rest LauncherLang={lang} LauncherLocale=1033";
                else if (LegacyBootstrapper.UserConfig?.MpSettings == null ||
                         LegacyBootstrapper.UserConfig.MpSettings.ConnectionType == ConnectionType.Wan)
                    arg = LegacyBootstrapper.UserConfig.MpSettings.PortMappingType == PortMappingType.NatPunch
                        ? $"--email \"{_currentEmail}\" --password \"{_currentPassword.GetValue()}\" --ignore_rest LauncherLang={lang} LauncherLocale=1033"
                        : $"--email \"{_currentEmail}\" --password \"{_currentPassword.GetValue()}\" --no-nat-punchthrough --ignore_rest LauncherLang={lang} LauncherLocale=1033";
                else
                    arg =
                        $"--email \"{_currentEmail}\" --password \"{_currentPassword.GetValue()}\" --online-ip \"{LegacyBootstrapper.UserConfig.MpSettings.PublicIp}\" --ignore_rest LauncherLang={lang} LauncherLocale=1033";

                Logger.Information("Starting game {@GameExecutable} at {@GamePath}", spartanPath, gamePath);
                Process.Start(new ProcessStartInfo(spartanPath, arg) { WorkingDirectory = gamePath });
            }
            catch (Exception exception)
            {
                Logger.Error(exception, exception.Message);
                GenericMessageDialog.Show(Resources.StartGameError, DialogIcon.Error);
            }
        }
    }
}