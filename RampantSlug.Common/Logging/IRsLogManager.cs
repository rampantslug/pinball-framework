namespace RampantSlug.Common.Logging
{
    public interface IRsLogManager
    {
        void LogMessage(LogEventType eventType, OriginatorType originator, string originatorName, string status,
            string information);
    }
}