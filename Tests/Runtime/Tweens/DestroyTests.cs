using NUnit.Framework;
using Timespawn.EntityTween.Tweens;
using Unity.Entities;

#if UNITY_TINY_ALL_0_31_0
using Unity.Tiny;
#endif

#if UNITY_2D_ENTITIES
using Unity.U2D.Entities;
#endif

namespace Timespawn.EntityTween.Tests.Tweens
{
    public class DestoryTests : TweenTestFixture
    {
        [Test]
        public void Move()
        {
            Entity entity = EntityManager.CreateEntity();
            Tween.Move(EntityManager, entity, TestStartFloat3, TestEndFloat3, TestDuration);
            Test<TweenTranslation>(entity);
        }

        [Test]
        public void Rotate()
        {
            Entity entity = EntityManager.CreateEntity();
            Tween.Rotate(EntityManager, entity, TestStartQuat, TestEndQuat, TestDuration);
            Test<TweenRotation>(entity);
        }

        [Test]
        public void Scale()
        {
            Entity entity = EntityManager.CreateEntity();
            Tween.Scale(EntityManager, entity, TestStartFloat3, TestEndFloat3, TestDuration);
            Test<TweenScale>(entity);
        }

        [Test]
        public void MoveAndRotate()
        {
            Entity entity = EntityManager.CreateEntity();
            Tween.Move(EntityManager, entity, TestStartFloat3, TestEndFloat3, TestDuration);
            Tween.Rotate(EntityManager, entity, TestStartQuat, TestEndQuat, TestDuration);

            World.Update();
            World.Update();

            OverrideNextDeltaTime(TestDuration);

            World.Update();
            World.Update();
            
            Assert.IsFalse(EntityManager.HasComponent<TweenState>(entity));
            Assert.IsFalse(EntityManager.HasComponent<TweenTranslation>(entity));
            Assert.IsFalse(EntityManager.HasComponent<TweenRotation>(entity));
        }

#if UNITY_TINY_ALL_0_31_0 || UNITY_2D_ENTITIES
    
        [Test]
        public void Tint()
        {
            Entity entity = EntityManager.CreateEntity();
            Tween.Tint(EntityManager, entity, TestStartFloat4, TestEndFloat4, TestDuration);
            Test<TweenTint>(entity);
        }

#endif

        private void Test<TTweenInfo>(Entity entity)
            where TTweenInfo : struct, IComponentData
        {
            World.Update();
            World.Update();

            TweenState tween = GetSingletonTweenState(entity);
            tween.Time = tween.Duration;
            SetSingletonTweenState(entity, tween);

            World.Update();
            World.Update();

            Assert.IsFalse(EntityManager.HasComponent<TweenState>(entity));
            Assert.IsFalse(EntityManager.HasComponent<TTweenInfo>(entity));
        }
    }
}