﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Devices;
using MessageContracts;

namespace ServerLibrary.ContractImplementations
{
    class UpdateSwitchMessage : IUpdateSwitchMessage
    {
        public DateTime Timestamp { get; set; }

        public Switch Device { get; set; }
    }
    
    class UpdateCoilMessage: IUpdateCoilMessage
    {
        public DateTime Timestamp { get; set; }

        public Coil Device { get; set; }
    }

    class UpdateStepperMotorMessage : IUpdateStepperMotorMessage
    {
        public DateTime Timestamp { get; set; }

        public StepperMotor Device { get; set; }
    }

    class UpdateServoMessage : IUpdateServoMessage
    {
        public DateTime Timestamp { get; set; }

        public Servo Device { get; set; }
    }

    class UpdateLedMessage : IUpdateLedMessage
    {
        public DateTime Timestamp { get; set; }

        public Led Device { get; set; }
    }
}
