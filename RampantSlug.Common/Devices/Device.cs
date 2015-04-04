using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RampantSlug.Common.Devices
{
    /// <summary>
    /// Base class for every type of Device found in a Pinball Machine
    /// </summary>
    public abstract class Device : IDevice
    {
        #region Properties

        [JsonIgnore]
        public ushort Number { get; set; }

        public string Address { get; set; }

        public string Name { get; set; }
       

        [JsonIgnore]
        public DateTime LastChangeTimeStamp { get; set; }

        // TODO: Should this be an enum of available colours based on what is set for the project
        public string WiringColors { get; set; }

        #endregion

        protected Device()
        {

        }




        public abstract void UpdateNumberFromAddress();

    }
}

