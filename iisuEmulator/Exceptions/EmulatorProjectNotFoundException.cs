using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iisuEmulator.Exceptions
{
    public class EmulatorProjectNotFoundException : Exception
    {
        public EmulatorProjectNotFoundException(string message) : base(message)
        {
        }
    }
}
