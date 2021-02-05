using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Jobs;

namespace Assets.Code.Sources.BattleSimulation
{
    public class ProximalMovement
    {
        public void MovementToNearest(Transform[] unitTransforms,
            NativeList<float3> desination,
            float[] unitSize, 
            float[] movementSpeeds)
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

            var movementSpeedNativeList = new NativeList<float>(Allocator.TempJob);
            foreach (var movementSpeed in movementSpeeds)
            {
                movementSpeedNativeList.Add(movementSpeed);
            }

            var proximalMovementJob = new ProximalMovementJob()
            {
                DeltaTime = Time.deltaTime,
                Destinations = desination,
                UnitSizes = unitSizesNativeList,
                MovementSpeeds = movementSpeedNativeList
            };

            var movementHandle = proximalMovementJob.Schedule(transforms);
            movementHandle.Complete();

            transforms.Dispose();
            desination.Dispose(movementHandle);
            movementSpeedNativeList.Dispose(movementHandle);
            unitSizesNativeList.Dispose();
        }
    }
}