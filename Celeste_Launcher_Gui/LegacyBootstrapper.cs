#region Using directives

using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Celeste_AOEO_Controls.MsgBox;
using Celeste_Launcher_Gui.Account;
using Celeste_Launcher_Gui.Forms;
using Celeste_Public_Api.GameScanner_Api;
using Celeste_Public_Api.WebSocket_Api;
using Celeste_Public_Api.WebSocket_Api.WebSocket.CommandInfo.Member;

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

        public static void InitializeLegacyComponents()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // TODO: Move this to app.xaml.cs
            var mutex = new Mutex(true, AppName, out bool createdNew);

            //Only one instance
            if (!createdNew)
            {
                MsgBox.ShowMessage(
                    $@"""Celeste Fan Project Launcher"" v{
                            Assembly.GetEntryAssembly().GetName().Version
                        } already running!", "Celeste Fan Project",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            //
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

            //Load UserConfig
            try
            {
                if (File.Exists(UserConfigFilePath))
                    UserConfig = UserConfig.Load(UserConfigFilePath);
            }
            catch (Exception)
            {
                //
            }

            try
            {
                if (string.IsNullOrWhiteSpace(UserConfig.GameFilesPath))
                    UserConfig.GameFilesPath = GameScannnerApi.GetGameFilesRootPath();
            }
            catch (Exception)
            {
                //
            }

            //Check if Steam Version
            try
            {
                UserConfig.IsSteamVersion = Assembly.GetEntryAssembly().Location
                    .EndsWith("AOEOnline.exe", StringComparison.OrdinalIgnoreCase);
            }
            catch (Exception)
            {
                //
            }

            //Init WebSocketApi
            WebSocketApi = new WebSocketApi(UserConfig.ServerUri);

            //Start Gui
            //Application.Run(new MainForm());

            GC.KeepAlive(mutex);
        }
    }
}