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
            
            public BufferFromEntity<Tween> TweenBufferFromEntity;
            public EntityCommandBuffer CommandBuffer;

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
                            CommandBuffer.RemoveComponent<TTweenInfo>(entity);
                            break;
                        }
                    }

                    if (tweenBuffer.Length == 0)
                    {
                        CommandBuffer.RemoveComponent<Tween>(entity);
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
            EntityCommandBuffer commandBuffer = endSimECBSystem.CreateCommandBuffer();

            DestroyJob<TTweenInfo> job = new DestroyJob<TTweenInfo>
            {
                EntityType = GetEntityTypeHandle(),
                IdType = GetComponentTypeHandle<TTweenInfo>(true),
                TweenBufferFromEntity = GetBufferFromEntity<Tween>(),
                CommandBuffer = commandBuffer,
            };

            Dependency = job.ScheduleSingle(TweenInfoQuery, Dependency);
            Dependency.Complete();
        }
    }

    internal class TweenTranslationDestroySystem : TweenDestroySystem<TweenTranslation> {}
}