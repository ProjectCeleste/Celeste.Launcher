#region Using directives

using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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
            Parallel.ForEach(new DirectoryInfo(path).GetFiles(pattern, SearchOption.AllDirectories), file =>
            {
                try
                {
                    File.Delete(file.FullName);
                }
                catch (Exception)
                {
                    //
                }
            });
        }

        public static void MoveFile(string originalFilePath, string destFilePath, bool doBackup = true)
        {
            if (!File.Exists(originalFilePath))
                throw new FileNotFoundException(string.Empty, originalFilePath);

            if (File.Exists(destFilePath))
                if (doBackup)
                {
                    if (File.Exists(destFilePath + ".old"))
                        File.Delete(destFilePath + ".old");

                    File.Move(destFilePath, destFilePath + ".old");
                }

            if (File.Exists(destFilePath))
                File.Delete(destFilePath);

            File.Move(originalFilePath, destFilePath);
        }

        public static void MoveFiles(string originalPath, string destPath, bool doBackup = true,
            string backupExt = ".old")
        {
            if (!Directory.Exists(destPath))
                Directory.CreateDirectory(destPath);

            Parallel.ForEach(Directory.GetFiles(originalPath), file =>
            {
                var name = Path.GetFileName(file);
                if (name == null)
                    return;

                var dest = Path.Combine(destPath, name);

                if (File.Exists(dest))
                    if (doBackup)
                    {
                        if (File.Exists(dest + backupExt))
                            File.Delete(dest + backupExt);

                        File.Move(dest, dest + backupExt);
                    }

                if (File.Exists(dest))
                    File.Delete(dest);

                File.Move(file, dest);
            });

            foreach (var folder in Directory.GetDirectories(originalPath))
            {
                var name = Path.GetFileName(folder);
                if (name == null)
                    return;
                var dest = Path.Combine(destPath, name);
                MoveFiles(folder, dest, doBackup, backupExt);
            }
        }


        public static string ThreeNonZeroDigits(double value)
        {
            return value >= 100 ? value.ToString("0,0") : value.ToString(value >= 10 ? "0.0" : "0.00");
        }

        public static bool IsValideEmailAdress(string emailAdress)
        {
            return !string.IsNullOrWhiteSpace(emailAdress) && Regex.IsMatch(emailAdress, MatchEmailPattern);
        }

        public static bool IsValideUserName(string userName)
        {
            return !string.IsNullOrWhiteSpace(userName) && Regex.IsMatch(userName, MatchUserNamePattern);
        }

        public static bool IsValidePassword(string password)
        {
            return !string.IsNullOrWhiteSpace(password) && !password.Contains("'") && !password.Contains("\"");
        }
    }
}