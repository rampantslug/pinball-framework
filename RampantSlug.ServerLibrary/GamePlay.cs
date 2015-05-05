using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RampantSlug.Common;
using RampantSlug.ServerLibrary.Hardware.Proc;
using RampantSlug.ServerLibrary.Modes;

namespace RampantSlug.ServerLibrary
{
    public class GamePlay : IGamePlay
    {
        protected ModeQueue _modes;

        public GameController Game { get; set; }

        /// <summary>
        /// The current list of modes that are active in the game
        /// </summary>
        public ModeQueue Modes
        {
            get { return _modes; }
            set { _modes = value; }
        }

        public GamePlay(GameController gameController)
        {
            Game = gameController;
            _modes = new ModeQueue(Game);
        }


        public void Initialise()
        {
            Attract = new Attract(Game);
            BaseGameMode = new BaseGame(Game);
            string[] troughSwitchnames = new string[5] { "trough1", "trough2", "trough3", "trough4", "trough5" };
            BallTrough = new BallTrough(Game,
                                troughSwitchnames,
                                "trough5",
                                "trough",
                                new string[] { "leftOutlane", "rightOutlane" },
                                "shooterLane");
            _modes.Add(Attract);
            _modes.Add(BallTrough);

        }

        public void ProcessSwitchEvent(Event switchEvent)
        {
            _modes.handle_event(switchEvent);
        }


        #region Methods carried over from NetProcGame. May require rework

        public Attract Attract;
        public BaseGame BaseGameMode;
        public BallTrough BallTrough { get; private set; }
        private List<Player> _players;
        private double _ballEndTime;
        private double _ballStartTime;
        private int _currentPlayerIndex;

        public int Ball { get; set; }
        private int _ballsPerGame;
        private List<Player> _oldPlayers;


        /// <summary>
        /// The ball time for the current player
        /// </summary>
        /// <returns>The ball time (in seconds) that the current ball has been in play</returns>
        public double GetBallTime()
        {
            return this._ballEndTime - this._ballStartTime;
        }

        /// <summary>
        /// The game time for the given player index
        /// </summary>
        /// <param name="player">The player index to calculate the game time for</param>
        /// <returns>The time in seconds the player has been playing the entire game</returns>
        public double GetGameTime(int player)
        {
            return this._players[player].GameTime;
        }

        /// <summary>
        /// Save the ball start time into local memory
        /// </summary>
        public void SaveBallStartTime()
        {
            this._ballStartTime = Time.GetTime();
        }

        /// <summary>
        /// Called by the implementor to notify the game that the first ball should be started.
        /// </summary>
        public void StartBall()
        {
            this.BallStarting();
        }

        /// <summary>
        /// Called by the game framework when a new ball is starting
        /// </summary>
        public virtual void BallStarting()
        {
            this.SaveBallStartTime();
            _modes.Add(BaseGameMode);
        }

        /// <summary>
        /// Called by the game framework when a new ball is starting which was the result of a stored extra ball.
        /// The default implementation calls ball_starting() which is not called by the framework in this case.
        /// </summary>
        public virtual void ShootAgain()
        {
            this.BallStarting();
        }

        /// <summary>
        /// Called by the game framework when the current ball has ended
        /// </summary>
        public virtual void BallEnded()
        {
            _modes.Remove(BaseGameMode);
        }

        /// <summary>
        /// Called by the implementor to notify the game that the current ball has ended
        /// </summary>
        public void EndBall()
        {
            this._ballEndTime = Time.GetTime();
            this.CurrentPlayer().GameTime += this.GetBallTime();
            this.BallEnded();

            if (this.CurrentPlayer().ExtraBalls > 0)
            {
                this.CurrentPlayer().ExtraBalls -= 1;
                this.ShootAgain();
                return;
            }

            if (this._currentPlayerIndex + 1 == this._players.Count)
            {
                Ball += 1;
                this._currentPlayerIndex = 0;
            }
            else
            {
                this._currentPlayerIndex += 1;
            }

            if (Ball > this._ballsPerGame)
            {
                this.EndGame();
            }
            else
            {
                this.StartBall();
            }

        }

        /// <summary>
        /// Called by the GameController when a new game is starting.
        /// </summary>
        public virtual void GameStarted()
        {
            Ball = 1;
            this._players = new List<Player>();
            this._currentPlayerIndex = 0;
        }

        /// <summary>
        /// Called by the implementor to notify the game that the game has started.
        /// </summary>
        public virtual void StartGame()
        {
            this.GameStarted();
        }

        /// <summary>
        /// Called by the GameController when the current game has ended
        /// </summary>
        public virtual void GameEnded()
        {
            _modes.Add(Attract);
        }

        /// <summary>
        /// Called by the implementor to notify the game that the game as ended
        /// </summary>
        public void EndGame()
        {
            this.GameEnded();
            Ball = 0;
        }

        /// <summary>
        /// Reset the game state to normal (like a slam tilt)
        /// </summary>
        public virtual void Reset()
        {
            Ball = 0;
            _oldPlayers.Clear();
            _oldPlayers.AddRange(_players);
            _players.Clear();
            _currentPlayerIndex = 0;
            _modes.Clear();
        }

        /// <summary>
        /// Creates a new player with a given name
        /// </summary>
        /// <param name="name">The name for the player to use, usually auto generated</param>
        /// <returns>A new player object</returns>
        public Player CreatePlayer(string name)
        {
            return new Player(name);
        }

        /// <summary>
        /// Adds a new player to 'Players' and auto-assigns a name
        /// </summary>
        /// <returns></returns>
        public virtual Player AddPlayer()
        {
            Player newPlayer = this.CreatePlayer("Player " + (_players.Count + 1).ToString());
            _players.Add(newPlayer);
            return newPlayer;
        }

        /// <summary>
        /// Returns the current 'Player' object according to the current_player_index value
        /// </summary>
        /// <returns></returns>
        public Player CurrentPlayer()
        {
            if (this._players.Count > this._currentPlayerIndex)
                return this._players[this._currentPlayerIndex];
            else
                return null;
        }


        #endregion


    }
}
