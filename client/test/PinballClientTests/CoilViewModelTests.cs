using BusinessObjects.Devices;
using Caliburn.Micro;
using NSubstitute;
using PinballClient.ClientComms;
using PinballClient.CommonViewModels.Devices;
using PinballClient.Events;
using Xunit;

namespace PinballClientTests
{
    public class CoilViewModelTests : DeviceViewModelTests
    {
        [Fact]
        public void ConfigureDevice_PublishesShowConfigEvent_WithViewModelAsParameter()
        {
            var coil = Substitute.For<Coil>();
            var busController = Substitute.For<IClientToServerCommsController>();
            var eventAggregator = Substitute.For<IEventAggregator>();

            var coilVM = new CoilViewModel(coil, busController, eventAggregator);

            coilVM.ConfigureDevice();

            eventAggregator.Received().PublishOnUIThread(Arg.Is<ShowCoilConfigEvent>(x => x.CoilVm == coilVM));
        }


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
