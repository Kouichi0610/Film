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
            var sequencer = LifeSequencers.Breakable(100);
            Assert.That(sequencer, Is.Not.Null);
        }

        [Test, Order(2)]
        public void LifeSequencer_初期に設定したヒットポイントを超えるダメージを受けると死亡扱いになること()
        {
            var sequencer = LifeSequencers.Breakable(100);
            sequencer.Damage(90, WorldTime.FromFloat(15.0f));
            Assert.That(sequencer.Living(WorldTime.FromFloat(15.0f)), Is.True);
            sequencer.Damage(10, WorldTime.FromFloat(16.0f));
            Assert.That(sequencer.Living(WorldTime.FromFloat(16.0f)), Is.False);
        }

        [Test, Order(3)]
        public void LifeSequencer_過去からやりなおす_未来のダメージが無かったことになっていること()
        {
            var sequencer = LifeSequencers.Breakable(100);
            sequencer.Damage(100, WorldTime.FromFloat(10.0f));
            Assert.That(sequencer.Living(WorldTime.FromFloat(10.0f)), Is.False);
            sequencer.Rewind(WorldTime.FromFloat(9.9f));
            Assert.That(sequencer.Living(WorldTime.FromFloat(10.0f)), Is.True);
        }

        [Test, Order(4)]
        public void InvincibleLiveSequencer_ダメージの影響を受けないこと()
        {
            var sequencer = LifeSequencers.Invincible();
            sequencer.Damage(int.MaxValue, WorldTime.FromFloat(10));
            Assert.That(sequencer.Living(WorldTime.FromFloat(10)), Is.True);
        }

    }
}
