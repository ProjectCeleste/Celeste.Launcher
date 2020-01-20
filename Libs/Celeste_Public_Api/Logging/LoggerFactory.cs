using Serilog;

namespace Celeste_Public_Api.Logging
{
    public class LoggerFactory
    {
        private static readonly ILogger _logger = BuildLogger();
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
