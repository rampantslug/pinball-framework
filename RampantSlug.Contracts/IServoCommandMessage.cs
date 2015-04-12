using RampantSlug.Common.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RampantSlug.Contracts
{
    public interface IServoCommandMessage
    {        
        DateTime Timestamp { get; }

        Servo Device { get; }

        string TempControllerMessage { get; }
    }
}
