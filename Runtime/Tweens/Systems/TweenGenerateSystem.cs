using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Timespawn.EntityTween.Tweens
{
    [UpdateInGroup(typeof(TweenGenerateSystemGroup))]
    internal abstract class TweenGenerateSystem<TTweenCommand, TTweenInfo, TRequired, TTweenInfoValue> : SystemBase
        where TTweenCommand : struct, IComponentData, ITweenParams, ITweenInfo<TTweenInfoValue>
        where TTweenInfo : struct, IComponentData, ITweenId, ITweenInfo<TTweenInfoValue>
        where TRequired : struct, IComponentData
        where TTweenInfoValue : struct
    {
        [BurstCompile]
        private struct GenerateJob : IJobChunk
        {
            [ReadOnly] public int TweenInfoTypeIndex;
            [ReadOnly] public double ElapsedTime;
            [ReadOnly] public EntityTypeHandle EntityType;
            [ReadOnly] public ComponentTypeHandle<TTweenCommand> TweenCommandType;
            [ReadOnly] public ComponentTypeHandle<TRequired> RequiredType;
            [ReadOnly] public BufferTypeHandle<Tween> TweenBufferType;

            public EntityCommandBuffer.ParallelWriter ParallelWriter;

            public void Execute(ArchetypeChunk chunk, int chunkIndex, int firstEntityIndex)
            {
                bool hasTweenBuffer = chunk.Has(TweenBufferType);
                bool hasRequiredType = chunk.Has(RequiredType);

                NativeArray<Entity> entities = chunk.GetNativeArray(EntityType);
                NativeArray<TTweenCommand> commands = chunk.GetNativeArray(TweenCommandType);
                for (int i = 0; i < entities.Length; i++)
                {
                    Entity entity = entities[i];
                    TTweenCommand command = commands[i];

                    if (!hasTweenBuffer)
                    {
                        ParallelWriter.AddBuffer<Tween>(chunkIndex, entity);
                        break;
                    }

                    if (!hasRequiredType)
                    {
                        ParallelWriter.AddComponent<TRequired>(chunkIndex, entity);
                    }

                    Tween tween = new Tween(command.GetTweenParams(), ElapsedTime, chunkIndex, TweenInfoTypeIndex);
                    ParallelWriter.AppendToBuffer(chunkIndex, entity, tween);

                    TTweenInfo info = new TTweenInfo();
                    info.SetTweenId(tween.Id);
                    info.SetTweenInfo(command.GetTweenStart(), command.GetTweenEnd());
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
                TweenInfoTypeIndex = TypeManager.GetTypeIndex(typeof(TTweenInfo)),
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

    internal class TweenTranslationGenerateSystem : TweenGenerateSystem<TweenTranslationCommand, TweenTranslation, Translation, float3> {}
    internal class TweenRotationGenerateSystem : TweenGenerateSystem<TweenRotationCommand, TweenRotation, Rotation, quaternion> {}
    internal class TweenScaleGenerateSystem : TweenGenerateSystem<TweenScaleCommand, TweenScale, NonUniformScale, float3> {}
}