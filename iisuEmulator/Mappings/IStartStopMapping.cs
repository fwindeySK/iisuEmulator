using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iisuEmulator.Mappings
{
    public interface IStartStopMapping
    {
        bool IsActive
        {
            get;
        }

        string IIDOutputPath
        {
            get;
        }
    }
}
