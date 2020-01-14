#region Using directives

using System;

#endregion Using directives

namespace Celeste_Launcher_Gui.Helpers
{
    public sealed class OsVersionInfo
    {
        private OsVersionInfo()
        {
        }

        public string Name { get; set; }

        public string FullName => "Microsoft " + Name + " [Version " + Major + "." + Minor + "." + Build + "]";

        public int Minor { get; set; }

        public int Major { get; set; }

        public int Build { get; set; }

        /// <summary>
        ///     Init OSVersionInfo object by current windows environment
        /// </summary>
        /// <returns></returns>
        public static OsVersionInfo GetOsVersionInfo()
        {
            var osVersionObj = Environment.OSVersion;

            return new OsVersionInfo
            {
                Name = GetOsName(osVersionObj),
                Major = osVersionObj.Version.Major,
                Minor = osVersionObj.Version.Minor,
                Build = osVersionObj.Version.Build
            };
        }

        /// <summary>
        ///     Get current windows name
        /// </summary>
        /// <param name="osInfo"></param>
        /// <returns></returns>
        private static string GetOsName(OperatingSystem osInfo)
        {
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (osInfo.Platform)
            {
                //for old windows kernel
                case PlatformID.Win32Windows:
                    return ForWin32Windows(osInfo);
                //fow NT kernel
                case PlatformID.Win32NT:
                    return ForWin32Nt(osInfo);

                default:
                    throw new ArgumentOutOfRangeException(nameof(osInfo.Platform), osInfo.Platform, string.Empty);
            }
        }

        /// <summary>
        ///     for old windows kernel
        ///     this function is the child function for method GetOSName
        /// </summary>
        /// <param name="osInfo"></param>
        /// <returns></returns>
        private static string ForWin32Windows(OperatingSystem osInfo)
        {
            string osVersion;

            //Code to determine specific version of Windows 95,
            //Windows 98, Windows 98 Second Edition, or Windows Me.
            switch (osInfo.Version.Minor)
            {
                case 0:
                    osVersion = "Windows 95";
                    break;

                case 10:
                    switch (osInfo.Version.Revision.ToString())
                    {
                        case "2222A":
                            osVersion = "Windows 98 Second Edition";
                            break;

                        default:
                            osVersion = "Windows 98";
                            break;
                    }

                    break;

                case 90:
                    osVersion = "Windows Me";
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(osInfo.Version.Minor), osInfo.Version.Minor,
                        string.Empty);
            }

            return osVersion;
        }

        /// <summary>
        ///     fow NT kernel
        ///     this function is the child function for method GetOSName
        /// </summary>
        /// <param name="osInfo"></param>
        /// <returns></returns>
        private static string ForWin32Nt(OperatingSystem osInfo)
        {
            string osVersion;

            //Code to determine specific version of Windows NT 3.51,
            //Windows NT 4.0, Windows 2000, or Windows XP.
            switch (osInfo.Version.Major)
            {
                case 3:
                    osVersion = "Windows NT 3.51";
                    break;

                case 4:
                    osVersion = "Windows NT 4.0";
                    break;

                case 5:
                    switch (osInfo.Version.Minor)
                    {
                        case 0:
                            osVersion = "Windows 2000";
                            break;

                        case 1:
                            osVersion = "Windows XP";
                            break;

                        case 2:
                            osVersion = "Windows 2003";
                            break;

                        default:
                            throw new ArgumentOutOfRangeException(nameof(osInfo.Version.Minor), osInfo.Version.Minor,
                                string.Empty);
                    }

                    break;

                case 6:
                    switch (osInfo.Version.Minor)
                    {
                        case 0:
                            osVersion = "Windows Vista";
                            break;

                        case 1:
                            osVersion = "Windows 7";
                            break;

                        case 2:
                            osVersion = "Windows 8";
                            break;

                        case 3:
                            osVersion = "Windows 8.1";
                            break;

                        default:
                            throw new ArgumentOutOfRangeException(nameof(osInfo.Version.Minor), osInfo.Version.Minor,
                                string.Empty);
                    }

                    break;

                case 10:
                    osVersion = "Windows 10";
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(osInfo.Version.Major), osInfo.Version.Major,
                        string.Empty);
            }

            return osVersion;
        }
    }
}