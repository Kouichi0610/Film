using UnityEngine;
using Film.Domain.TimeStream;

namespace Film.Domain.Transport
{
    internal sealed class Vector2LinearTransporter : Vector2Transporter
    {
        readonly FloatTransporter xTransporter;
        readonly FloatTransporter yTransporter;

        internal Vector2LinearTransporter(Vector2 from, Vector2 to, MoveTime moveTime)
        {
            xTransporter = new FloatLinearTransporter(from.x, to.x, moveTime);
            yTransporter = new FloatLinearTransporter(from.y, to.y, moveTime);
        }

        Vector2 Vector2Transporter.Move(CurrentTime now)
        {
            var x = xTransporter.Move(now);
            var y = yTransporter.Move(now);
            return new Vector2(x, y);
        }
    }
}
