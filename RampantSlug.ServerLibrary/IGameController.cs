using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RampantSlug.Common;
using RampantSlug.Common.Devices;

namespace RampantSlug.ServerLibrary
{
    public interface IGameController
    {
        IServerBusController ServerBusController { get; }
        AttrCollection<ushort, string, Switch> Switches { get; set; }
        void SaveConfigurationToFile();

        void ConnectToHardware();
        void DisconnectFromHardware();
        bool Configure();
    }
}
