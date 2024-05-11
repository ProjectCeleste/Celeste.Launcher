﻿#region Using directives

using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Celeste_Public_Api.Helpers;
using Celeste_Public_Api.Logging;
using Celeste_Public_Api.WebSocket_Api;
using Celeste_Public_Api.WebSocket_Api.WebSocket.CommandInfo.Member;
using Serilog;
using ProjectCeleste.GameFiles.GameScanner;
using Celeste_Launcher_Gui.Windows;
using Celeste_Launcher_Gui.Helpers;

#endregion

namespace Celeste_Launcher_Gui
{
    public static class LegacyBootstrapper
    {
        public static UserConfig UserConfig { get; private set; } = new UserConfig();

        public static bool GameScannerNoConfirmDialog { get; private set; } = false;

        private static readonly string AppName = $"CelesteFanProject_v{Assembly.GetEntryAssembly().GetName().Version}";

        public static WebSocketApi WebSocketApi { get; private set; }

        public static User CurrentUser { get; set; }

        public static string UserConfigFilePath { get; } =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CelesteConfig.xml");

        private static readonly ILogger Logger = LoggerFactory.GetLogger();

        public static void InitializeLegacyComponents()
        {
            if (!FileSystem.IsWritableDirectory(AppDomain.CurrentDomain.BaseDirectory))
            {
                MessageBox.Show(string.Format(Properties.Resources.InstallationPathForbidden, AppDomain.CurrentDomain.BaseDirectory), "Celeste Launcher", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Windows.Application.Current.Shutdown();
                return;
            }

            Logger.Information("Initializing bootstrapper");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // TODO: Move this to app.xaml.cs
            var mutex = new Mutex(true, AppName, out bool createdNew);

            //Only one instance
            if (!createdNew)
            {
                Logger.Information("Launcher is already started, will exit");
                GenericMessageDialog.Show(Properties.Resources.LauncherAlreadyRunningMessage, DialogIcon.Warning, DialogOptions.Ok);
                return;
            }

            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

            LoadUserConfig();

            try
            {
                if (string.IsNullOrWhiteSpace(UserConfig.GameFilesPath))
                {
                    UserConfig.GameFilesPath = GameScannerManager.GetGameFilesRootPath();
                    Logger.Information("Game path set to {@Path}", UserConfigFilePath);
                }
                else
                {
                    Logger.Information("No game path is set");
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, ex.Message);
            }

            //Check if Steam Version
            try
            {
                var isSteamVersion = Assembly.GetEntryAssembly().Location
                    .EndsWith("AOEOnline.exe", StringComparison.OrdinalIgnoreCase);

                if (UserConfig.IsSteamVersion != isSteamVersion)
                {
                    UserConfig.IsSteamVersion = isSteamVersion;
                    UserConfig.Save(UserConfigFilePath);
                }

                Logger.Information("IsSteamVersion: {@IsSteamVersion}", UserConfig.IsSteamVersion);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, ex.Message);
            }

            //Init WebSocketApi
            WebSocketApi = new WebSocketApi(UserConfig.ServerUri);
            Logger.Information("Initialized web socket");

            GC.KeepAlive(mutex);

            Logger.Information("Initializing fingerprint provider");
            FingerPrintProvider.Initialize();

            SetUILanguage();

            Logger.Information("Bootstrapper initialized");
        }

        public static void SetUILanguage()
        {
            var localizedCultureInfo = MapGameLanguageToCultureInfo(UserConfig.GameLanguage);
            if (localizedCultureInfo != null)
            {
                Thread.CurrentThread.CurrentUICulture = localizedCultureInfo;
                Logger.Information("Localized UI language to {@Language}", localizedCultureInfo.DisplayName);
            }
            else
            {
                Logger.Information("Unknown culture {@Language}", localizedCultureInfo.DisplayName);
            }
        }

        private static CultureInfo MapGameLanguageToCultureInfo(GameLanguage gameLanguage)
        {
            switch (gameLanguage)
            {
                case GameLanguage.deDE:
                    return new CultureInfo("de-DE");
                case GameLanguage.esES:
                    return new CultureInfo("es-ES");
                case GameLanguage.frFR:
                    return new CultureInfo("fr-FR");
                case GameLanguage.itIT:
                    return new CultureInfo("it-IT");
                case GameLanguage.ptBR:
                    return new CultureInfo("pt-BR");
                case GameLanguage.zhCHT:
                    return new CultureInfo("zh-CHT");
                default:
                    return new CultureInfo("en-US");
            }
        }

        public static void LoadUserConfig()
        {
            try
            {
                if (File.Exists(UserConfigFilePath))
                {
                    UserConfig = UserConfig.Load(UserConfigFilePath);
                    Logger.Information("User config loaded from {@Path}", UserConfigFilePath);
                }
                else
                {
                    Logger.Information("No user config loaded, path {@Path} does not exist", UserConfigFilePath);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, ex.Message);
            }
        }

        public static void LoadOptionalOverrideArgs(string[] args)
        {
            if (args.Length >= 3)
            {
                try
                {
                    LegacyBootstrapper.UserConfig.GameFilesPath = args[0];
                    if (Enum.TryParse(args[1], true, out GameLanguage gamelang))
                    {
                        LegacyBootstrapper.UserConfig.GameLanguage = gamelang;
                    }
                    if (bool.TryParse(args[2], out bool isStream))
                    {
                        LegacyBootstrapper.UserConfig.IsSteamVersion = isStream;
                    }

                    if (args.Length >= 4)
                    {
                        if (bool.TryParse(args[3], out bool noConfirmDialog))
                        {
                            LegacyBootstrapper.GameScannerNoConfirmDialog = noConfirmDialog;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error(ex, ex.Message);
                }
            }
        }
    }
}