using System;

namespace Film.Domain.TimeStream
{
    internal sealed class SlowStreamer : TimeStreamer
    {
        WorldTime TimeStreamer.Now => now;

        WorldTime now;
        readonly double factor;

        internal SlowStreamer(WorldTime now, double factor)
        {
            if (factor >= 1.0 || factor <= 0.0)
            {
                throw new ArgumentOutOfRangeException(string.Format("Factor:{0} not supported.", factor));
            }
            this.now = now;
            this.factor = factor;
        }

        void TimeStreamer.Stream(DeltaTime delta)
        {
            now = now.Add(delta.Factor(factor));
        }
    }
}
