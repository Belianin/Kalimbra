using System.Collections.Generic;

namespace Kalimbra
{
    public static class NoteFrequencies
    {
        private static readonly Dictionary<NoteKey, float> Frequencies = new Dictionary<NoteKey, float>
        {
            [NoteKey.C3] = 130.8f,
            [NoteKey.Db3] = 138.6f,
            [NoteKey.D3] = 146.8f,
            [NoteKey.Eb3] = 155.6f,
            [NoteKey.E3] = 164.8f,
            [NoteKey.F3] = 174.6f,
            [NoteKey.Gb3] = 185.0f,
            [NoteKey.G3] = 196.0f,
            [NoteKey.Ab3] = 207.7f,
            [NoteKey.A3] = 220f,
            [NoteKey.Bb3] = 233.1f,
            [NoteKey.B3] = 246.9f,
        };

        public static float Get(NoteKey key) => Frequencies[key];
    }
}