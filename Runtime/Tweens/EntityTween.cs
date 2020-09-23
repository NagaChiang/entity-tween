using Timespawn.EntityTween.Math;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Timespawn.EntityTween.Tweens
{
    public static class EntityTween
    {
        public static void Move(
            EntityManager entityManager,
            Entity entity,
            float3 start, 
            float3 end, 
            float duration,
            EaseDesc easeDesc = default,
            bool isPingPong = false, 
            int loopCount = 1)
        {
            AssertParams(easeDesc.Exponent, loopCount);

            TweenParams tweenParams = new TweenParams(easeDesc.Type, easeDesc.Exponent, duration, isPingPong, loopCount);
            entityManager.AddComponentData(entity, new TweenTranslationCommand(tweenParams, start, end));
        }

        public static void Move(
            EntityCommandBuffer commandBuffer,
            Entity entity,
            float3 start, 
            float3 end, 
            float duration,
            EaseDesc easeDesc = default,
            bool isPingPong = false, 
            int loopCount = 1)
        {
            AssertParams(easeDesc.Exponent, loopCount);

            TweenParams tweenParams = new TweenParams(easeDesc.Type, easeDesc.Exponent, duration, isPingPong, loopCount);
            commandBuffer.AddComponent(entity, new TweenTranslationCommand(tweenParams, start, end));
        }

        public static void Move(
            EntityCommandBuffer.ParallelWriter parallelWriter,
            int sortKey,
            Entity entity,
            float3 start, 
            float3 end, 
            float duration,
            EaseDesc easeDesc = default,
            bool isPingPong = false, 
            int loopCount = 1)
        {
            AssertParams(easeDesc.Exponent, loopCount);

            TweenParams tweenParams = new TweenParams(easeDesc.Type, easeDesc.Exponent, duration, isPingPong, loopCount);
            parallelWriter.AddComponent(sortKey, entity, new TweenTranslationCommand(tweenParams, start, end));
        }

        public static void Rotate(
            EntityManager entityManager,
            Entity entity,
            quaternion start, 
            quaternion end, 
            float duration,
            EaseDesc easeDesc = default,
            bool isPingPong = false, 
            int loopCount = 1)
        {
            AssertParams(easeDesc.Exponent, loopCount);

            TweenParams tweenParams = new TweenParams(easeDesc.Type, easeDesc.Exponent, duration, isPingPong, loopCount);
            entityManager.AddComponentData(entity, new TweenRotationCommand(tweenParams, start, end));
        }

        public static void Rotate(
            EntityCommandBuffer commandBuffer,
            Entity entity,
            quaternion start, 
            quaternion end, 
            float duration,
            EaseDesc easeDesc = default,
            bool isPingPong = false, 
            int loopCount = 1)
        {
            AssertParams(easeDesc.Exponent, loopCount);

            TweenParams tweenParams = new TweenParams(easeDesc.Type, easeDesc.Exponent, duration, isPingPong, loopCount);
            commandBuffer.AddComponent(entity, new TweenRotationCommand(tweenParams, start, end));
        }

        public static void Rotate(
            EntityCommandBuffer.ParallelWriter parallelWriter,
            int sortKey,
            Entity entity,
            quaternion start, 
            quaternion end, 
            float duration,
            EaseDesc easeDesc = default,
            bool isPingPong = false, 
            int loopCount = 1)
        {
            AssertParams(easeDesc.Exponent, loopCount);

            TweenParams tweenParams = new TweenParams(easeDesc.Type, easeDesc.Exponent, duration, isPingPong, loopCount);
            parallelWriter.AddComponent(sortKey, entity, new TweenRotationCommand(tweenParams, start, end));
        }

        private static void AssertParams(int easeExponent, int loopCount)
        {
            Debug.Assert(byte.MinValue <= easeExponent && easeExponent <= byte.MaxValue, $"Exponent of ease function should be between {byte.MinValue} - {byte.MaxValue}.");
            Debug.Assert(byte.MinValue <= loopCount && loopCount <= byte.MaxValue, $"Loop count should be between {byte.MinValue} - {byte.MaxValue - 1}."); // Preserve 255 for Tween.LOOP_COUNT_PENDING_DESTROY
        }
    }
}