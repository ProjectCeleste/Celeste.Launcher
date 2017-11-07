#region Using directives

using System;
using System.IO;
using Crc32;

#endregion

namespace Celeste_Public_Api.Helpers
{
    public class Files
    {
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

        private static string ThreeNonZeroDigits(double value)
        {
            return value >= 100 ? value.ToString("0,0") : value.ToString(value >= 10 ? "0.0" : "0.00");
        }

        public static bool Crc32Check(string fileName, uint crc32)
        {
            if (!File.Exists(fileName))
                throw new FileNotFoundException($"File '{fileName}' not found!", fileName);

            bool retVal;
            try
            {
                var crc32Algo = new Crc32Algorithm();
                using (var fs = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    var result = crc32Algo.ComputeHash(fs);
                    Array.Reverse(result);

                    var realCrc32 = BitConverter.ToUInt32(result, 0);

                    retVal = realCrc32 == crc32;
                }
            }
            catch (Exception)
            {
                retVal = false;
            }
            return retVal;
        }

        public static bool FileCheck(string filePath, long fileSize, uint fileCrc32)
        {
            return File.Exists(filePath) && new FileInfo(filePath).Length == fileSize &&
                   Crc32Check(filePath, fileCrc32);
        }

        public static bool QuickFileCheck(string filePath, long fileSize)
        {
            return File.Exists(filePath) && new FileInfo(filePath).Length == fileSize;
        }
    }
}