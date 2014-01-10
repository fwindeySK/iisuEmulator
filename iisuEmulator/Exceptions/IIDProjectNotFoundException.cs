using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iisuEmulator.Exceptions
{
    public class IIDProjectNotFoundException : Exception
    {
        public IIDProjectNotFoundException(string message) : base(message)
        {
        }
    }
}
