using RampantSlug.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RampantSlug.ServerLibrary.ContractImplementations
{
    class SimpleMessage: ISimpleMessage
    {
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
