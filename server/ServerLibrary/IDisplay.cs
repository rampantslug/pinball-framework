using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using ServerLibrary.ServerDisplays;

namespace ServerLibrary
{
    public interface IDisplay
    {
        IDisplayBackgroundVideo BackgroundVideo { get;}
        IDisplayMainScore MainScore { get;}
        IDisplayOverrideDisplay OverrideDisplay { get; }
    }
}
