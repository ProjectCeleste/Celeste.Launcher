#region Using directives

using System;
using System.IO;
using Crc32;

#endregion

namespace Celeste_Public_Api.Helpers
{
    public class FileCheck
    {
        public static bool RunCrc32Check(string fileName, uint crc32)
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

        public static bool RunFileCheck(string filePath, long fileSize, uint fileCrc32)
        {
            return File.Exists(filePath) && new FileInfo(filePath).Length == fileSize &&
                   RunCrc32Check(filePath, fileCrc32);
        }

        public static bool RunFileQuickCheck(string filePath, long fileSize)
        {
            return File.Exists(filePath) && new FileInfo(filePath).Length == fileSize;
        }
    }
}