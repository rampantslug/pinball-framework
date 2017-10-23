using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Configuration;
using MessageContracts;

namespace ServerLibrary.ContractImplementations
{
    class ConfigMessage: IConfigMessage
    {
        public DateTime Timestamp { get; set; }

        public IMachineConfiguration MachineConfiguration { get; set; }
    }
}
