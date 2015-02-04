using Caliburn.Micro;
using RampantSlug.ServerLibrary;
namespace RampantSlug.PinballServerDemo
{
    public class ShellViewModel : Caliburn.Micro.PropertyChangedBase, IShell 
    {
        private IBusController _busController;

        public string TextToTransmit { get; set; }


        public void TransmitMessage() 
        {
            _busController.SendText(TextToTransmit);
        }

        public ShellViewModel() 
        {
            _busController = IoC.Get<IBusController>();
            _busController.Start();
        }

        public void Exit() { _busController.Stop(); }

    }
}