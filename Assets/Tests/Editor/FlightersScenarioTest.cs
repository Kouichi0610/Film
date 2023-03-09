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
    /// <summary>
    /// 
    /// ライフサイクル
    /// ・実体をレンタル
    /// ・出現
    /// ・移動
    /// ・射撃準備
    /// ・射撃
    /// ・退出
    /// ・実体をリリース
    /// 
    /// 
    /// EnemiesSequencer
    /// MoveSequencer
    /// ShotSequencer
    /// LifeSequencer(ダメージの記録、生存判定)
    ///  -> 死亡->巻き戻し->やり直しで生存
    /// </summary>
    public class FlightersScenarioTest
    {
        #region インターフェイス仮実装

        /// <summary>
        /// ・生存中であればEntityLenderから借りる、退場時に返却
        /// ・移動処理
        /// 
        /// ・ダメージ、ライフ
        /// 
        /// ・
        /// </summary>

        // FlightersScenario
        //  +LifeCycleSequencer 
        //  +MoveSequencer

        // 移動＆ライフサイクルの二つ(&攻撃)
        // MoveSequencerをinterfaceにする
        // RailSequnecer : interface
        // FreeMoveSequencer : interface...


        public sealed class FlightersScenario
        {
            readonly EntityLender lender;
            readonly LifeSequencer life;

            readonly MoveSequencer mover;
            delegate void UpdateAction(WorldTime now);

            Entity entity = null;
            UpdateAction update;

            public FlightersScenario(EntityLender lender, MoveSequencer mover, LifeSequencer life)
            {
                this.lender = lender;
                this.mover = mover;
                this.life = life;
                update = BirthAction;
            }

            public void Update(WorldTime now)
            {
                update(now);
            }

            void BirthAction(WorldTime now)
            {
                if (mover.Exists(now) == false) return;
                entity = lender.Lender();
                update = LivingAction;
                LivingAction(now);
            }
            void LivingAction(WorldTime now)
            {
                if (mover.Exists(now) == false)
                {
                    lender.Return(entity);
                    entity = null;
                    update = BirthAction;
                    return;
                }
                var position = mover.Move(now);
                entity.Locate(position);
            }
        }

        public interface EntityLender
        {
            Entity Lender();
            void Return(Entity entity);
        }
        
        public interface Entity
        {
            void Locate(Vector2 position);
            // Entity Hit();
        }
        #endregion

        LenderForTest testLender;
        MoveSequencer moveSequencer;
        LifeSequencer lifeSequencer;

        ExperimentLender experiment;

        [OneTimeSetUp]
        public void CreateLender()
        {
            testLender = new LenderForTest();
            moveSequencer = RoutedMoveSequencerBuilder.Start(new Vector2(5, 0), WorldTime.FromFloat(10.0f))
                .LinearMoveTo(new Vector2(10, 5), 10)
                .Build();
            lifeSequencer = LifeSequencers.Breakable(10);
            experiment = testLender;
        }

        [Test, Order(0)]
        public void シナリオは_MoveSequencer_LifeSequencer_EntityLender_から構成される()
        {
            var scenario = new FlightersScenario(testLender, moveSequencer, lifeSequencer);
            Assert.That(scenario, Is.Not.Null);
        }

        [Test, Order(1)]
        public void 移動期間中はEntityLenderからEntityを借りていること()
        {
            var scenario = new FlightersScenario(testLender, moveSequencer, lifeSequencer);
            scenario.Update(WorldTime.FromFloat(9.9f));
            Assert.That(experiment.Lendered, Is.False);

            scenario.Update(WorldTime.FromFloat(10.0f));
            Assert.That(experiment.Lendered, Is.True);
            scenario.Update(WorldTime.FromFloat(20.0f));
            Assert.That(experiment.Lendered, Is.True);

            scenario.Update(WorldTime.FromFloat(20.1f));
            Assert.That(experiment.Lendered, Is.False);
        }

        [Test, Order(2)]
        public void 移動期間中はMoveSequencerに従いEntityの位置が更新されていること()
        {
            var scenario = new FlightersScenario(testLender, moveSequencer, lifeSequencer);
            var expEntity = experiment.ExpEntity;
            scenario.Update(WorldTime.FromFloat(10.0f));
            Assert.That(expEntity.Position, Is.EqualTo(new Vector2(5, 0)));
            scenario.Update(WorldTime.FromFloat(15.0f));
            Assert.That(expEntity.Position, Is.EqualTo(new Vector2(7.5f, 2.5f)));
            scenario.Update(WorldTime.FromFloat(20.0f));
            Assert.That(expEntity.Position, Is.EqualTo(new Vector2(10, 5)));
        }

        // 与える？
        [Test, Order(3)]
        public void 移動期間中に当たったEntityからダメージを受けること()
        {
            Assert.That(false, Is.True);
        }

        #region テスト用実装

        public interface ExperimentLender
        {
            bool Lendered { get; }
            ExperimentEntity ExpEntity { get; }
        }
        public interface ExperimentEntity
        {
            Vector2 Position { get; }
        }
        sealed class LenderForTest : EntityLender, ExperimentLender
        {
            bool ExperimentLender.Lendered => entity == null;

            Entity entity;
            readonly ExperimentEntity expEntity;
            ExperimentEntity ExperimentLender.ExpEntity => expEntity;

            public LenderForTest()
            {
                var e = new EntityForTest();
                entity = e;
                expEntity = e;
            }

            Entity EntityLender.Lender()
            {
                if (entity == null) throw new System.InvalidOperationException();
                var res = entity;
                entity = null;
                return res;
            }
            void EntityLender.Return(Entity entity)
            {
                this.entity = entity;
            }
        }
        sealed class EntityForTest : Entity, ExperimentEntity
        {
            Vector2 ExperimentEntity.Position => position;
            Vector2 position;
            void Entity.Locate(Vector2 position)
            {
                this.position = position;
            }
        }
        #endregion

    }
}
