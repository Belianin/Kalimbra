using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Kalimbra.Instruments;

namespace Kalimbra
{
    public class Melody
    {
        public readonly Note[] Notes;
        public int Bpm { get; }
        public IInstrument Instrument { get; } = new FadeInInstrument(new SineInstrument());

        public Melody(IEnumerable<Note> notes, int bpm = 100)
        {
            Bpm = bpm;
            this.Notes = notes.ToArray();
        }
    }
}