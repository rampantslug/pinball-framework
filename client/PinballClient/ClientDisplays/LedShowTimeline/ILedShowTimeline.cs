using Caliburn.Micro;

namespace PinballClient.ClientDisplays.LedShowTimeline
{
    public interface ILedShowTimeline :IScreen
    {
        void PlayPause();
        void FirstFrame();
        void LastFrame();
    }
}
