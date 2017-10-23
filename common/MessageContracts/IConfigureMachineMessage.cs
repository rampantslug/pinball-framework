using System;

namespace MessageContracts
{
    /// <summary>
    /// Message containing configuration data of a machine.
    /// </summary>
    public interface IConfigureMachineMessage
    {
        DateTime Timestamp { get; }

        bool UseHardware { get; set; }
    }
}
