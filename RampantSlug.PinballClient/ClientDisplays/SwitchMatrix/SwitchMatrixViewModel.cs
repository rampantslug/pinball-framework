using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RampantSlug.PinballClient.ClientDisplays.SwitchMatrix
{
    [Export(typeof(SwitchMatrixViewModel))]
    public sealed class SwitchMatrixViewModel: Screen, ISwitchMatrix
    {

        public SwitchMatrixViewModel()
        {
            DisplayName = "Switch Matrix";
        }

    }
}
