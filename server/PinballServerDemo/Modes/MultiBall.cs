using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Devices;
using Hardware.Proc;
using ServerLibrary;

namespace PinballServerDemo.Modes
{
    public class MultiBall: ServerMode, IMode
    {
         [ImportingConstructor]
        public MultiBall(IGameController game)
            : base(game, 1)
        {
            ModeEvents.Add(new ModeEvent
            {
                Id = 1,
                Name = "3 Ball Multiball",
                AssociatedSwitch = null,
                Trigger = EventType.SwitchClosedDebounced,
                MethodToExecute = Start3BallMultiball
            });
        }

        public override string Title
        {
            get { return "MultiBall"; }
        }

        private bool Start3BallMultiball(Switch arg)
        {
            return false;
        }
    }
}