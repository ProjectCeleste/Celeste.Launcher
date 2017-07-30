#region Using directives

using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Celeste_Launcher_Gui.Forms;
using Celeste_Launcher_Gui.Helpers;

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

        public static WebSocketClient WebSocketClient = new WebSocketClient(WebSocketUri);
        public static UserConfig UserConfig = new UserConfig();
        public static string UserConfigFilePath = $"{AppDomain.CurrentDomain.BaseDirectory}CelesteConfig.xml";

        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Only one instance
            if (AlreadyRunning())
            {
                SkinHelper.ShowMessage(@"Launcher already runing!", "Celeste Fan Project", MessageBoxButtons.OK,
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

            //Start Gui
            Application.Run(new MainForm());
        }

        private static bool AlreadyRunning()
        {
            try
            {
                var currentProc = Process.GetCurrentProcess();

                if (Process.GetProcesses().Any(key => key.Id != currentProc.Id &&
                                                      string.Equals(key.Modules[0].FileName,
                                                          Assembly.GetExecutingAssembly().Location,
                                                          StringComparison.CurrentCultureIgnoreCase)))
                    return true;
            }
            catch
            {
                // ignored
            }

            return false;
        }
    }
}