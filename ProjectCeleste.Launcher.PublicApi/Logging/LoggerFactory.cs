#region Using directives

using Serilog;

#endregion Using directives

namespace ProjectCeleste.Launcher.PublicApi.Logging
{
    public static class LoggerFactory
    {
        private static readonly ILogger Logger = BuildLogger();

        public static ILogger GetLogger()
        {
            return Logger;
        }

        private static ILogger BuildLogger()
        {
            return new LoggerConfiguration()
                .ReadFrom.AppSettings()
                .CreateLogger();
        }
    }
}