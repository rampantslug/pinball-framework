using Caliburn.Micro;

namespace ServerLibrary.ServerDisplays
{
    public interface IDisplayMainScore: IScreen
    {
        int PlayerScore { get; set; }
        bool IsVisible { get; set; }

        string ModeText { get; set; }
        string HeaderText { get; set; }
        string BallText { get; set; }
    }
}