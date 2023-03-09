using System.Collections.Generic;
using UnityEngine;
using Film.Domain.TimeStream;

namespace Film.Domain.Sequence
{
    public interface LifeSequencer
    {
        bool Living(WorldTime now);
        void Damage(int damagePoint, WorldTime now);
        void Rewind(WorldTime redo);
    }

    public static class LifeSequencers
    {
        public static LifeSequencer Breakable(int hitPoint)
        {
            return new BreakableLifeSequencer(hitPoint);
        }

        public static LifeSequencer Invincible()
        {
            return new InvincibleLifeSequencer();
        }
    }
}
