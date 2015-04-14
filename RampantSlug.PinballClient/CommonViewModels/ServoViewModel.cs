using RampantSlug.Common.Devices;

namespace RampantSlug.PinballClient.CommonViewModels
{
    public class ServoViewModel : DeviceViewModel
    {
        readonly Servo _servo;

        public ServoViewModel(Servo servoDevice)
        {
            _servo = servoDevice;
            _device = servoDevice;
        }

        public string ServoName
        {
            get { return _servo.Name; }
        }
    }
}
