using System;
using BusinessObjects.Devices;
using Caliburn.Micro;
using Common;
using NSubstitute;
using PinballClient.ClientComms;
using PinballClient.CommonViewModels;
using Xunit;

namespace PinballClientTests
{
    public class DeviceViewModelTests
    {
        // Device Test Values
        protected ushort _testNumber = 3;
        protected string _testAddress = "testAddress1"; // Add test to handle incorrect addresses
        protected ushort _testDeviceId = 5;
        protected string _testName = "testName1";

        protected double _testVirtualLocationX = 50.0;
        protected double _testVirtualLocationY = 60.0;
        protected double _testAngle = 90.0;
        protected double _testScale = 1.5;

        protected string _testInputWirePrimaryColor = "Green";
        protected string _testInputWireSecondaryColor = "Blue";
        protected string _testOutputWirePrimaryColor = "Red";
        protected string _testOutputWireSecondaryColor = "Yellow";
        protected string _testRefinedType = "testRefinedType1";

        private DeviceViewModel _deviceViewModel;

        [Fact]
        public void PreviousStatesMaxSizeShouldBe10()
        {
            // Arrange
            var busController = Substitute.For<IClientToServerCommsController>();
            var eventAggregator = Substitute.For<IEventAggregator>();
            _deviceViewModel = new DeviceViewModel(busController, eventAggregator);
            var subDevice = Substitute.For<IDevice>();
            

            // Act
            for (int i = 0 ; i < 15; i++)
            {
                _deviceViewModel.UpdateDeviceInfo(subDevice, DateTime.Now);
            }

            // Assert
            Assert.Equal(10, _deviceViewModel.PreviousStates.Count);
        }

        protected void PropertiesArePopulatedOnConstruction(DeviceViewModel deviceVm)
        {
            Assert.Equal(_testNumber, deviceVm.Number);
            Assert.Equal(_testDeviceId, deviceVm.Device.DeviceId);
            Assert.Equal(_testName, deviceVm.Name);

            Assert.Equal(_testVirtualLocationX, deviceVm.VirtualLocationX);
            Assert.Equal(_testVirtualLocationY, deviceVm.VirtualLocationY);
            Assert.Equal(_testAngle, deviceVm.Angle);
            Assert.Equal(_testScale, deviceVm.Scale);

            Assert.Equal(_testInputWirePrimaryColor, ColorBrushesHelper.ConvertBrushToString(deviceVm.InputWirePrimaryBrush));
            Assert.Equal(_testInputWireSecondaryColor, ColorBrushesHelper.ConvertBrushToString(deviceVm.InputWireSecondaryBrush));
            Assert.Equal(_testOutputWirePrimaryColor, ColorBrushesHelper.ConvertBrushToString(deviceVm.OutputWirePrimaryBrush));
            Assert.Equal(_testOutputWireSecondaryColor, ColorBrushesHelper.ConvertBrushToString(deviceVm.OutputWireSecondaryBrush));
            Assert.Equal(_testRefinedType, deviceVm.RefinedType);
        }


    }
}
