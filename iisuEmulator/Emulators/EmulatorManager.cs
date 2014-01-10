using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iisuEmulator.Emulators;

namespace iisuEmulator
{
    public enum MappingType {ANALOG, DIGITAL}

    public class EmulatorManager
    {
        private Dictionary<string, IEmulator> _emulators;

        public EmulatorManager()
        {
            _emulators = new Dictionary<string, IEmulator>();
        }

        public void AddEmulator(IEmulator emulator)
        {
            _emulators.Add(emulator.Name, emulator);
        }

        public IEmulator GetEmulator(string emulatorName)
        {
            if (emulatorName == "NONE")
                return null;
            else
            {
                if (_emulators.ContainsKey(emulatorName))
                {
                    return _emulators[emulatorName];
                }
                else
                {
                    throw new Exceptions.EmulatorNotFoundException(emulatorName);
                }
            }
        }

        public string[] GetEmulatorDataSources(string emulatorName, MappingType type)
        {
            IEmulator emulator = GetEmulator(emulatorName);

            if (type == MappingType.ANALOG)
            {
                return emulator.AnalogDataSourceNames;
            }
            else
                return emulator.DigitalDataSourceNames;
        }

        public string[] GetEmulatorList()
        {
            List<string> emulators = new List<string>();
            emulators.Add("NONE");

            IEmulator[] emulatorCollection = _emulators.Values.ToArray();

            for (int i = 0; i < _emulators.Values.Count; ++i)
            {
                emulators.Add(emulatorCollection[i].Name);
            }
            return emulators.ToArray();
        }
    }
}
