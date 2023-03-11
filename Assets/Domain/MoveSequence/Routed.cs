using System;
using UnityEngine;
using System.Collections.Generic;
using Film.Domain.TimeStream;
using Film.Domain.Transport;
using System.Linq;

namespace Film.Domain.MoveSequence
{
    internal sealed class Routed : MoveSequencer
    {
        readonly IReadOnlyList<Vector2Transporter> transporters;
        readonly Vector2Transporter before;
        readonly Vector2Transporter after;

        internal Routed(IReadOnlyList<Vector2Transporter> transporters)
        {
            this.transporters = transporters;
            before = transporters.First();
            after = transporters.Last();
        }

        Vector2 MoveSequencer.Move(WorldTime now)
        {
            foreach (var transporter in transporters)
            {
                if (transporter.Exists(now) == false) continue;

                return transporter.Move(now);
            }
            throw new ArgumentOutOfRangeException("Can not argument.");
        }

        bool MoveSequencer.Exists(WorldTime now)
        {
            if (before.Exists(now)) return false;
            if (after.Exists(now)) return false;
            return true;
        }

        void MoveSequencer.Rewind(WorldTime redoTime)
        {
            // ルートが決まっているため巻き戻しの影響を受けない。
        }
    }
}
