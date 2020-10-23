using System;
using Timespawn.EntityTween.Math;
using Timespawn.EntityTween.Tweens;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Random = Unity.Mathematics.Random;

namespace Timespawn.EntityTween.Samples.StressTest
{
    public class StressTestSystem : SystemBase
    {
        protected override void OnStartRunning()
        {
#if !ENTITYTWEEN_HYBRID_RENDERER
            Debug.LogWarning("Please install Hybrid Renderer (com.unity.rendering.hybrid) to enable rendering.");
#endif
        }

        protected override void OnUpdate()
        {
            EndSimulationEntityCommandBufferSystem endSimECBSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
            EntityCommandBuffer.ParallelWriter parallelWriter = endSimECBSystem.CreateCommandBuffer().AsParallelWriter();
            Random random = new Random((uint) Environment.TickCount);

            Entities.ForEach((Entity entity, int entityInQueryIndex, in StressTestCommand cmd) =>
            {
                for (int i = 0; i < cmd.Count; i++)
                {
                    Entity obj = parallelWriter.Instantiate(entityInQueryIndex, cmd.Prefab);

                    float3 moveStart = random.NextFloat3Direction() * cmd.StartMoveRadius;
                    float3 moveEnd = random.NextFloat3Direction() * cmd.EndMoveRadius;
                    EaseDesc moveEaseDesc = new EaseDesc(cmd.MoveEaseType, cmd.MoveEaseExponent);
                    Tween.Move(parallelWriter, entityInQueryIndex, obj, moveStart, moveEnd, cmd.MoveDuration, moveEaseDesc, cmd.MoveIsPingPong, cmd.MoveLoopCount);

                    quaternion rotateEnd = quaternion.AxisAngle(random.NextFloat3Direction(), random.NextFloat(cmd.MinRotateDegree, cmd.MaxRotateDegree));
                    EaseDesc rotateEaseDesc = new EaseDesc(cmd.RotateEaseType, cmd.RotateEaseExponent);
                    Tween.Rotate(parallelWriter, entityInQueryIndex, obj, quaternion.identity, rotateEnd, cmd.RotateDuration, rotateEaseDesc, cmd.RotateIsPingPong, cmd.RotateLoopCount);

                    float3 scaleStart = new float3(random.NextFloat(cmd.MinStartScale, cmd.MaxStartScale));
                    float3 scaleEnd = new float3(random.NextFloat(cmd.MinEndScale, cmd.MaxEndScale));
                    EaseDesc scaleEaseDesc = new EaseDesc(cmd.ScaleEaseType, cmd.ScaleEaseExponent);
                    Tween.Scale(parallelWriter, entityInQueryIndex, obj, scaleStart, scaleEnd, cmd.ScaleDuration, scaleEaseDesc, cmd.ScaleIsPingPong, cmd.ScaleLoopCount);
                }

                parallelWriter.RemoveComponent<StressTestCommand>(entityInQueryIndex, entity);
            }).ScheduleParallel();

            endSimECBSystem.AddJobHandleForProducer(Dependency);
        }
    }
}
