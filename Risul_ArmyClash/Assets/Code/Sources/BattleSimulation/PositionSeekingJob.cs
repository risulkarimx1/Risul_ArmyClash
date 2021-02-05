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
                var destination = float3.zero;
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
}