using Timespawn.EntityTween.Tweens;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Timespawn.EntityTween
{
    [UpdateInGroup(typeof(TweenApplySystemGroup))]
    internal class TweenTranslationSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            Entities.ForEach((ref Translation translation, in DynamicBuffer<Tween> tweenBuffer, in TweenTranslation tweenInfo) =>
            {
                for (int i = 0; i < tweenBuffer.Length; i++)
                {
                    Tween tween = tweenBuffer[i];
                    if (tween.Id == tweenInfo.Id)
                    {
                        translation.Value = math.lerp(tweenInfo.Start, tweenInfo.End, tween.EasePercentage);
                        break;
                    }
                }
            }).ScheduleParallel();
        }
    }
}