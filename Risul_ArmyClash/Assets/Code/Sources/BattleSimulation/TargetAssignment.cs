using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;

namespace Assets.Code.Sources.BattleSimulation
{
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
            guildAPosNativeArray.Dispose();
            guildBPosNativeArray.Dispose();
            return (closestPositionsA, closestPositionsB);
        }
    }
}
