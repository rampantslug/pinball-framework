using System.ComponentModel.Composition;
using Caliburn.Micro;
using Logging;
using Common;

namespace Hardware.Arduino
{
    [Export(typeof(IArduinoController))]
    public class ArduinoController : IArduinoController
    {
        #region Fields

        private IEventAggregator _eventAggregator;
        private IRsLogger _logger;
        private IDevices _devices;

        #endregion

        [ImportingConstructor]
        public ArduinoController(IEventAggregator eventAggregator, IRsLogger logger, IDevices devices)
        {
            _eventAggregator = eventAggregator;
            _logger = logger;
            _devices = devices;
        }

        public void Close()
        {
            
        }

        public bool Setup()
        {
            return true;
        }

        public void Start()
        {
            
        }
    }
}
