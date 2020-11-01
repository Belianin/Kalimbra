using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Kalimbra
{
    public class Melody
    {
        public readonly Note[] Notes;

        public Melody(IEnumerable<Note> notes)
        {
            this.Notes = notes.ToArray();
        }
    }
}