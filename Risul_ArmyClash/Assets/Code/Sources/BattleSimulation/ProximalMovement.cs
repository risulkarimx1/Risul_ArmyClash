using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Jobs;

namespace Assets.Code.Sources.BattleSimulation
{
    public struct ProximalMovementJob : IJobParallelForTransform
    {
        public NativeArray<float3> Destinations;
        [ReadOnly]
        public NativeList<float> UnitSizes;
        public float DeltaTime;

        public void Execute(int index, TransformAccess transform)
        {
            if (Vector3.Distance(transform.position, Destinations[index]) > UnitSizes[index])
            {
                transform.position = Vector3.Lerp(transform.position, Destinations[index], DeltaTime * 2);
                var lookPos = Destinations[index] - (float3) transform.position;
                lookPos.y = 0;
                var rotation = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, DeltaTime * 2);
            }
        }
    }

    public class ProximalMovement
    {
        public void MovementToNearest(Transform[] unitTransforms, NativeList<float3> desination, float[] unitSize)
        {
            var transforms = new TransformAccessArray(unitTransforms.Length);
            foreach (var unitTransform in unitTransforms)
            {
                transforms.Add(unitTransform);
            }

            var unitSizesNativeList = new NativeList<float>(Allocator.TempJob);
            foreach (var size in unitSize)
            {
                unitSizesNativeList.Add(size);
            }

            var proximalMovementJob = new ProximalMovementJob()
            {
                DeltaTime = Time.deltaTime,
                Destinations = desination,
                UnitSizes = unitSizesNativeList
            };

            var movementHandle = proximalMovementJob.Schedule(transforms);
            movementHandle.Complete();

            transforms.Dispose();
            desination.Dispose(movementHandle);
            unitSizesNativeList.Dispose();
        }
    }
}