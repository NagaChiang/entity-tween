using NUnit.Framework;
using Timespawn.EntityTween.Math;
using Timespawn.EntityTween.Tweens;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Timespawn.EntityTween.Tests.Tweens
{
    public class ApplyTests : TweenTestFixture
    {
        [Test]
        public void Translation()
        {
            Entity entity = EntityManager.CreateEntity();
            Tween.Move(EntityManager, entity, TestStartFloat3, TestEndFloat3, TestDuration);

            World.Update();
            World.Update();
            OverrideNextDeltaTime(TestDeltaTime);
            World.Update();

            float percentage = Ease.CalculatePercentage(TestDeltaTime, default, default);
            float3 expected = math.lerp(TestStartFloat3, TestEndFloat3, percentage);
            Translation translation = EntityManager.GetComponentData<Translation>(entity);
            Assert.AreEqual(expected, translation.Value);
        }

        [Test]
        public void Rotation()
        {
            Entity entity = EntityManager.CreateEntity();
            Tween.Rotate(EntityManager, entity, TestStartQuat, TestEndQuat, TestDuration);

            World.Update();
            World.Update();
            OverrideNextDeltaTime(TestDeltaTime);
            World.Update();

            float percentage = Ease.CalculatePercentage(TestDeltaTime, default, default);
            quaternion expected = math.slerp(TestStartQuat, TestEndQuat, percentage);
            Rotation rotation = EntityManager.GetComponentData<Rotation>(entity);
            Assert.AreEqual(expected, rotation.Value);
        }

        [Test]
        public void Scale()
        {
            Entity entity = EntityManager.CreateEntity();
            Tween.Scale(EntityManager, entity, TestStartFloat3, TestEndFloat3, TestDuration);

            World.Update();
            World.Update();
            OverrideNextDeltaTime(TestDeltaTime);
            World.Update();

            float percentage = Ease.CalculatePercentage(TestDeltaTime, default, default);
            float3 expected = math.lerp(TestStartFloat3, TestEndFloat3, percentage);
            NonUniformScale scale = EntityManager.GetComponentData<NonUniformScale>(entity);
            Assert.AreEqual(expected, scale.Value);
        }
    }
}