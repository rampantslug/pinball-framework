using System;

namespace RampantSlug.PinballClient.Events
{
    public class DisplayMessageResults
    {
        
        public DateTime Timestamp { get; set; }

        public string EventType { get; set; }

        public string Name { get; set; }

        public string State { get; set; }

        public string Information { get; set; }
    }
}
