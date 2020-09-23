using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Kalimbra.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int SAMPLE_RATE = 44100;
        private const short BITS_PER_SAMPLE = 16;
        
        public MainWindow()
        {
            InitializeComponent();

            //PlaySound(440f);
            // var cts = new CancellationTokenSource();
            // Task.Run(() => PlayMelody(cts.Token, 440f, 500));
            // Task.Run(() => PlayMelody(cts.Token, 600, 200));
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                PlaySound(2000f);
        }

        private async Task PlayMelody(CancellationToken token, float frequency, int duration)
        {
            while (!token.IsCancellationRequested)
            {
                PlaySound(frequency);
                await Task.Delay(duration, token).ConfigureAwait(false);
            }
        }

        private void PlaySound(float frequency)
        {
            Console.WriteLine(1);
            try
            {
                var wave = new short[SAMPLE_RATE];
                var binaryWave = new byte[SAMPLE_RATE * sizeof(short)];
                for (int i = 0; i < SAMPLE_RATE; i++)
                {
                    wave[i] = Convert.ToInt16(frequency); //Convert.ToInt16(short.MaxValue * Math.Sin(((Math.PI * 2 * frequency) / SAMPLE_RATE) * i));
                }
                Buffer.BlockCopy(wave, 0, binaryWave, 0, wave.Length * sizeof(short));
                using (var memoryStream = new MemoryStream())
                using (var binaryWriter = new BinaryWriter(memoryStream))
                {
                    var blockAlign = BITS_PER_SAMPLE / 8;
                    var subChunkTwoSize = SAMPLE_RATE * blockAlign;
                    binaryWriter.Write(new [] {'R', 'I', 'F', 'F'});
                    binaryWriter.Write(36 + subChunkTwoSize);
                    binaryWriter.Write(new[] {'W', 'A', 'V', 'E', 'f', 'm', 't', ' '});
                    binaryWriter.Write(16);
                    binaryWriter.Write((short) 1);
                    binaryWriter.Write((short) 1);
                    binaryWriter.Write(SAMPLE_RATE);
                    binaryWriter.Write(SAMPLE_RATE * blockAlign);
                    binaryWriter.Write(blockAlign);
                    binaryWriter.Write(BITS_PER_SAMPLE);
                    binaryWriter.Write(new[] {'d', 'a', 't', 'a'});
                    binaryWriter.Write(subChunkTwoSize);
                    binaryWriter.Write(binaryWave);
                    memoryStream.Position = 0;
                    new SoundPlayer(memoryStream).Play();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}