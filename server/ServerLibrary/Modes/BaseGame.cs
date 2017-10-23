using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Devices;
using Common;
using Hardware.Proc;
using ServerLibrary.Events;

namespace ServerLibrary.Modes
{
    [Export(typeof(IMode))]
    public class BaseGame : ServerMode, IMode
    {
        public bool BallStarting = false;

        public override string Title
        {
            get { return "Base Game"; }
        }

        public BaseGame(IGameController game) 
            : base(game, 2)
        {
            BallStarting = true;

            ModeEvents.Add(new ModeEvent
            {
                Id = 1,
                Name = "Lower Target Active",
                Trigger = EventType.SwitchClosedDebounced,
                MethodToExecute = LowerTargetActive
            });
            ModeEvents.Add(new ModeEvent
            {
                Id = 2,
                Name = "Middle Target Active",
                Trigger = EventType.SwitchClosedDebounced,
                MethodToExecute = MiddleTargetActive
            });
            ModeEvents.Add(new ModeEvent
            {
                Id = 3,
                Name = "Right Outlane Active",
                Trigger = EventType.SwitchClosedDebounced,
                MethodToExecute = RightOutlaneActive
            });
        }

      

      /*  public override void InitialiseModeEvents(object obj)
        {

        }*/


        public override void ModeStarted()
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
  //          Game.GamePlay.BallTrough.launch_callback = new AnonDelayedHandler(ball_launch_callback);
  //          Game.GamePlay.BallTrough.drain_callback = new AnonDelayedHandler(ball_drained_callback);
            //Game.trough.ball_save_callback = new AnonDelayedHandler(ball_saved_callback);
            // Enable ball search in case a ball gets stuck

            // In case a higher priority mode doesn't install its own ball drain handler
            //Game.trough.onBallDrained += new DrainCallbackHandler(ball_drained_callback);

            // Each time this mode is added, ball_starting should be set to true
            BallStarting = true;

  //          Game.GamePlay.BallTrough.launch_balls(1, null, false);

            Game.Display.MainScore.HeaderText = "";
            Game.Display.MainScore.ModeText = "";
            Game.Display.MainScore.IsVisible = true;
            Game.Display.MainScore.PlayerScore = 0;

            Game.GamePlay.BallTrough.EjectBall();
        }


        public bool LowerTargetActive(Switch sw)
        {
            Game.Display.MainScore.IsVisible = false;
            Game.Display.MainScore.HeaderText = "Lower Target!!";
            var currentScore = Game.Display.MainScore.PlayerScore;

            Game.Display.MainScore.PlayerScore = 1000;
            Game.Display.MainScore.IsVisible = true;

            TimedAction.ExecuteWithDelay(new System.Action(delegate
            {
                Game.Display.MainScore.IsVisible = false;
            }), TimeSpan.FromSeconds(0.6));

            TimedAction.ExecuteWithDelay(new System.Action(delegate
            {
                Game.Display.MainScore.HeaderText = "";
                Game.Display.MainScore.IsVisible = true;
                Game.Display.MainScore.PlayerScore = currentScore + 1000;

            }), TimeSpan.FromSeconds(0.8));


            return SWITCH_CONTINUE;
        }

        public bool MiddleTargetActive(Switch sw)
        {
            Game.Display.MainScore.IsVisible = false;
            Game.Display.MainScore.HeaderText = "Middle Target!!";
            var currentScore = Game.Display.MainScore.PlayerScore;

            Game.Display.MainScore.PlayerScore = 30;
            Game.Display.MainScore.IsVisible = true;

            TimedAction.ExecuteWithDelay(new System.Action(delegate
            {
                Game.Display.MainScore.IsVisible = false;

            }), TimeSpan.FromSeconds(0.6));

            TimedAction.ExecuteWithDelay(new System.Action(delegate
            {
                Game.Display.MainScore.HeaderText = "";
                Game.Display.MainScore.IsVisible = true;
                Game.Display.MainScore.PlayerScore = currentScore + 30;

            }), TimeSpan.FromSeconds(0.8));

            return SWITCH_CONTINUE;
        }

        private bool RightOutlaneActive(Switch sw)
        {
            Game.Display.MainScore.IsVisible = false;
            Game.Display.MainScore.HeaderText = "Right outlane!!";
            var currentScore = Game.Display.MainScore.PlayerScore;

            Game.Display.MainScore.PlayerScore = 30;
            Game.Display.MainScore.IsVisible = true;

            TimedAction.ExecuteWithDelay(new System.Action(delegate
            {
                Game.Display.MainScore.IsVisible = false;

            }), TimeSpan.FromSeconds(0.6));

            TimedAction.ExecuteWithDelay(new System.Action(delegate
            {
                Game.Display.MainScore.HeaderText = "";
                Game.Display.MainScore.IsVisible = true;
                Game.Display.MainScore.PlayerScore = currentScore + 30;

            }), TimeSpan.FromSeconds(0.8));

            return SWITCH_CONTINUE;
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
                Game.Display.MainScore.BallText = "Ball " + Game.GamePlay.Ball;

            }
                
        }

        public void ball_drained_callback()
        {
 //           if (Game.GamePlay.BallTrough.num_balls_in_play == 0)
 //           {
                finish_ball();
  //          }
        }

        public void finish_ball()
        {
            // Turn off tilt display if it was on now that the ball was drained
            EndBall();
        }

        public void EndBall()
        {
            // Tell the game object it can process the end of ball (to end the players turn or shoot again)
            Game.GamePlay.EndBall();
        }

      
    }
}
