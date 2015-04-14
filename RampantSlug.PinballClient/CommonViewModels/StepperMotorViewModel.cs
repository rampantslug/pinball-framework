using Caliburn.Micro;
using RampantSlug.Common.Devices;

namespace RampantSlug.PinballClient.CommonViewModels
{
    public class StepperMotorViewModel : DeviceViewModel
    {
        readonly StepperMotor _stepperMotor;

        public StepperMotorViewModel(StepperMotor stepperMotorDevice)
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
