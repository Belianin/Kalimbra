using System;

namespace Kalimbra.Instruments
{
    public class SineInstrument : IInstrument
    {
        private int volume = int.MaxValue / 4;
        private double SAMPLE_RATE = 44100d; // хм, зачем?
        
        public int[] Play(Note note)
        {
            var duration = GetNoteDuration(note);
            var wave = new int[duration];
            for (int i = 0; i < duration; i++)
            {
                wave[i] = (int) (volume * Math.Sin(((Math.PI * 2 * i) / SAMPLE_RATE) * note.Frequency));
            }

            return wave;
        }
        
        private int GetNoteDuration(Note note)
        {
            return (int) SAMPLE_RATE / (int) note.Duration;
        }
    }
}