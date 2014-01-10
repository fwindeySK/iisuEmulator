using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WindowsInput;
using System.Drawing;

namespace iisuEmulator.Emulators
{
    public class MouseEmulator : IEmulator
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct CursorInfo
        {
            public int Size;
            public int Flags;
            public IntPtr Handle;
            public Point Position;
        }

        [DllImport("user32.dll")]
        private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, uint dwExtraInf);

        [DllImport("user32.dll")]
        public static extern bool GetCursorInfo(out CursorInfo info);

        private string[] _analogDataSourceNames = { "X", "Y", "Mouse Wheel up/down" };
        private string[] _digitalDataSourceNames = { "Left button down/up" , "Right button down/up", "Middle button down/up"};

        private const UInt32 MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const UInt32 MOUSEEVENTF_LEFTUP = 0x0004;
        private const UInt32 MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const UInt32 MOUSEEVENTF_RIGHTUP = 0x10;
        private const UInt32 MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        private const UInt32 MOUSEEVENTF_MIDDLEUP = 0x0040;
        private const UInt32 MOUSEEVENTF_ABSOLUTE = 0x8000;
        private const UInt32 MOUSEEVENTF_MOVE = 0x0001;
        private const UInt32 MOUSEEVENTF_WHEEL = 0x0800;

        private bool _leftMouseDown;
        private bool _rightMouseDown;
        private bool _middleMouseDown;

        private int _cursurInfoSize;

        public MouseEmulator()
        {
            _leftMouseDown = false;
            _rightMouseDown = false;
            _middleMouseDown = false;

            CursorInfo info = new CursorInfo();
            _cursurInfoSize = Marshal.SizeOf(info.GetType());
        }

        #region IEmulator Members

        public string[] AnalogDataSourceNames
        {
            get 
            {
                return _analogDataSourceNames;
            }
        }

        public string[] DigitalDataSourceNames
        {
            get 
            {
                return _digitalDataSourceNames;
            }
        }

        public void SetAnalogDataSource(int id, float value)
        {
            CursorInfo info = new CursorInfo();
            info.Size = _cursurInfoSize;
            if (id == 0)
            {
                //Cursor.Position = new System.Drawing.Point((int)(value * SystemInformation.PrimaryMonitorSize.Width), Cursor.Position.Y);
                
                if (GetCursorInfo(out info))
                {
                    mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE, (uint)(value * 65535), (uint)(((float)info.Position.Y / SystemInformation.PrimaryMonitorSize.Height) * 65535), 0, 0);
                }
            }
            else if (id == 1)
            {
                //Cursor.Position = new System.Drawing.Point(Cursor.Position.X, (int)(value * SystemInformation.PrimaryMonitorSize.Height));
                if (GetCursorInfo(out info))
                {
                    mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE, (uint)(((float)info.Position.X / SystemInformation.PrimaryMonitorSize.Width) * 65535), (uint)(value * 65535), 0, 0);
                }
            }
            else if (id == 2)
            {
                uint wheelMove = (uint)(-120f + 240f * value);
                mouse_event(MOUSEEVENTF_WHEEL, 0, 0, wheelMove, 0);
            }
        }

        public void SetDigitalDataSource(int id, int value)
        {
            //Left mouse button
            if (id == 0)
            {
                if (value == 0 && _leftMouseDown)
                {
                    mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                    _leftMouseDown = false;
                }
                else if(value != 0 && !_leftMouseDown)
                {
                    mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                    _leftMouseDown = true;
                }
            }
            //Right mouse button
            else if(id == 1)
            {
                if (value == 0 && _rightMouseDown)
                {
                    mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
                    _rightMouseDown = false;
                }
                else if (value != 0)
                {
                    mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
                    _rightMouseDown = true;
                }
            }
            //Middle mouse button
            else if (id == 2)
            {
                if (value == 0 && _middleMouseDown)
                {
                    mouse_event(MOUSEEVENTF_MIDDLEUP, 0, 0, 0, 0);
                    _middleMouseDown = false;
                }
                else if (value != 0)
                {
                    mouse_event(MOUSEEVENTF_MIDDLEDOWN, 0, 0, 0, 0);
                    _middleMouseDown = true;
                }
            }
        }

        public void SetDigitalDataSource(int id, bool value)
        {
            if (value)
            {
                SetDigitalDataSource(id, 1);
            }
            else
            {
                SetDigitalDataSource(id, 0);
            }
        }

        public int GetAnalogDataSourceID(string dataSourceName)
        {
            int i = 0;
            while (i < _analogDataSourceNames.Length && _analogDataSourceNames[i] != dataSourceName)
            {
                ++i;
            }

            if (i != _analogDataSourceNames.Length)
                return i;
            else
                return -1;
        }

        public int GetDigitalDataSourceID(string dataSourceName)
        {
            int i = 0;
            while (i < _digitalDataSourceNames.Length && _digitalDataSourceNames[i] != dataSourceName)
            {
                ++i;
            }

            if (i != _digitalDataSourceNames.Length)
                return i;
            else
                return -1;
        }

        public string Name
        {
            get
            {
                return "Mouse";
            }
        }

        public void ShutDown()
        {
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
            mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
            mouse_event(MOUSEEVENTF_MIDDLEUP, 0, 0, 0, 0);
        }

        #endregion
    }
}
