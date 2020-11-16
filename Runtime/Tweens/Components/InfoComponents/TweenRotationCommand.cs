using Unity.Entities;
using Unity.Mathematics;

namespace Timespawn.EntityTween.Tweens
{
    internal struct TweenRotationCommand : IComponentData, ITweenParams, ITweenInfo<quaternion>
    {
        public TweenParams TweenParams;
        public quaternion Start;
        public quaternion End;

        public TweenRotationCommand(in TweenParams tweenParams, in quaternion start, in quaternion end)
        {
            TweenParams = tweenParams;
            Start = start;
            End = end;
        }

        public void SetTweenInfo(in quaternion start, in quaternion end)
        {
            Start = start;
            End = end;
        }

        public void SetTweenParams(in TweenParams tweenParams)
        {
            TweenParams = tweenParams;
        }

        public TweenParams GetTweenParams()
        {
            return TweenParams;
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