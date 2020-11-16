using System.Runtime.CompilerServices;
using Timespawn.EntityTween.Math;
using Unity.Entities;
using Unity.Mathematics;

[assembly: InternalsVisibleTo("Timespawn.EntityTween.Tests")]

namespace Timespawn.EntityTween.Tweens
{
    public static class Tween
    {
        public const byte Infinite = TweenState.LOOP_COUNT_INFINITE;

        public static void Move(
            in EntityManager entityManager,
            in Entity entity,
            in float3 start,
            in float3 end, 
            in TweenParams tweenParams)
        {
            Move(entityManager, entity, start, end, tweenParams.Duration, new EaseDesc(tweenParams.EaseType, tweenParams.EaseExponent), tweenParams.IsPingPong, tweenParams.LoopCount, tweenParams.StartDelay);
        }

        public static void Move(
            in EntityCommandBuffer commandBuffer, 
            in Entity entity, 
            in float3 start, 
            in float3 end,
            in TweenParams tweenParams)
        {
            Move(commandBuffer, entity, start, end, tweenParams.Duration, new EaseDesc(tweenParams.EaseType, tweenParams.EaseExponent), tweenParams.IsPingPong, tweenParams.LoopCount, tweenParams.StartDelay);
        }

        public static void Move(
            in EntityCommandBuffer.ParallelWriter parallelWriter, 
            in int sortKey, 
            in Entity entity, 
            in float3 start, 
            in float3 end, 
            in TweenParams tweenParams)
        {
            Move(parallelWriter, sortKey, entity, start, end, tweenParams.Duration, new EaseDesc(tweenParams.EaseType, tweenParams.EaseExponent), tweenParams.IsPingPong, tweenParams.LoopCount, tweenParams.StartDelay);
        }

        public static void Move(
            in EntityManager entityManager,
            in Entity entity,
            in float3 start, 
            in float3 end, 
            in float duration,
            in EaseDesc easeDesc = default,
            in bool isPingPong = false, 
            in int loopCount = 1,
            in float startDelay = 0.0f)
        {
            if (!CheckParams(easeDesc.Exponent, loopCount))
            {
                return;
            }

            TweenParams tweenParams = new TweenParams(duration, easeDesc.Type, easeDesc.Exponent, isPingPong, loopCount, startDelay);
            entityManager.AddComponentData(entity, new TweenTranslationCommand(tweenParams, start, end));
        }

        public static void Move(
            in EntityCommandBuffer commandBuffer,
            in Entity entity,
            in float3 start, 
            in float3 end, 
            in float duration,
            in EaseDesc easeDesc = default,
            in bool isPingPong = false, 
            in int loopCount = 1,
            in float startDelay = 0.0f)
        {
            if (!CheckParams(easeDesc.Exponent, loopCount))
            {
                return;
            }

            TweenParams tweenParams = new TweenParams(duration, easeDesc.Type, easeDesc.Exponent, isPingPong, loopCount, startDelay);
            commandBuffer.AddComponent(entity, new TweenTranslationCommand(tweenParams, start, end));
        }

        public static void Move(
            in EntityCommandBuffer.ParallelWriter parallelWriter,
            in int sortKey,
            in Entity entity,
            in float3 start, 
            in float3 end, 
            in float duration,
            in EaseDesc easeDesc = default,
            in bool isPingPong = false, 
            in int loopCount = 1,
            in float startDelay = 0.0f)
        {
            if (!CheckParams(easeDesc.Exponent, loopCount))
            {
                return;
            }

            TweenParams tweenParams = new TweenParams(duration, easeDesc.Type, easeDesc.Exponent, isPingPong, loopCount, startDelay);
            parallelWriter.AddComponent(sortKey, entity, new TweenTranslationCommand(tweenParams, start, end));
        }

        public static void Rotate(
            in EntityManager entityManager, 
            in Entity entity, 
            in quaternion start, 
            in quaternion end, 
            in TweenParams tweenParams)
        {
            Rotate(entityManager, entity, start, end, tweenParams.Duration, new EaseDesc(tweenParams.EaseType, tweenParams.EaseExponent), tweenParams.IsPingPong, tweenParams.LoopCount, tweenParams.StartDelay);
        }

        public static void Rotate(
            in EntityCommandBuffer commandBuffer, 
            in Entity entity, 
            in quaternion start, 
            in quaternion end, 
            in TweenParams tweenParams)
        {
            Rotate(commandBuffer, entity, start, end, tweenParams.Duration, new EaseDesc(tweenParams.EaseType, tweenParams.EaseExponent), tweenParams.IsPingPong, tweenParams.LoopCount, tweenParams.StartDelay);
        }

        public static void Rotate(
            in EntityCommandBuffer.ParallelWriter parallelWriter, 
            in int sortKey, 
            in Entity entity, 
            in quaternion start, 
            in quaternion end,
            in TweenParams tweenParams)
        {
            Rotate(parallelWriter, sortKey, entity, start, end, tweenParams.Duration, new EaseDesc(tweenParams.EaseType, tweenParams.EaseExponent), tweenParams.IsPingPong, tweenParams.LoopCount, tweenParams.StartDelay);
        }

        public static void Rotate(
            in EntityManager entityManager,
            in Entity entity,
            in quaternion start, 
            in quaternion end, 
            in float duration,
            in EaseDesc easeDesc = default,
            in bool isPingPong = false, 
            in int loopCount = 1,
            in float startDelay = 0.0f)
        {
            if (!CheckParams(easeDesc.Exponent, loopCount))
            {
                return;
            }

            TweenParams tweenParams = new TweenParams(duration, easeDesc.Type, easeDesc.Exponent, isPingPong, loopCount, startDelay);
            entityManager.AddComponentData(entity, new TweenRotationCommand(tweenParams, start, end));
        }

        public static void Rotate(
            in EntityCommandBuffer commandBuffer,
            in Entity entity,
            in quaternion start, 
            in quaternion end, 
            in float duration,
            in EaseDesc easeDesc = default,
            in bool isPingPong = false, 
            in int loopCount = 1,
            in float startDelay = 0.0f)
        {
            if (!CheckParams(easeDesc.Exponent, loopCount))
            {
                return;
            }

            TweenParams tweenParams = new TweenParams(duration, easeDesc.Type, easeDesc.Exponent, isPingPong, loopCount, startDelay);
            commandBuffer.AddComponent(entity, new TweenRotationCommand(tweenParams, start, end));
        }

        public static void Rotate(
            in EntityCommandBuffer.ParallelWriter parallelWriter,
            in int sortKey,
            in Entity entity,
            in quaternion start, 
            in quaternion end, 
            in float duration,
            in EaseDesc easeDesc = default,
            in bool isPingPong = false, 
            in int loopCount = 1,
            in float startDelay = 0.0f)
        {
            if (!CheckParams(easeDesc.Exponent, loopCount))
            {
                return;
            }

            TweenParams tweenParams = new TweenParams(duration, easeDesc.Type, easeDesc.Exponent, isPingPong, loopCount, startDelay);
            parallelWriter.AddComponent(sortKey, entity, new TweenRotationCommand(tweenParams, start, end));
        }

        public static void Scale(
            in EntityManager entityManager, 
            in Entity entity, 
            in float3 start,
            in float3 end, 
            in TweenParams tweenParams)
        {
            Scale(entityManager, entity, start, end, tweenParams.Duration, new EaseDesc(tweenParams.EaseType, tweenParams.EaseExponent), tweenParams.IsPingPong, tweenParams.LoopCount, tweenParams.StartDelay);
        }

        public static void Scale(
            in EntityCommandBuffer commandBuffer, 
            in Entity entity, 
            in float3 start, 
            in float3 end,
            in TweenParams tweenParams)
        {
            Scale(commandBuffer, entity, start, end, tweenParams.Duration, new EaseDesc(tweenParams.EaseType, tweenParams.EaseExponent), tweenParams.IsPingPong, tweenParams.LoopCount, tweenParams.StartDelay);
        }

        public static void Scale(
            in EntityCommandBuffer.ParallelWriter parallelWriter, 
            in int sortKey, 
            in Entity entity, 
            in float3 start, 
            in float3 end, 
            in TweenParams tweenParams)
        {
            Scale(parallelWriter, sortKey, entity, start, end, tweenParams.Duration, new EaseDesc(tweenParams.EaseType, tweenParams.EaseExponent), tweenParams.IsPingPong, tweenParams.LoopCount, tweenParams.StartDelay);
        }

        public static void Scale(
            in EntityManager entityManager,
            in Entity entity,
            in float3 start, 
            in float3 end, 
            in float duration,
            in EaseDesc easeDesc = default,
            in bool isPingPong = false, 
            in int loopCount = 1,
            in float startDelay = 0.0f)
        {
            if (!CheckParams(easeDesc.Exponent, loopCount))
            {
                return;
            }

            TweenParams tweenParams = new TweenParams(duration, easeDesc.Type, easeDesc.Exponent, isPingPong, loopCount, startDelay);
            entityManager.AddComponentData(entity, new TweenScaleCommand(tweenParams, start, end));
        }

        public static void Scale(
            in EntityCommandBuffer commandBuffer,
            in Entity entity,
            in float3 start, 
            in float3 end, 
            in float duration,
            in EaseDesc easeDesc = default,
            in bool isPingPong = false, 
            in int loopCount = 1,
            in float startDelay = 0.0f)
        {
            if (!CheckParams(easeDesc.Exponent, loopCount))
            {
                return;
            }

            TweenParams tweenParams = new TweenParams(duration, easeDesc.Type, easeDesc.Exponent, isPingPong, loopCount, startDelay);
            commandBuffer.AddComponent(entity, new TweenScaleCommand(tweenParams, start, end));
        }

        public static void Scale(
            in EntityCommandBuffer.ParallelWriter parallelWriter,
            in int sortKey,
            in Entity entity,
            in float3 start, 
            in float3 end, 
            in float duration,
            in EaseDesc easeDesc = default,
            in bool isPingPong = false, 
            in int loopCount = 1,
            in float startDelay = 0.0f)
        {
            if (!CheckParams(easeDesc.Exponent, loopCount))
            {
                return;
            }

            TweenParams tweenParams = new TweenParams(duration, easeDesc.Type, easeDesc.Exponent, isPingPong, loopCount, startDelay);
            parallelWriter.AddComponent(sortKey, entity, new TweenScaleCommand(tweenParams, start, end));
        }

#if UNITY_TINY_ALL_0_31_0 || UNITY_2D_ENTITIES

        public static void Tint(
            in EntityManager entityManager, 
            in Entity entity, 
            in float4 start, 
            in float4 end, 
            in TweenParams tweenParams)
        {
            Tint(entityManager, entity, start, end, tweenParams.Duration, new EaseDesc(tweenParams.EaseType, tweenParams.EaseExponent), tweenParams.IsPingPong, tweenParams.LoopCount, tweenParams.StartDelay);
        }

        public static void Tint(
            in EntityCommandBuffer commandBuffer, 
            in Entity entity, 
            in float4 start, 
            in float4 end, 
            in TweenParams tweenParams)
        {
            Tint(commandBuffer, entity, start, end, tweenParams.Duration, new EaseDesc(tweenParams.EaseType, tweenParams.EaseExponent), tweenParams.IsPingPong, tweenParams.LoopCount, tweenParams.StartDelay);
        }

        public static void Tint(
            in EntityCommandBuffer.ParallelWriter parallelWriter, 
            in int sortKey, 
            in Entity entity, 
            in float4 start, 
            in float4 end, 
            in TweenParams tweenParams)
        {
            Tint(parallelWriter, sortKey, entity, start, end, tweenParams.Duration, new EaseDesc(tweenParams.EaseType, tweenParams.EaseExponent), tweenParams.IsPingPong, tweenParams.LoopCount, tweenParams.StartDelay);
        }

        public static void Tint(
            in EntityManager entityManager,
            in Entity entity,
            in float4 start, 
            in float4 end, 
            in float duration,
            in EaseDesc easeDesc = default,
            in bool isPingPong = false, 
            in int loopCount = 1,
            in float startDelay = 0.0f)
        {
            if (!CheckParams(easeDesc.Exponent, loopCount))
            {
                return;
            }

            TweenParams tweenParams = new TweenParams(duration, easeDesc.Type, easeDesc.Exponent, isPingPong, loopCount, startDelay);
            entityManager.AddComponentData(entity, new TweenTintCommand(tweenParams, start, end));
        }

        public static void Tint(
            in EntityCommandBuffer commandBuffer,
            in Entity entity,
            in float4 start, 
            in float4 end, 
            in float duration,
            in EaseDesc easeDesc = default,
            in bool isPingPong = false, 
            in int loopCount = 1,
            in float startDelay = 0.0f)
        {
            if (!CheckParams(easeDesc.Exponent, loopCount))
            {
                return;
            }

            TweenParams tweenParams = new TweenParams(duration, easeDesc.Type, easeDesc.Exponent, isPingPong, loopCount, startDelay);
            commandBuffer.AddComponent(entity, new TweenTintCommand(tweenParams, start, end));
        }

        public static void Tint(
            in EntityCommandBuffer.ParallelWriter parallelWriter,
            in int sortKey,
            in Entity entity,
            in float4 start, 
            in float4 end, 
            in float duration,
            in EaseDesc easeDesc = default,
            in bool isPingPong = false, 
            in int loopCount = 1,
            in float startDelay = 0.0f)
        {
            if (!CheckParams(easeDesc.Exponent, loopCount))
            {
                return;
            }

            TweenParams tweenParams = new TweenParams(duration, easeDesc.Type, easeDesc.Exponent, isPingPong, loopCount, startDelay);
            parallelWriter.AddComponent(sortKey, entity, new TweenTintCommand(tweenParams, start, end));
        }

#endif

        public static void Pause(in EntityManager entityManager, in Entity entity)
        {
            entityManager.AddComponent<TweenPause>(entity);
        }

        public static void Pause(in EntityCommandBuffer commandBuffer, in Entity entity)
        {
            commandBuffer.AddComponent<TweenPause>(entity);
        }

        public static void Pause(in EntityCommandBuffer.ParallelWriter parallelWriter, in int sortKey, in Entity entity)
        {
            parallelWriter.AddComponent<TweenPause>(sortKey, entity);
        }

        public static void Resume(in EntityManager entityManager, in Entity entity)
        {
            entityManager.AddComponent<TweenResumeCommand>(entity);
        }

        public static void Resume(in EntityCommandBuffer commandBuffer, in Entity entity)
        {
            commandBuffer.AddComponent<TweenResumeCommand>(entity);
        }

        public static void Resume(in EntityCommandBuffer.ParallelWriter parallelWriter, in int sortKey, in Entity entity)
        {
            parallelWriter.AddComponent<TweenResumeCommand>(sortKey, entity);
        }

        public static void Stop(in EntityManager entityManager, in Entity entity)
        {
            entityManager.AddComponent<TweenStopCommand>(entity);
        }

        public static void Stop(in EntityCommandBuffer commandBuffer, in Entity entity)
        {
            commandBuffer.AddComponent<TweenStopCommand>(entity);
        }

        public static void Stop(in EntityCommandBuffer.ParallelWriter parallelWriter, in int sortKey, in Entity entity)
        {
            parallelWriter.AddComponent<TweenStopCommand>(sortKey, entity);
        }

        private static bool CheckParams(in int easeExponent, in int loopCount)
        {
            if (easeExponent < byte.MinValue || easeExponent > byte.MaxValue)
            {
                return false;
            }

            if (loopCount < byte.MinValue || loopCount > byte.MaxValue)
            {
                return false;
            }

            return true;
        }
    }
}