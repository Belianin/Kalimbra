using System.Collections.Generic;
using System.Text;

namespace Kalimbra
{
    public class WaveFile
    {
        public int SampleRate { get; set; }= 44100;
        public short BitsPerSample { get; set; } = 32;
        public ChannelsCount ChannelsCount { get; set; }= ChannelsCount.Mono;
        public int[] Wave { get; set; }
    }

    public enum ChannelsCount
    {
        Mono = 1,
        Stereo = 2
    }
}