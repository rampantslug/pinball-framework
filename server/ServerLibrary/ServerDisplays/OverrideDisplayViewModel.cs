using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ServerLibrary.ServerDisplays
{
    [Export(typeof(IDisplayOverrideDisplay))]
    public sealed class OverrideDisplayViewModel : Conductor<Screen>, IDisplayOverrideDisplay
    {
        private Screen _content;

        public Screen Content
        {
            get { return _content; }
            set
            {
                _content = value;
                ActivateItem(_content);

                NotifyOfPropertyChange(()=> Content);
            }
        }

        public OverrideDisplayViewModel()
        {

        }

    }

    public interface IDisplayOverrideDisplay
    {
        Screen Content { get; set; }
    }
}
