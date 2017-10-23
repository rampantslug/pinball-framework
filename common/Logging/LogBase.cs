using NLog;

namespace Logging
{
    public class LogBase
    {
        const string LoggerName = "Log";

        protected LogBase()
        {
            _log = LogManager.GetLogger(LoggerName);
        }

        public void Info(string source, string format, params object[] formatArguments)
        {
            WriteLog(LogLevel.Info, source, format, formatArguments);
        }

        public void Debug(string source, string format, params object[] formatArguments)
        {
            WriteLog(LogLevel.Debug, source, format, formatArguments);
        }

        public void Warn(string source, string format, params object[] formatArguments)
        {
            WriteLog(LogLevel.Warn, source, format, formatArguments);
        }

        public void Error(string source, string format, params object[] formatArguments)
        {
            WriteLog(LogLevel.Error, source, format, formatArguments);
        }

        protected void WriteLog(LogLevel level, string source, string format, params object[] formatArguments)
        {
            if (LogManager.IsLoggingEnabled())
            {
                var logEvent = new LogEventInfo(level, source, string.Format(format, formatArguments));

                _log.Log(logEvent);
            }
        }

        private readonly ILogger _log;
    }
}
