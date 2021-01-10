using System;
using System.Diagnostics;
using System.Threading;

namespace Celeste_Launcher_Gui.Win32
{
    public static class ProcesInvoker
    {
        public static void StartNewProcessAsDialog(string processName, string processArguments = "", bool runAsElevated = false)
        {
            var process = StartNewProcess(processName, processArguments, runAsElevated);

            var processWindowHandle = WaitForWindowToAppear(process);
            WindowsInterop.SetWindowOwner(processWindowHandle, Process.GetCurrentProcess().MainWindowHandle);

            process.WaitForExit();
        }

        public static Process StartNewProcess(string processName, string processArguments = "", bool runAsElevated = false)
        {
            var startInfo = new ProcessStartInfo(processName)
            {
                Arguments = processArguments,
            };

            if (runAsElevated)
                startInfo.Verb = "runas";

            return Process.Start(startInfo);
        }

        private static IntPtr WaitForWindowToAppear(Process process)
        {
            var timeStartedWaiting = DateTime.Now;

            while (process.MainWindowHandle == IntPtr.Zero && (DateTime.Now - timeStartedWaiting).TotalSeconds < 10)
                Thread.Sleep(100);

            return process.MainWindowHandle;
        }
    }
}
