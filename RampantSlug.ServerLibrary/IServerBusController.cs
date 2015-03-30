using RampantSlug.Common.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RampantSlug.Common;

namespace RampantSlug.ServerLibrary
{
    public interface IServerBusController
    {
        void Start();

        void SendEventMessage(string text);

        void SendSimpleMessage(string text);

        void SendConfigurationMessage(Configuration configuration);

        void Stop() ;
    }
}
