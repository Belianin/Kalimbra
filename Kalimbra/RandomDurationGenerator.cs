using System;
using System.Collections.Generic;
using System.Linq;

namespace Kalimbra
{
    public class RandomDurationGenerator : IDurationGenerator
    {
        private readonly Random random;
        private readonly NoteDuration[] durations;

        public RandomDurationGenerator(Random random = null, IEnumerable<NoteDuration> durations = null)
        {
            this.random = random ?? new Random();
            this.durations = durations != null
                ? durations.ToArray()
                : new[]
                {
                    NoteDuration.Whole,
                    NoteDuration.Half,
                    NoteDuration.Quarter,
                    NoteDuration.Eighth
                };
        }

        public NoteDuration GetNext()
        {
            return durations[random.Next(durations.Length)];
        }
    }
}