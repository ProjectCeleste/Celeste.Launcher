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

#endregion

namespace Celeste_Launcher_Gui
{
    internal static class LegacyBootstrapper
    {
        public static UserConfig UserConfig { get; private set; } = new UserConfig();
        
        private static readonly string AppName = $"CelesteFanProject_v{Assembly.GetEntryAssembly().GetName().Version}";

        public static WebSocketApi WebSocketApi { get; private set; }

        public static User CurrentUser { get; set; }

        public static string UserConfigFilePath { get; } =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CelesteConfig.xml");

        private static readonly ILogger Logger = LoggerFactory.GetLogger();

        public static void InitializeLegacyComponents()
        {
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

            //
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

            //Load UserConfig
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

            try
            {
                if (string.IsNullOrWhiteSpace(UserConfig.GameFilesPath))
                {
                    UserConfig.GameFilesPath = GameScannnerManager.GetGameFilesRootPath();
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
                UserConfig.IsSteamVersion = Assembly.GetEntryAssembly().Location
                    .EndsWith("AOEOnline.exe", StringComparison.OrdinalIgnoreCase);

                Logger.Information("IsSteamVersion: {@IsSteamVersion}", UserConfig.IsSteamVersion);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, ex.Message);
            }

            //SslFix (invalid cert)
            InternetUtils.SslFix();

            //Init WebSocketApi
            WebSocketApi = new WebSocketApi(UserConfig.ServerUri);
            Logger.Information("Initialized web socket");

            //Start Gui
            //Application.Run(new MainForm());

            GC.KeepAlive(mutex);

            Logger.Information("Initializing fingerprint provider");
            FingerPrintProvider.Initialize();

            Logger.Information("Bootstrapper initialized");
        }
    }
}