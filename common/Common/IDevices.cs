using System.Collections.Generic;
using BusinessObjects.Devices;

namespace Common
{
    public interface IDevices
    {
        AttrCollection<ushort, string, Switch> Switches { get; set; }

        Dictionary<ushort, ushort> SwitchIdAddressLookup { get; set; } 
           
        AttrCollection<ushort, string, Coil> Coils { get; set; }

        AttrCollection<ushort, string, StepperMotor> StepperMotors { get; set; }

        AttrCollection<ushort, string, Servo> Servos { get; set; }

        AttrCollection<ushort, string, Led> Leds { get; set; }


        
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
