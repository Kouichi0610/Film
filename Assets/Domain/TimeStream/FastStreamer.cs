using System;

namespace Film.Domain.TimeStream
{
    internal sealed class FastStreamer : TimeStreamer
    {
        WorldTime TimeStreamer.Now => now;

        WorldTime now;
        readonly double factor;

        internal FastStreamer(WorldTime now, double factor)
        {
            if (factor <= 1.0)
            {
                throw new ArgumentOutOfRangeException(string.Format("Factor:{0} not supported.", factor));
            }

            this.factor = factor;
            this.now = now;
        }

        void TimeStreamer.Stream(DeltaTime delta)
        {
            now = now.Add(delta.Factor(factor));
        }
    }
}
