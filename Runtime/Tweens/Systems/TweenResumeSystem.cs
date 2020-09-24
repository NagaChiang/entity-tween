using Unity.Entities;

namespace Timespawn.EntityTween.Tweens
{
    [UpdateInGroup(typeof(TweenSystemGroup))]
    [UpdateAfter(typeof(TweenApplySystemGroup))]
    internal class TweenResumeSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            EndSimulationEntityCommandBufferSystem endSimECBSystem = World.DefaultGameObjectInjectionWorld.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
            EntityCommandBuffer.ParallelWriter parallelWriter = endSimECBSystem.CreateCommandBuffer().AsParallelWriter();
            ComponentDataFromEntity<TweenPause> pauseFromEntity = GetComponentDataFromEntity<TweenPause>();

            Entities
                .WithReadOnly(pauseFromEntity)
                .WithAll<TweenResumeCommand>()
                .ForEach((int entityInQueryIndex, Entity entity) =>
                {
                    if (pauseFromEntity.HasComponent(entity))
                    {
                        parallelWriter.RemoveComponent<TweenPause>(entityInQueryIndex, entity);
                    }

                    parallelWriter.RemoveComponent<TweenResumeCommand>(entityInQueryIndex, entity);
                }).ScheduleParallel();

            endSimECBSystem.AddJobHandleForProducer(Dependency);
        }
    }
}