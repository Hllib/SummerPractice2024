using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace HlStudio
{
    [BurstCompile]
    public struct AccelerationJob: IJobParallelFor
    {
        [ReadOnly] public NativeArray<Vector3> Positions;
        [ReadOnly] public NativeArray<Vector3> Velocities;
        public NativeArray<Vector3> Accelerations;

        public float DestinationThreshold;
        public int Count => Positions.Length - 1;

        public Vector3 Weights;
        
        public void Execute(int index)
        {
            Vector3 averageSpeed = Vector3.zero,
                averageVelocity = Vector3.zero,
                averagePosition = Vector3.zero;

            for (int i = 0; i < Count; i++)
            {
                if(i == index) continue;

                var targetPosition = Positions[i];
                var positionDifference = Positions[index] - targetPosition;
                
                if (positionDifference.magnitude > DestinationThreshold) continue;

                averageSpeed += positionDifference.normalized;
                averageVelocity += Velocities[i];
                averagePosition += targetPosition;
            }

            Accelerations[index] += (averageSpeed / Count) * Weights.x + (averageVelocity / Count) * Weights.y
                                                                       + (averagePosition / Count - Positions[index]) * Weights.z;
        }
    }
}