using System;
using System.Drawing;
using System.Linq;

namespace Kalimbra.App.Visualisation
{
    public class WaveVisualiser
    {
        public Image Visualize(int[] wave, int width, int height)
        {
            var maxVolume = wave.Max();
            var bitmap = new Bitmap(width, height);
            using var graphics = Graphics.FromImage(bitmap);
            for (int i = 0; i < width - 1; i++)
            {
                var j = i * 4;
                var x = j + 4;
                //graphics.DrawRectangle(Pens.Aqua, i, wave[i], 1, 1);
                graphics.DrawLine(
                    Pens.Blue,
                    i,
                    GetY((wave[j] + wave[j + 1] + wave[j + 2] + wave[j + 3]) / 4, height, maxVolume),
                    i + 1,
                    GetY((wave[x] + wave[x + 1] + wave[x + 2] + wave[x + 3]) / 4, height, maxVolume));
            }

            graphics.Save();

            return bitmap;
        }

        private int GetY(int wave, int height, int maxVolume)
        {
            return (int) ((wave / (double) maxVolume) * height);
        }

        private int GetSample(int[] wave, int width, int i)
        {
            var result = wave[(int) (((double) i / width) * wave.Length)];
            
            return result;
        }
    }
}