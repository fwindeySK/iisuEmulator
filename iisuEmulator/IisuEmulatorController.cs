using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Iisu;
using iisuEmulator.Emulators;
using iisuEmulator.Exceptions;
using iisuEmulator.Mappings;
using iisuEmulator.Persistence;
using System.IO;

namespace iisuEmulator
{
    class IisuEmulatorController
    {
        private IEmulatorView _emulatorView;
        private IisuInputProvider _inputProvider;
        private MappingManager _mappingManager;
        private EmulatorManager _emulatorManager;
        private PersistenceManager _persistenceManager;

        private object updateLock = new object();

        private bool _consoleMode;

        private void initializeViewEvents(IEmulatorView view)
        {
            view.PlayPressed += new EventHandler<EventArgs>(view_PlayPressed);
            view.IIDProjectOpened += new EventHandler<OpenIIDProjectEventArgs>(view_IIDProjectOpened);
            view.MappingChanged += new EventHandler<ChangedMappingEventArgs>(view_MappingChanged);
            view.EmulatorChanged += new EventHandler<ChangedMappingEventArgs>(view_EmulatorChanged);
            view.StartStopMappingChanged += new EventHandler<ChangedStartStopMappingEventArgs>(view_StartStopMappingChanged);
            view.MappingEnabledChanged += new EventHandler<ChangedMappingEnabledEventArgs>(view_MappingEnabledChanged);
            view.SaveProject += new EventHandler<SaveEventArgs>(view_Save);
            view.LoadProject += new EventHandler<LoadEventArgs>(view_Load);
            view.NewProject += new EventHandler<EventArgs>(view_NewProject);
            view.LaunchToolBox += new EventHandler<EventArgs>(view_LaunchToolBox);
            view.Quit += new EventHandler<EventArgs>(view_Quit);
            view.OpenIIDProject += new EventHandler<OpenIIDProjectEventArgs>(view_OpenIIDProject);
        }

        private void initializeEmulatorProject(string emulatorProjectPath, bool consoleMode)
        {
            _consoleMode = consoleMode;

            string iidProject = "";
            try
            {
                iidProject = _persistenceManager.GetIIDProjectPath(emulatorProjectPath);
            }
            catch (FileNotFoundException)
            {
                throw new EmulatorProjectNotFoundException("Emulator project not found.");
            }
            if (!File.Exists(iidProject))
            {
                if (consoleMode)
                {
                    throw new IIDProjectNotFoundException("IID Project not found.");
                }
                else
                {
                    iidProject = _emulatorView.GetIIDProjectFile(iidProject);
                }
            }

            loadIIDProject(iidProject);
            if (!consoleMode)
            {
                _emulatorView.SetIIDProjectPath(iidProject);
            }
            string startStopMapping = _persistenceManager.GetStartStopMappingName(emulatorProjectPath);
            setStartStopMapping(startStopMapping);
            if (!consoleMode && startStopMapping != "NONE")
            {
                _emulatorView.SetStartStopMapping(startStopMapping);
            }

            _persistenceManager.UpdateMappings(emulatorProjectPath, _mappingManager, _emulatorManager);

            if (!consoleMode)
            {
                IMapping[] mappings = _mappingManager.Mappings;
                updateViewMappings(mappings);
            }
            if (consoleMode)
            {
                _inputProvider.Start();
            }
        }

        public IisuEmulatorController(IEmulatorView view)
        {
            _consoleMode = false;

            _emulatorView = view;
            initializeViewEvents(view);
            initializeManagers();
            initializeEmulators();
        }

        public IisuEmulatorController(IEmulatorView view, string emulatorProjectPath)
        {
            _emulatorView = view;
            initializeViewEvents(view);
            initializeManagers();
            initializeEmulators();
            initializeEmulatorProject(emulatorProjectPath, false);
        }

        public IisuEmulatorController(string emulatorProjectPath)
        {
            initializeManagers();
            initializeEmulators();
            initializeEmulatorProject(emulatorProjectPath, true);
        }

        private void initializeManagers()
        {
            _inputProvider = new IisuInputProvider();
            _inputProvider.DeviceUpdated += new EventHandler<EventArgs>(_inputProvider_DeviceUpdated);
            _mappingManager = new MappingManager();
            _persistenceManager = new PersistenceManager();
            _emulatorManager = new EmulatorManager();
        }

        private void initializeEmulators()
        {
            INIReader iniReader = new INIReader(Directory.GetCurrentDirectory() + "/settings.ini");

            int virtualJoystickNumber;
            if (!int.TryParse(iniReader.IniReadValue("PPJoy configuration", "VirtualJoystickNumber"), out virtualJoystickNumber))
            {
                virtualJoystickNumber = 0;
            }

            try
            {
                JoystickEmulator joyEmu = new JoystickEmulator(virtualJoystickNumber);
                _emulatorManager.AddEmulator(joyEmu);
            }
            catch (VirtualJoystickNotFoundException)
            {

            }

            MouseEmulator mouseEmu = new MouseEmulator();
            _emulatorManager.AddEmulator(mouseEmu);

            KeyboardEmulator keyboardEmu = new KeyboardEmulator();
            _emulatorManager.AddEmulator(keyboardEmu);

            try
            {
                ArduinoFirmataEmulator arduinoEmu = new ArduinoFirmataEmulator(iniReader.IniReadValue("Arduino configuration", "COMPort"));
                _emulatorManager.AddEmulator(arduinoEmu);
            }
            catch (DeviceNotConnectedException)
            {

            }
        }

        private bool openIIDProject(string path, out string errorMessage)
        {
            if (File.Exists(path))
            {
                if (!_inputProvider.OpenIIDProject(path))
                {
                    errorMessage = "Interaction Designer process already running.";
                    return false;
                }
                else
                {
                    errorMessage = "Loaded IID project successfully";
                    return true;   
                }
            }
            else
            {
                errorMessage = "IID Project file not found";
                return false;
            }
        }

        public void Quit()
        {
            _inputProvider.Dispose();
            string[] emulatorNames = _emulatorManager.GetEmulatorList();
            foreach (string emulatorName in emulatorNames)
            {
                IEmulator emulator = _emulatorManager.GetEmulator(emulatorName);
                if (emulator != null)
                    emulator.ShutDown();
            }
        }

        private void updateViewMappings(IMapping[] mappings)
        {
            for (int i = 0; i < mappings.Length; ++i)
            {
                string dataSource = "";
                if (mappings[i].Type == MappingType.ANALOG)
                {
                    if (mappings[i].Emulator != null && mappings[i].DataSourceId >= 0)
                        dataSource = mappings[i].Emulator.AnalogDataSourceNames[mappings[i].DataSourceId];
                    else
                        dataSource = "NONE";
                }
                else
                {
                    if (mappings[i].Emulator != null && mappings[i].DataSourceId >= 0)
                        dataSource = mappings[i].Emulator.DigitalDataSourceNames[mappings[i].DataSourceId];
                    else
                        dataSource = "NONE";
                }

                string[] dataSourceItems = { "NONE" };

                if (mappings[i].Emulator != null)
                {
                    if (mappings[i].Type == MappingType.ANALOG)
                    {
                        dataSourceItems = mappings[i].Emulator.AnalogDataSourceNames;
                    }
                    else
                    {
                        dataSourceItems = mappings[i].Emulator.DigitalDataSourceNames;
                    }
                }

                _emulatorView.UpdateMappings(mappings[i], dataSourceItems, dataSource);
            }
        }

        void view_OpenIIDProject(object sender, OpenIIDProjectEventArgs e)
        {
            string errorMessage;
            if (!openIIDProject(e.Path, out errorMessage))
            {
                _emulatorView.ShowPopUp(errorMessage);
            }
        }

        void view_Quit(object sender, EventArgs e)
        {
            Quit();
        }

        void view_LaunchToolBox(object sender, EventArgs e)
        {
            _inputProvider.StartToolBox();
        }

        void view_NewProject(object sender, EventArgs e)
        {
            _mappingManager.ClearMappings();
            _emulatorView.SetIIDProjectPath("");
            _emulatorView.ClearMappings();
            _emulatorView.ClearStartStopMapping();
        }

        void view_Load(object sender, LoadEventArgs e)
        {
            string iidProject = _persistenceManager.GetIIDProjectPath(e.Path);

            if (!File.Exists(iidProject))
            {
                iidProject = _emulatorView.GetIIDProjectFile(iidProject);
            }

            if(!File.Exists(iidProject))
            {
                throw new IIDProjectNotFoundException("IID Project not found.");
            }

            loadIIDProject(iidProject);
            _emulatorView.SetIIDProjectPath(iidProject);

            string startStopMapping = _persistenceManager.GetStartStopMappingName(e.Path);
            if (startStopMapping != "NONE")
            {
                _emulatorView.SetStartStopMapping(startStopMapping);
            }
            setStartStopMapping(startStopMapping);

            _persistenceManager.UpdateMappings(e.Path, _mappingManager, _emulatorManager);
            IMapping[] mappings = _mappingManager.Mappings;
            updateViewMappings(mappings);
        }

        void view_Save(object sender, SaveEventArgs e)
        {
            _persistenceManager.StoreProject(_mappingManager.Mappings, e.IIDProjectPath, _mappingManager.StartStopMapping, e.SavePath);
        }

        void view_MappingEnabledChanged(object sender, ChangedMappingEnabledEventArgs e)
        {
            lock(updateLock)
            {
                IMapping mapping = _mappingManager.GetMapping(e.IIDOutput);
                mapping.Enabled = e.Enabled;
            }
        }

        void _inputProvider_DeviceUpdated(object sender, EventArgs e)
        {
            lock (updateLock)
            {
                _mappingManager.UpdateMappings();
            }
        }

        private void setStartStopMapping(string iidOutput)
        {
            if (iidOutput == "NONE")
            {
                _mappingManager.StartStopMapping = null;
            }
            else
            {
                IMapping mapping = _mappingManager.GetMapping(iidOutput);
                if (mapping.DataType == typeof(int))
                {
                    DigitalMappingInt intMapping = (DigitalMappingInt)mapping;
                    StartStopMappingInt startStop = new StartStopMappingInt(intMapping.IIDOutput);
                    _mappingManager.StartStopMapping = startStop;
                }
                else
                {
                    DigitalMappingBool boolMapping = (DigitalMappingBool)mapping;
                    StartStopMappingBool startStop = new StartStopMappingBool(boolMapping.IIDOutput);
                    _mappingManager.StartStopMapping = startStop;
                }
            }
        }

        void view_StartStopMappingChanged(object sender, ChangedStartStopMappingEventArgs e)
        {
            lock (updateLock)
            {
                setStartStopMapping(e.IIDOutput);
            }
        }

        private void setEmulator(string iidOutput, int viewRow, string emulatorName)
        {
            IMapping mapping = _mappingManager.GetMapping(iidOutput);

            if (emulatorName != "NONE")
            {
                IEmulator emulator = _emulatorManager.GetEmulator(emulatorName);
                if (mapping.Emulator == null || mapping.Emulator.Name != emulatorName)
                {
                    mapping.Emulator = emulator;
                    if (mapping.Type == MappingType.ANALOG)
                    {
                        _emulatorView.UpdateDataSourceItems(viewRow, emulator.AnalogDataSourceNames);
                        mapping.DataSourceId = mapping.Emulator.GetAnalogDataSourceID(emulator.AnalogDataSourceNames[0]);
                    }
                    else
                    {
                        _emulatorView.UpdateDataSourceItems(viewRow, emulator.DigitalDataSourceNames);
                        mapping.DataSourceId = mapping.Emulator.GetDigitalDataSourceID(emulator.DigitalDataSourceNames[0]);
                    }
                }
            }
            else
            {
                string[] items = { "NONE" };
                _emulatorView.UpdateDataSourceItems(viewRow, items);
                mapping.Emulator = null;
            }
        }

        void view_EmulatorChanged(object sender, ChangedMappingEventArgs e)
        {
            lock (updateLock)
            {
                setEmulator(e.IIDPath, e.Row, e.Emulator);
            }
        }

        void view_MappingChanged(object sender, ChangedMappingEventArgs e)
        {
            lock (updateLock)
            {
                IMapping mapping = _mappingManager.GetMapping(e.IIDPath);

                if (mapping.Emulator != null)
                {
                    if (e.Type == "ANALOG")
                        mapping.DataSourceId = mapping.Emulator.GetAnalogDataSourceID(e.EmulatedDataSource);
                    else
                        mapping.DataSourceId = mapping.Emulator.GetDigitalDataSourceID(e.EmulatedDataSource);
                }
            }
        }

        private void loadIIDProject(string iidProjectPath)
        {
            lock (updateLock)
            {
                _inputProvider.LoadIIDGraph(iidProjectPath);
                List<string> iidOutputPaths = _inputProvider.GetIIDDataPaths();

                if (!_consoleMode)
                {
                    _emulatorView.ClearMappings();
                    _emulatorView.ClearStartStopMapping();
                    _emulatorView.AddStartStopMapping("NONE");
                }
                _mappingManager.ClearMappings();

                registerIIDOutputs(iidOutputPaths);
            }
        }

        void view_IIDProjectOpened(object sender, OpenIIDProjectEventArgs e)
        {
            loadIIDProject(e.Path);
        }

        private void registerIIDOutputs(List<string> iidOutputPaths)
        {
            foreach (string iidOutputPath in iidOutputPaths)
            {
                string type = _inputProvider.GetIIDOutputType(iidOutputPath);

                switch (type)
                {
                    case "float":
                        addAnalogMapping(iidOutputPath);
                        break;

                    case "int":
                        addDigitalMapping(iidOutputPath, false);
                        break;

                    case "bool":
                        addDigitalMapping(iidOutputPath, true);
                        break;
                }
            }
        }

        private void addAnalogMapping(string iidDataPath)
        {
            IDataHandle<float> dataHandle = _inputProvider.RegisterDataHandle<float>(iidDataPath);
            IIDOutput<float> iidOutput = new IIDOutput<float>(dataHandle);
            AnalogMapping mapping = new AnalogMapping(iidOutput, null);
            _mappingManager.AddMapping(mapping);

            if(!_consoleMode)
                _emulatorView.AddMapping(mapping, _emulatorManager.GetEmulatorList(), 0, MappingType.ANALOG);
        }
        
        private void addDigitalMapping(string iidDataPath, bool booleanMapping)
        {
            if (booleanMapping)
            {
                IDataHandle<bool> dataHandle = _inputProvider.RegisterDataHandle<bool>(iidDataPath);
                IIDOutput<bool> iidOutput = new IIDOutput<bool>(dataHandle);
                DigitalMappingBool mapping = new DigitalMappingBool(iidOutput, null);
                _mappingManager.AddMapping(mapping);

                if (!_consoleMode)
                {
                    _emulatorView.AddMapping(mapping, _emulatorManager.GetEmulatorList(), 0, MappingType.DIGITAL);
                    _emulatorView.AddStartStopMapping(dataHandle.Path);
                }
            }
            else
            {
                IDataHandle<int> dataHandle = _inputProvider.RegisterDataHandle<int>(iidDataPath);
                IIDOutput<int> iidOutput = new IIDOutput<int>(dataHandle);
                DigitalMappingInt mapping = new DigitalMappingInt(iidOutput, null);
                _mappingManager.AddMapping(mapping);

                if (!_consoleMode)
                {
                    _emulatorView.AddMapping(mapping, _emulatorManager.GetEmulatorList(), 0, MappingType.DIGITAL);
                    _emulatorView.AddStartStopMapping(dataHandle.Path);
                }
            }
        }

        void view_PlayPressed(object sender, EventArgs e)
        {
            if (_inputProvider.Running)
            {
                _inputProvider.Stop();
                _emulatorView.SetPlayingIcon(false);
                _emulatorView.SetRuntimeCriticalControlsEnabled(true);
            }
            else
            {
                _inputProvider.Start();
                _emulatorView.SetPlayingIcon(true);
                _emulatorView.SetRuntimeCriticalControlsEnabled(false);
            }
        }

    }
}
