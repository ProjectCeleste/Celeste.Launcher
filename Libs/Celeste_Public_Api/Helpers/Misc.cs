#region Using directives

using System;

#endregion

namespace Celeste_Public_Api.Helpers
{
    public class Misc
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
    }
}