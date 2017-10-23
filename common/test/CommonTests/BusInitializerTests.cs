using System;
using System.Threading;
using System.Threading.Tasks;
using Common;
using MassTransit;
using MessageContracts;
using ServerLibrary.ContractImplementations;
using Xunit;
using Xunit.Abstractions;

namespace CommonTests
{
    class TestConsumer : IConsumer<ILogMessage>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public async Task Consume(ConsumeContext<ILogMessage> message)
        {
            await Task.Run( ()=> Console.WriteLine(@"Test Log message received"));
        }
    }

    [Trait("TestTime", "Long")]
    public class BusInitializerTests
    {
        private string workingUserName = "pinball";
        private string workingUserPassword = "pinpass";
        private string workingQueueIpAddress = "127.0.0.1";
        private string workingQueueName = "TestServer";

        private readonly ITestOutputHelper _output;



        public BusInitializerTests(ITestOutputHelper output)
        {
            _output = output;
        }


        [Fact]
        public void CreateBus_Succeeds_WithValidData()
        {
            var busInit = new BusInitializer();
            var logMessage = new LogMessage();

            // If RabbitMq is installed correctly then this should work
            var bus = busInit.CreateBus(workingQueueIpAddress, workingQueueName, workingUserName, workingUserPassword, ep =>
            {
                ep.Consumer<TestConsumer>();
            });

            Assert.NotNull(bus);

            bus.Publish(logMessage);
            //bus.Publish<LogMessage>(logMessage, x => { x.SetDeliveryMode(MassTransit.DeliveryMode.InMemory); });

            Thread.Sleep(2000);

            // If all good then kill bus
            bus.Stop();

            _output.WriteLine("If this has passed with no exceptions then RabbitMq is setup correctly");
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("145.124.1.23")]
        public void CreateBus_HandlesException_OnBadQueueIpAddress(string inputData)
        {
            var busInit = new BusInitializer();
            var logMessage = new LogMessage();

            var bus = busInit.CreateBus(inputData, workingQueueName, workingUserName, workingUserPassword, ep =>
            {
                ep.Consumer<TestConsumer>();
            });

            Assert.NotNull(bus);

            bus.Publish(logMessage);
            //bus.Publish<LogMessage>(logMessage, x => { x.SetDeliveryMode(MassTransit.DeliveryMode.InMemory); });

            Thread.Sleep(2000);

            // If all good then kill bus
            bus.Stop();

        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("145.124.1.23")]
        public void CreateBus_HandlesException_OnBadQueueName(string inputData)
        {
            var busInit = new BusInitializer();
            var logMessage = new LogMessage();

            var bus = busInit.CreateBus(workingQueueIpAddress, inputData, workingUserName, workingUserPassword, ep =>
            {
                ep.Consumer<TestConsumer>();
            });

            bus.Publish(logMessage);
            //bus.Publish<LogMessage>(logMessage, x => { x.SetDeliveryMode(MassTransit.DeliveryMode.InMemory); });

            Thread.Sleep(2000);

            // If all good then kill bus
            bus.Stop();
        }

        [Fact]
        public void CreateBus_HandlesException_OnUserNotExistingInRabbitMq()
        {
            var busInit = new BusInitializer();
            var logMessage = new LogMessage();

            var bus = busInit.CreateBus(workingQueueIpAddress, workingQueueName, "SomeFalseUser", workingUserPassword, ep =>
            {
                ep.Consumer<TestConsumer>();
            });

            bus.Publish(logMessage);
            //bus.Publish<LogMessage>(logMessage, x => { x.SetDeliveryMode(MassTransit.DeliveryMode.InMemory); });

            Thread.Sleep(2000);

            // If all good then kill bus
            bus.Stop();
        }

        [Fact]
        public void CreateBus_HandlesException_OnIncorrectPasswordForValidUserInRabbitMq()
        {
            var busInit = new BusInitializer();
            var logMessage = new LogMessage();

            var bus = busInit.CreateBus(workingQueueIpAddress, workingQueueName, workingUserName, "SomeFalseUserPassword", ep =>
            {
                ep.Consumer<TestConsumer>();
            });

            bus.Publish(logMessage);
            //bus.Publish<LogMessage>(logMessage, x => { x.SetDeliveryMode(MassTransit.DeliveryMode.InMemory); });

            Thread.Sleep(2000);

            // If all good then kill bus
            bus.Stop();
        }

    }
}
