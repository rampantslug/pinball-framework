namespace Logging
{
    public interface IRsLogger
    {

        void LogMessage(LogEventType eventType, OriginatorType originator, string originatorName, string status,
            string information);

        void Info(string source, string format, params object[] formatArguments);

        void Debug(string source, string format, params object[] formatArguments);

        void Warn(string source, string format, params object[] formatArguments);

        void Error(string source, string format, params object[] formatArguments);
    }
}