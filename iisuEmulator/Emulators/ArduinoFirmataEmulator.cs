using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rhyduino;
using iisuEmulator.Exceptions;

namespace iisuEmulator.Emulators
{
    public class ArduinoFirmataEmulator : IEmulator
    {
        struct ArduinoDataSource
        {
            public int PortNumber;
            public Rhyduino.PinMode Mode;

            private string _name;

            public ArduinoDataSource(int portNumber, Rhyduino.PinMode mode)
            {
                PortNumber = portNumber;
                Mode = mode;
                _name = "Port " + portNumber + " " + Enum.GetName(typeof(Rhyduino.PinMode), mode);
            }

            public string Name
            {
                get
                {
                    return _name;
                }
            }
        }
        
        private ArduinoDataSource[] _analogDataSources;
        private ArduinoDataSource[] _digitalDataSources;

        private Arduino _arduino;

        private int _startPort = 2;
        private int _endPort = 13;

        public ArduinoFirmataEmulator(string comPort)
        {
            _arduino = new Arduino("COM"+comPort);

            int cycles = 0;
            while (!_arduino.IsConnected && cycles < 30)
            {
                ++cycles;
                System.Threading.Thread.Sleep(100);
            }

            try
            {
                int portAmount = _endPort - _startPort + 1;
                _analogDataSources = new ArduinoDataSource[portAmount * 2];
                _digitalDataSources = new ArduinoDataSource[portAmount];
                for (int index = 0; index < portAmount; ++index)
                {
                    _analogDataSources[index] = new ArduinoDataSource(index + _startPort, Rhyduino.PinMode.Pwm);
                    _digitalDataSources[index] = new ArduinoDataSource(index + _startPort, Rhyduino.PinMode.Output);
                }
                for (int index = portAmount; index < _analogDataSources.Length; ++index)
                {
                    _analogDataSources[index] = new ArduinoDataSource(index + _startPort - portAmount, Rhyduino.PinMode.Servo);
                }
            }
            catch (Exception)
            {
                throw new DeviceNotConnectedException("Arduino bord not found on port " + "COM" + comPort);
            }
        }

        #region IEmulator Members

        public string[] AnalogDataSourceNames
        {
            get 
            {
                string[] names = new string[_analogDataSources.Length];
                for(int i = 0; i<_analogDataSources.Length; ++i)
                {
                    names[i] = _analogDataSources[i].Name;
                }
                return names;
            }
        }

        public string[] DigitalDataSourceNames
        {
            get 
            {
                string[] names = new string[_digitalDataSources.Length];
                for (int i = 0; i < _digitalDataSources.Length; ++i)
                {
                    names[i] = _digitalDataSources[i].Name;
                }
                return names;
            }
        }

        public void SetAnalogDataSource(int id, float value)
        {
            short analogValue = (short)(value * 180);

            if (_arduino.DigitalPins[_analogDataSources[id].PortNumber].Mode != _analogDataSources[id].Mode)
            {
                _arduino.DigitalPins[_analogDataSources[id].PortNumber].SetPinMode(_analogDataSources[id].Mode);
            }

            _arduino.DigitalPins[_analogDataSources[id].PortNumber].SetPinValue(analogValue);
        }

        public void SetDigitalDataSource(int id, int value)
        {
            if (_arduino.DigitalPins[_digitalDataSources[id].PortNumber].Mode != _digitalDataSources[id].Mode)
            {
                _arduino.DigitalPins[_digitalDataSources[id].PortNumber].SetPinMode(_digitalDataSources[id].Mode);
            }

            if (value != 0)
            {
                _arduino.DigitalPins[_digitalDataSources[id].PortNumber].SetPinValue(DigitalPinValue.High);
            }
            else
            {
                _arduino.DigitalPins[_digitalDataSources[id].PortNumber].SetPinValue(DigitalPinValue.Low);
            }
        }

        public void SetDigitalDataSource(int id, bool value)
        {
            SetDigitalDataSource(id, value == false ? 0 : 1);   
        }

        public int GetAnalogDataSourceID(string dataSourceName)
        {
            int i = 0;
            while (i < _analogDataSources.Length && _analogDataSources[i].Name != dataSourceName)
            {
                ++i;
            }

            if (i != _analogDataSources.Length)
                return i;
            else
                return -1;
        }

        public int GetDigitalDataSourceID(string dataSourceName)
        {
            int i = 0;
            while (i < _digitalDataSources.Length && _digitalDataSources[i].Name != dataSourceName)
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
                return "Arduino Firmata";
            }
        }

        public void ShutDown()
        {
            _arduino.Disconnect();
            _arduino.Dispose();
        }

        #endregion
    }
}
