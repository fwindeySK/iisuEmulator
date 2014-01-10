using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iisuEmulator.Exceptions
{
    class DeviceNotConnectedException : Exception
    {
        public DeviceNotConnectedException(string message) : base(message)
        {
            
        }
    }
}
