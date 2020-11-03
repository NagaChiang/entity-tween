using Unity.Core;
using Unity.Entities;

namespace Timespawn.EntityTween.Tests.Common
{
    [DisableAutoCreation]
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    [UpdateAfter(typeof(UpdateWorldTimeSystem))]
    public class OverrideTimeSystem : SystemBase
    {
        private bool IsPendingOverride;
        private float OverrideDeltaTime;

        public void OverrideNextDeltaTime(float deltaTime)
        {
            IsPendingOverride = true;
            OverrideDeltaTime = deltaTime;
        }

        protected override void OnUpdate()
        {
            if (!IsPendingOverride)
            {
                return;
            }

            IsPendingOverride = false;
            World.SetTime(new TimeData(0.0f, OverrideDeltaTime));
        }
    }
}