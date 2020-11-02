using System;
using System.Drawing;
using System.Linq;

namespace Kalimbra.App.Visualisation
{
    public class WaveVisualiser
    {
        public Image Visualize(byte[] wave, int width, int height)
        {
            var maxVolume = wave.Max();
            var bitmap = new Bitmap(width, height);
            using var graphics = Graphics.FromImage(bitmap);
            for (int i = 0; i < width - 1; i++)
            {
                graphics.DrawLine(
                    Pens.Blue,
                    i,
                    GetY(GetSample(wave, width, i), height, maxVolume),
                    i + 1,
                    GetY(GetSample(wave, width, i + 1), height, maxVolume));
            }

            graphics.Save();

            return bitmap;
        }

        private int GetY(byte wave, int height, byte maxVolume)
        {
            return (int) ((double) wave / (double)maxVolume) * height;
        }

        private byte GetSample(byte[] wave, int width, int i)
        {
            var result = wave[(int) (((double) i / width) * wave.Length)];
            
            return result;
        }
    }
}