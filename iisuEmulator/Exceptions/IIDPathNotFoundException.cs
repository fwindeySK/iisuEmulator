using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iisuEmulator.Exceptions
{
    class IIDPathNotFoundException : Exception
    {
        public IIDPathNotFoundException(string message): base(message)
        {
            
        }
    }
}
