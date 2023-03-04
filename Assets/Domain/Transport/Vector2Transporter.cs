using UnityEngine;
using Film.Domain.TimeStream;

namespace Film.Domain.Transport
{
    public interface Vector2Transporter
    {
        Vector2 Move(WorldTime now);
        bool Exists(WorldTime now);
    }

    public static class Vector2Transporters
    {
        public static Vector2Transporter Linear(Vector2 from, Vector2 to, ValidatedTime moveTime)
        {
            return new Vector2LinearTransporter(from, to, moveTime);
        }
        public static Vector2Transporter BeforeLiving(Vector2 startPosition, WorldTime startTime)
        {
            return new Vector2BeforeLivingTransporter(startPosition, startTime);
        }
        public static Vector2Transporter AfterLiving(Vector2 endPosition, WorldTime endTime)
        {
            return new Vector2AfterLivingTransporter(endPosition, endTime);
        }
    }
}
