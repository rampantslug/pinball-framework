using BusinessObjects.Devices;
using Caliburn.Micro;
using PinballClient.ClientComms;

namespace PinballClient.CommonViewModels.Devices
{
    public class DCMotorViewModel : DeviceViewModel
    {
        readonly DCMotor _dcMotor;

        public DCMotorViewModel(DCMotor dcMotorDevice, IClientToServerCommsController busController, IEventAggregator eventAggregator)
            : base(busController, eventAggregator)
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
