using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

namespace Timespawn.EntityTween.Tweens
{
    [UpdateInGroup(typeof(TweenGenerationSystemGroup))]
    internal abstract class TweenGenerationSystem<TTweenCommand, TTweenInfo, TTweenInfoValue>: SystemBase
        where TTweenCommand : struct, IComponentData, ITweenParams, ITweenInfo<TTweenInfoValue>
        where TTweenInfo : struct, IComponentData, ITweenId, ITweenInfo<TTweenInfoValue>
    {
        [BurstCompatible]
        private struct GenerateJob<TCommand, TInfo> : IJobChunk
            where TCommand : struct, IComponentData, ITweenParams, ITweenInfo<TTweenInfoValue>
            where TInfo : struct, IComponentData, ITweenId, ITweenInfo<TTweenInfoValue>
        {
            [ReadOnly] public double ElapsedTime;
            [ReadOnly] public EntityTypeHandle EntityType;
            [ReadOnly] public ComponentTypeHandle<TCommand> TweenCommandType;
            [ReadOnly] public BufferFromEntity<Tween> TweenBufferFromEntity;

            public EntityCommandBuffer.ParallelWriter ParallelWriter;

            public void Execute(ArchetypeChunk chunk, int chunkIndex, int firstEntityIndex)
            {
                NativeArray<Entity> entities = chunk.GetNativeArray(EntityType);
                NativeArray<TCommand> commands = chunk.GetNativeArray(TweenCommandType);
                for (int i = 0; i < entities.Length; i++)
                {
                    Entity entity = entities[i];
                    TCommand command = commands[i];
                    if (!TweenBufferFromEntity.HasComponent(entity))
                    {
                        ParallelWriter.AddBuffer<Tween>(chunkIndex, entity);
                    }

                    Tween tween = new Tween(command.GetTweenParams(), ElapsedTime, chunkIndex);
                    TInfo info = new TInfo();
                    info.SetTweenId(tween.Id);
                    info.SetTweenInfo(command.GetTweenStart(), command.GetTweenEnd());

                    ParallelWriter.AppendToBuffer(chunkIndex, entity, tween);
                    ParallelWriter.AddComponent(chunkIndex, entity, info);
                    ParallelWriter.RemoveComponent<TCommand>(chunkIndex, entity);
                }
            }
        }

        private EntityQuery TweenCommandQuery;

        protected override void OnCreate()
        {
            TweenCommandQuery = GetEntityQuery(ComponentType.ReadOnly<TTweenCommand>());
        }

        protected override void OnUpdate()
        {
            double elapsedTime = Time.ElapsedTime;
            EndSimulationEntityCommandBufferSystem endSimECBSystem = World.DefaultGameObjectInjectionWorld.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();

            GenerateJob<TTweenCommand, TTweenInfo> job = new GenerateJob<TTweenCommand, TTweenInfo>
            {
                ElapsedTime = elapsedTime,
                EntityType = GetEntityTypeHandle(),
                TweenCommandType = GetComponentTypeHandle<TTweenCommand>(true),
                TweenBufferFromEntity = GetBufferFromEntity<Tween>(true),
                ParallelWriter = endSimECBSystem.CreateCommandBuffer().AsParallelWriter(),
            };

            Dependency = job.ScheduleParallel(TweenCommandQuery, Dependency);
            endSimECBSystem.AddJobHandleForProducer(Dependency);
        }
    }

    internal class TweenTranslationGenerationSystem : TweenGenerationSystem<TweenTranslationCommand, TweenTranslation, float3> {}
}