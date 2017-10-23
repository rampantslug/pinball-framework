using System;
using Logging;

namespace MessageContracts
{
    public interface ILogMessage
    {
        DateTime Timestamp { get; set; }

        LogEventType EventType { get; set; }

        OriginatorType OriginatorType { get; set; }

        string OriginatorName { get; set; }

        string Status { get; set; }

        string Information { get; set; }
    }
}
