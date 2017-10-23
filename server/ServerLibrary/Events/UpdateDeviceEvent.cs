﻿using System;
using BusinessObjects.Devices;
using Hardware.Proc;

namespace ServerLibrary.Events
{
    public class UpdateSwitchEvent
    {
        public Event SwitchEvent { get; set; }
        public Switch Device { get; set; }
    }

    public class UpdateCoilEvent
    {
        public Coil Device { get; set; }
    }

    public class UpdateStepperMotorEvent
    {
        public StepperMotor Device { get; set; }
    }

    public class UpdateServoEvent
    {
        public Servo Device { get; set; }
    }

    public class UpdateLedEvent
    {
        public Led Device { get; set; }
    }
}
