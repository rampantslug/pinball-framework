using System;
using Logging;

namespace PinballClient.Events
{
    /// <summary>
    /// Event containing data to be logged in log UI
    /// </summary>
    public class LogEvent
    {    
        /// <summary>
        /// Timestamp for the log entry.
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Event Type for the log entry.
        /// </summary>
        public LogEventType EventType { get; set; }

        /// <summary>
        /// Type of the the object initiating this log entry.
        /// </summary>
        public OriginatorType OriginatorType { get; set; }

        /// <summary>
        /// Name of the object initiating this log entry.
        /// </summary>
        public string OriginatorName { get; set; }

        /// <summary>
        /// Status of the object initiating this log entry.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Additional information.
        /// </summary>
        public string Information { get; set; }
    }

}
