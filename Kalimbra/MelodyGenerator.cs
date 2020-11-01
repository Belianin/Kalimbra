using System;
using System.Collections.Generic;
using System.Linq;

namespace Kalimbra
{
    public class MelodyGenerator
    {
        private readonly IDurationGenerator durationGenerator;

        public MelodyGenerator(IDurationGenerator durationGenerator)
        {
            this.durationGenerator = durationGenerator;
        }

        public Melody Generate(IEnumerable<NoteKey> possibleNotes, int noteCount)
        {
            var random = new Random();
            var notes = possibleNotes.ToArray();

            var result = new List<Note>();
            for (int i = 0; i < noteCount; i++)
            {
                var key = notes[random.Next(notes.Length)];
                var frequency = NoteFrequencies.Get(key);
                var duration = durationGenerator.GetNext();
                
                var note = new Note(frequency, duration);
                
                result.Add(note);
            }
            
            return new Melody(result);
        }
    }
}