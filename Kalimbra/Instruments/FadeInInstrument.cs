namespace Kalimbra.Instruments
{
    public class FadeInInstrument : IInstrument
    {
        private readonly IInstrument instrument;
        private int duration = 44100 / 16;

        public FadeInInstrument(IInstrument instrument)
        {
            this.instrument = instrument;
        }

        public int[] Play(Note note)
        {
            var wave = instrument.Play(note);
            for (int i = 0; i < duration; i++)
            {
                wave[i] *= (int) (i / (double) duration);
            }

            return wave;
        }
    }
}