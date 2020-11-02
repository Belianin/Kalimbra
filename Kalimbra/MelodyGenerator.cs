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
            
            return new Melody(new [] {result.ToArray()});
        }
        
        public Melody GenerateMajChords(IEnumerable<NoteKey> possibleNotes, int noteCount)
        {
            var random = new Random();
            var notes = possibleNotes.ToArray();

            var result = new []
            {
                new List<Note>(),
                new List<Note>(),
                new List<Note>()
            };
            
            for (int i = 0; i < noteCount; i++)
            {
                var key = notes[random.Next(notes.Length)];
                var chord = Chord.Maj(key);
                Console.WriteLine(string.Join(", ", chord.Keys.Select(k => k.ToString())));
                var duration = durationGenerator.GetNext();
                var j = 0;
                foreach (var chordKey in chord.Keys)
                {
                    var frequency = NoteFrequencies.Get(chordKey);
                
                    var note = new Note(frequency, duration);
                    
                    result[j].Add(note);
                    j++;
                }
            }
            
            return new Melody(result.Select(r => r.ToArray()).ToArray());
        }
    }
}