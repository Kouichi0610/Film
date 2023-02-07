
namespace Film.Domain.TimeStream
{
    internal class StopStreamer : TimeStreamer
    {
        CurrentTime TimeStreamer.Now => now;
        readonly CurrentTime now;

        internal StopStreamer(CurrentTime now)
        {
            this.now = now;
        }
        void TimeStreamer.Stream(DeltaTime delta)
        {
        }
    }
}
