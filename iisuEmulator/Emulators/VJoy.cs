using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace iisuEmulator.Emulators
{
    class VJoy
    {
        [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
        public struct JoystickState
        {
            public byte ReportId;
            public byte Slider;
            public byte XAxis;
            public byte YAxis;
            public byte ZAxis;
            public byte ZRotation;
            public byte POV;
            public ushort Buttons;
        };

        [DllImport("VJoy.dll", CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool VJoy_Initialize(StringBuilder name, StringBuilder serial);

        [DllImport("VJoy.dll", CallingConvention = CallingConvention.StdCall)]
        private static extern void VJoy_Shutdown();

        [DllImport("VJoy.dll", CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool VJoy_UpdateJoyState(int id, ref JoystickState joyState);

        private JoystickState[] m_joyState;

        public VJoy()
        {
        }

        public bool Initialize()
        {
            m_joyState = new JoystickState[2];

            m_joyState[0] = new JoystickState();
            m_joyState[1] = new JoystickState();

            StringBuilder Name = new StringBuilder("Softkinetic (iisu Device Emulator)");
            StringBuilder Serial = new StringBuilder("c724c01fadd4967b398d742b2b637676");

            return VJoy_Initialize(Name, Serial);
        }

        public void Shutdown()
        {
            VJoy_Shutdown();
        }

        public bool Update(int id)
        {
            return VJoy_UpdateJoyState(id, ref m_joyState[id]);
        }

        public void Reset()
        {
            m_joyState[0].ReportId = 0;
            m_joyState[0].Slider = 0;
            m_joyState[0].XAxis = 0;
            m_joyState[0].YAxis = 0;
            m_joyState[0].ZAxis = 0;
            m_joyState[0].ZRotation = 0;
            m_joyState[0].POV = 0;
            m_joyState[0].Buttons = 0;

            m_joyState[1].ReportId = 0;
            m_joyState[1].Slider = 0;
            m_joyState[1].XAxis = 0;
            m_joyState[1].YAxis = 0;
            m_joyState[1].ZAxis = 0;
            m_joyState[1].ZRotation = 0;
            m_joyState[1].POV = 0;
            m_joyState[1].Buttons = 0;
        }

        public byte GetSlider(int index)
        {
            return m_joyState[index].Slider;
        }

        public void SetSlider(int index, byte value)
        {
            m_joyState[index].Slider = value;
        }

        public byte GetXAxis(int index)
        {
            return m_joyState[index].XAxis;
        }

        public void SetXAxis(int index, byte value)
        {
            m_joyState[index].XAxis = value;
        }

        public byte GetYAxis(int index)
        {
            return m_joyState[index].YAxis;
        }

        public void SetYAxis(int index, byte value)
        {
            m_joyState[index].YAxis = value;
        }

        public byte GetZAxis(int index)
        {
            return m_joyState[index].ZAxis;
        }

        public void SetZAxis(int index, byte value)
        {
            m_joyState[index].ZAxis = value;
        }

        public byte GetZRotation(int index)
        {
            return m_joyState[index].ZRotation;
        }

        public void SetZRotation(int index, byte value)
        {
            m_joyState[index].ZRotation = value;
        }

        public void SetButton(int index, int button, bool value)
        {
            if(value)
                m_joyState[index].Buttons |= (ushort)(1 << button);
            else
                m_joyState[index].Buttons &= (ushort)~(1 << button);
        }

        public bool GetButton(int index, int button)
        {
            return ((m_joyState[index].Buttons & (1 << button)) == 1);
        }
    }
}
