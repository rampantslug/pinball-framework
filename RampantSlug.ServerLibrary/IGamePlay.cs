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
    public interface IGamePlay
    {
        void Initialise();
        void ProcessSwitchEvent(Event switchEvent);
        BallTrough BallTrough { get; }
        ModeQueue Modes { get; set; }
        int Ball { get; set; }
        void StartGame();
        Player AddPlayer();
        void StartBall();
        void EndBall();
    }
}
