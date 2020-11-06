using NUnit.Framework;
using Timespawn.EntityTween.Tweens;
using Unity.Entities;

namespace Timespawn.EntityTween.Tests.Tweens
{
    public class StateTests : TweenTestFixture
    {
        [Test]
        public void Ease()
        {
            Entity entity = EntityManager.CreateEntity();
            Tween.Move(EntityManager, entity, TestStartFloat3, TestEndFloat3, TestDuration);

            World.Update();
            World.Update();
            OverrideNextDeltaTime(TestDeltaTime);
            World.Update();

            TweenState tween = GetSingletonTweenState(entity);
            Assert.AreEqual(TestDeltaTime, tween.Time);

            float easePercentage = Math.Ease.CalculatePercentage(TestDeltaTime / TestDuration, tween.EaseType, tween.EaseExponent);
            Assert.AreEqual(easePercentage, tween.EasePercentage);
        }

        [Test]
        public void Ease_Reverting()
        {
            Entity entity = EntityManager.CreateEntity();
            Tween.Move(EntityManager, entity, TestStartFloat3, TestEndFloat3, TestDuration, default, true);

            World.Update();
            World.Update();

            TweenState tween = GetSingletonTweenState(entity);
            tween.Time = TestHalfDuration;
            tween.IsReverting = true;
            SetSingletonTweenState(entity, tween);

            OverrideNextDeltaTime(TestDeltaTime);
            World.Update();

            tween = GetSingletonTweenState(entity);
            Assert.AreEqual(TestHalfDuration - TestDeltaTime, tween.Time);

            float easePercentage = Math.Ease.CalculatePercentage((TestHalfDuration - TestDeltaTime) / TestHalfDuration, tween.EaseType, tween.EaseExponent);
            Assert.AreEqual(easePercentage, tween.EasePercentage);
        }

        [Test]
        public void Ease_DelayedStart()
        {
            const float delayedStartTime = 3.0f;
            const float deltaTime = delayedStartTime / 2.0f;

            Entity entity = EntityManager.CreateEntity();
            Tween.Move(EntityManager, entity, TestStartFloat3, TestEndFloat3, TestDuration, delayedStartTime: delayedStartTime);

            World.Update();
            World.Update();
            OverrideNextDeltaTime(deltaTime);
            World.Update();

            TweenState tween = GetSingletonTweenState(entity);
            Assert.AreEqual(-deltaTime, tween.Time);
            Assert.AreEqual(0.0f, tween.EasePercentage);

            OverrideNextDeltaTime(deltaTime);
            World.Update();

            tween = GetSingletonTweenState(entity);
            Assert.AreEqual(0.0f, tween.Time);
            Assert.AreEqual(0.0f, tween.EasePercentage);
        }

        [Test]
        public void AtEnd()
        {
            Entity entity = EntityManager.CreateEntity();
            Tween.Move(EntityManager, entity, TestStartFloat3, TestEndFloat3, TestDuration);

            World.Update();
            World.Update();
            OverrideNextDeltaTime(TestDuration);
            World.Update();

            Assert.IsFalse(EntityManager.HasComponent<TweenState>(entity));
        }

        [Test]
        public void AtEnd_Loop()
        {
            Entity entity = EntityManager.CreateEntity();
            Tween.Move(EntityManager, entity, TestStartFloat3, TestEndFloat3, TestDuration, default, false, TestLoopCount);

            World.Update();
            World.Update();

            TweenState tween = GetSingletonTweenState(entity);
            Assert.AreEqual(TestLoopCount, tween.LoopCount);

            OverrideNextDeltaTime(TestDuration);
            World.Update();

            Assert.IsTrue(EntityManager.HasComponent<TweenState>(entity));

            tween = GetSingletonTweenState(entity);
            Assert.AreEqual(0.0f, tween.Time);
            Assert.IsFalse(tween.IsReverting);
            Assert.AreEqual(TestLoopCount - 1, tween.LoopCount);
        }

        [Test]
        public void AtEnd_Pingpong()
        {
            Entity entity = EntityManager.CreateEntity();
            Tween.Move(EntityManager, entity, TestStartFloat3, TestEndFloat3, TestDuration, default, true, TestLoopCount);

            World.Update();
            World.Update();

            TweenState tween = GetSingletonTweenState(entity);
            Assert.AreEqual(TestLoopCount, tween.LoopCount);

            OverrideNextDeltaTime(TestDuration);
            World.Update();

            Assert.IsTrue(EntityManager.HasComponent<TweenState>(entity));

            tween = GetSingletonTweenState(entity);
            Assert.AreEqual(TestHalfDuration, tween.Time);
            Assert.AreEqual(TestLoopCount, tween.LoopCount);
            Assert.IsTrue(tween.IsReverting);
        }

        [Test]
        public void AtEnd_InfiniteLoop()
        {
            Entity entity = EntityManager.CreateEntity();
            Tween.Move(EntityManager, entity, TestStartFloat3, TestEndFloat3, TestDuration, default, false, TweenState.LOOP_COUNT_INFINITE);

            World.Update();
            World.Update();

            TweenState tween = GetSingletonTweenState(entity);
            Assert.AreEqual(TweenState.LOOP_COUNT_INFINITE, tween.LoopCount);

            OverrideNextDeltaTime(TestDuration);
            World.Update();

            Assert.IsTrue(EntityManager.HasComponent<TweenState>(entity));

            tween = GetSingletonTweenState(entity);
            Assert.AreEqual(0.0f, tween.Time);
            Assert.IsFalse(tween.IsReverting);
            Assert.AreEqual(TweenState.LOOP_COUNT_INFINITE, tween.LoopCount);
        }

        [Test]
        public void AtStart_Pingpong()
        {
            Entity entity = EntityManager.CreateEntity();
            Tween.Move(EntityManager, entity, TestStartFloat3, TestEndFloat3, TestDuration, default, true);

            World.Update();
            World.Update();

            TweenState tween = GetSingletonTweenState(entity);
            tween.IsReverting = true;
            SetSingletonTweenState(entity, tween);

            World.Update();

            Assert.IsFalse(EntityManager.HasComponent<TweenState>(entity));
        }

        [Test]
        public void AtStart_Pingpong_Loop()
        {
            Entity entity = EntityManager.CreateEntity();
            Tween.Move(EntityManager, entity, TestStartFloat3, TestEndFloat3, TestDuration, default, true, TestLoopCount);

            World.Update();
            World.Update();

            TweenState tween = GetSingletonTweenState(entity);
            Assert.AreEqual(TestLoopCount, tween.LoopCount);

            tween.IsReverting = true;
            SetSingletonTweenState(entity, tween);

            World.Update();

            Assert.IsTrue(EntityManager.HasComponent<TweenState>(entity));

            tween = GetSingletonTweenState(entity);
            Assert.AreEqual(0.0f, tween.Time);
            Assert.AreEqual(TestLoopCount - 1, tween.LoopCount);
            Assert.IsFalse(tween.IsReverting);
        }

        [Test]
        public void AtStart_Pingpong_InfiniteLoop()
        {
            Entity entity = EntityManager.CreateEntity();
            Tween.Move(EntityManager, entity, TestStartFloat3, TestEndFloat3, TestDuration, default, true, TweenState.LOOP_COUNT_INFINITE);

            World.Update();
            World.Update();

            TweenState tween = GetSingletonTweenState(entity);
            Assert.AreEqual(TweenState.LOOP_COUNT_INFINITE, tween.LoopCount);

            tween.IsReverting = true;
            SetSingletonTweenState(entity, tween);

            World.Update();

            Assert.IsTrue(EntityManager.HasComponent<TweenState>(entity));

            tween = GetSingletonTweenState(entity);
            Assert.AreEqual(0.0f, tween.Time);
            Assert.AreEqual(TweenState.LOOP_COUNT_INFINITE, tween.LoopCount);
            Assert.IsFalse(tween.IsReverting);
        }

        [Test]
        public void PauseAndResume()
        {
            Entity entity = EntityManager.CreateEntity();
            Tween.Move(EntityManager, entity, TestStartFloat3, TestEndFloat3, TestDuration);

            World.Update();
            World.Update();

            Tween.Pause(EntityManager, entity);
            Assert.IsTrue(EntityManager.HasComponent<TweenPause>(entity));

            OverrideNextDeltaTime(TestDeltaTime);
            World.Update();

            TweenState tween = GetSingletonTweenState(entity);
            Assert.AreEqual(0.0f, tween.Time);

            Tween.Resume(EntityManager, entity);

            World.Update();

            Assert.IsFalse(EntityManager.HasComponent<TweenResumeCommand>(entity));
            Assert.IsFalse(EntityManager.HasComponent<TweenPause>(entity));

            OverrideNextDeltaTime(TestDeltaTime);
            World.Update();

            tween = GetSingletonTweenState(entity);
            Assert.AreEqual(TestDeltaTime, tween.Time);
        }

        [Test]
        public void Stop()
        {
            Entity entity = EntityManager.CreateEntity();
            Tween.Move(EntityManager, entity, TestStartFloat3, TestEndFloat3, TestDuration);

            World.Update();
            World.Update();

            Tween.Stop(EntityManager, entity);

            World.Update();

            Assert.IsFalse(EntityManager.HasComponent<TweenStopCommand>(entity));
            Assert.IsFalse(EntityManager.HasComponent<TweenState>(entity));
        }

        [Test]
        public void Stop_Paused()
        {
            Entity entity = EntityManager.CreateEntity();
            Tween.Move(EntityManager, entity, TestStartFloat3, TestEndFloat3, TestDuration);
            
            World.Update();
            World.Update();

            Tween.Pause(EntityManager, entity);
            Tween.Stop(EntityManager, entity);

            World.Update();

            Assert.IsFalse(EntityManager.HasComponent<TweenStopCommand>(entity));
            Assert.IsFalse(EntityManager.HasComponent<TweenPause>(entity));
            Assert.IsFalse(EntityManager.HasComponent<TweenState>(entity));
        }

        [Test]
        public void Stop_Paused_Resumed()
        {
            Entity entity = EntityManager.CreateEntity();
            Tween.Move(EntityManager, entity, TestStartFloat3, TestEndFloat3, TestDuration);
            
            World.Update();
            World.Update();

            Tween.Pause(EntityManager, entity);
            Tween.Resume(EntityManager, entity);
            Tween.Stop(EntityManager, entity);

            World.Update();

            Assert.IsFalse(EntityManager.HasComponent<TweenResumeCommand>(entity));
            Assert.IsFalse(EntityManager.HasComponent<TweenStopCommand>(entity));
            Assert.IsFalse(EntityManager.HasComponent<TweenPause>(entity));
            Assert.IsFalse(EntityManager.HasComponent<TweenState>(entity));
        }
    }
}