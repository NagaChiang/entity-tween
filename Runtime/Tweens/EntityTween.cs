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
            float duration,
            float3 start, 
            float3 end, 
            EaseDesc easeDesc = default,
            bool isPingPong = false, 
            int loopCount = 1)
        {
            AssertParams(easeDesc.Exponent, loopCount);

            TweenParams tweenParams = new TweenParams(easeDesc.Type, (byte) easeDesc.Exponent, duration, isPingPong, (byte) loopCount);
            entityManager.AddComponentData(entity, new TweenTranslationCommand(tweenParams, start, end));
        }

        private static void AssertParams(int easeExponent, int loopCount)
        {
            Debug.Assert(byte.MinValue <= easeExponent && easeExponent <= byte.MaxValue, $"Exponent of ease function should be between {byte.MinValue} - {byte.MaxValue}.");
            Debug.Assert(byte.MinValue <= loopCount && loopCount <= byte.MaxValue, $"Loop count should be between {byte.MinValue} - {byte.MaxValue - 1}."); // Preserve 255 for Tween.LOOP_COUNT_PENDING_DESTROY
        }
    }
}