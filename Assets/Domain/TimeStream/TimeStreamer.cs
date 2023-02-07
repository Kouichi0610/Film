
namespace Film.Domain.TimeStream
{
    public interface TimeStreamer
    {
        void Stream(DeltaTime delta);
        CurrentTime Now { get; }
    }
}
