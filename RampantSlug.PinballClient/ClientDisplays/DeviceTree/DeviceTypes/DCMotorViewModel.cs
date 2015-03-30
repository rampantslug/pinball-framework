using RampantSlug.Common.Devices;

namespace RampantSlug.PinballClient.ClientDisplays.DeviceTree.DeviceTypes
{
    public class DCMotorViewModel : DeviceItemViewModel
    {
        readonly DCMotor _dcMotor;

        public DCMotorViewModel(DCMotor dcMotorDevice, DeviceTypeViewModel parentDeviceType)
            : base(parentDeviceType)
        {
            _dcMotor = dcMotorDevice;
            _device = dcMotorDevice;
        }

        public string DCMotorName
        {
            get { return _dcMotor.Name; }
        }
    }
}
