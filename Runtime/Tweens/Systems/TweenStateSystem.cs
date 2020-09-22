using Unity.Entities;

namespace Timespawn.EntityTween.Tweens
{
    [UpdateInGroup(typeof(TweenSystemGroup))]
    [UpdateAfter(typeof(TweenApplySystemGroup))]
    internal class TweenStateSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            Entities.ForEach((Entity entity, int entityInQueryIndex, ref DynamicBuffer<Tween> tweenBuffer) => 
            {
                for (int i = tweenBuffer.Length - 1; i >= 0 ; i--)
                {
                    Tween tween = tweenBuffer[i];

                    bool isInfiniteLoop = tween.LoopCount == Tween.LOOP_COUNT_INFINITE;
                    float normalizedTime = tween.GetNormalizedTime();
                    if (tween.IsReverting && normalizedTime <= 0.0f)
                    {
                        if (!isInfiniteLoop)
                        {
                            tween.LoopCount--;
                        }

                        tween.IsReverting = false;
                        tween.Time = 0.0f;
                    }
                    else if (!tween.IsReverting && normalizedTime >= 1.0f)
                    {
                        if (tween.IsPingPong)
                        {
                            tween.IsReverting = true;
                            tween.Time = tween.Duration / 2.0f;
                        }
                        else
                        {
                            if (!isInfiniteLoop)
                            {
                                tween.LoopCount--;
                            }

                            tween.Time = 0.0f;
                        }
                    }

                    if (!isInfiniteLoop && tween.LoopCount == 0)
                    {
                        tween.SetPendingDestroy();
                    }

                    tweenBuffer[i] = tween;
                }
            }).ScheduleParallel();
        }
    }
}