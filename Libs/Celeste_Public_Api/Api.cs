#region Using directives

using System;
using System.IO;
using Celeste_Public_Api.GameFileInfo;

#endregion

namespace Celeste_Public_Api
{
    public static class Api
    {
        public static GameFiles GameFiles = GameFiles.GetGameFiles();

        public static string FindGameFileDirectory()
        {
            {
                //Custom Path 1
                var path = $"{AppDomain.CurrentDomain.BaseDirectory}Spartan.exe";
                if (File.Exists(path))
                {
                    goto spartanFound;
                }

                //Custom Path 2
                path = $"{AppDomain.CurrentDomain.BaseDirectory}\\AOEO\\Spartan.exe";
                if (File.Exists(path))
                {
                    goto spartanFound;
                }

                //Custom Path 3
                if (Environment.Is64BitOperatingSystem)
                {
                    path = $"{Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86)}\\Age Of Empires Online\\Spartan.exe";
                    if (File.Exists(path))
                    {
                        goto spartanFound;
                    }
                }

                //Custom Path 4
                path = $"{Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)}\\Age Of Empires Online\\Spartan.exe";
                if (File.Exists(path))
                {
                    goto spartanFound;
                }

                //Steam 1
                if (Environment.Is64BitOperatingSystem)
                {
                    path = $"{Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86)}\\Steam\\steamapps\\common\\Age Of Empires Online\\Spartan.exe";
                    if (File.Exists(path))
                    {
                        goto spartanFound;
                    }
                }

                //Steam 2
                path = $"{Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)}\\Steam\\steamapps\\common\\Age Of Empires Online\\Spartan.exe";
                if (File.Exists(path))
                {
                    goto spartanFound;
                }


                //Original Game Path
                path = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\Local\\Microsoft\\Age of Empires Online\\Spartan.exe";
                if (File.Exists(path))
                {
                    goto spartanFound;
                }

                path = $"{AppDomain.CurrentDomain.BaseDirectory}\\AOEO\\Spartan.exe";

                spartanFound:
                return Path.GetDirectoryName(path);
            }
        }
    }
}