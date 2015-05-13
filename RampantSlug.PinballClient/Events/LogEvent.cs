using System;
using RampantSlug.Common.Logging;

namespace RampantSlug.PinballClient.Events
{
    public class LogEvent
    {       
        public DateTime Timestamp { get; set; }

        public LogEventType EventType { get; set; }

        public OriginatorType OriginatorType { get; set; }

        public string OriginatorName { get; set; }

        public string Status { get; set; }

        public string Information { get; set; }
    }

}
