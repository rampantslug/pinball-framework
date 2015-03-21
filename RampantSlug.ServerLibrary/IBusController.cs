using RampantSlug.Common.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RampantSlug.ServerLibrary
{
    public interface IBusController
    {
        void Start();

        void SendEventMessage(string text);

        void SendSimpleMessage(string text);

        void SendSettingsMessage(List<Switch> switches);

        void Stop() ;
    }
}
