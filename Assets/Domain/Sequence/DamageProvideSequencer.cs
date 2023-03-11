using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Film.Domain.TimeStream;

namespace Film.Domain.Sequence
{
    public interface DamageProvideSequencer
    {
        void Provide(WorldTime now);
    }
}
