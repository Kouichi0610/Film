using Film.Domain.TimeStream;

namespace Film.Domain.Transport
{
    internal sealed class FloatLinearTransporter : FloatTransporter
    {
        readonly FromTo fromTo;
        readonly ValidatedTime moveTime;

        readonly float delta;

        internal FloatLinearTransporter(float from, float to, ValidatedTime moveTime)
        {
            fromTo = new FromTo(from, to);
            this.moveTime = moveTime;

            var distance = fromTo.Distance;
            delta = moveTime.Delta(distance);
        }

        float FloatTransporter.Move(WorldTime now)
        {
            var t = moveTime.LocalSeconds(now);
            if (t < 0) t = 0;
            var res = fromTo.From + delta * t;
            return  fromTo.Clamp(res);
        }

        bool FloatTransporter.Exists(WorldTime now)
        {
            return moveTime.Exists(now);
        }
    }
}
