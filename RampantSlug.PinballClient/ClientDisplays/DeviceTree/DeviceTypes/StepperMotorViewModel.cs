using RampantSlug.Common.Devices;

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
    }
}
