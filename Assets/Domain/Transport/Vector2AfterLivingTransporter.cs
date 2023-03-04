using UnityEngine;
using Film.Domain.TimeStream;

namespace Film.Domain.Transport
{
    internal class Vector2AfterLivingTransporter : Vector2Transporter
    {
        readonly Vector2 position;
        readonly WorldTime endTime;

        internal Vector2AfterLivingTransporter(Vector2 endPosition, WorldTime endTime)
        {
            position = endPosition;
            this.endTime = endTime;
        }

        bool Vector2Transporter.Exists(WorldTime now)
        {
            return now.After(endTime);
        }

        Vector2 Vector2Transporter.Move(WorldTime now)
        {
            return position;
        }
    }
}
