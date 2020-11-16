#if UNITY_TINY_ALL_0_31_0 || UNITY_2D_ENTITIES
using Unity.Entities;
using Unity.Mathematics;

namespace Timespawn.EntityTween.Tweens
{
    public struct TweenTint : IComponentData, ITweenId, ITweenInfo<float4>
    {
        public int Id;
        public float4 Start;
        public float4 End;

        public TweenTint(in int id, in float4 start, in float4 end)
        {
            Id = id;
            Start = start;
            End = end;
        }

        public void SetTweenId(in int id)
        {
            Id = id;
        }

        public int GetTweenId()
        {
            return Id;
        }

        public void SetTweenInfo(in float4 start, in float4 end)
        {
            Start = start;
            End = end;
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