namespace RampantSlug.ServerLibrary.Hardware
{
    public interface IHardwareController
    {
        IGameController GameController { get; }

        void Close();
        bool Setup();
        void Start();
    }
}