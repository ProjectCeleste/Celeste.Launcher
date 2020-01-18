#region Using directives

using System;
using System.IO;
using Celeste_Public_Api.Helpers;

#endregion

namespace Celeste_Launcher_Gui.Helpers
{
    public static class Steam
    {
        private const string AOEOnlineExecutableName = "AOEOnline.exe";
        private const string CelesteLauncherExecutableName = "Celeste Launcher.exe";

        public static void ConvertToSteam(string gameBasePath)
        {
            var fileIn = Path.Combine(gameBasePath, CelesteLauncherExecutableName);
            var fileOut = Path.Combine(gameBasePath, AOEOnlineExecutableName);
            Misc.MoveFile(fileIn, fileOut);

            try
            {
                fileIn = Path.Combine(gameBasePath, $"{CelesteLauncherExecutableName}.config");
                fileOut = Path.Combine(gameBasePath, $"{AOEOnlineExecutableName}.config");
                Misc.MoveFile(fileIn, fileOut);
            }
            catch (Exception)
            {
                //Optional
            }
        }

        public static void ConvertBackFromSteam(string gameBasePath)
        {
            var fileOut = Path.Combine(gameBasePath, CelesteLauncherExecutableName);
            var fileIn = Path.Combine(gameBasePath, AOEOnlineExecutableName);
            Misc.MoveFile(fileIn, fileOut);

            try
            {
                fileOut = Path.Combine(gameBasePath, $"{CelesteLauncherExecutableName}.config");
                fileIn = Path.Combine(gameBasePath, $"{AOEOnlineExecutableName}.config");
                Misc.MoveFile(fileIn, fileOut);
            }
            catch (Exception)
            {
                //Optional
            }
        }
    }
}