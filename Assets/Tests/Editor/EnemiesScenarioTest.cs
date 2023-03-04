using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Film.Domain.TimeStream;

namespace Film.Tests
{
    /// <summary>
    /// 実体をレンタル
    /// 出現
    /// 移動
    /// 射撃準備
    /// 射撃
    /// 退出
    /// 実体をリリース
    /// 
    /// 各行動に終了待ち
    /// 
    /// EnemiesSequencer
    /// MoveSequencer
    /// ShotSequencer
    /// LifeSequencer(ダメージの記録、生存判定)
    ///  -> 死亡->巻き戻し->やり直しで生存
    /// </summary>

    [System.Obsolete("各SequencersTestに移行予定")]
    public class EnemiesScenarioTest
    {
        #region インターフェイス仮実装

        // TODO:開始時刻をSequencerFactoryに設定する
        // TODO:Sequencer.Startは削除

        sealed class EnemySequencerFactory
        {
            List<CommandExecutor> commands = new List<CommandExecutor>();

            public EnemySequencerFactory(WorldTime startTime, Vector2 startposition)
            {
            }

            public EnemySequencerFactory Add(CommandExecutor executor)
            {
                commands.Add(executor);
                return this;
            }
            public EnemySequencer Create(EntityLender lender)
            {
                return new EnemySequencer(lender, commands);
            }
        }

        interface CommandExecutor
        {
            void Execute(LocalTime time);
        }

        sealed class StartPositionSetter : CommandExecutor
        {
            readonly Vector2 position;
            public StartPositionSetter(Vector2 position)
            {
                this.position = position;
            }
            void CommandExecutor.Execute(LocalTime time)
            {
            }
        }
        sealed class LinearMover : CommandExecutor
        {
            public LinearMover(Vector2 to, float duration)
            {
            }
            void CommandExecutor.Execute(LocalTime time)
            {
            }
        }



        sealed class EnemySequencer
        {
            readonly EntityLender lender;
            readonly IReadOnlyList<CommandExecutor> commands;
            Entity entity;
            LocalTime localTime;

            internal EnemySequencer(EntityLender lender, IReadOnlyList<CommandExecutor> commands)
            {
                this.lender = lender;
                this.commands = commands;
            }
            public void Start(WorldTime now)
            {
                localTime = LocalTime.FromWorldTime(now);
                entity = lender.Rent(0);
            }
            public void Execute(WorldTime now)
            {
                if (Expired(now))
                {
                    lender.Return(entity);
                    return;
                }
            }

            bool Expired(WorldTime now)
            {
                if (commands.Count == 0) return true;
                return false;
            }
        }
        interface Entity
        {
        }

        // TODO:必要な分だけ生成しておく
        interface EntityLender
        {
            Entity Rent(int id);
            void Return(Entity rental);
        }
        #endregion


        EntityLender lender;
        LenderState lenderState;
        EnemySequencer sequencer;

        WorldTime startTime = WorldTime.FromFloat(10);

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
#if false
            var l = new LenderImpl();
            lender = l;
            lenderState = l;
            var factory = new EnemySequencerFactory();
            sequencer = factory.Create(lender);
#endif
        }
        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
        }

        [Test, Order(0)]
        public void 処理を開始時にEntityをRentalしていること()
        {
            sequencer.Start(startTime);
            Assert.That(lenderState.Rentaled, Is.True);
        }
        [Test, Order(1)]
        public void 初期位置の設定()
        {
        }
        [Test, Order(2)]
        public void 出現コマンドを追加()
        {
        }

        [Test, Order(10000)]
        public void コマンドを何も指定していない場合_Update後にEntityを返却していること()
        {
#if false
            var factory = new EnemySequencerFactory();
            var noCommandSequencer = factory.Create(lender);
            noCommandSequencer.Start(WorldTime.FromFloat(10));
            noCommandSequencer.Execute(WorldTime.FromFloat(10));
            Assert.That(lenderState.Rentaled, Is.False);
#endif
        }


#region テスト用実装
        interface LenderState
        {
            bool Rentaled { get; }
        }
        class EntityImpl : Entity
        {
        }
        class LenderImpl : EntityLender, LenderState
        {
            bool LenderState.Rentaled => rentaled;
            bool rentaled = false;

            Entity EntityLender.Rent(int id)
            {
                rentaled = true;
                return new EntityImpl();
            }
            void EntityLender.Return(Entity rental)
            {
                rentaled = false;
            }
        }
#endregion

    }
}
