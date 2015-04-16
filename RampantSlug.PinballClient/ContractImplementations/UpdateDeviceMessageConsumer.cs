using Caliburn.Micro;
using MassTransit;
using RampantSlug.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RampantSlug.PinballClient.Events;

namespace RampantSlug.PinballClient.ContractImplementations
{
    class UpdateDeviceMessageConsumer: 
        Consumes<IUpdateSwitchMessage>.Context,
        Consumes<IUpdateCoilMessage>.Context,
        Consumes<IUpdateStepperMotorMessage>.Context,
        Consumes<IUpdateServoMessage>.Context
    {
        public void Consume(IConsumeContext<IUpdateSwitchMessage> message)
        {
            /*var shell = IoC.Get<IShell>();
            
            // Update View Model with change
            var switchDevice = message.Message.Device;          
            foreach (var swVM in shell.Switches.Where(swVM => swVM.Number == switchDevice.Number))
            {
                swVM.UpdateDeviceInfo(switchDevice, message.Message.Timestamp);             
            }

            // Now create log message...
            // TODO: This needs to be cleaned up to have better information            
            var eventAggregator = IoC.Get<IEventAggregator>();
            eventAggregator.PublishOnUIThread(new DisplayMessageResults
            {
                Timestamp = message.Message.Timestamp,
                EventType = "System",
                Name = "Event Message",
                State = "OK",
                Information = "Switch Event for: " + switchDevice.Name
            });*/

            var eventAggregator = IoC.Get<IEventAggregator>();
            eventAggregator.PublishOnUIThread(new UpdateSwitch()
            {
                Timestamp = message.Message.Timestamp,
                Device = message.Message.Device
               
            });

        }

        public void Consume(IConsumeContext<IUpdateCoilMessage> message)
        {
            var shell = IoC.Get<IShell>();

            // Update View Model with change
            var coilDevice = message.Message.Device;
            foreach (var coilVM in shell.Coils.Where(coilVM => coilVM.Number == coilDevice.Number))
            {
                coilVM.Device = coilDevice;
                coilVM.Refresh();
            }

            // Now create log message...
            // TODO: This needs to be cleaned up to have better information            
            var eventAggregator = IoC.Get<IEventAggregator>();
            eventAggregator.PublishOnUIThread(new DisplayMessageResults
            {
                Timestamp = message.Message.Timestamp,
                EventType = "System",
                Name = "Event Message",
                State = "OK",
                Information = "Coil Event for: " + coilDevice.Name
            });
        }

        public void Consume(IConsumeContext<IUpdateStepperMotorMessage> message)
        {
            var shell = IoC.Get<IShell>();

            // Update View Model with change
            var stepperMotorDevice = message.Message.Device;
            foreach (var stepperMotorVM in shell.StepperMotors.Where(stepperMotorVM => stepperMotorVM.Number == stepperMotorDevice.Number))
            {
                stepperMotorVM.Device = stepperMotorDevice;
                stepperMotorVM.Refresh();
            }

            // Now create log message...
            // TODO: This needs to be cleaned up to have better information            
            var eventAggregator = IoC.Get<IEventAggregator>();
            eventAggregator.PublishOnUIThread(new DisplayMessageResults
            {
                Timestamp = message.Message.Timestamp,
                EventType = "System",
                Name = "Event Message",
                State = "OK",
                Information = "Stepper Motor Event for: " + stepperMotorDevice.Name
            });
        }

        public void Consume(IConsumeContext<IUpdateServoMessage> message)
        {
            var shell = IoC.Get<IShell>();

            // Update View Model with change
            var servoDevice = message.Message.Device;
            foreach (var servoVM in shell.Servos.Where(servoVM => servoVM.Number == servoDevice.Number))
            {
                servoVM.Device = servoDevice;
                servoVM.Refresh();
            }

            // Now create log message...
            // TODO: This needs to be cleaned up to have better information            
            var eventAggregator = IoC.Get<IEventAggregator>();
            eventAggregator.PublishOnUIThread(new DisplayMessageResults
            {
                Timestamp = message.Message.Timestamp,
                EventType = "System",
                Name = "Event Message",
                State = "OK",
                Information = "Servo Event for: " + servoDevice.Name
            });
        }


    }
}
