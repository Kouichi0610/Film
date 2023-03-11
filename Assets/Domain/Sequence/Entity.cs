using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Film.Domain.Sequence
{
    public interface Entity
    {
        void Locate(Vector2 position);
        void ReturnToLender();
        IEnumerable<EffectReceiver> HitTargets();
    }
}
