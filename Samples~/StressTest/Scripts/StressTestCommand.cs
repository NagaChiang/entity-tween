using Timespawn.EntityTween.Math;
using Unity.Entities;
using UnityEngine;

namespace Timespawn.EntityTween.Samples.StressTest
{
    [GenerateAuthoringComponent]
    public struct StressTestCommand : IComponentData
    {
        public Entity Prefab;
        public ushort Count;

        [Header("Move")]
        public float MoveDuration;
        public EaseType MoveEaseType;
        public ushort MoveEaseExponent;
        public bool MoveIsPingPong;
        public ushort MoveLoopCount;
        public float StartMoveRadius;
        public float EndMoveRadius;

        [Header("Rotate")]
        public float RotateDuration;
        public EaseType RotateEaseType;
        public ushort RotateEaseExponent;
        public bool RotateIsPingPong;
        public ushort RotateLoopCount;
        public float MinRotateDegree;
        public float MaxRotateDegree;

        [Header("Scale")]
        public float ScaleDuration;
        public EaseType ScaleEaseType;
        public ushort ScaleEaseExponent;
        public bool ScaleIsPingPong;
        public ushort ScaleLoopCount;
        public float MinStartScale;
        public float MaxStartScale;
        public float MinEndScale;
        public float MaxEndScale;
    }
}