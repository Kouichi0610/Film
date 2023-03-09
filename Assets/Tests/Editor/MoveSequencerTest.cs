using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Film.Domain.TimeStream;
using Film.Domain.Sequence;

namespace Film.Tests
{
    /// <summary>
    /// 主に敵の移動ルーチン
    /// ・生成
    /// 　開始時刻と移動ルートを設定
    /// ・時間通りに進行すること
    /// ・巻き戻しに対応すること
    /// ・活動時間外の確認
    /// 
    /// ・スキップ確認
    /// 
    /// </summary>
    public class MoveSequencerTest
    {

        [Test, Order(0)]
        public void MoveSequencerBuilder_まず初期位置と開始時刻を設定する_これだけでは例外を投げること()
        {
            var builder = RoutedMoveSequencerBuilder.Start(new Vector2(0, 0), WorldTime.FromFloat(15.0f));

            Assert.Throws<ArgumentException>(() =>
            {
                builder.Build();
            });
        }
        [Test, Order(1)]
        public void MoveSequencerBuilder_一つ以上の目的位置を設定するとMoveSequencerを生成できること()
        {
            var builder = RoutedMoveSequencerBuilder.Start(new Vector2(0, 0), WorldTime.FromFloat(15.0f))
                .LinearMoveTo(new Vector2(15.0f, -15.0f), 10.0f);
            var sequencer = builder.Build();
            Assert.That(sequencer, Is.Not.Null);
        }
        [Test, Order(2)]
        public void MoveSequencerBuilder_移動時間に0以下を設定できないこと()
        {
            var builder = RoutedMoveSequencerBuilder.Start(new Vector2(0, 0), WorldTime.FromFloat(15.0f));

            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                builder.LinearMoveTo(Vector2.zero, 0);
            });
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                builder.LinearMoveTo(Vector2.zero, -1);
            });
        }
        [Test, Order(3)]
        public void MoveSequencerBuilder_一定時間停止を設定できること()
        {
            var builder = RoutedMoveSequencerBuilder.Start(new Vector2(0, 0), WorldTime.FromFloat(15.0f))
                .Stay(10.0f);
            var sequencer = builder.Build();
            Assert.That(sequencer, Is.Not.Null);
        }

        [Test, Order(5)]
        public void MoveSequencer_時刻と位置が対応していること()
        {
            var builder = RoutedMoveSequencerBuilder.Start(new Vector2(15, 30), WorldTime.FromFloat(15.0f))
                .Stay(10.0f)
                .LinearMoveTo(new Vector2(8, 36), 10.0f);
            var sequencer = builder.Build();

            Assert.That(sequencer.Move(WorldTime.FromFloat(14.9f)), Is.EqualTo(new Vector2(15, 30)));
            Assert.That(sequencer.Move(WorldTime.FromFloat(15.0f)), Is.EqualTo(new Vector2(15, 30)));
            Assert.That(sequencer.Move(WorldTime.FromFloat(20.0f)), Is.EqualTo(new Vector2(15, 30)));
            Assert.That(sequencer.Move(WorldTime.FromFloat(25.0f)), Is.EqualTo(new Vector2(15, 30)));
            Assert.That(sequencer.Move(WorldTime.FromFloat(35.0f)), Is.EqualTo(new Vector2(8, 36)));
            Assert.That(sequencer.Move(WorldTime.FromFloat(35.1f)), Is.EqualTo(new Vector2(8, 36)));
        }

        [Test, Order(6)]
        public void MoveSequencerBuilder_Duration()
        {
            var builder = RoutedMoveSequencerBuilder.Start(new Vector2(15, 30), WorldTime.FromFloat(15.0f))
                .Stay(10.0f)
                .LinearMoveTo(new Vector2(8, 36), 10.0f);
            Assert.That(builder.Duration(), Is.EqualTo(20));

        }

        #region テスト用実装
        #endregion
    }
}
