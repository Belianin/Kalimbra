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
        public int[] ToBytes(Melody[] melody)
        {
            var melodies = melody.SelectMany(m => m.Notes.Select(n => PlaySineWave(n, m.Instrument))).ToArray();
            var payload = new long[melodies.Max(m => m.Length)];
            long max = 0;
            for (int i = 0; i < payload.Length; i++)
            {
                long l = 0;
                foreach (var m in melodies)
                {
                    if (i < m.Length)
                    {
                        l += m[i];
                    }
                }
                
                if (l > max)
                    max = l;
                payload[i] = l;
            }

            var normalized = new int[payload.Length];
            for (int i = 0; i < payload.Length; i++)
            {
                normalized[i] = (int) ((int.MaxValue / 2) * (payload[i] / (double) max)); // * (payload[i] / (double) max)) * (int.MaxValue));
            }

            return normalized;
        }
        
        public void Record(Stream stream, WaveFile wave)
        {
            var blockAlign = wave.BitsPerSample / 8; // количество байт на семпл с учетом всех каналов
            var subChunkTwoSize = blockAlign * wave.Wave.Length; // длинна данных

            var result = wave.Wave.SelectMany(BitConverter.GetBytes).ToArray();
            //var result = new byte[wave.Wave.Length * sizeof(int)];
            //Buffer.BlockCopy(wave.Wave, 0, result, 0, result.Length);

            using var binaryWriter = new BinaryWriter(stream, Encoding.ASCII, true);
            binaryWriter.Write(Encoding.ASCII.GetBytes("RIFF"));
            binaryWriter.Write(36 + subChunkTwoSize);
            binaryWriter.Write(Encoding.ASCII.GetBytes("WAVEfmt "));
            binaryWriter.Write(16);
            binaryWriter.Write((short) 1); // линейное квантование
            binaryWriter.Write((short) 1); // моно
            binaryWriter.Write(wave.SampleRate);
            binaryWriter.Write(wave.SampleRate * blockAlign); // количество данных в секунду
            binaryWriter.Write((short) blockAlign);
            binaryWriter.Write((short) wave.BitsPerSample);
            binaryWriter.Write(Encoding.ASCII.GetBytes("data"));
            binaryWriter.Write(subChunkTwoSize);
            binaryWriter.Write(result);
        }

        private int[] PlaySineWave(Note[] notes, IInstrument instrument)
        {
            var length = notes.Sum(GetNoteDuration);
            var wave = new int[length];
            var i = 0;
            foreach (var note in notes)
            {
                var result = instrument.Play(note);
                for (int j = 0; j < result.Length; j++)
                {
                    if (i + j >= wave.Length)
                        return wave;
                    wave[i + j] += Convert.ToInt32(result[j]);
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