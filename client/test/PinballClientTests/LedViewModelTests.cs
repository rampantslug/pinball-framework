using System.Windows.Media;
using BusinessObjects.Devices;
using BusinessObjects.Shapes;
using Caliburn.Micro;
using NSubstitute;
using PinballClient.ClientComms;
using PinballClient.CommonViewModels.Devices;
using Xunit;

namespace PinballClientTests
{
    public class LedViewModelTests : DeviceViewModelTests
    {

        // Led Test Values
        private bool _testIsSingleColor = false;
        private Color _testSingleColor = Colors.HotPink;
        private LedShape _testShape = LedShape.Rectangle;

        [Fact]
        public void PropertiesArePopulatedOnConstruction()
        {
            // Arrange
            var busController = Substitute.For<IClientToServerCommsController>();
            var eventAggregator = Substitute.For<IEventAggregator>();

            var led = new Led
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

                IsSingleColor = _testIsSingleColor,
                SingleColor = _testSingleColor,
                Shape = _testShape,
            };


            // Act
            var ledVM = new LedViewModel(led, busController, eventAggregator);

            // Assert
            PropertiesArePopulatedOnConstruction(ledVM);

            Assert.Equal(_testIsSingleColor, ledVM.IsSingleColor);
            Assert.Equal(_testSingleColor, ledVM.SingleColor);
            Assert.Equal(_testShape, ledVM.Shape);
        }
    }
}
