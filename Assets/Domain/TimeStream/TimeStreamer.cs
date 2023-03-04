
namespace Film.Domain.TimeStream
{
    public interface TimeStreamer
    {
        void Stream(DeltaTime delta);
        WorldTime Now { get; }
    }
}
