using Film.Domain.TimeStream;

namespace Film.Domain.Transport
{
    public interface FloatTransporter
    {
        float Move(WorldTime now);
        bool Exists(WorldTime now);
    }

    public static class FloatTransporters
    {
        public static FloatTransporter Linear(float from, float to, ValidatedTime moveTime)
        {
            return new FloatLinearTransporter(from, to, moveTime);
        }
    }

}
