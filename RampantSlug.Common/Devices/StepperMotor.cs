﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RampantSlug.Common.Devices
{
    /// <summary>
    /// Represents a stepper motor device in a pinball machine.
    /// </summary>
    public class StepperMotor : Motor, IDevice
    {
        public StepperMotor()
        {
            State = "Inactive";
        }
    }
}
