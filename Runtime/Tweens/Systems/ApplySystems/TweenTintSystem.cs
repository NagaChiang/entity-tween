#if UNITY_TINY_ALL_0_31_0
using Unity.Tiny;
#endif

#if UNITY_2D_ENTITIES
using Unity.U2D.Entities;
#endif

#if UNITY_TINY_ALL_0_31_0 || UNITY_2D_ENTITIES
using Timespawn.EntityTween.Tweens;
using Unity.Entities;
using Unity.Mathematics;

namespace Timespawn.EntityTween
{
    [UpdateInGroup(typeof(TweenApplySystemGroup))]
    internal class TweenTintSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            Entities
                .WithNone<TweenPause>()
                .ForEach((ref SpriteRenderer spriteRenderer, in DynamicBuffer<TweenState> tweenBuffer, in TweenTint tweenInfo) =>
                {
                    for (int i = 0; i < tweenBuffer.Length; i++)
                    {
                        TweenState tween = tweenBuffer[i];
                        if (tween.Id == tweenInfo.Id)
                        {
                            spriteRenderer.Color = math.lerp(tweenInfo.Start, tweenInfo.End, tween.EasePercentage);
                            break;
                        }
                    }
                }).ScheduleParallel();
        }
    }
}
#endif