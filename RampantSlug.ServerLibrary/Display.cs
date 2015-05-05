using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RampantSlug.ServerLibrary.ServerDisplays;

namespace RampantSlug.ServerLibrary
{
    public class Display : IDisplay
    {
        public IDisplayBackgroundVideo BackgroundVideo { get; private set; }
        public IDisplayMainScore MainScore { get; private set; }


        public Display()
        {
            BackgroundVideo = new BackgroundVideoViewModel();
            MainScore = new MainScoreViewModel();
        }

    }
}
