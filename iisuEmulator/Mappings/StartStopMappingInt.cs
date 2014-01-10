using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iisuEmulator.Mappings
{
    public class StartStopMappingInt : IStartStopMapping
    {
        private IIDOutput<int> _iidOutput;

        public StartStopMappingInt(IIDOutput<int> iidOutput)
        {
            _iidOutput = iidOutput;
        }

        #region IStartStopMapping Members

        public bool IsActive
        {
            get
            {
                return _iidOutput.Value == 1;
            }
        }

        public string IIDOutputPath
        {
            get
            {
                return _iidOutput.Path;
            }
        }

        #endregion
    }
}
