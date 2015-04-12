using Caliburn.Micro;
using RampantSlug.Common.Devices;
using RampantSlug.PinballClient.Events;

namespace RampantSlug.PinballClient.ClientDisplays.DeviceTree.DeviceTypes
{
    public class StepperMotorViewModel : DeviceItemViewModel
    {
        readonly StepperMotor _stepperMotor;

        public StepperMotorViewModel(StepperMotor stepperMotorDevice, DeviceTypeViewModel parentDeviceType)
            : base(parentDeviceType)
        {
            _stepperMotor = stepperMotorDevice;
            _device = stepperMotorDevice;
        }

        public string StepperMotorName
        {
            get { return _stepperMotor.Name; }
        }


        public void RotateRight()
        {
            var busController = IoC.Get<IClientBusController>();
            //busController.SendDeviceCommandMessage(_stepperMotor,"Right");
        }

        public void RotateLeft()
        {
            var busController = IoC.Get<IClientBusController>();
            //busController.SendDeviceCommandMessage(_stepperMotor, "Left");
        }
    }
}
