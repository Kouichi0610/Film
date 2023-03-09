using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Film.Domain.TimeStream;
using Film.Domain.Transport;
using Film.Domain.Sequence;

namespace Film.Tests
{
    public class LifeSequencerTest
    {
        [Test, Order(0)]
        public void LifeSequencer_初期ヒットポイントを設定()
        {
            var sequencer = LifeSequencer.FromHitPoint(100);
            Assert.That(sequencer, Is.Not.Null);
        }

        [Test, Order(2)]
        public void LifeSequencer_ダメージログを取り特定の時刻のヒットポイントを返す()
        {
            var sequencer = LifeSequencer.FromHitPoint(100);
            sequencer.Damage(12, WorldTime.FromFloat(15.0f));
            sequencer.Damage(5, WorldTime.FromFloat(16.0f));
            sequencer.Damage(8, WorldTime.FromFloat(20.0f));
            Assert.That(sequencer.HitPoint(WorldTime.FromFloat(14.9f)), Is.EqualTo(100));
            Assert.That(sequencer.HitPoint(WorldTime.FromFloat(15.0f)), Is.EqualTo(88));
            Assert.That(sequencer.HitPoint(WorldTime.FromFloat(15.9f)), Is.EqualTo(88));
            Assert.That(sequencer.HitPoint(WorldTime.FromFloat(16.0f)), Is.EqualTo(83));
            Assert.That(sequencer.HitPoint(WorldTime.FromFloat(19.9f)), Is.EqualTo(83));
            Assert.That(sequencer.HitPoint(WorldTime.FromFloat(20.0f)), Is.EqualTo(75));
        }
        [Test, Order(3)]
        public void LifeSequencer_0未満にならないこと()
        {
            var sequencer = LifeSequencer.FromHitPoint(100);
            sequencer.Damage(150, WorldTime.FromFloat(15.0f));
            Assert.That(sequencer.HitPoint(WorldTime.FromFloat(20)), Is.Zero);

        }

        [Test, Order(2)]
        public void LifeSequencer_死亡確認()
        {
            var sequencer = LifeSequencer.FromHitPoint(100);
            sequencer.Damage(90, WorldTime.FromFloat(15.0f));
            Assert.That(sequencer.Defeated(WorldTime.FromFloat(15.0f)), Is.False);
            sequencer.Damage(10, WorldTime.FromFloat(16.0f));
            Assert.That(sequencer.Defeated(WorldTime.FromFloat(16.0f)), Is.True);
        }

        [Test, Order(3)]
        public void LifeSequencer_過去からやりなおす_発生する予定のダメージが無かったことになっていること()
        {
            var sequencer = LifeSequencer.FromHitPoint(100);
            sequencer.Damage(10, WorldTime.FromFloat(15.0f));
            sequencer.Damage(15, WorldTime.FromFloat(18.0f));
            Assert.That(sequencer.HitPoint(WorldTime.FromFloat(18.0f)), Is.EqualTo(75));
            var redoSequencer = sequencer.Rewind(WorldTime.FromFloat(15.0f));
            Assert.That(redoSequencer.HitPoint(WorldTime.FromFloat(18.0f)), Is.EqualTo(90));
        }

    }
}
