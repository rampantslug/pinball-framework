using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RampantSlug.ServerLibrary.ServerDisplays;

namespace RampantSlug.ServerLibrary
{
    public interface IDisplay
    {
        IDisplayBackgroundVideo BackgroundVideo { get;}
        IDisplayMainScore MainScore { get;}
    }
}
