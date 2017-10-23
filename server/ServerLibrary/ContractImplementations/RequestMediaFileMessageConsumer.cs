using MassTransit;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows;
using MessageContracts;

namespace ServerLibrary.ContractImplementations
{
    class RequestMediaFileMessageConsumer : IConsumer<IRequestMediaFileMessage>
    {
        public async Task Consume(ConsumeContext<IRequestMediaFileMessage> message)
        {
            await Task.Run(() => PerformClientConnectionTransfer(message.Message.MediaFileLocation));           
        }

        private void PerformClientConnectionTransfer(string mediaFileLocation)
        {
            try
            {
                var sr = new StreamReader(mediaFileLocation);

                var tcpClient = new TcpClient();
                tcpClient.Connect(new IPEndPoint(IPAddress.Parse(GetIp()), 8085));

                var buffer = new byte[1500];
                long bytesSent = 0;

                while (bytesSent < sr.BaseStream.Length)
                {
                    int bytesRead = sr.BaseStream.Read(buffer, 0, 1500);
                    tcpClient.GetStream().Write(buffer, 0, bytesRead);
                    //Message(bytesRead + " bytes sent.");

                    bytesSent += bytesRead;
                }

                tcpClient.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public string GetIp()
        {
            var name = Dns.GetHostName();
            var entry = Dns.GetHostEntry(name);
            var addr = entry.AddressList;
            return addr[1].ToString().Split('.').Length == 4 ? addr[1].ToString() : addr[2].ToString();
        }
    }
}
