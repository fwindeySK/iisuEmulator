using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iisuEmulator
{
    interface IEmulatorView
    {
        event EventHandler<EventArgs> PlayPressed;
        event EventHandler<OpenIIDProjectEventArgs> IIDProjectOpened;
        event EventHandler<ChangedMappingEventArgs> MappingChanged;
        event EventHandler<ChangedMappingEventArgs> EmulatorChanged;
        event EventHandler<ChangedStartStopMappingEventArgs> StartStopMappingChanged;
        event EventHandler<ChangedMappingEnabledEventArgs> MappingEnabledChanged;
        event EventHandler<SaveEventArgs> SaveProject;
        event EventHandler<LoadEventArgs> LoadProject;
        event EventHandler<EventArgs> NewProject;
        event EventHandler<EventArgs> LaunchToolBox;
        event EventHandler<EventArgs> Quit;
        event EventHandler<OpenIIDProjectEventArgs> OpenIIDProject;

        void SetPlayingIcon(bool playing);
        void AddMapping(IMapping mapping, string[] emulatorItems, int defaultValueIndex, MappingType mappingType);
        void AddStartStopMapping(string item);
        void ClearStartStopMapping();
        void ClearMappings();
        void UpdateDataSourceItems(int row, string[] dataSourceItems);
        void SetIIDProjectPath(string path);
        void SetStartStopMapping(string iidOutputPath);
        void UpdateMappings(IMapping mapping, string[] dataSourceItems, string dataSource);
        string GetIIDProjectFile(string file);
        void SetRuntimeCriticalControlsEnabled(bool enabled);
        void ShowPopUp(string message);
    }
}
