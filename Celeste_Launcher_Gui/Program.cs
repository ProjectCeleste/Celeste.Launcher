#region Using directives

using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Celeste_Launcher_Gui.Forms;
using Celeste_Launcher_Gui.Helpers;
using Celeste_Launcher_Gui.xLiveBridgeServer;
using Celeste_User.Remote;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Config;

//using Mono.Nat;

#endregion

namespace Celeste_Launcher_Gui
{
    internal static class Program
    {
#if DEBUG
        public static string WebSocketUri = "ws://127.0.0.1:4508/";
#else
        public static string WebSocketUri = "ws://66.70.180.188:4508/";
#endif

        public static WebSocketClient WebSocketClient = new WebSocketClient(WebSocketUri);
        public static RemoteUser RemoteUser;
        public static readonly Server Server = new Server();
        public static UserConfig UserConfig = new UserConfig();
        public static string UserConfigFilePath = $"{AppDomain.CurrentDomain.BaseDirectory}CelesteConfig.xml";

        public static readonly ServerConfig ServerConfig = new ServerConfig
        {
            Name = "",
            Port = 4510,
            Mode = SocketMode.Udp,
            MaxConnectionNumber = 10
        };


        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                //
                var pname = Process.GetProcessesByName("spartan");
                if (pname.Length > 0)
                {
                    SkinHelper.ShowMessage(@"Game already runing! Close it first.");
                    return;
                }
            }
            catch (Exception)
            {
                //
            }

            try
            {
                //Only one instance
                if (AlreadyRunning())
                    return;
            }
            catch (Exception)
            {
                //
            }

            //Load UserConfig
            if (File.Exists(UserConfigFilePath))
                UserConfig = UserConfig.Load(UserConfigFilePath);

            //Start Gui
            Application.Run(new MainForm());
        }

        private static bool AlreadyRunning()
        {
            var processes = Process.GetProcesses();
            var currentProc = Process.GetCurrentProcess();

            foreach (var process in processes)
                try
                {
                    if (process.Modules[0].FileName == Assembly.GetExecutingAssembly().Location
                        && currentProc.Id != process.Id)
                        return true;
                }
                catch (Exception)
                {
                    // ignored
                }

            return false;
        }
    }
}