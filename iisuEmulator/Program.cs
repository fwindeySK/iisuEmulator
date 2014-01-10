using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using iisuEmulator.Exceptions;
using System.IO;

namespace iisuEmulator
{
    static class Program
    {
        private static IisuEmulatorController _emulatorController;
        private static EmulatorView _emulatorForm;

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool AllocConsole();

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool FreeConsole();

        [DllImport("kernel32", SetLastError = true)]
        static extern bool AttachConsole(int dwProcessId);

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", SetLastError = true)]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            string value;
            bool console = getArgumentValue("-console", args, out value);

            if (getArgumentValue("-help", args, out value) || getArgumentValue("--help", args, out value) || getArgumentValue("/?", args, out value))
            {
                IntPtr ptr = GetForegroundWindow();

                int u;

                GetWindowThreadProcessId(ptr, out u);

                Process process = Process.GetProcessById(u);

                if (process.ProcessName == "cmd")
                {
                    AttachConsole(process.Id);
                }
                else
                {
                    AllocConsole();
                }

                Console.WriteLine("\n\nSoftKinetic iisu Emulator console help:\n");
                Console.WriteLine("--help -help /?       \t console commands");
                Console.WriteLine("-project projectName\t open iisu emulator project");
                Console.WriteLine("-console \t run iisu emulator in console mode");
                Console.WriteLine("-process processName \t close iisu emulator when this process is ended");

                FreeConsole();
            }
            else if (!console)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                _emulatorForm = new EmulatorView();
                if (getArgumentValue("-project", args, out value))
                {
                    try
                    {
                        _emulatorController = new IisuEmulatorController(_emulatorForm, value);
                    }
                    catch (Exception e)
                    {
                        _emulatorForm.ShowPopUp(e.Message);
                    }
                }
                else if (args.Length > 0 && File.Exists(args[0]))
                {
                    try
                    {
                        _emulatorController = new IisuEmulatorController(_emulatorForm, args[0]);
                    }
                    catch (Exception e)
                    {
                        _emulatorForm.ShowPopUp(e.Message);
                    }
                }
                else
                {
                    try
                    {
                        _emulatorController = new IisuEmulatorController(_emulatorForm);
                    }
                    catch (Exception e)
                    {
                        _emulatorForm.ShowPopUp(e.Message);
                    }
                }
                Application.Run(_emulatorForm);
            }
            else if (console)
            {
                if (getArgumentValue("-project", args, out value))
                {
                    //Get a pointer to the forground window.  The idea here is that
                    //IF the user is starting our application from an existing console
                    //shell, that shell will be the uppermost window.  We'll get it
                    //and attach to it
                    IntPtr ptr = GetForegroundWindow();

                    int u;

                    GetWindowThreadProcessId(ptr, out u);

                    Process process = Process.GetProcessById(u);

                    if (process.ProcessName == "cmd")    //Is the uppermost window a cmd process?
                    {
                        AttachConsole(process.Id);
                    }
                    else
                    {
                        //no console AND we're in console mode ... create a new console.
                        AllocConsole();
                    }

                    Console.WriteLine("\n\n");
                    Console.WriteLine("SoftKinetic iisu Emulator");
                    Console.WriteLine("-------------------------");
                    Console.WriteLine("Emulator project: " + value);

                    try
                    {
                        _emulatorController = new IisuEmulatorController(value);
                    }
                    catch (EmulatorProjectNotFoundException e)
                    {
                        Console.WriteLine(e.Message + " You could try to resave the emulator project in the GUI mode.");
                    }
                    catch (IIDProjectNotFoundException e)
                    {
                        Console.WriteLine(e.Message + " You could try to resave the emulator project in the GUI mode.");
                    }
                    catch (IIDPathNotFoundException e)
                    {
                        Console.WriteLine(e.Message + " The outputs of the IID project have probably been changed.");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }

                    if (getArgumentValue("-process", args, out value))
                    {
                        if (File.Exists(value))
                        {
                            Console.WriteLine("Starting process " + value);
                            Process monitoredProcess = new Process();
                            ProcessStartInfo info = monitoredProcess.StartInfo;
                            info.FileName = value;
                            monitoredProcess.Start();
                            monitoredProcess.WaitForExit();
                            Console.WriteLine("Process ended. Quiting iisu Emulator");
                            if(_emulatorController != null)
                                _emulatorController.Quit();
                            FreeConsole();
                        }
                        else
                        {
                            Console.WriteLine("Invalid process path: " + value + ". The file can not be found."); 
                        }
                    }
                }
            }
        }

        private static bool getArgumentValue(string argument, string[] args, out string value)
        {
            for (int i = 0; i < args.Length; ++i)
            {
                if (args[i] == argument)
                {
                    if (i + 1 < args.Length)
                    {
                        value = args[i + 1];
                        return true;
                    }
                    else
                    {
                        value = "";
                        return true;
                    }
                }
            }
            
            value = "";
            return false;
        }
    }
}
