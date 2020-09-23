using Unity.Entities;
using Unity.Mathematics;

namespace Timespawn.EntityTween.Tweens
{
    internal struct TweenRotationCommand : IComponentData, ITweenParams, ITweenInfo<quaternion>
    {
        public TweenParams TweenParams;
        public quaternion Start;
        public quaternion End;

        public TweenRotationCommand(TweenParams tweenParams, quaternion start, quaternion end)
        {
            TweenParams = tweenParams;
            Start = start;
            End = end;
        }

        public void SetTweenInfo(quaternion start, quaternion end)
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