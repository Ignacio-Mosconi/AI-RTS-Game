using System.Collections.Generic;
using UnityEngine;

namespace GreenNacho.AI.Flocking
{
    [System.Serializable]
    public class FollowTargetBehavior : FlockBehavior
    {
        [SerializeField] Transform target;

        public override Vector3 ComputeVector(Boid boid, List<Boid> neighbors)
        {
            Vector3 followTargetVector = target.position - boid.transform.position;

            ClampVectorToWeight(ref followTargetVector);

            return followTargetVector;
        }
    }
}