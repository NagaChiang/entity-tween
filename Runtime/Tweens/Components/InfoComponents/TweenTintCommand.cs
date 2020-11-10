#if UNITY_TINY_ALL_0_31_0 || UNITY_2D_ENTITIES
using Unity.Entities;
using Unity.Mathematics;

namespace Timespawn.EntityTween.Tweens
{
    internal struct TweenTintCommand : IComponentData, ITweenParams, ITweenInfo<float4>
    {
        public TweenParams TweenParams;
        public float4 Start;
        public float4 End;

        public TweenTintCommand(TweenParams tweenParams, float4 start, float4 end)
        {
            TweenParams = tweenParams;
            Start = start;
            End = end;
        }

        public void SetTweenInfo(float4 start, float4 end)
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

        public float4 GetTweenStart()
        {
            return Start;
        }

        public float4 GetTweenEnd()
        {
            return End;
        }
    }
} 
#endif