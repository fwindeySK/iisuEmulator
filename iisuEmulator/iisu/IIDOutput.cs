using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Iisu;

namespace iisuEmulator
{
    public class IIDOutput<T>
    {
        private IDataHandle<T> _iidDataHandle;
        private Type _t;

        public Type Type
        {
            get
            {
                return _t;
            }
        }

        public T Value
        {
            get
            {
                return _iidDataHandle.Value;
            }
        }

        public string Path
        {
            get
            {
                return _iidDataHandle.Path;
            }
        }

        public IIDOutput(IDataHandle<T> dataHandle)
        {
            _iidDataHandle = dataHandle;
            _t = typeof(T);
        }

    }
}
