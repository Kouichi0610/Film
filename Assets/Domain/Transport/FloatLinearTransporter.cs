using Film.Domain.TimeStream;

namespace Film.Domain.Transport
{
    internal sealed class FloatLinearTransporter : FloatTransporter
    {
        readonly FromTo fromTo;
        readonly MoveTime moveTime;

        readonly float delta;

        internal FloatLinearTransporter(float from, float to, MoveTime moveTime)
        {
            fromTo = new FromTo(from, to);
            this.moveTime = moveTime;

            var distance = fromTo.Distance;
            delta = distance / moveTime.Duration;
        }

        float FloatTransporter.Move(CurrentTime now)
        {
            var t = now.Seconds - moveTime.Start;
            if (t < 0) t = 0;
            var res = fromTo.From + delta * t;
            return  fromTo.Clamp(res);
        }
    }
}
