﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Devices;
using Configuration;
using Logging;


namespace ServerLibrary
{
    public interface IServerBusController
    {
        void Start();

        void SendLogMessage(LogEventType eventType, OriginatorType originator, string originatorName, string status, string information);

        void SendConfigurationMessage(IMachineConfiguration configuration);

        void Stop() ;

        void SendUpdateDeviceMessage(Switch device);

        void SendUpdateDeviceMessage(Coil device);

        void SendUpdateDeviceMessage(StepperMotor device);

        void SendUpdateDeviceMessage(Servo device);

        void SendUpdateDeviceMessage(Led device);
    }
}
