using Unity.Burst;
using Unity.Collections;
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

            [NativeDisableParallelForRestriction] public BufferFromEntity<Tween> TweenBufferFromEntity;

            public EntityCommandBuffer.ParallelWriter ParallelWriter;

            public void Execute(ArchetypeChunk chunk, int chunkIndex, int firstEntityIndex)
            {
                NativeArray<Entity> entities = chunk.GetNativeArray(EntityType);
                NativeArray<TTweenInfo> infos = chunk.GetNativeArray(InfoType);
                for (int i = 0; i < entities.Length; i++)
                {
                    Entity entity = entities[i];
                    
                    DynamicBuffer<Tween> tweenBuffer = TweenBufferFromEntity[entity];
                    for (int j = 0; j < tweenBuffer.Length; j++)
                    {
                        Tween tween = tweenBuffer[j];
                        if (infos[i].GetTweenId() == tween.Id && tween.IsPendingDestroy())
                        {
                            tweenBuffer.RemoveAt(j);
                            ParallelWriter.RemoveComponent<TTweenInfo>(chunkIndex, entity);
                            break;
                        }
                    }

                    if (tweenBuffer.IsEmpty)
                    {
                        ParallelWriter.RemoveComponent<Tween>(chunkIndex, entity);
                    }
                }
            }
        }

        private EntityQuery TweenInfoQuery;

        protected override void OnCreate()
        {
            TweenInfoQuery = GetEntityQuery(ComponentType.ReadOnly<TTweenInfo>());
        }

        protected override void OnUpdate()
        {
            EndSimulationEntityCommandBufferSystem endSimECBSystem = World.DefaultGameObjectInjectionWorld.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();

            DestroyJob job = new DestroyJob
            {
                EntityType = GetEntityTypeHandle(),
                InfoType = GetComponentTypeHandle<TTweenInfo>(true),
                TweenBufferFromEntity = GetBufferFromEntity<Tween>(),
                ParallelWriter = endSimECBSystem.CreateCommandBuffer().AsParallelWriter(),
            };

            Dependency = job.ScheduleParallel(TweenInfoQuery, Dependency);
            endSimECBSystem.AddJobHandleForProducer(Dependency);
        }
    }

    internal class TweenTranslationDestroySystem : TweenDestroySystem<TweenTranslation> {}
    internal class TweenRotationDestroySystem : TweenDestroySystem<TweenRotation> {}
    internal class TweenScaleDestroySystem : TweenDestroySystem<TweenScale> {}
}