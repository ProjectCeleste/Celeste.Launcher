#region Using directives

using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Win32.SafeHandles;

#endregion

namespace Celeste_Public_Api.Helpers
{
    public static class Misc
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

        public static bool IsValidEmailAdress(string emailAdress)
        {
            return !string.IsNullOrWhiteSpace(emailAdress) && Regex.IsMatch(emailAdress, MatchEmailPattern);
        }

        public static bool IsValidUserName(string userName)
        {
            return !string.IsNullOrWhiteSpace(userName) && Regex.IsMatch(userName, MatchUserNamePattern);
        }

        public static bool IsValidPassword(string password)
        {
            return !string.IsNullOrWhiteSpace(password) && !password.Contains("'") && !password.Contains("\"");
        }

        #region SymLink

        public enum SymLinkFlag
        {
            File = 0,
            Directory = 1
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool CreateSymbolicLink(string lpSymlinkFileName, string lpTargetFileName,
            SymLinkFlag dwFlags);

        public static bool IsSymLink(string path,
            SymLinkFlag dwFlags)
        {
            switch (dwFlags)
            {
                case SymLinkFlag.File:
                    return new FileInfo(path).Attributes.HasFlag(FileAttributes.ReparsePoint);
                case SymLinkFlag.Directory:
                    return new DirectoryInfo(path).Attributes.HasFlag(FileAttributes.ReparsePoint);
                default:
                    throw new ArgumentOutOfRangeException(nameof(dwFlags), dwFlags, null);
            }
        }

        [DllImport("kernel32.dll", EntryPoint = "CreateFileW", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern SafeFileHandle CreateFile(string lpFileName, int dwDesiredAccess, int dwShareMode,
            IntPtr securityAttributes, int dwCreationDisposition, int dwFlagsAndAttributes, IntPtr hTemplateFile);

        [DllImport("kernel32.dll", EntryPoint = "GetFinalPathNameByHandleW", CharSet = CharSet.Unicode,
            SetLastError = true)]
        private static extern int GetFinalPathNameByHandle([In] IntPtr hFile, [Out] StringBuilder lpszFilePath,
            [In] int cchFilePath, [In] int dwFlags);

        // ReSharper disable InconsistentNaming
        private const int CREATION_DISPOSITION_OPEN_EXISTING = 3;

        private const int FILE_FLAG_BACKUP_SEMANTICS = 0x02000000;
        // ReSharper restore InconsistentNaming

        public static string GetRealPath(string path)
        {
            if (!Directory.Exists(path) && !File.Exists(path))
                throw new IOException("Path not found");

            var symlink = new DirectoryInfo(path); // No matter if it's a file or folder
            var directoryHandle = CreateFile(symlink.FullName, 0, 2, IntPtr.Zero, CREATION_DISPOSITION_OPEN_EXISTING,
                FILE_FLAG_BACKUP_SEMANTICS, IntPtr.Zero); //Handle file / folder

            if (directoryHandle.IsInvalid)
                throw new Win32Exception(Marshal.GetLastWin32Error());

            var result = new StringBuilder(512);
            var mResult = GetFinalPathNameByHandle(directoryHandle.DangerousGetHandle(), result, result.Capacity, 0);

            if (mResult < 0)
                throw new Win32Exception(Marshal.GetLastWin32Error());

            if (result.Length >= 4 && result[0] == '\\' && result[1] == '\\' && result[2] == '?' && result[3] == '\\')
                return result.ToString().Substring(4); // "\\?\" remove

            return result.ToString();
        }

        #endregion
    }
}