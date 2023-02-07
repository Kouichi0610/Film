
namespace Film.Domain.TimeStream
{
    internal sealed class SequentialStreamer : TimeStreamer
    {
        CurrentTime TimeStreamer.Now => now;

        CurrentTime now;

        internal SequentialStreamer()
        {
            now = CurrentTime.FromFloat(0);
        }

        internal SequentialStreamer(CurrentTime now)
        {
            this.now = now;
        }

        void TimeStreamer.Stream(DeltaTime delta)
        {
            now = now.Add(delta);
        }
    }
}
