using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using ServerLibrary.ServerDisplays;

namespace ServerLibrary
{
    public interface IGameController
    {
        IServerBusController ServerBusController { get; }
        //AttrCollection<ushort, string, Switch> Switches { get; set; }
        //IDisplayBackgroundVideo BackgroundVideo { get; }
        //IDisplayMainScore MainScore { get; }
        IDevices Devices { get; }
        IDisplay Display { get; set; }
        IGamePlay GamePlay { get; set; }

       // void SaveConfigurationToFile();

        void ConnectToHardware();
        void DisconnectFromHardware();
        bool Configure();
    }
}
