using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RampantSlug.Common;
using RampantSlug.Common.Devices;
using RampantSlug.ServerLibrary.ServerDisplays;

namespace RampantSlug.ServerLibrary
{
    public interface IGameController
    {
        IServerBusController ServerBusController { get; }
        //AttrCollection<ushort, string, Switch> Switches { get; set; }
        //IDisplayBackgroundVideo BackgroundVideo { get; }
        //IDisplayMainScore MainScore { get; }
        IDevices Devices { get; set; }
        IDisplay Display { get; set; }
        IGamePlay GamePlay { get; set; }

        void SaveConfigurationToFile();

        void ConnectToHardware();
        void DisconnectFromHardware();
        bool Configure(bool isRestart = false);
    }
}
