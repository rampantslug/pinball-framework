using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using ServerLibrary.Events;
using ServerLibrary.Modes;

namespace ServerLibrary
{
    public interface IGamePlay
    {
        void Initialise();
        void ProcessSwitchEvent(UpdateSwitchEvent switchEvent);
     //   BallTrough BallTrough { get; }
        ModeQueue ActiveModes { get; set; }
        List<IMode> AllModes { get; set; }

        int Ball { get; set; }
        void StartGame();
        Player AddPlayer();
        void StartBall();
        void EndBall();


        Attract Attract { get; }
        BaseGame BaseGame { get; }
        BallTrough BallTrough { get; }
    }
}
