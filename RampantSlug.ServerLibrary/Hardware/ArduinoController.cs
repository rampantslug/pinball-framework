using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace RampantSlug.ServerLibrary.Hardware
{
    class ArduinoController : IArduinoController
    {
        public ArduinoController()
        {
            GameController = IoC.Get<IGameController>();
        }

        public IGameController GameController { get; private set; }

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
