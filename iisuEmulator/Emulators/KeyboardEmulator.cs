using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsInput;

namespace iisuEmulator.Emulators
{
    public class KeyboardEmulator : IEmulator
    {
        private string[] _digitalDataSources;
        private string[] _analogDataSources;

        private ushort[] _virtualKeyCodes;

        #region IEmulator Members

        public KeyboardEmulator()
        {
            _analogDataSources = new string[] {"N/A"};
            
            _digitalDataSources = Enum.GetNames(typeof(VirtualKeyCode));
            Array.Sort(_digitalDataSources);
            _virtualKeyCodes = new ushort[_digitalDataSources.Length];
            for(int i=0; i< _digitalDataSources.Length; ++i)
            {
                _virtualKeyCodes[i] = (ushort)Enum.Parse(typeof(VirtualKeyCode), _digitalDataSources[i]);
            }
        }

        public string[] AnalogDataSourceNames
        {
            get 
            {
                return _analogDataSources;
            }
        }

        public string[] DigitalDataSourceNames
        {
            get 
            {
                return _digitalDataSources;
            }
        }

        public void SetAnalogDataSource(int id, float value)
        {
            
        }

        public void SetDigitalDataSource(int id, int value)
        {
            VirtualKeyCode keyCode = (VirtualKeyCode)_virtualKeyCodes[id];

            if (value == 0)
            {
                if (InputSimulator.IsKeyDown(keyCode))
                {
                    InputSimulator.SimulateKeyUp(keyCode);
                }
            }
            else
            {
                //if (!InputSimulator.IsKeyDown(keyCode))
                //{
                    InputSimulator.SimulateKeyDown(keyCode);
                //}
            }
        }

        public void SetDigitalDataSource(int id, bool value)
        {
            if (value)
            {
                SetDigitalDataSource(id, 1);
            }
            else
            {
                SetDigitalDataSource(id, 0);
            }
        }

        public int GetAnalogDataSourceID(string dataSourceName)
        {
            return 0;
        }

        public int GetDigitalDataSourceID(string dataSourceName)
        {
            int i = 0;
            while (i < _digitalDataSources.Length && _digitalDataSources[i] != dataSourceName)
            {
                ++i;
            }

            if (i != _digitalDataSources.Length)
                return i;
            else
                return -1;
        }

        public string Name
        {
            get 
            {
                return "Keyboard";    
            }
        }

        public void ShutDown()
        {
            for(int i=0; i<_virtualKeyCodes.Length; ++i)
            {
                SetDigitalDataSource(i, false);
            }
        }

        #endregion
    }
}
