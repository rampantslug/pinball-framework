using Caliburn.Micro;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Devices;
using MessageContracts;

namespace PinballClient.ContractImplementations
{
    /// <summary>
    /// 
    /// </summary>
    class ConfigureSwitchMessage: IConfigureSwitchMessage
    {        
        public DateTime Timestamp { get; set; }

        public Switch Device { get; set; }

        public bool RemoveDevice { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    class ConfigureCoilMessage : IConfigureCoilMessage
    {
        public DateTime Timestamp { get; set; }

        public Coil Device { get; set; }

        public bool RemoveDevice { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    class ConfigureStepperMotorMessage : IConfigureStepperMotorMessage
    {
        public DateTime Timestamp { get; set; }

        public StepperMotor Device { get; set; }

        public bool RemoveDevice { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    class ConfigureServoMessage : IConfigureServoMessage
    {
        public DateTime Timestamp { get; set; }

        public Servo Device { get; set; }

        public bool RemoveDevice { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    class ConfigureLedMessage : IConfigureLedMessage
    {
        public DateTime Timestamp { get; set; }

        public Led Device { get; set; }

        public bool RemoveDevice { get; set; }
    }
}
