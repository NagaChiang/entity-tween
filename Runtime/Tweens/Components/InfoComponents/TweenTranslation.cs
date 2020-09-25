using Unity.Entities;
using Unity.Mathematics;

namespace Timespawn.EntityTween.Tweens
{
    public struct TweenTranslation : IComponentData, ITweenId, ITweenInfo<float3>
    {
        public int Id;
        public float3 Start;
        public float3 End;

        public TweenTranslation(int id, float3 start, float3 end)
        {
            Id = id;
            Start = start;
            End = end;
        }

        public void SetTweenId(int id)
        {
            Id = id;
        }

        public int GetTweenId()
        {
            return Id;
        }

        public void SetTweenInfo(float3 start, float3 end)
        {
            Start = start;
            End = end;
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