using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iisuEmulator.Emulators;

namespace iisuEmulator
{
    public interface IMapping
    {
        void Update();

        string IIDOutputPath
        {
            get;
        }

        MappingType Type
        {
            get;
        }

        IEmulator Emulator
        {
            get;
            set;
        }
        int DataSourceId
        {
            get;
            set;
        }

        Type DataType
        {
            get;
        }

        bool Enabled { get; set; }
    }
}
