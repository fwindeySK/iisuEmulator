using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iisuEmulator.Emulators;

namespace iisuEmulator
{
    public class DigitalMappingBool : IMapping
    {
        private IIDOutput<bool> _iidOutput;
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

        public IIDOutput<bool> IIDOutput
        {
            get
            {
                return _iidOutput;
            }
        }

        public DigitalMappingBool(IIDOutput<bool> iidOutput, IEmulator emulator)
        {
            _iidOutput = iidOutput;
            _emulator = emulator;
            _dataSourceId = -1;
        }

        #region IMapping Members

        public void Update()
        {
            if (_emulator != null && _dataSourceId != -1)
            {
                _emulator.SetDigitalDataSource(_dataSourceId, _iidOutput.Value);
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
                return MappingType.DIGITAL;
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
