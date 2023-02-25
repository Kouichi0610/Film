using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Film.Domain.TimeStream;

namespace Film.Tests
{
    public class TimeTest
    {
        [Test]
        public void ローカルタイムは生成したワールドタイムからの経過時間を返す()
        {
            var worldTime = WorldTime.FromFloat(15.0f);
            var localTime = LocalTime.FromWorldTime(worldTime);
            Assert.That(localTime.LocalSeconds(WorldTime.FromFloat(17.5f)), Is.EqualTo(2.5f));
        }
        [Test]
        public void ローカルタイム生成前()
        {
            var worldTime = WorldTime.FromFloat(15.0f);
            var localTime = LocalTime.FromWorldTime(worldTime);
            Assert.That(localTime.Created(WorldTime.FromFloat(14.9f)), Is.False);
            Assert.That(localTime.Created(WorldTime.FromFloat(15.0f)), Is.True);
        }
    }
}
