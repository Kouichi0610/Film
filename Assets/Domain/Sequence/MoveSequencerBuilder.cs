using System;
using UnityEngine;
using System.Collections.Generic;
using Film.Domain.TimeStream;
using Film.Domain.Transport;

namespace Film.Domain.Sequence
{
    public sealed class MoveSequencerBuilder
    {
        Vector2 from;
        WorldTime currentStart;
        WorldTime start;
        WorldTime end;
        List<Vector2Transporter> transporters = new List<Vector2Transporter>();
        readonly Vector2Transporter before;

        public static MoveSequencerBuilder Start(Vector2 startPosition, WorldTime startTime)
        {
            return new MoveSequencerBuilder(startPosition, startTime);
        }

        MoveSequencerBuilder(Vector2 startPosition, WorldTime worldTime)
        {
            from = startPosition;
            currentStart = worldTime;
            start = worldTime;
            before = Vector2Transporters.BeforeLiving(startPosition, worldTime);
        }

        public MoveSequencerBuilder LinearMoveTo(Vector2 to, float duration)
        {
            var validatedTime = ValidatedTime.FromWorldTimeAndDuration(currentStart, duration);
            var transporter = Vector2Transporters.Linear(from, to, validatedTime);
            transporters.Add(transporter);
            from = to;
            currentStart = currentStart.AddSeconds(duration);
            end = currentStart;
            return this;
        }

        public MoveSequencerBuilder Stay(float duration)
        {
            return LinearMoveTo(from, duration);
        }
        public MoveSequencer Build()
        {
            if (transporters.Count == 0)
            {
                throw new ArgumentException("no transporters.");
            }

            List<Vector2Transporter> buildList = new List<Vector2Transporter>();
            buildList.Add(before);
            buildList.AddRange(transporters);
            var after = Vector2Transporters.AfterLiving(from, end);
            buildList.Add(after);

            return new MoveSequencer(buildList);
        }

        public float Duration()
        {
            return end.Seconds - start.Seconds;
        }
    }
}
