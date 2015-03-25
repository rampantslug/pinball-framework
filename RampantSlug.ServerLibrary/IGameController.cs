using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RampantSlug.ServerLibrary
{
    public interface IGameController
    {
        IServerBusController ServerBusController { get; }
        void SaveConfigurationToFile();

        void ConnectToHardware();
        void CloseHardware();
    }
}
