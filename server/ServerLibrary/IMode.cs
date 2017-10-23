using System.Collections.Generic;

namespace ServerLibrary
{
    public interface IMode
    {
        string Title { get; }
        List<ModeEvent> ModeEvents { get; set; }
        List<RequiredDevice> RequiredDevices { get; set; }
        List<RequiredMedia> RequiredMedia { get; set; }
            
        IGameController Game { get; set; }

        //void InitialiseModeEvents(object obj);
    }
}