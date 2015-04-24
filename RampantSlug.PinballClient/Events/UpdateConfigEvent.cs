using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RampantSlug.Common;

namespace RampantSlug.PinballClient.Events
{
    public class UpdateConfigEvent
    {
        public Configuration MachineConfiguration { get; set; }
    }
}
