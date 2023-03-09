using System;
using UnityEngine;
using System.Collections.Generic;
using Film.Domain.TimeStream;
using Film.Domain.Transport;
using System.Linq;

namespace Film.Domain.Sequence
{
    internal sealed class RoutedMoveSequencer : MoveSequencer
    {
        readonly IReadOnlyList<Vector2Transporter> transporters;
        readonly Vector2Transporter before;
        readonly Vector2Transporter after;

        internal RoutedMoveSequencer(IReadOnlyList<Vector2Transporter> transporters)
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
            throw new ArgumentOutOfRangeException(string.Format("Can not Time {0}", now));
        }

        bool MoveSequencer.Exists(WorldTime now)
        {
            if (before.Exists(now)) return false;
            if (after.Exists(now)) return false;
            return true;
        }
    }
}
