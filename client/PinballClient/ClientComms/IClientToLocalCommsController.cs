using Common;
using Configuration;

namespace PinballClient.ClientComms
{
    public interface IClientToLocalCommsController : IClientCommsController
    {
        IRsConfiguration Configuration { get; set; }

        // TODO: Create interface and Export! Use ImportingConstructor to get the above 2?
        MachineState MachineState { get; set; }
    }
}
