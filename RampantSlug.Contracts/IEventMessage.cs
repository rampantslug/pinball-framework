using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RampantSlug.Contracts
{
    public interface IEventMessage
    {
        
        DateTime Timestamp { get; }

        // TODO: Possible replace these 3 with the object or interface of a device that the event applies to
        string DeviceType { get; }
        string Name { get; }        
        string Message { get; }

    }
}
