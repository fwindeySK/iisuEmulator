using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iisuEmulator.Exceptions
{
    public class EmulatorNotFoundException : Exception
    {
        public EmulatorNotFoundException(string message) : base(message)
        {
        }
    }
}
