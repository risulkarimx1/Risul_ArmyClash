using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Jobs;
using Random = UnityEngine.Random;

namespace Assets.Code.Sources.BattleSimulation
{
    public struct ProximalMovementJob: IJobParallelForTransform
    {
        public NativeArray <float3> Destination;
        public float Proximity;
        public float DeltaTime;
        
        public void Execute(int index, TransformAccess transform)
        {
            if (Vector3.Distance(transform.position, Destination[index]) > Proximity)
            {
                transform.position = Vector3.Lerp(transform.position, Destination[index], DeltaTime / 2);
            }
        }
    }
    
    public class ProximalMovement
    {
        public void MovementToNearest(Transform [] unitTransforms, NativeList<float3> desination)
        {
            var transforms = new TransformAccessArray(unitTransforms.Length);
            foreach (var unitTransform in unitTransforms)
            {
                transforms.Add(unitTransform);
            }

            var proximalMovementJob = new ProximalMovementJob()
            {
                DeltaTime = Time.deltaTime,
                Destination = desination,
                Proximity = 2 
            };

            var movementHandle = proximalMovementJob.Schedule(transforms);
            movementHandle.Complete();
            
            transforms.Dispose();
            desination.Dispose(movementHandle);
        }
    }
}
