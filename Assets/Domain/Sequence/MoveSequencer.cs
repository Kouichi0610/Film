using UnityEngine;
using Film.Domain.TimeStream;

namespace Film.Domain.Sequence
{
    public interface MoveSequencer
    {
        Vector2 Move(WorldTime now);
        bool Exists(WorldTime now);
        void Rewind(WorldTime redo);
    }
}
