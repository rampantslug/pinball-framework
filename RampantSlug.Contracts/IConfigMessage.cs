using RampantSlug.Common.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RampantSlug.Common;

namespace RampantSlug.Contracts
{
    public interface IConfigMessage
    { 
        Configuration MachineConfiguration { get; }  
    }
}
