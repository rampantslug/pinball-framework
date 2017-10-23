using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Primitives;
using BusinessObjects.Devices;
using Logging;
using Common;
using Common.Commands;
using Configuration;

namespace PinballClient.ClientComms
{
    [Export(typeof(IClientToLocalCommsController))]
    public class ClientToLocalCommsController: IClientToLocalCommsController
    {
        public IClientCommsController CurrentController { get; set; }

        public IRsConfiguration Configuration { get; set; }

        public MachineState MachineState { get; set; }

        // TODO: May need to links these to return back to the client to update its state


        public void SendConfigureMachineMessage(bool useHardware)
        {
            if (null != Configuration)
            {
                Configuration.MachineConfiguration.UseHardware = useHardware;
                Configuration.WriteMachineToFile();
            }
            RsLog.LogMessage(LogEventType.Info, OriginatorType.System, "Client", "", "Sent configure machine message to Local Client");
        }

        public void SendConfigureDeviceMessage(Switch device, bool removeDevice = false)
        {
            if (null != Configuration)
            {
                // Update local state
                MachineState.UpdateDevice(device, removeDevice);
                
                // Write to config
                Configuration.MachineConfiguration.Switches = MachineState.Devices.AllSwitches();
                Configuration.WriteMachineToFile();
            }

            RsLog.LogMessage(LogEventType.Info, OriginatorType.System, "Client", "", "Sent configure device message to Local Client: " + device.Name);
        }

        public void SendConfigureDeviceMessage(Coil device, bool removeDevice = false)
        {
            if (null != Configuration)
            {
                // Update local state
                MachineState.UpdateDevice(device, removeDevice);

                // Write to config
                Configuration.MachineConfiguration.Coils = MachineState.Devices.AllCoils();
                Configuration.WriteMachineToFile();
            }
            RsLog.LogMessage(LogEventType.Info, OriginatorType.System, "Client", "", "Sent configure device message to Local Client: " + device.Name);
        }

        public void SendConfigureDeviceMessage(StepperMotor device, bool removeDevice = false)
        {
            if (null != Configuration)
            {
                // Update local state
                MachineState.UpdateDevice(device, removeDevice);

                // Write to config
                Configuration.MachineConfiguration.StepperMotors = MachineState.Devices.AllStepperMotors();
                Configuration.WriteMachineToFile();
            }
            RsLog.LogMessage(LogEventType.Info, OriginatorType.System, "Client", "", "Sent configure device message to Local Client: " + device.Name);
        }

        public void SendConfigureDeviceMessage(Servo device, bool removeDevice = false)
        {
            if (null != Configuration)
            {
                // Update local state
                MachineState.UpdateDevice(device, removeDevice);

                // Write to config
                Configuration.MachineConfiguration.Servos = MachineState.Devices.AllServos();
                Configuration.WriteMachineToFile();
            }
            RsLog.LogMessage(LogEventType.Info, OriginatorType.System, "Client", "", "Sent configure device message to Local Client: " + device.Name);
        }

        public void SendConfigureDeviceMessage(Led device, bool removeDevice = false)
        {
            if (null != Configuration)
            {
                // Update local state
                MachineState.UpdateDevice(device, removeDevice);

                // Write to config
                Configuration.MachineConfiguration.Leds = MachineState.Devices.AllLeds();
                Configuration.WriteMachineToFile();
            }
            RsLog.LogMessage(LogEventType.Info, OriginatorType.System, "Client", "", "Sent configure device message to Local Client: " + device.Name);
        }

        public void SendCommandDeviceMessage(Switch device, SwitchCommand command)
        {
            RsLog.LogMessage(LogEventType.Info, OriginatorType.System, "Client", "", "Sent command device message to Local Client: " + device.Name + " Command: " + command);
        }

        public void SendCommandDeviceMessage(Coil device, CoilCommand command)
        {
            RsLog.LogMessage(LogEventType.Info, OriginatorType.System, "Client", "", "Sent command device message to Local Client: " + device.Name + " Command: " + command);
        }

        public void SendCommandDeviceMessage(StepperMotor device, StepperMotorCommand command)
        {
            RsLog.LogMessage(LogEventType.Info, OriginatorType.System, "Client", "", "Sent command device message to Local Client: " + device.Name + " Command: " + command);
        }

        public void SendCommandDeviceMessage(Servo device, ServoCommand command)
        {
            RsLog.LogMessage(LogEventType.Info, OriginatorType.System, "Client", "", "Sent command device message to Local Client: " + device.Name + " Command: " + command);
        }

        public void SendCommandDeviceMessage(Led device, LedCommand command)
        {
            RsLog.LogMessage(LogEventType.Info, OriginatorType.System, "Client", "", "Sent command device message to Local Client: " + device.Name + " Command: " + command);
        }

        public void RequestSettings()
        {
            RsLog.LogMessage(LogEventType.Info, OriginatorType.System, "Client", "", "Sent request settings message to Local Client");
        }

        public void RequestFile(string mediaFilename)
        {
            RsLog.LogMessage(LogEventType.Info, OriginatorType.System, "Client", "", "Sent request file message to Local Client");
        }

        
    }
}
