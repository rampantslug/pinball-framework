using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RampantSlug.ServerLibrary.Modes
{
    public class BaseGame : Mode
    {
        public bool BallStarting = false;


        public BaseGame(GameController game) 
            : base(game, 2)
        {
            BallStarting = true;
        }

        public override void mode_started()
        {
            // Disable any previously active lamp
            //foreach (Driver lamp in Game.Lamps.Values)
            //{
            //    lamp.Disable();
            //}

            //foreach (Driver lamp in Game.GI.Values)
            //    lamp.Enable();

            // Enable flippers
           // Game.FlippersEnabled = true;

            // Put the ball into play and start tracking it
            //Game.trough.onBallLaunched += new LaunchCallbackHandler(ball_launch_callback);
            Game.BallTrough.launch_callback = new AnonDelayedHandler(ball_launch_callback);
            Game.BallTrough.drain_callback = new AnonDelayedHandler(ball_drained_callback);
            //Game.trough.ball_save_callback = new AnonDelayedHandler(ball_saved_callback);
            // Enable ball search in case a ball gets stuck

            // In case a higher priority mode doesn't install its own ball drain handler
            //Game.trough.onBallDrained += new DrainCallbackHandler(ball_drained_callback);

            // Each time this mode is added, ball_starting should be set to true
            BallStarting = true;

            //Game.trough.launch_balls(1, null, false);
        }

        public override void mode_stopped()
        {
            // Ensure flippers are disabled
            //Game.FlippersEnabled = false;
            // Disable ball search
        }

        public void ball_launch_callback()
        {
            if (BallStarting)
            {
                //((StarterGame)Game).ball_save.start_lamp();
                // TODO: Start Ball save lamp animation...
            }
                
        }

        public void ball_drained_callback()
        {
            if (Game.BallTrough.num_balls_in_play == 0)
            {
                finish_ball();
            }
        }

        public void finish_ball()
        {
            // Turn off tilt display if it was on now that the ball was drained
            EndBall();
        }

        public void EndBall()
        {
            // Tell the game object it can process the end of ball (to end the players turn or shoot again)
            Game.EndBall();
        }
    }
}
