using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RampantSlug.PinballClient.ClientDisplays.GameStatus
{

    [Export(typeof(GameStatusViewModel))]
    public sealed class GameStatusViewModel : Screen, IGameStatus
    {

        public GameStatusViewModel()
        {
            DisplayName = "Game Status";
        }
    }
}
