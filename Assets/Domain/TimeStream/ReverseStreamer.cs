using System;

namespace Film.Domain.TimeStream
{
    internal class ReverseStreamer : TimeStreamer
    {
        WorldTime TimeStreamer.Now => now;
        WorldTime now;
        readonly double factor;

        internal ReverseStreamer(WorldTime now, double factor)
        {
            if (factor <= 0)
            {
                throw new ArgumentOutOfRangeException(string.Format("Factor:{0} not supported.", factor));
            }
            this.now = now;
            this.factor = factor;
        }

        void TimeStreamer.Stream(DeltaTime delta)
        {
            now = now.Sub(delta.Factor(factor));
        }
    }
}
