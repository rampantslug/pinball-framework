using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Caliburn.Micro;
using ServerLibrary.ServerDisplays;

namespace ServerLibrary
{
    public class Display : Screen, IDisplay
    {
        public IDisplayBackgroundVideo BackgroundVideo { get; private set; }
        public IDisplayMainScore MainScore { get; private set; }
        public IDisplayOverrideDisplay OverrideDisplay { get; private set; }

        public Display()
        {
            BackgroundVideo = new BackgroundVideoViewModel();
            MainScore = new MainScoreViewModel();
            OverrideDisplay = new OverrideDisplayViewModel();
           
        }

    }
}
