using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iisuEmulator.Emulators;

namespace iisuEmulator
{

    public class AnalogMapping : IMapping
    {
        private IIDOutput<float> _iidOutput;
        private IEmulator _emulator;
        private int _dataSourceId;
        
        private bool _enabled = true;

        public bool Enabled
        {
            get
            {
                return _enabled;
            }
            set
            {
                _enabled = value;
            }
        }

        public AnalogMapping(IIDOutput<float> iidOutput, IEmulator emulator)
        {
            _iidOutput = iidOutput;
            _emulator = emulator;
            _dataSourceId = 0;
        }

        #region IMapping Members

        public void Update()
        {
            if (_emulator != null && _dataSourceId != -1)
            {
                _emulator.SetAnalogDataSource(_dataSourceId, _iidOutput.Value);
            }
        }

        public string IIDOutputPath
        {
            get
            {
                return _iidOutput.Path;
            }
        }

        public MappingType Type
        {
            get
            {
                return MappingType.ANALOG;
            }
        }

        public Type DataType
        {
            get
            {
                return _iidOutput.Type;
            }
        }

        public IEmulator Emulator
        {
            get
            {
                return _emulator;
            }
            set
            {
                _emulator = value;
            }
        }

        public int DataSourceId
        {
            get
            {
                return _dataSourceId;
            }
            set
            {
                _dataSourceId = value;
            }
        }


        #endregion
    }
}
