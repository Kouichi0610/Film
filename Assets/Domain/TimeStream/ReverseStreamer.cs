using System;

namespace Film.Domain.TimeStream
{
    internal class ReverseStreamer : TimeStreamer
    {
        CurrentTime TimeStreamer.Now => now;
        CurrentTime now;
        readonly double factor;

        internal ReverseStreamer(CurrentTime now, double factor)
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
