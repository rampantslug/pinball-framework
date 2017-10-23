using BusinessObjects.Devices;
using Caliburn.Micro;
using NSubstitute;
using PinballClient.ClientComms;
using PinballClient.CommonViewModels.Devices;
using Xunit;

namespace PinballClientTests
{
    public class ServoViewModelTests : DeviceViewModelTests
    {

        // Servo Test Values

        [Fact]
        public void PropertiesArePopulatedOnConstruction()
        {
            // Arrange
            var busController = Substitute.For<IClientToServerCommsController>();
            var eventAggregator = Substitute.For<IEventAggregator>();

            var servo = new Servo
            {
                Number = _testNumber,
                Address = _testAddress,
                DeviceId = _testDeviceId,
                Name = _testName,

                VirtualLocationX = _testVirtualLocationX,
                VirtualLocationY = _testVirtualLocationY,
                Angle = _testAngle,
                Scale = _testScale,

                InputWirePrimaryColor = _testInputWirePrimaryColor,
                InputWireSecondaryColor = _testInputWireSecondaryColor,
                OutputWirePrimaryColor = _testOutputWirePrimaryColor,
                OutputWireSecondaryColor = _testOutputWireSecondaryColor,
                RefinedType = _testRefinedType,
            };


            // Act
            var servoVM = new ServoViewModel(servo, busController, eventAggregator);

            // Assert
            PropertiesArePopulatedOnConstruction(servoVM);

            //Assert.Equal(_testIsSingleColor, ledVM.IsSingleColor);
            //Assert.Equal(_testSingleColor, ledVM.SingleColor);
            //Assert.Equal(_testShape, ledVM.Shape);
        }
    }
}
