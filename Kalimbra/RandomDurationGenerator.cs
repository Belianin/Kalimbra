using System;

namespace Kalimbra
{
    public class RandomDurationGenerator : IDurationGenerator
    {
        private readonly Random random;

        public RandomDurationGenerator(Random random = null)
        {
            this.random = random ?? new Random();
        }

        public NoteDuration GetNext()
        {
            return new[]
            {
                NoteDuration.Whole,
                NoteDuration.Half,
                NoteDuration.Quarter,
                NoteDuration.Eighth
            }[random.Next(0, 4)];
        }
    }
}