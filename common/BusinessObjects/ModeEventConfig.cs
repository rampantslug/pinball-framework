using System;
using BusinessObjects.Devices;
using Newtonsoft.Json;

namespace BusinessObjects
{
    public class ModeEventConfig
    {
        [JsonIgnore]
        public ushort Id { get; set; }

        public string Name { get; set; }

        public string AssociatedSwitchName { get; set; }

        public Switch AssociatedSwitch { get; set; }

        // TODO: Need EventType to be moved from Hardware into here... that includes PROC stuff
        // Maybe create a new library for Hardware that can be shared
      //  public EventType Trigger { get; set; }



        public Func<Switch, bool> MethodToExecute { get; set; }
    }
}