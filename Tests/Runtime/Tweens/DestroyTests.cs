using NUnit.Framework;
using Timespawn.EntityTween.Tweens;
using Unity.Entities;
using Unity.Mathematics;

namespace Timespawn.EntityTween.Tests.Tweens
{
    public class DestoryTests : TweenTestFixture
    {
        [Test]
        public void Translation()
        {
            Entity entity = EntityManager.CreateEntity();
            Tween.Move(EntityManager, entity, TestStartFloat3, TestEndFloat3, TestDuration);
            Test<TweenTranslation>(entity);
        }

        [Test]
        public void Rotation()
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

        private void Test<TTweenInfo>(Entity entity)
            where TTweenInfo : struct, IComponentData
        {
            World.Update();
            World.Update();

            TweenState tween = GetSingletonTweenState(entity);
            tween.Time = tween.Duration;
            SetSingletonTweenState(entity, tween);

            World.Update();

            Assert.IsFalse(EntityManager.HasComponent<TweenState>(entity));
            Assert.IsFalse(EntityManager.HasComponent<TTweenInfo>(entity));
        }
    }
}