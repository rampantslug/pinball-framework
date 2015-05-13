using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RampantSlug.Common.Logging;

namespace RampantSlug.Contracts
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
