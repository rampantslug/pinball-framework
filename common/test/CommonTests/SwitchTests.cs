using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Devices;
using Xunit;

namespace CommonTests
{
    public class SwitchTests
    {
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsActive_ReturnsCorrectStateAfterSettingState_ForNormallyOpenSwitch(bool desiredState)
        {
            // Arrange
            var testSwitch = new Switch
            {
                Type = SwitchType.NO
            };
            testSwitch.SetState(desiredState);           

            // Act
            var result = testSwitch.IsActive();

            // Assert
            Assert.Equal(desiredState, result);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsActive_ReturnsCorrectStateAfterSettingState_ForNormallyClosedSwitch(bool desiredState)
        {
            // Arrange
            var testSwitch = new Switch
            {
                Type = SwitchType.NC
            };
            testSwitch.SetState(!desiredState);

            // Act
            var result = testSwitch.IsActive();

            // Assert
            Assert.Equal(desiredState, result);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsInactive_ReturnsCorrectStateAfterSettingState_ForNormallyOpenSwitch(bool desiredState)
        {
            // Arrange
            var testSwitch = new Switch
            {
                Type = SwitchType.NO
            };
            testSwitch.SetState(!desiredState);

            // Act
            var result = testSwitch.IsInactive();

            // Assert
            Assert.Equal(desiredState, result);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsInactive_ReturnsCorrectStateAfterSettingState_ForNormallyClosedSwitch(bool desiredState)
        {
            // Arrange
            var testSwitch = new Switch
            {
                Type = SwitchType.NC
            };
            testSwitch.SetState(desiredState);

            // Act
            var result = testSwitch.IsInactive();

            // Assert
            Assert.Equal(desiredState, result);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsOpen_ReturnsCorrectStateAfterSettingState_ForNormallyOpenSwitch(bool desiredState)
        {
            // Arrange
            var testSwitch = new Switch
            {
                Type = SwitchType.NO
            };
            testSwitch.SetState(!desiredState);

            // Act
            var result = testSwitch.IsOpen();

            // Assert
            Assert.Equal(desiredState, result);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsOpen_ReturnsCorrectStateAfterSettingState_ForNormallyClosedSwitch(bool desiredState)
        {
            // Arrange
            var testSwitch = new Switch
            {
                Type = SwitchType.NC
            };
            testSwitch.SetState(!desiredState);

            // Act
            var result = testSwitch.IsOpen();

            // Assert
            Assert.Equal(desiredState, result);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsClosed_ReturnsCorrectStateAfterSettingState_ForNormallyOpenSwitch(bool desiredState)
        {
            // Arrange
            var testSwitch = new Switch
            {
                Type = SwitchType.NO
            };
            testSwitch.SetState(desiredState);

            // Act
            var result = testSwitch.IsClosed();

            // Assert
            Assert.Equal(desiredState, result);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsClosed_ReturnsCorrectStateAfterSettingState_ForNormallyClosedSwitch(bool desiredState)
        {
            // Arrange
            var testSwitch = new Switch
            {
                Type = SwitchType.NC
            };
            testSwitch.SetState(desiredState);

            // Act
            var result = testSwitch.IsClosed();

            // Assert
            Assert.Equal(desiredState, result);
        }

    }
}
