namespace RampantSlug.ServerLibrary.Hardware
{
    public interface IHardwareController
    {
        void Close();
        bool Setup();
        void Start();
    }
}