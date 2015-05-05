using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RampantSlug.Common.Devices
{
    /// <summary>
    /// Base class for each type of Device found in a Pinball Machine
    /// </summary>
    public abstract class Device : IDevice
    {

        public ushort Number { get; set; }

        public string Address { get; set; }

        public string Name { get; set; }

        public string State { get; set; }

        public double VirtualLocationX { get; set; }

        public double VirtualLocationY { get; set; }

        public DateTime LastChangeTimeStamp { get; set; }

        public string RefinedType { get; set; }

        // TODO: Should this be an enum of available colours based on what is set for the project
        public string WiringColors { get; set; }


        protected Device()
        {

        }

        public abstract bool IsActive { get; }

    }
}

