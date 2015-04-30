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
        private bool _isVisible;
        private string _modeText;
        private string _headerText;
        private string _ballText;

        public string BallText
        {
            get
            {
                return _ballText;
            }
            set
            {
                _ballText = value;
                NotifyOfPropertyChange(() => BallText);
            }
        }

        public string HeaderText
        {
            get
            {
                return _headerText;
            }
            set
            {
                _headerText = value;
                NotifyOfPropertyChange(() => HeaderText);
            }
        }

        public string ModeText
        {
            get
            {
                return _modeText;
            }
            set
            {
                _modeText = value;
                NotifyOfPropertyChange(() => ModeText);
            }
        }

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

        public bool IsVisible
        {
            get
            {
                return _isVisible;
            }
            set
            {
                _isVisible = value;
                NotifyOfPropertyChange(() => IsVisible);
            }
        }

        public MainScoreViewModel()
        {
            PlayerScore = 0;
        }
    }
}
