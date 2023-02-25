using UnityEngine;
using Film.Domain.TimeStream;

namespace Film.Domain.Transport
{
    public interface Vector2Transporter
    {
        Vector2 Move(WorldTime now);
    }

    public static class Vector2Transporters
    {
        public static Vector2Transporter Linear(Vector2 from, Vector2 to, ValidatedTime moveTime)
        {
            return new Vector2LinearTransporter(from, to, moveTime);
        }
    }
}
