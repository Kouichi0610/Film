using Film.Domain.TimeStream;

namespace Film.Domain.Transport
{
    public interface FloatTransporter
    {
        float Move(CurrentTime now);
    }

    public static class FloatTransporters
    {
        public static FloatTransporter Linear(float from, float to, MoveTime moveTime)
        {
            return new FloatLinearTransporter(from, to, moveTime);
        }
    }

}
