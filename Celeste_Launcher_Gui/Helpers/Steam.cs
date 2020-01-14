#region Using directives

using System;
using System.IO;

#endregion

namespace Celeste_Launcher_Gui.Helpers
{
    public static class Steam
    {
        public static void ConvertToSteam(string gamePath)
        {
            string fileIn = Path.Combine(gamePath, "Celeste Launcher.exe");
            string fileOut = Path.Combine(gamePath, "AOEOnline.exe");
            Files.MoveFile(fileIn, fileOut);

            try
            {
                fileIn = Path.Combine(gamePath, "Celeste Launcher.exe.config");
                fileOut = Path.Combine(gamePath, "AOEOnline.exe.config");
                Files.MoveFile(fileIn, fileOut);
            }
            catch (Exception)
            {
                //Optional
            }
        }

        public static void ConvertBackFromSteam(string gamePath)
        {
            string fileOut = Path.Combine(gamePath, "Celeste Launcher.exe");
            string fileIn = Path.Combine(gamePath, "AOEOnline.exe");
            Files.MoveFile(fileIn, fileOut);

            try
            {
                fileOut = Path.Combine(gamePath, "Celeste Launcher.exe.config");
                fileIn = Path.Combine(gamePath, "AOEOnline.exe.config");
                Files.MoveFile(fileIn, fileOut);
            }
            catch (Exception)
            {
                //Optional
            }
        }
    }
}