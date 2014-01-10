using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iisuEmulator.Emulators
{
    public interface IEmulator
    {
        string[] AnalogDataSourceNames { get; }
        string[] DigitalDataSourceNames { get; }
        void SetAnalogDataSource(int id, float value);
        void SetDigitalDataSource(int id, int value);
        void SetDigitalDataSource(int id, bool value);
        int GetAnalogDataSourceID(string dataSourceName);
        int GetDigitalDataSourceID(string dataSourceName);
        string Name { get; }
        void ShutDown();
    }
}
