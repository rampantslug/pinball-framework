using NLog;

namespace Logging
{
    class FileLogger: LogBase, IRsLogger
    {
        public void LogMessage(LogEventType eventType, OriginatorType originator, string originatorName, string status,
            string information)
        {
            WriteLog(LogLevel.Info, originatorName, status + " | " + information );
        }
    }
}
