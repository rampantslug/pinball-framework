using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RampantSlug.Common;
using RampantSlug.Common.Devices;
using RampantSlug.ServerLibrary.Hardware.DeviceControl;

namespace RampantSlug.ServerLibrary
{
    public interface IDevices
    {
        AttrCollection<ushort, string, Switch> Switches { get; set; }

        AttrCollection<ushort, string, CoilControl> Coils { get; set; }

        AttrCollection<ushort, string, StepperMotorControl> StepperMotors { get; set; }

        AttrCollection<ushort, string, ServoControl> Servos { get; set; }

        AttrCollection<ushort, string, LedControl> Leds { get; set; }


        
        void LoadSwitches(List<Switch> switches);
        void LoadCoils(List<Coil> coils);
        void LoadStepperMotors(List<StepperMotor> stepperMotors);
        void LoadServos(List<Servo> servos);
        void LoadLeds(List<Led> leds);
        void UpdateSwitch(ushort number, Switch sw);
        void UpdateCoil(ushort number, Coil coil);
        void UpdateStepperMotor(ushort number, StepperMotor stepperMotor);
        void UpdateServo(ushort number, Servo servo);
        void UpdateLed(ushort number, Led led);
        bool AddSwitch(Switch updatedSwitch);

        void RemoveSwitch(ushort number);
        bool AddCoil(Coil updatedCoil);
        void RemoveCoil(ushort number);
        bool AddStepperMotor(StepperMotor updatedStepperMotor);
        void RemoveStepperMotor(ushort number);
        bool AddServo(Servo updatedServo);
        void RemoveServo(ushort number);
        bool AddLed(Led updatedLed);
        void RemoveLed(ushort number);

        List<Switch> AllSwitches();
        List<Coil> AllCoils();
        List<StepperMotor> AllStepperMotors();
        List<Servo> AllServos();
        List<Led> AllLeds();
    }
}
