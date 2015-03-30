using RampantSlug.Common.Devices;

namespace RampantSlug.PinballClient.ClientDisplays.DeviceTree.DeviceTypes
{
    public class CoilViewModel : DeviceItemViewModel
    {
        readonly Coil _coil;

        public CoilViewModel(Coil coilDevice, DeviceTypeViewModel parentDeviceType)
            : base(parentDeviceType)
        {
            _coil = coilDevice;
            _device = coilDevice;
        }

        public string CoilName
        {
            get { return _coil.Name; }
        }
    }
}
