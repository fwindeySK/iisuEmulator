using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Iisu;
using Iisu.Data;
using System.Threading;
using System.Diagnostics;
using System.IO;

namespace iisuEmulator
{
    public class IisuInputProvider
    {
        private IHandle _handle;
        private IDevice _device;
        private Thread _updateThread;
        private bool _running;
        private IDeviceConfiguration _deviceConfig;

        public event EventHandler<EventArgs> DeviceUpdated;

        public bool Running
        {
            get
            {
                return _running;
            }
        }

        public IisuInputProvider()
        {
            IApplicationConfigurator configurator = Iisu.Iisu.CreateConfigurator("config_server.xml");
            _handle = Iisu.Iisu.Context.CreateHandle(configurator);
            //_handle.SetConfigString("/CONFIG/PROCESSING/CI", "0", false);
            //_handle.SetConfigString("/CONFIG/PROCESSING/SKELETON", "1", false);
            _deviceConfig = configurator.GetDeviceConfiguration();
            _deviceConfig.IsAsynchronous = false;
            _device = _handle.InitializeDevice(_deviceConfig);

            _running = false;
        }

        public void StartToolBox()
        {
            
        }

        public void LoadIIDGraph(string iidProject)
        {
            _device.CommandManager.SendCommand("IID.loadGraph", iidProject);
        }

        public string GetIIDOutputType(string iidOutputPath)
        {
            string[] splitString = _device.GetDataType(iidOutputPath).Split(' ');
            return splitString[splitString.Length - 1];
        }

        public List<string> GetIIDDataPaths()
        {
            List<string> outputs = new List<string>();
            foreach (string data in _device.GetDataNameCollection(false))
            {
                if(data.StartsWith("IID"))
                    outputs.Add(data);
            }

            return outputs;
        }

        public bool OpenIIDProject(string iidProjectPath)
        {
            foreach (Process process in Process.GetProcesses())
            {
                if (process.ProcessName == "iisu_InteractionDesigner")
                    return false;
            }

            Process p = new Process();
            ProcessStartInfo info = p.StartInfo;
            info.FileName = System.Environment.GetEnvironmentVariable("IISU_SDK_DIR") + "/bin/iisu_InteractionDesigner.exe";
            info.WorkingDirectory = System.Environment.GetEnvironmentVariable("IISU_SDK_DIR") + "/bin";
            info.Arguments = info.Arguments + "\"" + iidProjectPath + "\"";
            p.Start();

            return true;
        }

        public IDataHandle<T> RegisterDataHandle<T>(string path)
        {
            return _device.RegisterDataHandle<T>(path);
        }
        
        public void Start()
        {
            if (!_running)
            {
                _running = true;
                _device.Start();
                _updateThread = new Thread(new ThreadStart(updateThread));
                _updateThread.Start();
            }
        }

        public void Stop()
        {
            if (_running)
                _updateThread.Join(2000);
            _running = false;
            _device.Stop(true);
        }

        private void updateThread()
        {
            while (_running)
            {
                _device.UpdateFrame(true);
                if (DeviceUpdated != null)
                {
                    DeviceUpdated(this, new EventArgs());
                }
                _device.ReleaseFrame();
            }
        }

        public void Dispose()
        {
            Stop();
            _handle.Dispose();
        }
    }

}
