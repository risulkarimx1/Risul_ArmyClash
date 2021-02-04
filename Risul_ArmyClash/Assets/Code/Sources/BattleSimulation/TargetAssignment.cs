using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace Assets.Code.Sources.BattleSimulation
{
    public struct PositionSeekingJob : IJob
    {
        public NativeList<float3> GuildAPositions;
        public NativeList<float3> GuildBPositions;
        public NativeList<float3> ClosestListA;
        public NativeList<float3> ClosestListB;

        public void Execute()
        {
            foreach (var aPos in GuildAPositions)
            {
                float minDistance = 65536;
                float3 destination = float3.zero;
                foreach (var bPos in GuildBPositions)
                {
                    var distance = Vector3.Distance(aPos, bPos);
                    if (distance < minDistance)
                    {
                        destination = bPos;
                        minDistance = distance;
                    }
                }

                ClosestListA.Add(destination);
            }

            foreach (var bPos in GuildBPositions)
            {
                float minDistance = 65536;
                float3 destination = float3.zero;
                foreach (var aPos in GuildAPositions)
                {
                    var distance = Vector3.Distance(aPos, bPos);
                    if (distance < minDistance)
                    {
                        destination = aPos;
                        minDistance = distance;
                    }
                }

                ClosestListB.Add(destination);
            }
        }
    }
    
    public class TargetAssignment
    {
        public (NativeList<float3> closeToA, NativeList<float3> closeToB) LocateNearest(float3 [] guildAPositions, float3 [] guildBPositions)
        {
            var guildAPosNativeArray = new NativeList<float3>(Allocator.TempJob);
            var guildBPosNativeArray = new NativeList<float3>(Allocator.TempJob);

            foreach (var pos in guildAPositions)
            {
                guildAPosNativeArray.Add(pos);
            }

            foreach (var pos in guildBPositions)
            {
                guildBPosNativeArray.Add(pos);
            }

            var closestPositionsA = new NativeList<float3>(Allocator.TempJob);
            var closestPositionsB = new NativeList<float3>(Allocator.TempJob);

            var positionSeekingJob = new PositionSeekingJob
            {
                GuildAPositions = guildAPosNativeArray,
                GuildBPositions = guildBPosNativeArray,
                ClosestListA = closestPositionsA,
                ClosestListB = closestPositionsB
            };

            var handle = positionSeekingJob.Schedule();
            handle.Complete();
            
            // var closestToA = new float3[closestPositionsA.Length];
            // for (int i = 0; i < closestPositionsA.Length; i++)
            // {
            //     closestToA[i] = closestPositionsA[i];
            // }
            //
            // var closestToB = new float3[closestPositionsB.Length];
            // for (int i = 0; i < closestPositionsB.Length; i++)
            // {
            //     closestToB[i] = closestPositionsB[i];
            // }
            
            guildAPosNativeArray.Dispose();
            guildBPosNativeArray.Dispose();
            // closestPositionsA.Dispose();
            // closestPositionsB.Dispose();

            // return (closestToA, closestToB);
            return (closestPositionsA, closestPositionsB);
        }
    }
}
