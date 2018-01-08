#region Using directives

using System;
using System.IO;
using Celeste_Public_Api.Helpers;

#endregion

namespace Celeste_Launcher_Gui.Helpers
{
    public static class Steam
    {
        public static void ConvertToSteam(string gamePath)
        {
            var fileIn = Path.Combine(gamePath, "Celeste_Launcher_Gui.exe");
            var fileOut = Path.Combine(gamePath, "AOEOnline.exe");
            Misc.MoveFile(fileIn, fileOut);

            try
            {
                fileIn = Path.Combine(gamePath, "Celeste_Launcher_Gui.exe.config");
                fileOut = Path.Combine(gamePath, "AOEOnline.exe.config");
                Misc.MoveFile(fileIn, fileOut);
            }
            catch (Exception)
            {
               //Optional
            }
        }

        public static void ConvertBackFromSteam(string gamePath)
        {
            var fileOut = Path.Combine(gamePath, "Celeste_Launcher_Gui.exe");
            var fileIn = Path.Combine(gamePath, "AOEOnline.exe");
            Misc.MoveFile(fileIn, fileOut);

            try
            {
                fileOut = Path.Combine(gamePath, "Celeste_Launcher_Gui.exe.config");
                fileIn = Path.Combine(gamePath, "AOEOnline.exe.config");
                Misc.MoveFile(fileIn, fileOut);
            }
            catch (Exception)
            {
                //Optional
            }
        }
    }
}