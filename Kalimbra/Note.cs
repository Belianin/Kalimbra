namespace Kalimbra
{
    public struct Note
    {
        public Note(float frequency, NoteDuration duration)
        {
            Frequency = frequency;
            Duration = duration;
        }

        public float Frequency { get; }
        public NoteDuration Duration { get; }
    }
}