
namespace Film.Domain.TimeStream
{
    public static class TimeStreamerFactory
    {
        public static TimeStreamer CreateStart()
        {
            return new SequentialStreamer();
        }
    }

    public static class TimeStreamerExtension
    {
        public static TimeStreamer ToSequential(this TimeStreamer s)
        {
            return new SequentialStreamer(s.Now);
        }
        public static TimeStreamer ToFastMode(this TimeStreamer s, double factor)
        {
            return new FastStreamer(s.Now, factor);
        }
        public static TimeStreamer ToSlowMode(this TimeStreamer s, double factor)
        {
            return new SlowStreamer(s.Now, factor);
        }
        public static TimeStreamer ToStopMode(this TimeStreamer s)
        {
            return new StopStreamer(s.Now);
        }
        public static TimeStreamer ToReverseMove(this TimeStreamer s, double factor = 1.0)
        {
            return new ReverseStreamer(s.Now, factor);
        }
        public static TimeStreamer Skip(this TimeStreamer s, DeltaTime skipSeconds)
        {
            return new SequentialStreamer(s.Now.Add(skipSeconds));

        }
    }
}
