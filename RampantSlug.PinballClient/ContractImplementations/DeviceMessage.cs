using Caliburn.Micro;
using MassTransit;
using RampantSlug.Common.Devices;
using RampantSlug.Contracts;
using RampantSlug.PinballClient.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RampantSlug.PinballClient.ContractImplementations
{
    class DeviceMessage: IDeviceMessage
    {        
        public DateTime Timestamp { get; set; }

        public Switch Device { get; set; }

    }
}
