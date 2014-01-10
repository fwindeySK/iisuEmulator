using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iisuEmulator
{
    public class OpenIIDProjectEventArgs : EventArgs
    {
        public string Path;

        public OpenIIDProjectEventArgs(string path)
        {
            Path = path;
        }
    }

    public class ChangedStartStopMappingEventArgs : EventArgs
    {
        public string IIDOutput;

        public ChangedStartStopMappingEventArgs(string iidOutput)
        {
            IIDOutput = iidOutput;
        }
    }

    public class ChangedMappingEventArgs : EventArgs
    {
        public string IIDPath;
        public string Emulator;
        public string EmulatedDataSource;
        public int Row;
        public string Type;

        public ChangedMappingEventArgs(string path, string emulator, string emulatedDataSource, string type, int row)
        {
            IIDPath = path;
            EmulatedDataSource = emulatedDataSource;
            Emulator = emulator;
            Row = row;
            Type = type;
        }
    }

    public class ChangedMappingEnabledEventArgs : EventArgs
    {
        public bool Enabled;
        public string IIDOutput;

        public ChangedMappingEnabledEventArgs(bool enabled, string iidOutput)
        {
            Enabled = enabled;
            IIDOutput = iidOutput;
        }
    }

    public class SaveEventArgs : EventArgs
    {
        public string IIDProjectPath;
        public string SavePath;

        public SaveEventArgs(string iidProjectPath, string savePath)
        {
            IIDProjectPath = iidProjectPath;
            SavePath = savePath;
        }
    }

    public class LoadEventArgs : EventArgs
    {
        public string Path;

        public LoadEventArgs(string path)
        {
            Path = path;
        }
    }
}
