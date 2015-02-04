using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RampantSlug.Contracts
{
    public interface ISimpleMessage
    {
        string Message { get; }
        DateTime Timestamp { get; }

    }
}
