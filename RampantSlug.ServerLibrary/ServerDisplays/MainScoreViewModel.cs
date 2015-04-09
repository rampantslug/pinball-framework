using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RampantSlug.ServerLibrary.ServerDisplays
{
    [Export(typeof(IDisplayMainScore))]
    public sealed class MainScoreViewModel : Screen, IDisplayMainScore

    {
        private int _playerScore;

        public int PlayerScore
        {
            get
            {
                return _playerScore;
            }
            set
            {
                _playerScore = value;
                NotifyOfPropertyChange(() => PlayerScore);
            }
        }

        public MainScoreViewModel()
        {
            PlayerScore = 0;
        }
    }
}
