using System.Collections.Generic;
using System.Linq;

namespace Kalimbra
{
    public class Chord
    {
        public NoteKey[] Keys { get; }

        public Chord(IEnumerable<NoteKey> keys)
        {
            Keys = keys.ToArray();
        }

        public static Chord Maj(NoteKey key)
        {
            return new Chord(new []
            {
                key,
                key + 4,
                key + 7
            });
        }
    }
}