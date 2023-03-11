using Film.Domain.TimeStream;
using Film.Domain.Entity;

namespace Film.Domain.LifeSequence
{
    public interface LifeSequencer
    {
        bool Living(WorldTime now);
        DamageReceiver DamageReceiver { get; }
        void Rewind(WorldTime redoTime);
    }
}
