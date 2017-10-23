using System;
using System.Threading;
using Hardware.Arduino;
using Xunit;

namespace ServerLibraryTests.Hardware
{
    [Trait("RequiresHardware", "Arduino")]
    public class ArduinoDeviceTests
    {

        #region Servo Tests

        [Fact]
        public void RotateServo_ThrowsException_OnInvalidControllerName()
        {
            var arduinoDevice = new ArduinoDevice();

            var ex = Assert.Throws<ArgumentException>(()=> arduinoDevice.RotateServo("ERROR", 1, 1));

            Assert.Equal("Invalid Controller Name", ex.Message);
            arduinoDevice.CloseArduinoConnection();
        }

        [Theory]
        [InlineData("ASS", 22)]
        [InlineData("AMS", 5)]
        public void RotateServo_ThrowsException_OnInvalidServoId(string controller, uint servoId)
        {
            var arduinoDevice = new ArduinoDevice();

            var ex = Assert.Throws<ArgumentException>(() => arduinoDevice.RotateServo(controller, servoId, 1));

            Assert.Equal("Invalid Servo Id", ex.Message);
            arduinoDevice.CloseArduinoConnection();
        }

        [Fact]
        public void RotateServo_ThrowsException_OnInvalidAngle()
        {
            var arduinoDevice = new ArduinoDevice();

            var ex = Assert.Throws<ArgumentException>(() => arduinoDevice.RotateServo("ASS", 1, 210));

            Assert.Equal("Invalid Angle", ex.Message);
            arduinoDevice.CloseArduinoConnection();
        }


        #endregion






        [Fact]
        public void SendRequestRight_Causes_ArduinoToRotate_Left()
        {
            //var arduinoDevice = new ArduinoDevice();
            
           // arduinoDevice.SendRequestToArduinoBoard("Left");

            //arduinoDevice.CloseArduinoConnection();

            var pulselength = Map(45, 0, 180, 150, 550);
        }

        private long Map(long x, long in_min, long in_max, long out_min, long out_max)
        {
            return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
        }

        [Fact]
        public void TestAdvancedMessageHandle()
        {
            var arduinoDevice = new ArduinoDevice();

            //arduinoDevice.SendRequestToArduinoBoard("7-120#");

            arduinoDevice.CloseArduinoConnection();
        }

        [Fact]
        public void SendMultiplePositionRequest90()
        {
            var arduinoDevice = new ArduinoDevice();

            //arduinoDevice.SendRequestToArduinoBoard("ASS90");

            arduinoDevice.CloseArduinoConnection();
        }
        [Fact]
        public void SendMultiplePositionRequest45()
        {
            var arduinoDevice = new ArduinoDevice();

//arduinoDevice.SendRequestToArduinoBoard("ASS745");

            arduinoDevice.CloseArduinoConnection();
        }

        [Fact]
        public void SendMultiplePositionRequest0()
        {
            var arduinoDevice = new ArduinoDevice();

            //arduinoDevice.SendRequestToArduinoBoard("ASS0#");

            arduinoDevice.CloseArduinoConnection();
        }

        [Fact]
        public void SendMultiplePositionRequest180()
        {
            var arduinoDevice = new ArduinoDevice();

            //arduinoDevice.SendRequestToArduinoBoard("ASS180#");

            arduinoDevice.CloseArduinoConnection();
        }

        [Fact]
        public void SendMultiplePositionRequests()
        {
            var arduinoDevice = new ArduinoDevice();

            //Task.Run(() => arduinoDevice.SendRequestToArduinoBoard("ASS-7-1#"));       
            Thread.Sleep(1000);
            //Task.Run(() => arduinoDevice.SendRequestToArduinoBoard("ASS-7-180#"));
            Thread.Sleep(1000);
            //Task.Run(() => arduinoDevice.SendRequestToArduinoBoard("ASS-7-30#"));
            //Thread.Sleep(1000);
            //Task.Run(() => arduinoDevice.SendRequestToArduinoBoard("ASS-7-150#"));
            Thread.Sleep(1000);
           // Task.Run(() => arduinoDevice.SendRequestToArduinoBoard("ASS-7-60#"));
            Thread.Sleep(1000);
          //  Task.Run(() => arduinoDevice.SendRequestToArduinoBoard("ASS-7-120#"));
            Thread.Sleep(1000);
          //  Task.Run(() => arduinoDevice.SendRequestToArduinoBoard("ASS-7-90#"));
            Thread.Sleep(1000);

            arduinoDevice.CloseArduinoConnection();
        }
    }
}
