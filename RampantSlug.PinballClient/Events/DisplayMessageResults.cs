using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RampantSlug.PinballClient.ContractImplementations
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
