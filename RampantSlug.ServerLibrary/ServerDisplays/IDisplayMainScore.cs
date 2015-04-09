using Caliburn.Micro;

namespace RampantSlug.ServerLibrary.ServerDisplays
{
    public interface IDisplayMainScore: IScreen
    {
        int PlayerScore { get; set; }
    }
}