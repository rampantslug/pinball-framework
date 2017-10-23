using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common;
using Logging;
using MassTransit;
using PinballClient.ClientComms;
using ServerLibrary;
using Xunit;
using Xunit.Abstractions;

namespace RampantSlug.Tests
{
    [Trait("Category", "Functional")]
    public class ClientServerFunctionalTests
    {
        private readonly ITestOutputHelper _output;

        public ClientServerFunctionalTests(ITestOutputHelper output)
        {
            this._output = output;
        }


        /// <summary>
        /// General Test for Connection Functionality of Client-Server comms.
        /// </summary>
        [Fact]
        public void Client_Connects_To_Server_On_LocalHost()
        {
            IServerBusController testServer = new ServerBusController(null, new BusInitializer());
            IClientToServerCommsController testClient = new ClientToServerCommsController(null, new BusInitializer());

            try
            {
 
                Task task = Task.Factory.StartNew(() => testServer.Start());
                Task task2 = Task.Factory.StartNew(() => testClient.Start());

                // Allow time for server and client to start up...
                var testDurationInSeconds = 5;
                Thread.Sleep(testDurationInSeconds*1000);

                task.ContinueWith(
                    delegate
                    {
                        testServer.SendLogMessage(LogEventType.Info, OriginatorType.System, "test", "test", "test");
                    });

                // Need to check Client received message...

                //eventAggregator.Received().PublishOnUIThread(Arg.Is<ShowCoilConfigEvent>(x => x.CoilVm == coilVM));

                Thread.Sleep(200);

                task.ContinueWith(
                   delegate
                   {
                       testClient.Stop();
                   });

                task2.ContinueWith(
                   delegate
                   {
                       testClient.Stop();
                   });

            }
            catch (Exception ex)
            {
                _output.WriteLine("Expected no exception, but got: " + ex.Message);
                Assert.Null(ex);
            }
        }


/*
        [Fact]
        public void PassingTest()
        {
            Assert.Equal(4, Add(2, 2));
        }

        [Fact]
        public void FailingTest()
        {
            Assert.Equal(5, Add(2, 2));
        }

        [Theory]
        [InlineData(3)]
        [InlineData(5)]
        [InlineData(6)]
        public void MyFirstTheory(int value)
        {
            _output.WriteLine("Test output from MyFirstTheory. Value tested {0}", value);
            Assert.True(IsOdd(value));          
        }

        bool IsOdd(int value)
        {
            return value % 2 == 1;
        }

        int Add(int x, int y)
        {
            return x + y;
        }*/
    }
}
