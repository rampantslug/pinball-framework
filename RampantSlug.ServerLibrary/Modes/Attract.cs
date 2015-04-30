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
            Game.MainScore.IsVisible = false;
            Game.MainScore.HeaderText = "Game of Thrones";

            Game.MainScore.ModeText = "Press Start to Play";
        }

        public bool sw_startButton_active(Switch sw)
        {
            //if (Game.trough.is_full())
            //{
                // Remove attract mode from queue
                Game.Modes.Remove(this);

                // Initialize game
                Game.StartGame();

                // Add first player
                Game.AddPlayer();
                Game.StartBall();
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
