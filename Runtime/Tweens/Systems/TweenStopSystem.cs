using Unity.Entities;

namespace Timespawn.EntityTween.Tweens
{
    [UpdateInGroup(typeof(TweenSimulationSystemGroup))]
    [UpdateAfter(typeof(TweenStateSystem))]
    [UpdateBefore(typeof(TweenDestroySystemGroup))]
    internal class TweenStopSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            BeginSimulationEntityCommandBufferSystem beginSimECBSystem = World.DefaultGameObjectInjectionWorld.GetOrCreateSystem<BeginSimulationEntityCommandBufferSystem>();
            EntityCommandBuffer.ParallelWriter parallelWriter = beginSimECBSystem.CreateCommandBuffer().AsParallelWriter();

            Entities
                .WithAll<TweenStopCommand>()
                .ForEach((int entityInQueryIndex, Entity entity, ref DynamicBuffer<Tween> tweenBuffer) =>
                {
                    for (int i = 0; i < tweenBuffer.Length; i++)
                    {
                        Tween tween = tweenBuffer[i];
                        tween.SetPendingDestroy();

                        tweenBuffer[i] = tween;    
                    }

                    parallelWriter.RemoveComponent<TweenStopCommand>(entityInQueryIndex, entity);

                    if (HasComponent<TweenPause>(entity))
                    {
                        parallelWriter.RemoveComponent<TweenPause>(entityInQueryIndex, entity);
                    }
                }).ScheduleParallel();

            beginSimECBSystem.AddJobHandleForProducer(Dependency);
        }
    }
}