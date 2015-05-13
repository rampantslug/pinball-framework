using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RampantSlug.Common.Devices;

namespace RampantSlug.ServerLibrary.Modes
{
    public class Attract : Mode
    {
        public Attract(GameController game)
            : base(game, 1)
        {
        }

        public override void mode_started()
        {
 

            // Blinky start button
            // Game.Lamps["startButton"].Schedule(0x00ff00ff, 0, false);
            Game.Display.MainScore.IsVisible = false;
            Game.Display.MainScore.HeaderText = "Server Demo";

            Game.Display.MainScore.ModeText = "Test";
        }

        public bool sw_Start_Button_active(Switch sw)
        {
            //if (Game.trough.is_full())
            //{
                // Remove attract mode from queue
            Game.GamePlay.Modes.Remove(this);

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
