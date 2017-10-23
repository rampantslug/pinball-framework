using System;
using Configuration;

namespace MessageContracts
{
    /// <summary>
    /// Message containing configuration data of a machine.
    /// </summary>
    public interface IConfigMessage
    {
        DateTime Timestamp { get; }

        IMachineConfiguration MachineConfiguration { get; }  
    }
}
