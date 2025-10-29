using System.IO;
using Celeste_Public_Api.Helpers;

namespace Celeste_Launcher_Gui.Helpers
{
    public static class Steam
    {
        private const string AOEOnlineExecutableName = "AOEOnline.exe";
        private const string CelesteLauncherExecutableName = "Celeste Launcher.exe";
        private const string CelesteLauncherExecutableNameCopy = "Celeste Launcher.exe.copy";

        public static void ConvertToSteam(string gameBasePath)
        {
            var celesteLauncherExePath = Path.Combine(gameBasePath, CelesteLauncherExecutableName);
            var celesteLauncherExeCopy = Path.Combine(gameBasePath, CelesteLauncherExecutableNameCopy);
            var aoeoOnlineExePath = Path.Combine(gameBasePath, AOEOnlineExecutableName);

            OverwriteFile(celesteLauncherExePath, celesteLauncherExeCopy);
            Misc.MoveFile(celesteLauncherExeCopy, aoeoOnlineExePath);

            var celesteLauncherExeConfigPath = Path.Combine(gameBasePath, $"{CelesteLauncherExecutableName}.config");
            var celesteLauncherExeConfigPathCopy = Path.Combine(gameBasePath, $"{CelesteLauncherExecutableNameCopy}.config");
            var aoeoOnlineExeConfigPath = Path.Combine(gameBasePath, $"{AOEOnlineExecutableName}.config");

            OverwriteFile(celesteLauncherExeConfigPath, celesteLauncherExeConfigPathCopy);
            Misc.MoveFile(celesteLauncherExeConfigPathCopy, aoeoOnlineExeConfigPath);
        }

        private static void OverwriteFile(string source, string target)
        {
            if (File.Exists(target))
                File.Delete(target);

            File.Copy(source, target);
        }
    }
}