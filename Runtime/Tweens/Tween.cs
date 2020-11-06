using System.Runtime.CompilerServices;
using Timespawn.EntityTween.Math;
using Unity.Entities;
using Unity.Mathematics;

[assembly: InternalsVisibleTo("Timespawn.EntityTween.Tests")]

namespace Timespawn.EntityTween.Tweens
{
    public static class Tween
    {
        public static void Move(EntityManager entityManager, Entity entity, float3 start, float3 end, TweenParams tweenParams)
        {
            Move(entityManager, entity, start, end, tweenParams.Duration, new EaseDesc(tweenParams.EaseType, tweenParams.EaseExponent), tweenParams.IsPingPong, tweenParams.LoopCount, tweenParams.StartDelay);
        }

        public static void Move(EntityCommandBuffer commandBuffer, Entity entity, float3 start, float3 end, TweenParams tweenParams)
        {
            Move(commandBuffer, entity, start, end, tweenParams.Duration, new EaseDesc(tweenParams.EaseType, tweenParams.EaseExponent), tweenParams.IsPingPong, tweenParams.LoopCount, tweenParams.StartDelay);
        }

        public static void Move(EntityCommandBuffer.ParallelWriter parallelWriter, int sortKey, Entity entity, float3 start, float3 end, TweenParams tweenParams)
        {
            Move(parallelWriter, sortKey, entity, start, end, tweenParams.Duration, new EaseDesc(tweenParams.EaseType, tweenParams.EaseExponent), tweenParams.IsPingPong, tweenParams.LoopCount, tweenParams.StartDelay);
        }

        public static void Move(
            EntityManager entityManager,
            Entity entity,
            float3 start, 
            float3 end, 
            float duration,
            EaseDesc easeDesc = default,
            bool isPingPong = false, 
            int loopCount = 1,
            float startDelay = 0.0f)
        {
            if (!CheckParams(easeDesc.Exponent, loopCount))
            {
                return;
            }

            TweenParams tweenParams = new TweenParams(duration, easeDesc.Type, easeDesc.Exponent, isPingPong, loopCount, startDelay);
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
            int loopCount = 1,
            float startDelay = 0.0f)
        {
            if (!CheckParams(easeDesc.Exponent, loopCount))
            {
                return;
            }

            TweenParams tweenParams = new TweenParams(duration, easeDesc.Type, easeDesc.Exponent, isPingPong, loopCount, startDelay);
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
            int loopCount = 1,
            float startDelay = 0.0f)
        {
            if (!CheckParams(easeDesc.Exponent, loopCount))
            {
                return;
            }

            TweenParams tweenParams = new TweenParams(duration, easeDesc.Type, easeDesc.Exponent, isPingPong, loopCount, startDelay);
            parallelWriter.AddComponent(sortKey, entity, new TweenTranslationCommand(tweenParams, start, end));
        }

        public static void Rotate(EntityManager entityManager, Entity entity, quaternion start, quaternion end, TweenParams tweenParams)
        {
            Rotate(entityManager, entity, start, end, tweenParams.Duration, new EaseDesc(tweenParams.EaseType, tweenParams.EaseExponent), tweenParams.IsPingPong, tweenParams.LoopCount, tweenParams.StartDelay);
        }

        public static void Rotate(EntityCommandBuffer commandBuffer, Entity entity, quaternion start, quaternion end, TweenParams tweenParams)
        {
            Rotate(commandBuffer, entity, start, end, tweenParams.Duration, new EaseDesc(tweenParams.EaseType, tweenParams.EaseExponent), tweenParams.IsPingPong, tweenParams.LoopCount, tweenParams.StartDelay);
        }

        public static void Rotate(EntityCommandBuffer.ParallelWriter parallelWriter, int sortKey, Entity entity, quaternion start, quaternion end, TweenParams tweenParams)
        {
            Rotate(parallelWriter, sortKey, entity, start, end, tweenParams.Duration, new EaseDesc(tweenParams.EaseType, tweenParams.EaseExponent), tweenParams.IsPingPong, tweenParams.LoopCount, tweenParams.StartDelay);
        }

        public static void Rotate(
            EntityManager entityManager,
            Entity entity,
            quaternion start, 
            quaternion end, 
            float duration,
            EaseDesc easeDesc = default,
            bool isPingPong = false, 
            int loopCount = 1,
            float startDelay = 0.0f)
        {
            if (!CheckParams(easeDesc.Exponent, loopCount))
            {
                return;
            }

            TweenParams tweenParams = new TweenParams(duration, easeDesc.Type, easeDesc.Exponent, isPingPong, loopCount, startDelay);
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
            int loopCount = 1,
            float startDelay = 0.0f)
        {
            if (!CheckParams(easeDesc.Exponent, loopCount))
            {
                return;
            }

            TweenParams tweenParams = new TweenParams(duration, easeDesc.Type, easeDesc.Exponent, isPingPong, loopCount, startDelay);
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
            int loopCount = 1,
            float startDelay = 0.0f)
        {
            if (!CheckParams(easeDesc.Exponent, loopCount))
            {
                return;
            }

            TweenParams tweenParams = new TweenParams(duration, easeDesc.Type, easeDesc.Exponent, isPingPong, loopCount, startDelay);
            parallelWriter.AddComponent(sortKey, entity, new TweenRotationCommand(tweenParams, start, end));
        }

        public static void Scale(EntityManager entityManager, Entity entity, float3 start, float3 end, TweenParams tweenParams)
        {
            Scale(entityManager, entity, start, end, tweenParams.Duration, new EaseDesc(tweenParams.EaseType, tweenParams.EaseExponent), tweenParams.IsPingPong, tweenParams.LoopCount, tweenParams.StartDelay);
        }

        public static void Scale(EntityCommandBuffer commandBuffer, Entity entity, float3 start, float3 end, TweenParams tweenParams)
        {
            Scale(commandBuffer, entity, start, end, tweenParams.Duration, new EaseDesc(tweenParams.EaseType, tweenParams.EaseExponent), tweenParams.IsPingPong, tweenParams.LoopCount, tweenParams.StartDelay);
        }

        public static void Scale(EntityCommandBuffer.ParallelWriter parallelWriter, int sortKey, Entity entity, float3 start, float3 end, TweenParams tweenParams)
        {
            Scale(parallelWriter, sortKey, entity, start, end, tweenParams.Duration, new EaseDesc(tweenParams.EaseType, tweenParams.EaseExponent), tweenParams.IsPingPong, tweenParams.LoopCount, tweenParams.StartDelay);
        }

        public static void Scale(
            EntityManager entityManager,
            Entity entity,
            float3 start, 
            float3 end, 
            float duration,
            EaseDesc easeDesc = default,
            bool isPingPong = false, 
            int loopCount = 1,
            float startDelay = 0.0f)
        {
            if (!CheckParams(easeDesc.Exponent, loopCount))
            {
                return;
            }

            TweenParams tweenParams = new TweenParams(duration, easeDesc.Type, easeDesc.Exponent, isPingPong, loopCount, startDelay);
            entityManager.AddComponentData(entity, new TweenScaleCommand(tweenParams, start, end));
        }

        public static void Scale(
            EntityCommandBuffer commandBuffer,
            Entity entity,
            float3 start, 
            float3 end, 
            float duration,
            EaseDesc easeDesc = default,
            bool isPingPong = false, 
            int loopCount = 1,
            float startDelay = 0.0f)
        {
            if (!CheckParams(easeDesc.Exponent, loopCount))
            {
                return;
            }

            TweenParams tweenParams = new TweenParams(duration, easeDesc.Type, easeDesc.Exponent, isPingPong, loopCount, startDelay);
            commandBuffer.AddComponent(entity, new TweenScaleCommand(tweenParams, start, end));
        }

        public static void Scale(
            EntityCommandBuffer.ParallelWriter parallelWriter,
            int sortKey,
            Entity entity,
            float3 start, 
            float3 end, 
            float duration,
            EaseDesc easeDesc = default,
            bool isPingPong = false, 
            int loopCount = 1,
            float startDelay = 0.0f)
        {
            if (!CheckParams(easeDesc.Exponent, loopCount))
            {
                return;
            }

            TweenParams tweenParams = new TweenParams(duration, easeDesc.Type, easeDesc.Exponent, isPingPong, loopCount, startDelay);
            parallelWriter.AddComponent(sortKey, entity, new TweenScaleCommand(tweenParams, start, end));
        }

        public static void Pause(EntityManager entityManager, Entity entity)
        {
            entityManager.AddComponent<TweenPause>(entity);
        }

        public static void Pause(EntityCommandBuffer commandBuffer, Entity entity)
        {
            commandBuffer.AddComponent<TweenPause>(entity);
        }

        public static void Pause(EntityCommandBuffer.ParallelWriter parallelWriter, int sortKey, Entity entity)
        {
            parallelWriter.AddComponent<TweenPause>(sortKey, entity);
        }

        public static void Resume(EntityManager entityManager, Entity entity)
        {
            entityManager.AddComponent<TweenResumeCommand>(entity);
        }

        public static void Resume(EntityCommandBuffer commandBuffer, Entity entity)
        {
            commandBuffer.AddComponent<TweenResumeCommand>(entity);
        }

        public static void Resume(EntityCommandBuffer.ParallelWriter parallelWriter, int sortKey, Entity entity)
        {
            parallelWriter.AddComponent<TweenResumeCommand>(sortKey, entity);
        }

        public static void Stop(EntityManager entityManager, Entity entity)
        {
            entityManager.AddComponent<TweenStopCommand>(entity);
        }

        public static void Stop(EntityCommandBuffer commandBuffer, Entity entity)
        {
            commandBuffer.AddComponent<TweenStopCommand>(entity);
        }

        public static void Stop(EntityCommandBuffer.ParallelWriter parallelWriter, int sortKey, Entity entity)
        {
            parallelWriter.AddComponent<TweenStopCommand>(sortKey, entity);
        }

        private static bool CheckParams(int easeExponent, int loopCount)
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