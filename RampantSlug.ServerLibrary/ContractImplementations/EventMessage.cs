using RampantSlug.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RampantSlug.ServerLibrary.ContractImplementations
{
    class EventMessage: IEventMessage
    {
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }


        // TODO: Possible replace these 3 with the object or interface of a device that the event applies to
        public string DeviceType { get; set; }
        public string Name { get; set; }

    }
}
