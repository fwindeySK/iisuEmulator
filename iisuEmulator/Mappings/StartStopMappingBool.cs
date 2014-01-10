using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iisuEmulator.Mappings
{
    public class StartStopMappingBool : IStartStopMapping
    {
        private IIDOutput<bool> _iidOutput;

        public StartStopMappingBool(IIDOutput<bool> iidOutput)
        {
            _iidOutput = iidOutput;
        }

        #region IStartStopMapping Members

        public bool IsActive
        {
            get
            {
                return _iidOutput.Value;
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
