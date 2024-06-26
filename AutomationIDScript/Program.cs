using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Automation;

namespace AutomationIDScript
{
    public class Program
    {
        public static int Main(string[] args)
        {
            if(args.Length < 3) 
            {
                Console.WriteLine("Not enough arguments provided");
                Console.WriteLine("ex: ProcessName AutomationID InputText");
                return 1;
            }

            string ProcessName = args[0];
            string AutomationID = args[1];
            string InputText = args[2];

            try
            {
                Process[] proc = Process.GetProcessesByName(ProcessName);

                if (proc.Length > 0)
                {
                    // Gets the process handle
                    IntPtr processHndl = GetProcessHandle(proc[0]);

                    // Bring process to forground (gives it focus)
                    SetForegroundWindow(processHndl);

                    // Finds First Textbox found in given process
                    var textBoxElement = AutomationElement.RootElement.FindFirst(TreeScope.Descendants,
                        new PropertyCondition(AutomationElement.AutomationIdProperty, AutomationID));

                    // Attempts to get the textbox and sets the provided value
                    if (textBoxElement.TryGetCurrentPattern(ValuePattern.Pattern, out object textBox))
                    {
                        ((ValuePattern)textBox).SetValue(InputText);

                        // This can be removed or shortened. Just to visually verify the text was set.
                        Thread.Sleep(2000);

                        // Simulate pressing the Enter key
                        SendMessage(processHndl, WM_KEYDOWN, (IntPtr)VK_ENTER, IntPtr.Zero);
                        SendMessage(processHndl, WM_KEYUP, (IntPtr)VK_ENTER, IntPtr.Zero);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Error occurred: {0}", ex.Message));
                return 1;
            }

            return 0;
        }

        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("USER32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        const int WM_KEYDOWN = 0x0100;
        const int WM_KEYUP = 0x0101;
        const int VK_ENTER = 0x0D;

        public static IntPtr GetProcessHandle(Process process)
        {
            process.Refresh();
            return (process.MainWindowHandle != IntPtr.Zero) ? process.MainWindowHandle : IntPtr.Zero;
        }
    }
}
