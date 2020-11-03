using NUnit.Framework;
using Timespawn.EntityTween.Tweens;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Timespawn.EntityTween.Tests.Tweens
{
    public class GenerateTests : TweenTestFixture
    {
        [Test]
        public void Translation()
        {
            Entity entity = EntityManager.CreateEntity();
            Tween.Move(EntityManager, entity, TestStartFloat3, TestEndFloat3, TestDuration);
            Test<TweenTranslationCommand, TweenTranslation, Translation, float3>(entity, TestStartFloat3, TestEndFloat3, TestDuration);
        }

        [Test]
        public void Rotation()
        {
            Entity entity = EntityManager.CreateEntity();
            Tween.Rotate(EntityManager, entity, TestStartQuat, TestEndQuat, TestDuration);
            Test<TweenRotationCommand, TweenRotation, Rotation, quaternion>(entity, TestStartQuat, TestEndQuat, TestDuration);
        }

        [Test]
        public void Scale()
        {
            Entity entity = EntityManager.CreateEntity();
            Tween.Scale(EntityManager, entity, TestStartFloat3, TestEndFloat3, TestDuration);
            Test<TweenScaleCommand, TweenScale, NonUniformScale, float3>(entity, TestStartFloat3, TestEndFloat3, TestDuration);
        }

        private void Test<TTweenCommand, TTweenInfo, TTarget, TTweenInfoValue>(Entity entity, TTweenInfoValue start, TTweenInfoValue end, float duration)
            where TTweenCommand : struct, IComponentData, ITweenParams, ITweenInfo<TTweenInfoValue>
            where TTweenInfo : struct, IComponentData, ITweenId, ITweenInfo<TTweenInfoValue>
            where TTarget : struct, IComponentData
            where TTweenInfoValue : struct
        {
            Assert.IsTrue(EntityManager.HasComponent<TTweenCommand>(entity));

            World.Update();
            World.Update();

            Assert.IsFalse(EntityManager.HasComponent<TTweenCommand>(entity));
            Assert.IsTrue(EntityManager.HasComponent<TTweenInfo>(entity));
            Assert.IsTrue(EntityManager.HasComponent<TTarget>(entity));
            Assert.IsTrue(EntityManager.HasComponent<TweenState>(entity));

            TweenState tween = GetSingletonTweenState(entity);
            Assert.AreEqual(duration, tween.Duration);

            TTweenInfo info = EntityManager.GetComponentData<TTweenInfo>(entity);
            Assert.AreEqual(start, info.GetTweenStart());
            Assert.AreEqual(end, info.GetTweenEnd());
        }
    }
}