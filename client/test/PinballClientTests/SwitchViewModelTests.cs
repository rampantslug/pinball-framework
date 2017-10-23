using BusinessObjects.Devices;
using Caliburn.Micro;
using NSubstitute;
using PinballClient.ClientComms;
using PinballClient.CommonViewModels.Devices;
using Xunit;

namespace PinballClientTests
{
    class SwitchViewModelTests :DeviceViewModelTests
    {

        [Fact]
        public void PropertiesArePopulatedOnConstruction()
        {
            // Arrange
            var busController = Substitute.For<IClientToServerCommsController>();
            var eventAggregator = Substitute.For<IEventAggregator>();

            var sw = new Switch
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
            var switchVM = new SwitchViewModel(sw, busController, eventAggregator);

            // Assert
            PropertiesArePopulatedOnConstruction(switchVM);

            //Assert.Equal(_testIsSingleColor, ledVM.IsSingleColor);
            //Assert.Equal(_testSingleColor, ledVM.SingleColor);
            //Assert.Equal(_testShape, ledVM.Shape);
        }
    }
}
