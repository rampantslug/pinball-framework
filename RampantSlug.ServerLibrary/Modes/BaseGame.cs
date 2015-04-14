using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RampantSlug.ServerLibrary.Modes
{
    public class BaseGame : Mode
    {
        public BaseGame(GameController game) 
            : base(game, 2)
        {
        }
    }
}
