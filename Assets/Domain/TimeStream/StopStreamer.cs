
namespace Film.Domain.TimeStream
{
    internal class StopStreamer : TimeStreamer
    {
        WorldTime TimeStreamer.Now => now;
        readonly WorldTime now;

        internal StopStreamer(WorldTime now)
        {
            this.now = now;
        }
        void TimeStreamer.Stream(DeltaTime delta)
        {
        }
    }
}
