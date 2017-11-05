namespace Celeste_Public_Api.Logger
{
    public enum LogLevel
    {
        Info = 0,
        Warn = 1,
        Error = 2,
        Fatal = 3,
        Debug = 4,
        All = 999
    }

    public class ExLog
    {
        public ExLog(LogLevel logLevel, string message)
        {
            LogLevel = logLevel;
            Message = message;
        }

        public LogLevel LogLevel { get; }

        public string Message { get; }
    }
}