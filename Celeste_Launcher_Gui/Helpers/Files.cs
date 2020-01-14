#region Using directives

using Microsoft.Win32.SafeHandles;
using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

#endregion Using directives

namespace Celeste_Launcher_Gui.Helpers
{
    public static class Files
    {
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

            if (File.Exists(destFilePath) && doBackup)
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
                string name = Path.GetFileName(file);
                if (name == null)
                    return;

                string dest = Path.Combine(destPath, name);

                if (File.Exists(dest) && doBackup)
                {
                    if (File.Exists(dest + backupExt))
                        File.Delete(dest + backupExt);

                    File.Move(dest, dest + backupExt);
                }

                if (File.Exists(dest))
                    File.Delete(dest);

                File.Move(file, dest);
            });

            foreach (string folder in Directory.GetDirectories(originalPath))
            {
                string name = Path.GetFileName(folder);
                if (name == null)
                    return;
                string dest = Path.Combine(destPath, name);
                MoveFiles(folder, dest, doBackup, backupExt);
            }
        }

        #region SymLink

        public enum SymLinkFlag
        {
            File = 0,
            Directory = 1
        }

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.I1)]
        private static extern bool CreateSymbolicLink_(string lpSymlinkFileName, string lpTargetFileName,
            SymLinkFlag dwFlags);

        public static bool CreateSymbolicLink(string lpSymlinkFileName, string lpTargetFileName,
            SymLinkFlag dwFlags)
        {
            return CreateSymbolicLink_(lpSymlinkFileName, lpTargetFileName, dwFlags);
        }

        public static bool IsSymLink(string path,
            SymLinkFlag dwFlags)
        {
            switch (dwFlags)
            {
                case SymLinkFlag.File:
                    return (new FileInfo(path).Attributes & FileAttributes.ReparsePoint) != 0;

                case SymLinkFlag.Directory:
                    return (new DirectoryInfo(path).Attributes & FileAttributes.ReparsePoint) != 0;

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

            DirectoryInfo symlink = new DirectoryInfo(path); // No matter if it's a file or folder
            SafeFileHandle directoryHandle = CreateFile(symlink.FullName, 0, 2, IntPtr.Zero, CREATION_DISPOSITION_OPEN_EXISTING,
                FILE_FLAG_BACKUP_SEMANTICS, IntPtr.Zero); //Handle file / folder

            if (directoryHandle.IsInvalid)
                throw new Win32Exception(Marshal.GetLastWin32Error());

            StringBuilder result = new StringBuilder(512);
            int mResult = GetFinalPathNameByHandle(directoryHandle.DangerousGetHandle(), result, result.Capacity, 0);

            if (mResult < 0)
                throw new Win32Exception(Marshal.GetLastWin32Error());

            if (result.Length >= 4 && result[0] == '\\' && result[1] == '\\' && result[2] == '?' && result[3] == '\\')
                return result.ToString().Substring(4); // "\\?\" remove

            return result.ToString();
        }

        #endregion SymLink
    }
}