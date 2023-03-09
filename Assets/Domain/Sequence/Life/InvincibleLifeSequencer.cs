using System.Collections.Generic;
using UnityEngine;
using Film.Domain.TimeStream;

namespace Film.Domain.Sequence
{
    internal sealed class InvincibleLifeSequencer : LifeSequencer
    {
        void LifeSequencer.Damage(int damagePoint, WorldTime now)
        {
        }
        bool LifeSequencer.Living(WorldTime now)
        {
            return true;
        }
        void LifeSequencer.Rewind(WorldTime redo)
        {
        }
    }
}
