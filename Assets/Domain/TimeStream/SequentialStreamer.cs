
namespace Film.Domain.TimeStream
{
    internal sealed class SequentialStreamer : TimeStreamer
    {
        WorldTime TimeStreamer.Now => now;

        WorldTime now;

        internal SequentialStreamer()
        {
            now = WorldTime.FromFloat(0);
        }

        internal SequentialStreamer(WorldTime now)
        {
            this.now = now;
        }

        void TimeStreamer.Stream(DeltaTime delta)
        {
            now = now.Add(delta);
        }
    }
}
