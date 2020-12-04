using Unity.Entities;

namespace Timespawn.EntityTween.Tweens
{
    public struct TweenDestroyCommand : IBufferElementData
    {
        public TweenDestroyCommand(int id)
        {
            Id = id;
        }

        public int Id;
    }
}