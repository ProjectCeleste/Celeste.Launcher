using System.IO;
using Celeste_Public_Api.Helpers;

namespace Celeste_Launcher_Gui.Helpers
{
    public static class Steam
    {
        private const string AOEOnlineExecutableName = "AOEOnline.exe";
        private const string CelesteLauncherExecutableName = "Celeste Launcher.exe";

        public static void ConvertToSteam(string gameBasePath)
        {
            var celesteLauncherExePath = Path.Combine(gameBasePath, CelesteLauncherExecutableName);
            var aoeoOnlineExePath = Path.Combine(gameBasePath, AOEOnlineExecutableName);

            Misc.MoveFile(celesteLauncherExePath, aoeoOnlineExePath);

            var celesteLauncherExeConfigPath = Path.Combine(gameBasePath, $"{CelesteLauncherExecutableName}.config");
            var aoeoOnlineExeConfigPath = Path.Combine(gameBasePath, $"{AOEOnlineExecutableName}.config");
            Misc.MoveFile(celesteLauncherExeConfigPath, aoeoOnlineExeConfigPath);
        }
    }
}