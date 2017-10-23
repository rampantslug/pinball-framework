using Caliburn.Micro;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessageContracts;

namespace PinballClient.ContractImplementations
{
    /// <summary>
    /// 
    /// </summary>
    public class ConfigureMachineMessage: IConfigureMachineMessage
    {        
        public DateTime Timestamp { get; set; }

        public bool UseHardware { get; set; }
    }

}
