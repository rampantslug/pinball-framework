using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using BusinessObjects;
using BusinessObjects.Devices;
using Hardware.Proc;
using ServerLibrary;
using ServerLibrary.Modes;


namespace PinballServerDemo.Modes
{
    public class AdvancedAttract: Attract, IMode
    {
        /*
          Ideas for Advanced Attract. 
          Check out the HBO site to see how the data is presented. 
          Consider borders and other ornamental stuff that HBO uses
          
         Play background video of scenic stuff
          Place slightly opaque-transparent rectangle to block off part
          'Fullscreen' images go in that section
          Split screen into 4 qudrants
          Other images can go into any quad or half a screen with padding around
          
          Include background information - Check books and making of site
          
          House descriptions - Title, Text stay constant in their own quads but relevant images rotate in other quad
                    - Sync with backbox 'smoke' lighting    
          
          Character descriptions - Actor images, text about character. Light playfield character art and path on map?
          
          Listen for flipper button presses and then display stats.
          
         

        */


        public RequiredMedia IntroVideo { get; set; }


        [ImportingConstructor]
        public AdvancedAttract(IGameController game)
            : base(game)
        {
            // Clear base Required Devices and Mode Events if we dont need them...
            RequiredDevices.Clear();
            ModeEvents.Clear();
            RequiredMedia.Clear();

            // Initialise Required Devices
            RequiredDevices.Add(new RequiredDevice
            {
                Id = 1,
                Name = "Tree Servo",
                TypeOfDevice = typeof(Servo)
            });

            // Initialise Mode Events
            ModeEvents.Add(new ModeEvent
            {
                Id = 1,
                Name = "Start Button Pressed",
                Trigger = EventType.SwitchClosedDebounced,
                MethodToExecute = StartButtonActive
            });

            // Initialise Required Media
            IntroVideo = new RequiredMedia()
            {
                Name = "GoT Season 5 Main Text",
                TypeOfMedia = MediaType.Video
            };

            RequiredMedia.Add(IntroVideo);

        }

        

        public override string Title
        {
            get { return "Advanced Attract"; }
        }

        public override void ModeStarted()
        {
            // Mode started. Display the main game text.
            //Game.Display.OverrideDisplay.Content = new AdvancedAttractShellViewModel(this);
        }

        public override void mode_stopped()
        {
            Game.Display.OverrideDisplay.Content = null;
        }
    }
}
