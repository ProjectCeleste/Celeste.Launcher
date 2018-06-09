#region Using directives

using System;
using System.IO;
using System.Text;
using Crc32;

#endregion

namespace Celeste_Public_Api.Helpers
{
    public class Crc32Utils
    {
        public static uint GetCrc32File(string fileName)
        {
            if (!File.Exists(fileName))
                throw new FileNotFoundException($"File '{fileName}' not found!", fileName);

            uint retVal;

            using (var fs = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (var crc32Algo = new Crc32Algorithm())
                {
                    var result = crc32Algo.ComputeHash(fs);
                    Array.Reverse(result);

                    retVal = BitConverter.ToUInt32(result, 0);
                }
            }

            return retVal;
        }

        public static uint GetCrc32FromString(string str)
        {
            if (string.IsNullOrEmpty(str))
                throw new ArgumentException(@"IsNullOrEmpty!", nameof(str));

            return GetCrc32FromBytes(Encoding.Unicode.GetBytes(str));
        }

        public static uint GetCrc32FromBytes(byte[] bytes)
        {
            if (bytes == null || bytes.Length <= 0)
                throw new ArgumentException(@"Is Null Or Empty!", nameof(bytes));

            uint retVal;
            using (var crc32Algo = new Crc32Algorithm())
            {
                var result = crc32Algo.ComputeHash(bytes);
                Array.Reverse(result);
                retVal = BitConverter.ToUInt32(result, 0);
            }
            return retVal;
        }

        public static bool RunCrc32FileCheck(string fileName, uint crc32)
        {
            if (!File.Exists(fileName))
                throw new FileNotFoundException($"File '{fileName}' not found!", fileName);

            bool retVal;

            try
            {
                var realCrc32 = GetCrc32File(fileName);

                retVal = realCrc32 == crc32;
            }
            catch (Exception)
            {
                retVal = false;
            }

            return retVal;
        }
    }
}