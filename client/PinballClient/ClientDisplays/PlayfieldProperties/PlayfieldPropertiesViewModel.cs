using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace PinballClient.ClientDisplays.PlayfieldProperties
{
    [Export(typeof(IPlayfieldProperties))]
    class PlayfieldPropertiesViewModel : Screen, IPlayfieldProperties
    {
        [ImportingConstructor]
        public PlayfieldPropertiesViewModel()
        {
            DisplayName = "Playfield Properties";
        }
    }
}
