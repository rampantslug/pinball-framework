using Common.Commands;

namespace PinballClient.ClientComms
{
    /// <summary>
    /// 
    /// </summary>
    public interface IClientToServerCommsController : IClientCommsController
    {
        /// <summary>
        /// Start the Client Bus Controller with a connection to a server on a specific IP.
        /// </summary>
        /// <param name="serverIpAddress">IP address of server. Defaults to local machine.</param>
        void Start(string serverIpAddress = "127.0.0.1");

        /// <summary>
        /// Stop the Client Bus Controller.
        /// </summary>
        void Stop();

        void RestartServer();
    }
}
