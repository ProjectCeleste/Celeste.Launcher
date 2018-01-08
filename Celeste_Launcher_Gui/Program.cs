#region Using directives

using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Celeste_AOEO_Controls.MsgBox;
using Celeste_Launcher_Gui.Forms;
using Celeste_Public_Api.GameScanner_Api;
using Celeste_Public_Api.WebSocket_Api;
using Celeste_Public_Api.WebSocket_Api.Models;

#endregion

namespace Celeste_Launcher_Gui
{
    internal static class Program
    {

#if DEBUG 
        public static string WebSocketUri = "ws://127.0.0.1:4512/";
#else
        public static string WebSocketUri = "ws://66.70.180.188:4512/";
#endif

        public static WebSocketApi WebSocketApi { get; } = new WebSocketApi(WebSocketUri);

        public static RemoteUser CurrentUser { get; set; }

        public static UserConfig UserConfig = new UserConfig();

        public static string UserConfigFilePath { get; } = $"{AppDomain.CurrentDomain.BaseDirectory}CelesteConfig.xml";

        private static readonly string AppName = $"CelesteFanProject_v{Assembly.GetEntryAssembly().GetName().Version}";

        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

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
                if (string.IsNullOrEmpty(UserConfig.GameFilesPath))
                    UserConfig.GameFilesPath = GameScannnerApi.GetGameFilesRootPath();
            }
            catch (Exception)
            {
                //
            }

            //Check if Steam Version
            try
            {
                UserConfig.IsSteamVersion = Assembly.GetEntryAssembly().Location.EndsWith("AOEOnline.exe", StringComparison.OrdinalIgnoreCase);
            }
            catch (Exception)
            {
                //
            }

            //Start Gui
            Application.Run(new MainForm());

            GC.KeepAlive(mutex);
        }
    }
}