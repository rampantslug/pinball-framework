using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RampantSlug.ServerLibrary.ServerDisplays
{

    public sealed class MainScoreViewModel : Screen
    {

        public int PlayerScore { get; set; }

        public MainScoreViewModel()
        {
            PlayerScore = 0;
        }
    }
}
