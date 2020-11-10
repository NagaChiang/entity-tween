using NUnit.Framework;
using Timespawn.EntityTween.Tests.Common;
using Timespawn.EntityTween.Tweens;
using Unity.Entities;
using Unity.Mathematics;

namespace Timespawn.EntityTween.Tests.Tweens
{
    public abstract class TweenTestFixture : ECSTestFixture
    {
        protected const float TestDuration = 1.0f;
        protected const float TestHalfDuration = TestDuration / 2.0f;
        protected const float TestDeltaTime = 0.2f;
        protected const int TestLoopCount = 2;

        protected readonly float3 TestStartFloat3 = new float3(0.0f);
        protected readonly float3 TestEndFloat3 = new float3(1.0f);
        protected readonly float4 TestStartFloat4 = new float4(0.0f);
        protected readonly float4 TestEndFloat4 = new float4(1.0f);
        protected readonly quaternion TestStartQuat = quaternion.identity;
        protected readonly quaternion TestEndQuat = quaternion.Euler(90.0f, 0.0f, 0.0f);

        protected TweenState GetSingletonTweenState(Entity entity)
        {
            DynamicBuffer<TweenState> tweenBuffer = EntityManager.GetBuffer<TweenState>(entity);
            Assert.AreEqual(1, tweenBuffer.Length);

            return tweenBuffer[0];
        }

        protected void SetSingletonTweenState(Entity entity, TweenState tweenState)
        {
            DynamicBuffer<TweenState> tweenBuffer = EntityManager.GetBuffer<TweenState>(entity);
            Assert.AreEqual(1, tweenBuffer.Length);

            tweenBuffer[0] = tweenState;
        }
    }
}