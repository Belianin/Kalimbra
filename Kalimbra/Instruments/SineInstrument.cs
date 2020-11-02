using System;

namespace Kalimbra.Instruments
{
    public class SineInstrument : IInstrument
    {
        private short volume = short.MaxValue / 2;
        private double SAMPLE_RATE = 44100d; // хм, зачем?
        
        public double[] Play(Note note)
        {
            var duration = GetNoteDuration(note);
            var wave = new double[duration];
            for (int i = 0; i < duration; i++)
            {
                wave[i] = volume * Math.Sin(((Math.PI * 2 * i) / SAMPLE_RATE) * note.Frequency) +
                          volume;
            }

            return wave;
        }
        
        private int GetNoteDuration(Note note)
        {
            return (int) SAMPLE_RATE / (int) note.Duration;
        }
    }
}