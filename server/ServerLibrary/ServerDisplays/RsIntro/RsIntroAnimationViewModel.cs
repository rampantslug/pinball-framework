using System.Windows.Controls;
using Caliburn.Micro;

namespace ServerLibrary.ServerDisplays.RsIntro
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public class RsIntroAnimationViewModel : Screen
    {
        private bool _readyToStartIntro;

        public bool ReadyToStartIntro
        {
            get
            {
                return _readyToStartIntro;
            }
            set
            {
                _readyToStartIntro = value;
                NotifyOfPropertyChange(()=> ReadyToStartIntro);
            }
        }

        public void AnimationCompleted()
        {
            //var breakere = true;
        }

    }
}
