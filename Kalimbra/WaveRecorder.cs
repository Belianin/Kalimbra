using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Kalimbra.Instruments;

namespace Kalimbra
{
    public class WaveRecorder
    {
        private const int SAMPLE_RATE = 44100;
        private const short BITS_PER_SAMPLE = 16;
        
        public byte[] Record(BinaryWriter binaryWriter, Melody[] melody)
        {
            var melodies = melody.SelectMany(m => m.Notes.Select(n => GetPayloadBites(n, m.Instrument))).ToArray();
            var payload = new long[melodies.Max(m => m.Length)];
            long max = 0;
            for (int i = 0; i < payload.Length; i++)
            {
                //var count = 0;
                var l = 0L;
                foreach (var m in melodies)
                {
                    if (i < m.Length)
                    {
                        //count++;
                        l += m[i];
                    }
                }
                
                if (l > max)
                    max = l;
                payload[i] = l; //(l / count);
            }

            var normalized = new byte[payload.Length];
            for (int i = 0; i < payload.Length; i++)
            {
                normalized[i] = (byte) (payload[i] * (payload[i] / (double) max));
            }
            
            var blockAlign = BITS_PER_SAMPLE / 8;
            var subChunkTwoSize = blockAlign * normalized.Length;

            binaryWriter.Write(Encoding.ASCII.GetBytes("RIFF"));
            binaryWriter.Write(36 + subChunkTwoSize);
            binaryWriter.Write(Encoding.ASCII.GetBytes("WAVEfmt "));
            binaryWriter.Write(16);
            binaryWriter.Write((short) 1);
            binaryWriter.Write((short) 1);
            binaryWriter.Write(SAMPLE_RATE);
            binaryWriter.Write(SAMPLE_RATE * blockAlign);
            binaryWriter.Write((short) blockAlign);
            binaryWriter.Write((short) BITS_PER_SAMPLE);
            binaryWriter.Write(Encoding.ASCII.GetBytes("data"));
            binaryWriter.Write(subChunkTwoSize);
            binaryWriter.Write(normalized);

            return normalized;
        }

        private byte[] GetPayloadBites(Note[] notes, IInstrument instrument)
        {
            var wave = PlaySineWave(notes, instrument);
            var binaryWave = new byte[wave.Length * sizeof(short)];

            Buffer.BlockCopy(wave, 0, binaryWave, 0, wave.Length * sizeof(short));

            return binaryWave;
        }

        private short[] PlaySineWave(Note[] notes, IInstrument instrument)
        {
            var length = notes.Sum(GetNoteDuration);
            var wave = new short[length];
            var i = 0;
            foreach (var note in notes)
            {
                var result = instrument.Play(note);
                for (int j = 0; j < result.Length; j++)
                {
                    if (i + j >= wave.Length)
                        return wave;
                    wave[i + j] += Convert.ToInt16(result[j]);
                }

                i += GetNoteDuration(note);;
            }

            return wave;
        }

        private int GetNoteDuration(Note note)
        {
            return SAMPLE_RATE / (int) note.Duration;// * (bpm / 60));
        }
    }
}