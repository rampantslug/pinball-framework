using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessageContracts;

namespace PinballClient.ContractImplementations
{
    /// <summary>
    /// 
    /// </summary>
    class RequestMediaFileMessage: IRequestMediaFileMessage
    {
        public string MediaFileLocation { get; set; }
    }
}
