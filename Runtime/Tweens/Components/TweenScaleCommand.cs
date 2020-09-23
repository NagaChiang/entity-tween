using Unity.Entities;
using Unity.Mathematics;

namespace Timespawn.EntityTween.Tweens
{
    internal struct TweenScaleCommand : IComponentData, ITweenParams, ITweenInfo<float3>
    {
        public TweenParams TweenParams;
        public float3 Start;
        public float3 End;

        public TweenScaleCommand(TweenParams tweenParams, float3 start, float3 end)
        {
            TweenParams = tweenParams;
            Start = start;
            End = end;
        }

        public void SetTweenInfo(float3 start, float3 end)
        {
            Start = start;
            End = end;
        }

        public void SetTweenParams(TweenParams tweenParams)
        {
            TweenParams = tweenParams;
        }

        public TweenParams GetTweenParams()
        {
            return TweenParams;
        }

        public float3 GetTweenStart()
        {
            return Start;
        }

        public float3 GetTweenEnd()
        {
            return End;
        }
    }
}