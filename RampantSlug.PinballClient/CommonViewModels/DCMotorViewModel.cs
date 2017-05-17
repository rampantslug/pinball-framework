using RampantSlug.Common.Devices;

namespace RampantSlug.PinballClient.CommonViewModels
{
    public class DCMotorViewModel : DeviceViewModel
    {
        readonly DCMotor _dcMotor;

        public DCMotorViewModel(DCMotor dcMotorDevice)
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
