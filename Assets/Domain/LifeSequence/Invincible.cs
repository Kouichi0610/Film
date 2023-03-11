using Film.Domain.TimeStream;
using Film.Domain.Entity;

namespace Film.Domain.LifeSequence
{
    internal sealed class Invincible : LifeSequencer, DamageReceiver
    {
        DamageReceiver LifeSequencer.DamageReceiver => this;

        bool LifeSequencer.Living(WorldTime now)
        {
            return true;
        }

        void LifeSequencer.Rewind(WorldTime redoTime)
        {
        }

        void DamageReceiver.Damage(int damagePoint, WorldTime now)
        {
        }
    }
}
