using UnityEngine;
using Film.Domain.TimeStream;

namespace Film.Domain.Transport
{
    internal class Vector2BeforeLivingTransporter : Vector2Transporter
    {
        readonly Vector2 position;
        readonly WorldTime startTime;
        internal Vector2BeforeLivingTransporter(Vector2 startPosition, WorldTime startTime)
        {
            position = startPosition;
            this.startTime = startTime;

        }
        bool Vector2Transporter.Exists(WorldTime now)
        {
            return now.Before(startTime);
        }

        Vector2 Vector2Transporter.Move(WorldTime now)
        {
            return position;
        }
    }
}
