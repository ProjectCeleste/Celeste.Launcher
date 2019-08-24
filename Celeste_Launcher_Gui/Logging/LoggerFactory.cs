using Serilog;

namespace Celeste_Launcher_Gui.Logging
{
    public class LoggerFactory
    {
        private static ILogger _logger = BuildLogger();
        public static ILogger GetLogger()
        {
            return _logger;
        }

        private static ILogger BuildLogger()
        {
            return new LoggerConfiguration()
                  .ReadFrom.AppSettings()
                  .CreateLogger();
        }
    }
}
