using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
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
using Kalimbra.App.Visualisation;
using Microsoft.Win32.SafeHandles;
using Image = System.Drawing.Image;

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
            PlayNew();
            Application.Current.Shutdown();
            //PlaySound(440f);
            // var cts = new CancellationTokenSource();
            // Task.Run(() => PlayMelody(cts.Token, 440f, 500));
            // Task.Run(() => PlayMelody(cts.Token, 600, 200));
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                PlayNew();
            }
        }

        private void PlayNew()
        {
            var durations = new[]
            {
                NoteDuration.Eighth,
                NoteDuration.Quarter,
            };
            var generator = new MelodyGenerator(new RandomDurationGenerator(durations: durations));
            var melody = generator.Generate(Gammas.CMaj5, 80);

            var bassDurations = new[]
            {
                NoteDuration.Half,
                NoteDuration.Half,
                NoteDuration.Quarter
            };
            var bassGenerator = new MelodyGenerator(new RandomDurationGenerator(durations: bassDurations));
            var bass = bassGenerator.Generate(Gammas.CMaj3, 40);

            //var drums = new MelodyGenerator(new RandomDurationGenerator(durations: new [] {NoteDuration.Whole}))
            //    .Generate(new [] {NoteKey.C2}, 40);
            
            using var memoryStream = File.OpenWrite("temp.wav");
            using var binaryWriter = new BinaryWriter(memoryStream);
            var recorder = new WaveRecorder();
            
            var wave = recorder.Record(binaryWriter, new [] {melody, bass});
            
            new WaveVisualiser().Visualize(wave, 800, 600).Save("temp.png", ImageFormat.Png);
            //memoryStream.Position = 0;
            
            //new SoundPlayer(memoryStream).Play();
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
                    wave[i] = Convert.ToInt16(short.MaxValue * Math.Sin(((Math.PI * 2 * frequency) / SAMPLE_RATE) * i));
                }
                Buffer.BlockCopy(wave, 0, binaryWave, 0, wave.Length * sizeof(short));
                using (var memoryStream = new MemoryStream())
                using (var binaryWriter = new BinaryWriter(memoryStream))
                {
                    var blockAlign = BITS_PER_SAMPLE / 8;
                    var subChunkTwoSize = SAMPLE_RATE * blockAlign;
                    
                    binaryWriter.Write(Encoding.ASCII.GetBytes("RIFF"));
                    binaryWriter.Write(36 + subChunkTwoSize);
                    binaryWriter.Write(Encoding.ASCII.GetBytes("WAVEfmt "));
                    binaryWriter.Write(16);
                    binaryWriter.Write((short) 1);
                    binaryWriter.Write((short) 1);
                    binaryWriter.Write(SAMPLE_RATE);
                    binaryWriter.Write(SAMPLE_RATE * blockAlign);
                    binaryWriter.Write((short) blockAlign);
                    binaryWriter.Write((short) BITS_PER_SAMPLE);
                    binaryWriter.Write(new[] {'d', 'a', 't', 'a'});
                    binaryWriter.Write(subChunkTwoSize);
                    binaryWriter.Write(binaryWave);
                    memoryStream.Position = 0;
                    var player = new SoundPlayer(memoryStream);
                    player.Load();
                    player.PlaySync();
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