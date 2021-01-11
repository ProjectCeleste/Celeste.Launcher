using System.IO;

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

            OverwriteFile(celesteLauncherExePath, aoeoOnlineExePath);

            var celesteLauncherExeConfigPath = Path.Combine(gameBasePath, $"{CelesteLauncherExecutableName}.config");
            var aoeoOnlineExeConfigPath = Path.Combine(gameBasePath, $"{AOEOnlineExecutableName}.config");
            OverwriteFile(celesteLauncherExeConfigPath, aoeoOnlineExeConfigPath);
        }

        private static void OverwriteFile(string source, string target)
        {
            if (File.Exists(target))
                File.Delete(target);

            File.Copy(source, target);
        }
    }
}