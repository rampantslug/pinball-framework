using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RampantSlug.Common.Devices
{
    /// <summary>
    /// Base class for each type of Device found in a Pinball Machine.
    /// </summary>
    public abstract class Device : IDevice
    {
        /// <summary>
        /// Number used internally to reference the device.
        /// </summary>
        public ushort Number { get; set; }

        /// <summary>
        /// Address to locate the device on the physical hardware.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Name of the device.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Current state the device is in.
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Horizontal position of the device on the playfield. 
        /// 0 = left most side of the playfield image, 100 = right most .
        /// </summary>
        public double VirtualLocationX { get; set; }

        /// <summary>
        /// Vertical position of the device on the playfield. 
        /// 0 = top most side of the playfield image, 100 = bottom most .
        /// </summary>
        public double VirtualLocationY { get; set; }

        /// <summary>
        /// Time of the last state change of the device.
        /// </summary>
        public DateTime LastChangeTimeStamp { get; set; }

        /// <summary>
        /// More refined type definition of the device.
        /// Matches a suitable image for display in Client software.
        /// </summary>
        public string RefinedType { get; set; }

        protected Device()
        {

        }

        /// <summary>
        /// Is device currently active.
        /// </summary>
        public abstract bool IsActive { get; }

    }
}

