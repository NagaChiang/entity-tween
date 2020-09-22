using Timespawn.EntityTween.Math;
using Timespawn.EntityTween.Tweens;
using Unity.Entities;

namespace Timespawn.EntityTween
{
    [UpdateInGroup(typeof(TweenSystemGroup))]
    internal class TweenEaseSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            float deltaTime = Time.DeltaTime;

            Entities.ForEach((ref DynamicBuffer<Tween> tweenBuffer) => 
            {
                for (int i = 0; i < tweenBuffer.Length; i++)
                {
                    Tween tween = tweenBuffer[i];
                    tween.Time += tween.IsReverting ? -deltaTime : deltaTime;

                    float normalizedTime = tween.GetNormalizedTime();
                    tween.EasePercentage = Ease.CalculatePercentage(normalizedTime, tween.EaseType, tween.EaseExponent);

                    tweenBuffer[i] = tween;
                }
            }).ScheduleParallel();
        }
    }
}