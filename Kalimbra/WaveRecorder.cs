using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Kalimbra
{
    public class WaveRecorder
    {
        private const int SAMPLE_RATE = 44100;
        private const short BITS_PER_SAMPLE = 16;
        
        public void Record(BinaryWriter binaryWriter, Melody melody)
        {
            var payload = GetPayloadBites(melody);
            
            var blockAlign = BITS_PER_SAMPLE / 8;
            var subChunkTwoSize = blockAlign * payload.Length;

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
            binaryWriter.Write(payload);
        }

        private byte[] GetPayloadBites(Melody melody)
        {
            
            
            var notes = new List<short>();
            foreach (var note in melody.Notes)
            {
                var duration = SAMPLE_RATE / (int) note.Duration;
                var wave = new short[duration];
                for (int i = 0; i < duration; i++)
                {
                    wave[i] = Convert.ToInt16(short.MaxValue * Math.Sin(((Math.PI * 2 * note.Frequency) / duration) * i));
                }
                notes.AddRange(wave);
            }

            var totalWave = notes.ToArray();
            var binaryWave = new byte[totalWave.Length * sizeof(short)];

            Buffer.BlockCopy(totalWave, 0, binaryWave, 0, totalWave.Length * sizeof(short));

            return binaryWave;
        }

        private byte[] GetTemporaryPayload()
        {
            var wave = new short[SAMPLE_RATE];
            var binaryWave = new byte[SAMPLE_RATE * sizeof(short)];
            for (int i = 0; i < SAMPLE_RATE; i++)
            {
                wave[i] = Convert.ToInt16(short.MaxValue * Math.Sin(((Math.PI * 2 * 440f) / SAMPLE_RATE) * i));
            }
            Buffer.BlockCopy(wave, 0, binaryWave, 0, wave.Length * sizeof(short));

            return binaryWave;
        }
    }
}