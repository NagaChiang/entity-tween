using Unity.Collections;
using Unity.Entities;

namespace Timespawn.EntityTween.Tweens
{
    [UpdateInGroup(typeof(TweenDestroySystemGroup))]
    internal abstract class TweenDestroySystem<TTweenInfo> : SystemBase 
        where TTweenInfo : struct, IComponentData, ITweenId
    {
        [BurstCompatible]
        private struct DestroyJob<TInfo> : IJobChunk
            where TInfo : struct, IComponentData, ITweenId
        {
            [ReadOnly] public EntityTypeHandle EntityType;
            [ReadOnly] public ComponentTypeHandle<TInfo> IdType;
            
            [NativeDisableParallelForRestriction] public BufferFromEntity<Tween> TweenBufferFromEntity;

            public EntityCommandBuffer.ParallelWriter ParallelWriter;

            public void Execute(ArchetypeChunk chunk, int chunkIndex, int firstEntityIndex)
            {
                NativeArray<Entity> entities = chunk.GetNativeArray(EntityType);
                NativeArray<TInfo> infos = chunk.GetNativeArray(IdType);
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

                    if (tweenBuffer.Length == 0)
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

            DestroyJob<TTweenInfo> job = new DestroyJob<TTweenInfo>
            {
                EntityType = GetEntityTypeHandle(),
                IdType = GetComponentTypeHandle<TTweenInfo>(true),
                TweenBufferFromEntity = GetBufferFromEntity<Tween>(),
                ParallelWriter = endSimECBSystem.CreateCommandBuffer().AsParallelWriter(),
            };

            Dependency = job.ScheduleParallel(TweenInfoQuery, Dependency);
            endSimECBSystem.AddJobHandleForProducer(Dependency);
        }
    }

    internal class TweenTranslationDestroySystem : TweenDestroySystem<TweenTranslation> {}
}