using System;

namespace Kalimbra.Instruments
{
    public class SineInstrument : IInstrument
    {
        private short volume = short.MaxValue / 2;
        private double SAMPLE_RATE = 44100d; // хм, зачем?
        
        public short Play(float frequency, int time)
        {
            return Convert.ToInt16(
                volume * Math.Sin(((Math.PI * 2 * time) / SAMPLE_RATE) * frequency) +
                volume);
        }
    }
}