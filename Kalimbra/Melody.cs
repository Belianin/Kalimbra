using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Kalimbra
{
    public class Melody
    {
        public readonly Note[] Notes;
        public int Bpm { get; }

        public Melody(IEnumerable<Note> notes, int bpm = 100)
        {
            Bpm = bpm;
            this.Notes = notes.ToArray();
        }
    }
}