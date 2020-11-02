using System.Collections.Generic;

namespace Kalimbra
{
    public static class NoteFrequencies
    {
        private static readonly Dictionary<NoteKey, float> Frequencies = new Dictionary<NoteKey, float>
        {
            [NoteKey.C2] = 65.41f,
            [NoteKey.Db2] = 169.3f,
            [NoteKey.D2] = 73.42f,
            [NoteKey.Eb2] = 77.78f,
            [NoteKey.E2] = 82.41f,
            [NoteKey.F2] = 87.31f,
            [NoteKey.Gb2] = 92.5f,
            [NoteKey.G2] = 98f,
            [NoteKey.Ab2] = 103.8f,
            [NoteKey.A2] = 110f,
            [NoteKey.Bb2] = 116.5f,
            [NoteKey.B2] = 123.5f,
            
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
            
            [NoteKey.C4] = 261.6f,
            [NoteKey.Db4] = 277.2f,
            [NoteKey.D4] = 293.7f,
            [NoteKey.Eb4] = 311.1f,
            [NoteKey.E4] = 329.6f,
            [NoteKey.F4] = 349.2f,
            [NoteKey.Gb4] = 370f,
            [NoteKey.G4] = 392f,
            [NoteKey.Ab4] = 415.3f,
            [NoteKey.A4] = 440f,
            [NoteKey.Bb4] = 466.2f,
            [NoteKey.B4] = 493.9f,
            
            [NoteKey.C5] = 523.3f,
            [NoteKey.Db5] = 554.4f,
            [NoteKey.D5] = 587.3f,
            [NoteKey.Eb5] = 622.3f,
            [NoteKey.E5] = 659.3f,
            [NoteKey.F5] = 698.5f,
            [NoteKey.Gb5] = 740f,
            [NoteKey.G5] = 784f,
            [NoteKey.Ab5] = 830.6f,
            [NoteKey.A5] = 880,
            [NoteKey.Bb5] = 932.3f,
            [NoteKey.B5] = 987.8f,
        };

        public static float Get(NoteKey key) => Frequencies[key];
    }
}