using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RampantSlug.PinballClient.ClientDisplays.SwitchMatrix
{
  //  [Export(typeof(IClientDisplay))]
    [Export(typeof(SwitchMatrixViewModel))]
    public class SwitchMatrixViewModel: Screen, ISwitchMatrix
    {

        public string ClientDisplayName { get { return "Switch Matrix"; } }

    }
}
