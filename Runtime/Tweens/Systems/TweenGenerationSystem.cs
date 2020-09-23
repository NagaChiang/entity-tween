using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Timespawn.EntityTween.Tweens
{
    [UpdateInGroup(typeof(TweenGenerationSystemGroup))]
    internal abstract class TweenGenerationSystem<TTweenCommand, TTweenInfo, TRequired, TTweenInfoValue> : SystemBase
        where TTweenCommand : struct, IComponentData, ITweenParams, ITweenInfo<TTweenInfoValue>
        where TTweenInfo : struct, IComponentData, ITweenId, ITweenInfo<TTweenInfoValue>
        where TRequired : struct, IComponentData
        where TTweenInfoValue : struct
    {
        [BurstCompatible]
        private struct GenerateJob : IJobChunk
        {
            [ReadOnly] public double ElapsedTime;
            [ReadOnly] public EntityTypeHandle EntityType;
            [ReadOnly] public ComponentTypeHandle<TTweenCommand> TweenCommandType;
            [ReadOnly] public ComponentTypeHandle<TRequired> RequiredType;
            [ReadOnly] public BufferTypeHandle<Tween> TweenBufferType;

            public EntityCommandBuffer.ParallelWriter ParallelWriter;

            public void Execute(ArchetypeChunk chunk, int chunkIndex, int firstEntityIndex)
            {
                NativeArray<Entity> entities = chunk.GetNativeArray(EntityType);
                NativeArray<TTweenCommand> commands = chunk.GetNativeArray(TweenCommandType);

                bool hasTweenBuffer = chunk.Has(TweenBufferType);
                bool hasRequiredType = chunk.Has(RequiredType);

                for (int i = 0; i < entities.Length; i++)
                {
                    Entity entity = entities[i];
                    TTweenCommand command = commands[i];

                    if (!hasTweenBuffer)
                    {
                        ParallelWriter.AddBuffer<Tween>(chunkIndex, entity);
                    }

                    if (!hasRequiredType)
                    {
                        ParallelWriter.AddComponent<TRequired>(chunkIndex, entity);
                    }

                    Tween tween = new Tween(command.GetTweenParams(), ElapsedTime, chunkIndex);
                    TTweenInfo info = new TTweenInfo();
                    info.SetTweenId(tween.Id);
                    info.SetTweenInfo(command.GetTweenStart(), command.GetTweenEnd());

                    ParallelWriter.AppendToBuffer(chunkIndex, entity, tween);
                    ParallelWriter.AddComponent(chunkIndex, entity, info);
                    ParallelWriter.RemoveComponent<TTweenCommand>(chunkIndex, entity);
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

            GenerateJob job = new GenerateJob
            {
                ElapsedTime = elapsedTime,
                EntityType = GetEntityTypeHandle(),
                TweenCommandType = GetComponentTypeHandle<TTweenCommand>(true),
                RequiredType = GetComponentTypeHandle<TRequired>(true),
                TweenBufferType = GetBufferTypeHandle<Tween>(true),
                ParallelWriter = endSimECBSystem.CreateCommandBuffer().AsParallelWriter(),
            };

            Dependency = job.ScheduleParallel(TweenCommandQuery, Dependency);
            endSimECBSystem.AddJobHandleForProducer(Dependency);
        }
    }

    internal class TweenTranslationGenerationSystem : TweenGenerationSystem<TweenTranslationCommand, TweenTranslation, Translation, float3> {}
    internal class TweenRotationGenerationSystem : TweenGenerationSystem<TweenRotationCommand, TweenRotation, Rotation, quaternion> {}
    internal class TweenScaleGenerationSystem : TweenGenerationSystem<TweenScaleCommand, TweenScale, NonUniformScale, float3> {}
}