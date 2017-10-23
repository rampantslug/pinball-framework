using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Devices;
using Hardware.Proc;

namespace ServerLibrary
{
    public class ModeEvent
    {
        public ushort Id { get; set; }

        public string Name { get; set; }

        public Switch AssociatedSwitch { get; set; }

        public EventType Trigger { get; set; }

        public Func<Switch, bool> MethodToExecute { get; set; }
    }
}
