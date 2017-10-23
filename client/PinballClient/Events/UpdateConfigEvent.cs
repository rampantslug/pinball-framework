using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Configuration;

namespace PinballClient.Events
{
    /// <summary>
    /// Event to notify of a change in the server machine configuration
    /// </summary>
    public class UpdateConfigEvent
    {
        public IMachineConfiguration MachineConfiguration { get; set; }
    }
}
