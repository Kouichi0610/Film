﻿using System;
using UnityEngine;
using System.Collections.Generic;
using Film.Domain.TimeStream;
using Film.Domain.Transport;

namespace Film.Domain.Sequence
{
    public sealed class RoutedMoveSequencerBuilder
    {
        Vector2 from;
        WorldTime currentStart;
        WorldTime start;
        WorldTime end;
        List<Vector2Transporter> transporters = new List<Vector2Transporter>();
        readonly Vector2Transporter before;

        public static RoutedMoveSequencerBuilder Start(Vector2 startPosition, WorldTime startTime)
        {
            return new RoutedMoveSequencerBuilder(startPosition, startTime);
        }

        RoutedMoveSequencerBuilder(Vector2 startPosition, WorldTime worldTime)
        {
            from = startPosition;
            currentStart = worldTime;
            start = worldTime;
            before = Vector2Transporters.BeforeLiving(startPosition, worldTime);
        }

        public RoutedMoveSequencerBuilder LinearMoveTo(Vector2 to, float duration)
        {
            var validatedTime = ValidatedTime.FromWorldTimeAndDuration(currentStart, duration);
            var transporter = Vector2Transporters.Linear(from, to, validatedTime);
            transporters.Add(transporter);
            from = to;
            currentStart = currentStart.AddSeconds(duration);
            end = currentStart;
            return this;
        }

        public RoutedMoveSequencerBuilder Stay(float duration)
        {
            return LinearMoveTo(from, duration);
        }
        public RoutedMoveSequencer Build()
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

            return new RoutedMoveSequencer(buildList);
        }

        public float Duration()
        {
            return end.Seconds - start.Seconds;
        }
    }
}