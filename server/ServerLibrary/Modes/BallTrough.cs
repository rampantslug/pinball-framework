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
    /// <summary>
    /// Manages trough by providing the following functionality:
    ///     - Keeps track of the number of balls in play
    ///     - Keeps track of the number of balls in the trough
    ///     - Launches one or more balls on request and calls a launch_callback when complete (if exists)
    ///     - Auto-launches balls while ball save is active (if linked to a ball save object)
    ///     - Identifies when balls drain and calls a registered drain_callback, if one exists.
    ///     - Maintains a count of balls locked in playfield lock features (if externally incremented) 
    ///       and adjusts the count of number of balls in play appropriately
    /// 
    /// </summary>

   public class BallTrough : ServerMode, IMode
    {
       public override string Title
       {
           get { return "Ball Trough"; }
       }


        public int BallsInPlay { get; private set; }

        private RequiredDevice _troughEjectCoil;
        private RequiredDevice _plungerAutoFireCoil;
        private RequiredDevice _shooterLaneSwitch;


        /*public string[] position_switchnames;
        public string eject_switchname;
        public string eject_coilname;
        public string[] early_save_switchnames;
        public string shooter_lane_switchname;
        public Delegate drain_callback;

        public int num_balls_in_play;
        public int num_balls_locked;
        public int num_balls_to_launch;
        public int num_balls_to_stealth_launch;
        public bool launch_in_progress = false;
        public bool ball_save_active = false;

        public Delegate ball_save_callback = null;
        public Delegate num_balls_to_save = null;
        public Delegate launch_callback = null;*/

        public BallTrough(IGameController game)
            : base(game, 90)
        {
            // Initialise Required Devices
            _troughEjectCoil = new RequiredDevice
            {
                Id = 1,
                Name = "Trough Eject Coil",
                TypeOfDevice = typeof(Coil)
            };
            _plungerAutoFireCoil = new RequiredDevice
            {
                Id = 2,
                Name = "Plunger Auto Fire Coil ",
                TypeOfDevice = typeof(Coil)
            };
            _shooterLaneSwitch = new RequiredDevice
            {
                Id = 3,
                Name = "Shooter Lane Switch ",
                TypeOfDevice = typeof(Switch)
            };

            RequiredDevices.Add(_troughEjectCoil);
            RequiredDevices.Add(_plungerAutoFireCoil);
            RequiredDevices.Add(_shooterLaneSwitch);


            // Initialise Mode Events
            ModeEvents.Add(new ModeEvent
            {
                Id = 1,
                Name = "Shooter Lane Active",
                Trigger = EventType.SwitchClosedDebounced,
                MethodToExecute = ShooterLaneActive
            });
            ModeEvents.Add(new ModeEvent
            {
                Id = 2,
                Name = "Trough Eject Active",
                Trigger = EventType.SwitchClosedDebounced,
                MethodToExecute = TroughEjectActive
            });
            ModeEvents.Add(new ModeEvent
            {
                Id = 3,
                Name = "Ball Trough #1",
                Trigger = EventType.SwitchClosedDebounced,
                MethodToExecute = BallTroughActive
            });


            BallsInPlay = 0;
        }

        private bool ShooterLaneActive(Switch sw)
        {
            // Ball is now in play
            BallsInPlay++; 

            return false;
        }

        private bool TroughEjectActive(Switch sw)
        {
            // Check for ball jam in the trough...

            return false;
        }

        private bool BallTroughActive(Switch sw)
        {
            // Check the number of balls

            BallsInPlay--;

            return false;
        }

        public void EjectBall()
        {
            if (!RequiredDevicesExist())
                return;

            // Launch ball if shooter lane is empty
            if (!Game.Devices.Switches[_shooterLaneSwitch.DeviceName].IsActive())
            {
                // Fire launch coil provided we have its name
                Game.Devices.Coils[_troughEjectCoil.DeviceName].Pulse();                   
             }            
        }

       /// <summary>
       /// Determines whether Devices Required by this Mode exist...
       /// </summary>
       /// <returns></returns>
        private bool RequiredDevicesExist()
        {
            if (string.IsNullOrEmpty(_shooterLaneSwitch.DeviceName))
           {
               // TODO: Raise a logmessage indicating error and that this mode wont execute correctly
               return false;
           }
            if (string.IsNullOrEmpty(_troughEjectCoil.DeviceName))
           {
               return false;
           }
           return true;
        }

        #region Stuff from old NetProcGame - Possibly delete


        /*  public BallTrough(GameController game, string[] position_switchnames, string eject_switchname, string eject_coilname,
            string[] early_save_switchnames, string shooter_lane_switchname, Delegate drain_callback = null)
            : base(game, 90)
        {
            this.position_switchnames = position_switchnames;
            this.eject_switchname = eject_switchname;
            this.eject_coilname = eject_coilname;
            this.early_save_switchnames = early_save_switchnames;
            this.shooter_lane_switchname = shooter_lane_switchname;
            this.drain_callback = drain_callback;

            // Install switch handlers
            foreach (string sw in position_switchnames)
            {
                add_switch_handler(sw, "active", 0, new SwitchAcceptedHandler(position_switch_handler));
                add_switch_handler(sw, "inactive", 0, new SwitchAcceptedHandler(position_switch_handler));
            }

            // Install early save switch handlers
            foreach (string sw in early_save_switchnames)
            {
                add_switch_handler(sw, "active", 0, new SwitchAcceptedHandler(early_save_switch_handler));
            }

            // Reset all variables
            num_balls_in_play = 0;
            num_balls_locked = 0;
            num_balls_to_launch = 0;
            num_balls_to_stealth_launch = 0;
            launch_in_progress = false;
            ball_save_active = false;
            ball_save_callback = null;
            num_balls_to_save = null;
            launch_callback = null;
        }

        public void enable_ball_save(bool enabled = true)
        {
            ball_save_active = enabled;
        }

        private bool early_save_switch_handler(Switch sw)
        {
           // Game.Logger.Log("EARLY BALLSAVE: Active " + ball_save_active.ToString());
            if (ball_save_active)
            {
                // Only do an early ball save if a ball is ready to be launched
                // otherwise, let the trough switches handle it
               // Game.Logger.Log("EJECT COIL ACTIVE: " + Game.Switches[eject_switchname].IsActive().ToString());
                if (num_balls() > 0)
                {
                    launch_balls(1, ball_save_callback, true);
                }
            }
            return SWITCH_CONTINUE;
        }

        public override void mode_stopped()
        {
            cancel_delayed("check_switches");
        }

        private bool position_switch_handler(Switch sw)
        {
            cancel_delayed("check_switches");
            delay("check_switches", EventType.None, 0.50, new AnonDelayedHandler(check_switches));
            return SWITCH_CONTINUE;
        }

        private void check_switches()
        {
            if (num_balls_in_play > 0)
            {
                // Base future calculations on how many balls the machine thinks are
                // currently installed
                int num_current_machine_balls = 3; //TODO: Get this from Config?
                int temp_num_balls = num_balls();
                if (ball_save_active)
                {
                    int num_balls_to_save = 0;
                    if (this.num_balls_to_save != null)
                        num_balls_to_save = (int)this.num_balls_to_save.DynamicInvoke();

                    // Calculate how many balls shouldn't be in the trough assuming one just drained
                    int num_balls_out = num_balls_locked + (num_balls_to_save - 1);

                    // Translate that to how many balls should be in the trough if one is being saved
                    int balls_in_trough = num_current_machine_balls - num_balls_out;

                    if (temp_num_balls - num_balls_to_launch >= balls_in_trough)
                        launch_balls(1, ball_save_callback, true);
                    else
                        // If there are too few balls in the trough, ignore this one in an attempt to
                        // correct the tracking
                        return;
                }
                else
                {
                    // Calculate how many balls should be in the trough for various condition
                    int num_trough_balls_if_ball_ending = num_current_machine_balls - num_balls_locked;
                    int num_trough_balls_if_multiball_ending = num_trough_balls_if_ball_ending - 1;
                    int num_trough_balls_if_multiball_drain = num_trough_balls_if_ball_ending - (num_balls_in_play - 1);

                    // The ball should end if all of the balls are in the trough
                    if (temp_num_balls == num_current_machine_balls ||
                        temp_num_balls == num_trough_balls_if_ball_ending)
                    {
                        num_balls_in_play = 0;
                        if (drain_callback != null)
                            drain_callback.DynamicInvoke();
                    }

                    // Multiball is ending if all but 1 ball are in the trough.
                    else if (temp_num_balls == num_trough_balls_if_multiball_ending)
                    {
                        num_balls_in_play = 1;
                        if (drain_callback != null)
                        {
                            drain_callback.DynamicInvoke();
                        }
                    }

                    // Otherwise, another ball from multiball is draining if the trough gets one more than
                    // it would have if all num_balls_in_play are not in the trough
                    else if (temp_num_balls == num_trough_balls_if_multiball_drain)
                    {
                        // Fix num_balls_in_play if too low
                        if (num_balls_in_play < 3)
                            num_balls_in_play = 2;
                        // Otherwise, subtract 1
                        else
                            num_balls_in_play--;

                        if (drain_callback != null)
                            drain_callback.DynamicInvoke();
                    }
                }
            }
        }

        /// <summary>
        /// Returns the number of balls in the trough by counting the trough switches that are active
        /// </summary>
        /// <returns>The number of balls in the trough</returns>
        public int num_balls()
        {
            int ball_count = 0;
            foreach (string sw in position_switchnames)
            {
                if (Game.Devices.Switches[sw].IsActive)
                    ball_count++;
            }
            return ball_count;
        }

        /// <summary>
        /// Check whether or not the trough has all balls
        /// </summary>
        /// <returns>True if all balls are in the trough, false otherwise</returns>
        public bool is_full()
        {
            return this.num_balls() == 3;
        }

        /// <summary>
        /// Launches balls into play
        /// </summary>
        /// <param name="num">The number of balls to be launch. If ball launches are pending from a previous request
        /// this number will be added to the previously requested number.</param>
        /// <param name="callback">If specified, the callback will be called once all of the requested balls have been launched.</param>
        /// <param name="stealth">Set to true if the balls being launched should NOT be added to the number of balls in play.
        /// For instance, if a ball is being locked on the playfield and a new ball is being launched to replace it as the active ball
        /// then stealth should be true</param>
        public void launch_balls(int num, Delegate callback = null, bool stealth = false)
        {
            num_balls_to_launch += num;
            if (stealth)
            {
                num_balls_to_stealth_launch += num;
            }

            if (!launch_in_progress)
            {
                launch_in_progress = true;
                if (callback != null)
                    launch_callback = callback;

                common_launch_code();
            }
        }

        /// <summary>
        /// This is the part of the ball launch code that repeats for multiple launches
        /// </summary>
        private void common_launch_code()
        {
            // Only kick out another ball if the last ball is gone from the shooter lane
            if (!Game.Devices.Switches[shooter_lane_switchname].IsActive)
            {
                num_balls_to_launch -= 1;

                // TODO: Determine best way to do below?? Fit it in with Events system and command logic??
                //Game.Coils[eject_coilname].Pulse();

                // Only increment num_balls_in_play if there are no more stealth launches to complete.
                if (num_balls_to_stealth_launch > 0)
                    num_balls_to_stealth_launch--;
                else
                    num_balls_in_play++;

                // If more balls need to be launched, delay 1 second
                if (num_balls_to_launch > 0)
                    delay("launch", EventType.None, 1.0, new AnonDelayedHandler(common_launch_code));
                else
                {
                    launch_in_progress = false;
                    if (launch_callback != null)
                        launch_callback.DynamicInvoke();
                }
            }
            // Otherwise, wait 1 second before trying again
            else
            {
                delay("launch", EventType.None, 1.0, new AnonDelayedHandler(common_launch_code));
            }
        }*/

        #endregion
    }
}
