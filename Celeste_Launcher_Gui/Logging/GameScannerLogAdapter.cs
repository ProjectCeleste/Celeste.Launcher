using Celeste_Public_Api.GameScanner_Api.Models;
using Serilog;
using System;

namespace Celeste_Launcher_Gui.Logging
{
    public class GameScannerLogAdapter : IProgress<ScanAndRepairProgress>
    {
        private ILogger _logger;
        private LogLevel _minLevel;

        public GameScannerLogAdapter(ILogger logger, LogLevel minLevel)
        {
            _logger = logger;
            _minLevel = minLevel;
        }

        public void Report(ScanAndRepairProgress value)
        {
            if (value.ProgressLog == null || value.ProgressLog.LogLevel >= _minLevel)
            {
                _logger.Information("Game scanner {@ProgressPercentage}% done, file {@CurrentIndex}/{@TotalFile}: {@LogMessage}",
                    value.ProgressPercentage, value.CurrentIndex, value.TotalFile, value.ProgressLog?.Message);
            }
        }
    }
}
