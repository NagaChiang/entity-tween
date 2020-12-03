using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Entities;

namespace Timespawn.EntityTween.Tweens
{
    [UpdateInGroup(typeof(TweenDestroySystemGroup))]
    internal abstract class TweenDestroySystem<TTweenInfo> : SystemBase 
        where TTweenInfo : struct, IComponentData, ITweenId
    {
        [BurstCompile]
        private struct DestroyJob : IJobChunk
        {
            [ReadOnly] public EntityTypeHandle EntityType;
            [ReadOnly] public ComponentTypeHandle<TTweenInfo> InfoType;

            [NativeDisableContainerSafetyRestriction] public BufferTypeHandle<TweenState> TweenBufferType;

            public EntityCommandBuffer.ParallelWriter ParallelWriter;

            public void Execute(ArchetypeChunk chunk, int chunkIndex, int firstEntityIndex)
            {
                NativeArray<Entity> entities = chunk.GetNativeArray(EntityType);
                NativeArray<TTweenInfo> infos = chunk.GetNativeArray(InfoType);
                BufferAccessor<TweenState> tweenBuffers = chunk.GetBufferAccessor(TweenBufferType);
                for (int i = 0; i < entities.Length; i++)
                {
                    Entity entity = entities[i];
                    DynamicBuffer<TweenState> tweenBuffer = tweenBuffers[i];
                    for (int j = 0; j < tweenBuffer.Length; j++)
                    {
                        TweenState tween = tweenBuffer[j];
                        if (infos[i].GetTweenId() == tween.Id && tween.IsPendingDestroy())
                        {
                            tweenBuffer.RemoveAt(j);
                            ParallelWriter.RemoveComponent<TTweenInfo>(chunkIndex, entity);
                            break;
                        }
                    }
                    
                    if (tweenBuffer.IsEmpty)
                    {
                        ParallelWriter.RemoveComponent<TweenState>(chunkIndex, entity);
                    }
                }
            }
        }

        private EntityQuery TweenInfoQuery;

        protected override void OnCreate()
        {
            TweenInfoQuery = GetEntityQuery(ComponentType.ReadOnly<TTweenInfo>(), ComponentType.ReadOnly<TweenState>());
        }

        protected override void OnUpdate()
        {
            EndSimulationEntityCommandBufferSystem endSimECBSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();

            DestroyJob job = new DestroyJob
            {
                EntityType = GetEntityTypeHandle(),
                InfoType = GetComponentTypeHandle<TTweenInfo>(true),
                TweenBufferType = GetBufferTypeHandle<TweenState>(),
                ParallelWriter = endSimECBSystem.CreateCommandBuffer().AsParallelWriter(),
            };

            Dependency = job.ScheduleParallel(TweenInfoQuery, Dependency);
            endSimECBSystem.AddJobHandleForProducer(Dependency);
        }
    }

    internal class TweenTranslationDestroySystem : TweenDestroySystem<TweenTranslation> {}
    internal class TweenRotationDestroySystem : TweenDestroySystem<TweenRotation> {}
    internal class TweenScaleDestroySystem : TweenDestroySystem<TweenScale> {}

#if UNITY_TINY_ALL_0_31_0 || UNITY_2D_ENTITIES
    internal class TweenTintDestroySystem : TweenDestroySystem<TweenTint> {}
#endif
}