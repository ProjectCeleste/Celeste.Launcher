using System;

namespace Celeste_Launcher_Gui.Helpers
{
    public static class BytesSizeExtension
    {
        public const int Kb = 1024;

        public const int Mb = Kb * Kb;

        public const int Gb = Kb * Mb;

        public static string FormatToBytesSize(double value)
        {
            if (value < Kb)
                return $"{value} bytes";
            if (value < Mb)
                return $"{value / Kb:f2} KB";
            return value < Gb ? $"{value / Mb:f2} MB" : $"{value / Gb:f2} GB";
        }

        public static string FormatToBytesSizeThreeNonZeroDigits(double value)
        {
            string[] suffixes =
            {
                "bytes", "KB", "MB", "GB",
                "TB", "PB", "EB", "ZB", "YB"
            };

            for (int i = 0; i < suffixes.Length; i++)
            {
                if (value <= Math.Pow(Kb, i + 1))
                    return ThreeNonZeroDigits(value / Math.Pow(Kb, i)) + " " + suffixes[i];
            }

            return ThreeNonZeroDigits(value / Math.Pow(Kb, suffixes.Length - 1)) + " " +
                   suffixes[suffixes.Length - 1];
        }

        private static string ThreeNonZeroDigits(double value)
        {
            return value >= 100 ? value.ToString("0,0") : value.ToString(value >= 10 ? "0.0" : "0.00");
        }
    }
}