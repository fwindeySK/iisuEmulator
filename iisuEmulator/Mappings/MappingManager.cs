using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iisuEmulator.Mappings;
using iisuEmulator.Exceptions;
using System.Windows.Forms;

namespace iisuEmulator
{
    public class MappingManager
    {
        private Dictionary<string,IMapping> _mappings;
        private IStartStopMapping _startStopMapping;

        public IStartStopMapping StartStopMapping
        {
            get
            {
                return _startStopMapping;
            }
            set
            {
                _startStopMapping = value;
            }
        }

        public MappingManager()
        {
            _mappings = new Dictionary<string, IMapping>();
        }

        public void ClearMappings()
        {
            _mappings.Clear();
        }

        public void AddMapping(IMapping mapping)
        {
            _mappings.Add(mapping.IIDOutputPath, mapping);
        }

        public void UpdateMappings()
        {
            if (_startStopMapping != null && !_startStopMapping.IsActive)
                return;

            foreach (IMapping mapping in _mappings.Values)
            {
                if (mapping.Enabled)
                {
                    mapping.Update();
                }
            }
        }

        public IMapping GetMapping(string iidOutputPath)
        {
            try
            {
                return _mappings[iidOutputPath];
            }
            catch (KeyNotFoundException)
            {
                if (MessageBox.Show("IID Path not found: " + iidOutputPath) == DialogResult.OK)
                {
                    throw new IIDPathNotFoundException("IID Path not found: " + iidOutputPath);
                }
                return null;
            }
        }

        public IMapping[] Mappings
        {
            get
            {
                return _mappings.Values.ToArray();
            }
        }
    }
}
