using Timespawn.EntityTween.Tweens;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Timespawn.EntityTween
{
    [UpdateInGroup(typeof(TweenApplySystemGroup))]
    internal class TweenRotationSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            Entities
                .WithNone<TweenPause>()
                .ForEach((ref Rotation rotation, in DynamicBuffer<TweenState> tweenBuffer, in TweenRotation tweenInfo) =>
                {
                    for (int i = 0; i < tweenBuffer.Length; i++)
                    {
                        TweenState tween = tweenBuffer[i];
                        if (tween.Id == tweenInfo.Id)
                        {
                            rotation.Value = math.slerp(tweenInfo.Start, tweenInfo.End, tween.EasePercentage);
                            break;
                        }
                    }
                }).ScheduleParallel();
        }
    }
}