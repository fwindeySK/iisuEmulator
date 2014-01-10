using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace iisuEmulator
{
    public class INIReader
    {
        private readonly string _path;

        public INIReader(string path)
        {
            _path = path;
        }

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section,
                 string key, string def, StringBuilder retVal,
            int size, string filePath);


        public string IniReadValue(string section, string key)
        {
            StringBuilder output = new StringBuilder(255);
            int i = GetPrivateProfileString(section, key, "", output,
                                            255, _path);
            return output.ToString();

        }

    }
}
