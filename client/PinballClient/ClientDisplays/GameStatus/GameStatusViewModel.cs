using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinballClient.ClientDisplays.GameStatus
{

    [Export(typeof(IGameStatus))]
    public sealed class GameStatusViewModel : Screen, IGameStatus
    {
        [ImportingConstructor]
        public GameStatusViewModel()
        {    
            DisplayName = "Game Status";
        }
    }
}
