using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace Assets.Code.Sources.BattleSimulation
{
    [BurstCompile]
    public struct PositionSeekingJob : IJob
    {
        public NativeList<float3> GuildAPositions;
        public NativeList<float3> GuildBPositions;
        public NativeList<float3> ClosestListA;
        public NativeList<float3> ClosestListB;

        public void Execute()
        {
            for (var i = 0; i < GuildAPositions.Length; i++)
            {
                var aPos = GuildAPositions[i];
                float minDistance = 65536;
                var destination = float3.zero;
                for (var j = 0; j < GuildBPositions.Length; j++)
                {
                    var bPos = GuildBPositions[j];
                    var distance = Vector3.Distance(aPos, bPos);
                    if (distance < minDistance)
                    {
                        destination = bPos;
                        minDistance = distance;
                    }
                }

                ClosestListA.Add(destination);
            }

            for (var i = 0; i < GuildBPositions.Length; i++)
            {
                var bPos = GuildBPositions[i];
                float minDistance = 65536;
                float3 destination = float3.zero;
                for (var j = 0; j < GuildAPositions.Length; j++)
                {
                    var aPos = GuildAPositions[j];
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
}