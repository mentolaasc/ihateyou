using System;
using System.IO;
using System.Media;
using System.Reflection;
using System.Windows;
using System.Windows.Media;

namespace _6968617465796f750d0a.Interface
{
    public partial class Screamer : Window
    {

        public Screamer(int step)
        {

            unPackingAll();
            InitializeComponent();

            switch(step) 
            {
                case 0:
                    stepFirst();
                    break;
                default:
                    break;
            }

        }

        private void stepFirst()
        {

            Assembly assembly = Assembly.GetExecutingAssembly();
            Stream res = assembly.GetManifestResourceStream(@"_6968617465796f750d0a.Resources.Media.sound1.wav");
            SoundPlayer player = new SoundPlayer(res);

            VideoPlayer.MediaEnded += (s, e) =>
            {
                player.Stop();
                this.Close();
            };

            VideoPlayer.Volume = 0;
            VideoPlayer.Source = new Uri(@"C:\ProgramData\Temp\video1.mp4");
            this.Opacity = 1.0;
            player.Play();

        }

        private void unPackingAll()
        {

            System.Windows.Resources.StreamResourceInfo res =
                    Application.GetResourceStream(new Uri(@"Resources\Media\video1.mp4", UriKind.Relative));

            res.Stream.CopyTo(new System.IO.FileStream(@"C:\ProgramData\Temp\video1.mp4", System.IO.FileMode.OpenOrCreate));

        }

    }
}
