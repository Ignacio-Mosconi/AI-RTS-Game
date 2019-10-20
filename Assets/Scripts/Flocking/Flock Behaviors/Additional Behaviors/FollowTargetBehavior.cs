using System.Collections.Generic;
using UnityEngine;

namespace GreenNacho.AI.Flocking
{
    [System.Serializable]
    public class FollowTargetBehavior : FlockBehavior
    {
        [SerializeField] Transform target = default;

        const float MaxWeightSqrDistance = 10000f;

        public override Vector3 ComputeVector(Boid boid, List<Boid> neighbors)
        {
            Vector3 followTargetVector = target.position - boid.transform.position;
            float sqrDistanceToTarget = followTargetVector.sqrMagnitude;
            float dynamicWeight = Mathf.Clamp01(sqrDistanceToTarget / MaxWeightSqrDistance);

            followTargetVector = followTargetVector.normalized * staticWeight * dynamicWeight;

            return followTargetVector;
        }
    }
}