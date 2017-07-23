#region Using directives

using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Celeste_Launcher_Gui.Forms;
using Celeste_Launcher_Gui.Helpers;
using Celeste_User.Remote;

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
        public static UserConfig UserConfig = new UserConfig();
        public static string UserConfigFilePath = $"{AppDomain.CurrentDomain.BaseDirectory}CelesteConfig.xml";

        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                //Only one instance
                if (AlreadyRunning())
                {
                    SkinHelper.ShowMessage(@"Launcher already runing!", "Celeste Fan Project", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }
            }
            catch (Exception)
            {
                //
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

            //Start Gui
            Application.Run(new MainForm());
        }

        private static bool AlreadyRunning()
        {
            var currentProc = Process.GetCurrentProcess();
            try
            {
                if (Process.GetProcesses().Any(key => key.Id != currentProc.Id && key.Modules.Count > 0 &&
                                                      key.Modules[0].FileName ==
                                                      Assembly.GetExecutingAssembly().Location))
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