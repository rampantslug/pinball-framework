using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Caliburn.Micro;
using Common;
using ServerLibrary.Events;
using ServerLibrary.Modes;


namespace ServerLibrary
{
    public class GamePlay : IGamePlay
    {
        public IGameController Game { get; set; }

        /// <summary>
        /// The current list of modes that are active in the game
        /// </summary>
        public ModeQueue ActiveModes { get; set; }

        /// <summary>
        /// All modes that exist for this game
        /// </summary>
        public List<IMode> AllModes { get; set; }

        public Attract Attract { get; set; }
        public BaseGame BaseGame { get; set; }
        public BallTrough BallTrough { get; set; }


        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameController"></param>
        public GamePlay(IGameController gameController)
        {
            Game = gameController;
            _oldPlayers = new List<Player>();
            _ballsPerGame = 5; // TODO: This need to be initialised from Machine/Game config.
            ActiveModes = new ModeQueue(Game);

            AllModes = new List<IMode>();
            AllModes = IoC.GetAll<IMode>().ToList();

        }

        #endregion


        public void Initialise()
        {
            // Find all Attract/BaseGame and BallTrough modes in case they have been derived from for more functionality...

            // Attract
            var allAttractModeTypes = FindAllDerivedTypes<Attract>();
            if(allAttractModeTypes.Count == 1)
            {
                var modeType = allAttractModeTypes.First();
                Attract = AllModes.FirstOrDefault(mode => mode.GetType() == modeType) as Attract;
            }
            // Use the base Attract mode if we couldnt find a derived type
            if (Attract == null)
            {
                Attract = AllModes.FirstOrDefault(mode => mode.GetType() == typeof(Attract)) as Attract;
            }

            // Base Game
            var allBaseGameModeTypes = FindAllDerivedTypes<BaseGame>();
            if (allBaseGameModeTypes.Count == 1)
            {
                var modeType = allBaseGameModeTypes.First();
                BaseGame = AllModes.FirstOrDefault(mode => mode.GetType() == modeType) as BaseGame;
            }
            // Use the base BaseGame mode if we couldnt find a derived type
            if (BaseGame == null)
            {
                BaseGame = AllModes.FirstOrDefault(mode => mode.GetType() == typeof(BaseGame)) as BaseGame;
            }

            // Ball Trough
            var allBallTroughModeTypes = FindAllDerivedTypes<BallTrough>();
            if (allBallTroughModeTypes.Count == 1)
            {
                var modeType = allBallTroughModeTypes.First();
                BallTrough = AllModes.FirstOrDefault(mode => mode.GetType() == modeType) as BallTrough;
            }
            // Use the base BallTrough mode if we couldnt find a derived type
            if (BallTrough == null)
            {
                BallTrough = AllModes.FirstOrDefault(mode => mode.GetType() == typeof(BallTrough)) as BallTrough;
            }

            if (Attract == null || BaseGame == null || BallTrough == null)
            {
                // Need all 3 of these modes for some form of game so if any are null then dont bother
                // TODO: Raise some kind of error...
                return;
            }

            ActiveModes.Add(Attract);
            ActiveModes.Add(BallTrough);
        }



        public void ProcessSwitchEvent(UpdateSwitchEvent switchEvent)
        {
            ActiveModes.HandleEvent(switchEvent);
        }

        public static List<Type> FindAllDerivedTypes<T>()
        {
            // Use the executing assembly to find the types
            return FindAllDerivedTypes<T>(Assembly.GetEntryAssembly());
        }

        public static List<Type> FindAllDerivedTypes<T>(Assembly assembly)
        {
            var derivedType = typeof(T);
            return assembly
                .GetTypes()
                .Where(t =>
                    t != derivedType &&
                    derivedType.IsAssignableFrom(t)
                    ).ToList();

        } 


        #region Methods carried over from NetProcGame. May require rework


  //      public BallTrough BallTrough { get; private set; }
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
            return _ballEndTime - _ballStartTime;
        }

        /// <summary>
        /// The game time for the given player index
        /// </summary>
        /// <param name="player">The player index to calculate the game time for</param>
        /// <returns>The time in seconds the player has been playing the entire game</returns>
        public double GetGameTime(int player)
        {
            return _players[player].GameTime;
        }

        /// <summary>
        /// Save the ball start time into local memory
        /// </summary>
        public void SaveBallStartTime()
        {
            _ballStartTime = Time.GetTime();
        }

        /// <summary>
        /// Called by the implementor to notify the game that the first ball should be started.
        /// </summary>
        public void StartBall()
        {
            BallStarting();
        }

        /// <summary>
        /// Called by the game framework when a new ball is starting
        /// </summary>
        public virtual void BallStarting()
        {
            SaveBallStartTime();
            ActiveModes.Add(BaseGame);
        }

        /// <summary>
        /// Called by the game framework when a new ball is starting which was the result of a stored extra ball.
        /// The default implementation calls ball_starting() which is not called by the framework in this case.
        /// </summary>
        public virtual void ShootAgain()
        {
            BallStarting();
        }

        /// <summary>
        /// Called by the game framework when the current ball has ended
        /// </summary>
        public virtual void BallEnded()
        {
            ActiveModes.Remove(BaseGame);
        }

        /// <summary>
        /// Called by the implementor to notify the game that the current ball has ended
        /// </summary>
        public void EndBall()
        {
            _ballEndTime = Time.GetTime();
            CurrentPlayer().GameTime += GetBallTime();
            BallEnded();

            if (CurrentPlayer().ExtraBalls > 0)
            {
                CurrentPlayer().ExtraBalls -= 1;
                ShootAgain();
                return;
            }

            if (_currentPlayerIndex + 1 == _players.Count)
            {
                Ball += 1;
                _currentPlayerIndex = 0;
            }
            else
            {
                _currentPlayerIndex += 1;
            }

            if (Ball > _ballsPerGame)
            {
                EndGame();
            }
            else
            {
                StartBall();
            }

        }

      

        /// <summary>
        /// Called by the GameController when a new game is starting.
        /// </summary>
        public virtual void GameStarted()
        {
            Ball = 1;
            _players = new List<Player>();
            _currentPlayerIndex = 0;
        }

        /// <summary>
        /// Called by the implementor to notify the game that the game has started.
        /// </summary>
        public virtual void StartGame()
        {
            GameStarted();
        }

        /// <summary>
        /// Called by the GameController when the current game has ended
        /// </summary>
        public virtual void GameEnded()
        {
            ActiveModes.Add(Attract);
        }

        /// <summary>
        /// Called by the implementor to notify the game that the game as ended
        /// </summary>
        public void EndGame()
        {
            GameEnded();
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
            ActiveModes.Clear();
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
            var newPlayer = CreatePlayer("Player " + (_players.Count + 1).ToString());
            _players.Add(newPlayer);
            return newPlayer;
        }

        /// <summary>
        /// Returns the current 'Player' object according to the current_player_index value
        /// </summary>
        /// <returns></returns>
        public Player CurrentPlayer()
        {
            if (_players.Count > _currentPlayerIndex)
                return _players[_currentPlayerIndex];
            else
                return null;
        }


        #endregion



    }
}
