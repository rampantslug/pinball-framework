using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Devices;
using Hardware.Proc;

namespace ServerLibrary.Modes
{
    [Export(typeof(IMode))]
    public class Attract : ServerMode, IMode
    {
        public override string Title
        {
            get { return "Basic Attract"; }
        }

        [ImportingConstructor]
        public Attract(IGameController game)
            : base(game, 1)
        {
            ModeEvents.Add(new ModeEvent
            {
                Id = 1,
                Name = "Start Button Pressed",
                AssociatedSwitch = null,
                Trigger = EventType.SwitchClosedDebounced,
                MethodToExecute = StartButtonActive
            });

        }

        public override void ModeStarted()
        {
 

            // Blinky start button
            // Game.Lamps["startButton"].Schedule(0x00ff00ff, 0, false);
            Game.Display.MainScore.IsVisible = false;
            Game.Display.MainScore.HeaderText = "Server Demo";

            Game.Display.MainScore.ModeText = "Test";
        }

        public bool StartButtonActive(Switch sw)
        {
            //if (Game.trough.is_full())
            //{
                // Remove attract mode from queue
            Game.GamePlay.ActiveModes.Remove(this);

                // Initialize game
            Game.GamePlay.StartGame();

                // Add first player
            Game.GamePlay.AddPlayer();
            Game.GamePlay.StartBall();
            //}
            //else
            //{
                // Perform ball search
               // this.Game.set_status("Locating pinballs...", 5);
               // Game.Logger.Log("BALL SEARCH");
            //}
            return SWITCH_CONTINUE;
        }
    }
}
