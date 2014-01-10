using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iisuEmulator.Exceptions;

namespace iisuEmulator.Emulators
{
    public class JoystickEmulator : IEmulator
    {
        private VJoy _vJoy;
        private int _vJoyNumber;

        private string[] _analogJoystickDataSources = {"Slider", "XAxis", "YAxis", "ZAxis", "ZRotation"};
        private string[] _digitalJoystickDataSources;

        public JoystickEmulator(int virtualJoystickNumber)
        {
            _vJoy = new VJoy();

            if (!_vJoy.Initialize())
            {
                throw new VirtualJoystickNotFoundException();
            }

            _vJoy.Reset();
            
            _vJoyNumber = virtualJoystickNumber;

            _digitalJoystickDataSources = new string[16];
            for (int i = 0; i < _digitalJoystickDataSources.Length; ++i)
            {
                _digitalJoystickDataSources[i] = "Button" + (i+1);
            }
        }

        #region IEmulator Members

        public int GetAnalogDataSourceID(string dataSourceName)
        {
            int i = 0;
            while (i < _analogJoystickDataSources.Length && _analogJoystickDataSources[i] != dataSourceName)
            {
                ++i;
            }

            if (i != _analogJoystickDataSources.Length)
                return i;
            else
                return -1;
        }

        public int GetDigitalDataSourceID(string dataSourceName)
        {
            int i = 0;
            while (i < _digitalJoystickDataSources.Length && _digitalJoystickDataSources[i] != dataSourceName)
            {
                ++i;
            }

            if (i != _digitalJoystickDataSources.Length)
                return i;
            else
                return -1;
        }

        public void SetAnalogDataSource(int id, float value)
        {
            if (id == 0)
            {
                _vJoy.SetSlider(_vJoyNumber, (byte)(-127 + (254 * value)));
            }
            else if (id == 1)
            {
                _vJoy.SetXAxis(_vJoyNumber, (byte)(-127 + (254 * value)));
            }
            else if (id == 2)
            {
                _vJoy.SetYAxis(_vJoyNumber, (byte)(-127 + (254 * value)));
            }
            else if (id == 3)
            {
                _vJoy.SetZAxis(_vJoyNumber, (byte)(-127 + (254 * value)));
            }
            else if (id == 4)
            {
                _vJoy.SetZRotation(_vJoyNumber, (byte)(-127 + (254 * value)));
            }
            _vJoy.Update(_vJoyNumber);
        }

        public void SetDigitalDataSource(int id, int value)
        {
            SetDigitalDataSource(id, value == 0 ? false : true);
        }
        
        public void SetDigitalDataSource(int id, bool value)
        {
            _vJoy.SetButton(_vJoyNumber, id, value);
            _vJoy.Update(_vJoyNumber);
        }

        public string[] AnalogDataSourceNames
        {
            get
            {
                return _analogJoystickDataSources;
            }
        }

        public string[] DigitalDataSourceNames
        {
            get
            {
                return _digitalJoystickDataSources;
            }
        }

        public string Name
        {
            get
            {
                return "VJoy virtual joystick";
            }
        }

        public void ShutDown()
        {
            _vJoy.Shutdown();
        }

        #endregion
    }
}
