using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Timespawn.EntityTween.Tweens
{
    [WriteGroup(typeof(Rotation))]
    public struct TweenRotation : IComponentData, ITweenId, ITweenInfo<quaternion>
    {
        public int Id;
        public quaternion Start;
        public quaternion End;

        public TweenRotation(in int id, in quaternion start, in quaternion end)
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

        public void SetTweenInfo(in quaternion start, in quaternion end)
        {
            Start = start;
            End = end;
        }

        public quaternion GetTweenStart()
        {
            return Start;
        }

        public quaternion GetTweenEnd()
        {
            return End;
        }
    }
}