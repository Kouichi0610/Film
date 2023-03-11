using System;
using UnityEngine;
using System.Collections.Generic;
using Film.Domain.TimeStream;
using Film.Domain.Transport;

namespace Film.Domain.MoveSequence
{
    public sealed class RoutedBuilder
    {
        Vector2 from;
        WorldTime currentStart;
        WorldTime start;
        WorldTime end;
        List<Vector2Transporter> transporters = new List<Vector2Transporter>();
        readonly Vector2Transporter before;

        public static RoutedBuilder Start(Vector2 startPosition, WorldTime startTime)
        {
            return new RoutedBuilder(startPosition, startTime);
        }

        RoutedBuilder(Vector2 startPosition, WorldTime worldTime)
        {
            from = startPosition;
            currentStart = worldTime;
            start = worldTime;
            before = Vector2Transporters.BeforeLiving(startPosition, worldTime);
        }

        public RoutedBuilder LinearMoveTo(Vector2 to, float duration)
        {
            var validatedTime = ValidatedTime.FromWorldTimeAndDuration(currentStart, duration);
            var transporter = Vector2Transporters.Linear(from, to, validatedTime);
            transporters.Add(transporter);
            from = to;
            currentStart = currentStart.AddSeconds(duration);
            end = currentStart;
            return this;
        }

        public RoutedBuilder Stay(float duration)
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

            return new Routed(buildList);
        }

        public float Duration()
        {
            return end.ElaspedTime(start);
        }
    }
}
