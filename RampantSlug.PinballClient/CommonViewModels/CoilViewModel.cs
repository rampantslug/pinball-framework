using RampantSlug.Common.Devices;

namespace RampantSlug.PinballClient.CommonViewModels
{
    public class CoilViewModel : DeviceViewModel
    {
        readonly Coil _coil;

        public CoilViewModel(Coil coilDevice)
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
