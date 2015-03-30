using RampantSlug.Common.Devices;

namespace RampantSlug.PinballClient.ClientDisplays.DeviceTree.DeviceTypes
{
    public class ServoViewModel : DeviceItemViewModel
    {
        readonly Servo _servo;

        public ServoViewModel(Servo servoDevice, DeviceTypeViewModel parentDeviceType)
            : base(parentDeviceType)
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
