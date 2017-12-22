#region Using directives

using System;
using System.IO;
using System.Text.RegularExpressions;

#endregion

namespace Celeste_Public_Api.Helpers
{
    public class Misc
    {
        private const string MatchEmailPattern =
            @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
            + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
            + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
            + @"([a-zA-Z0-9]+[\w-]+\.)+[a-zA-Z]{1}[a-zA-Z0-9-]{1,23})$";

        private const string MatchUserNamePattern =
            @"^[A-Za-z0-9]{3,15}$";

        public static string ToFileSize(double value)
        {
            string[] suffixes =
            {
                "bytes", "KB", "MB", "GB",
                "TB", "PB", "EB", "ZB", "YB"
            };

            for (var i = 0; i < suffixes.Length; i++)
                if (value <= Math.Pow(1024, i + 1))
                    return ThreeNonZeroDigits(value / Math.Pow(1024, i)) + " " + suffixes[i];

            return ThreeNonZeroDigits(value / Math.Pow(1024, suffixes.Length - 1)) + " " +
                   suffixes[suffixes.Length - 1];
        }

        public static void CleanUpFiles(string path, string pattern = "*")
        {
            var files = new DirectoryInfo(path).GetFiles(pattern, SearchOption.AllDirectories);

            foreach (var file in files)
                try
                {
                    File.Delete(file.FullName);
                }
                catch (Exception)
                {
                    //
                }
        }

        public static void MoveFiles(string originalPath, string destPath, bool doBackup = true)
        {
            if (!Directory.Exists(destPath))
                Directory.CreateDirectory(destPath);

            foreach (var file in Directory.GetFiles(originalPath))
            {
                var name = Path.GetFileName(file);
                var dest = Path.Combine(destPath, name);

                if (File.Exists(dest))
                    if (doBackup)
                        File.Move(dest, dest + ".old");
                    else
                        File.Delete(dest);

                File.Move(file, dest);
            }

            var folders = Directory.GetDirectories(originalPath);

            foreach (var folder in folders)
            {
                var name = Path.GetFileName(folder);
                if (name == null) continue;
                var dest = Path.Combine(destPath, name);
                MoveFiles(folder, dest, doBackup);
            }
        }

        public static string ThreeNonZeroDigits(double value)
        {
            return value >= 100 ? value.ToString("0,0") : value.ToString(value >= 10 ? "0.0" : "0.00");
        }

        public static bool IsValideEmailAdress(string emailAdress)
        {
            return !string.IsNullOrEmpty(emailAdress) && Regex.IsMatch(emailAdress, MatchEmailPattern);
        }

        public static bool IsValideUserName(string userName)
        {
            return !string.IsNullOrEmpty(userName) && Regex.IsMatch(userName, MatchUserNamePattern);
        }

        public static bool IsValidePassword(string password)
        {
            return !password.Contains("'") && !password.Contains("\"");
        }
    }
}