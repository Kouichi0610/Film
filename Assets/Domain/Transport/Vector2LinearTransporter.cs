using UnityEngine;
using Film.Domain.TimeStream;

namespace Film.Domain.Transport
{
    internal sealed class Vector2LinearTransporter : Vector2Transporter
    {
        readonly FloatTransporter xTransporter;
        readonly FloatTransporter yTransporter;

        internal Vector2LinearTransporter(Vector2 from, Vector2 to, ValidatedTime moveTime)
        {
            xTransporter = new FloatLinearTransporter(from.x, to.x, moveTime);
            yTransporter = new FloatLinearTransporter(from.y, to.y, moveTime);
        }

        Vector2 Vector2Transporter.Move(WorldTime now)
        {
            var x = xTransporter.Move(now);
            var y = yTransporter.Move(now);
            return new Vector2(x, y);
        }
        bool Vector2Transporter.Exists(WorldTime now)
        {
            return xTransporter.Exists(now);
        }
    }
}
