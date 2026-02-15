using System;
using System.IO;
using System.Linq;

namespace Celeste_Public_Api.Logging
{
    public static class LogHelper
    {
        public static string GetLogFilePath()
        {
            try
            {
                var configPath = System.Configuration.ConfigurationManager.AppSettings["serilog:write-to:File.path"];
                if (string.IsNullOrWhiteSpace(configPath))
                    configPath = @"Logs\launcherlog.log";

                if (!Path.IsPathRooted(configPath))
                    configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, configPath);

                return FindMostRecentLogFile(configPath);
            }
            catch
            {
                var logsDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
                if (Directory.Exists(logsDir))
                {
                    var logFiles = Directory.GetFiles(logsDir, "launcherlog*.log")
                        .OrderByDescending(f => File.GetLastWriteTime(f))
                        .ToArray();

                    if (logFiles.Length > 0)
                        return logFiles[0];
                }

                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs", "launcherlog.log");
            }
        }

        public static string FindMostRecentLogFile(string baseLogPath)
        {
            try
            {
                if (!Path.IsPathRooted(baseLogPath))
                    baseLogPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, baseLogPath);

                var directory = Path.GetDirectoryName(baseLogPath);
                var fileNameWithoutExt = Path.GetFileNameWithoutExtension(baseLogPath);
                var extension = Path.GetExtension(baseLogPath);

                if (Directory.Exists(directory))
                {
                    var logFiles = Directory.GetFiles(directory, fileNameWithoutExt + "*" + extension)
                        .OrderByDescending(f => File.GetLastWriteTime(f))
                        .ToArray();

                    if (logFiles.Length > 0)
                        return logFiles[0];
                }
                return baseLogPath;
            }
            catch
            {
                return baseLogPath;
            }
        }
    }
}
