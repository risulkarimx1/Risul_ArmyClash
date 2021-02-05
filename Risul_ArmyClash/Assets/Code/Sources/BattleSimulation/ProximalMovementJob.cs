using Unity.Burst;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Jobs;

namespace Assets.Code.Sources.BattleSimulation
{
    [BurstCompile]
    public struct ProximalMovementJob : IJobParallelForTransform
    {
        public NativeArray<float3> Destinations;
        [ReadOnly]
        public NativeList<float> UnitSizes;
        [ReadOnly]
        public NativeList<float> MovementSpeeds;
        public float DeltaTime;

        public void Execute(int index, TransformAccess transform)
        {
            if (Vector3.Distance(transform.position, Destinations[index]) > UnitSizes[index] * 2)
            {
                transform.position = Vector3.MoveTowards(transform.position, Destinations[index], DeltaTime * MovementSpeeds[index]);
            }
            
            var lookPos = Destinations[index] - (float3)transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, DeltaTime * 10);
        }
    }
}