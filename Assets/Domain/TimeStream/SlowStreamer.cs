using System;

namespace Film.Domain.TimeStream
{
    internal sealed class SlowStreamer : TimeStreamer
    {
        CurrentTime TimeStreamer.Now => now;

        CurrentTime now;
        readonly double factor;

        internal SlowStreamer(CurrentTime now, double factor)
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
