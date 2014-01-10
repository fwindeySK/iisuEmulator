using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iisuEmulator.Exceptions
{
    public class VirtualJoystickNotFoundException : Exception
    {
        public VirtualJoystickNotFoundException() : base("No Virtual Joystick found")
        {
            
        }
    }
}
