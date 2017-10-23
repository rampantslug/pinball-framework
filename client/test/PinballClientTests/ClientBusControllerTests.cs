using Common;
using MassTransit;
using NSubstitute;
using PinballClient.ClientComms;
using PinballClient.ContractImplementations;
using Xunit;

namespace PinballClientTests
{
    public class ClientBusControllerTests
    {

        [Theory]
        [InlineData(true)]
        [InlineDataAttribute(false)]
        public void SendConfigureMachineMessage_CallsPublish_WithInitialisedMessage(bool input)
        {
            // Arrange
            var busInitializer = Substitute.For<IBusInitializer>();
            var serviceBus = Substitute.For<IBusControl>();

            var clientBusController = new ClientToServerCommsController(serviceBus, busInitializer);          

            // Act
            clientBusController.SendConfigureMachineMessage(input);

            // Assert
            serviceBus.Received().Publish(Arg.Is<ConfigureMachineMessage>(x => x.Timestamp != null && x.UseHardware == input));
        }
    }
}
