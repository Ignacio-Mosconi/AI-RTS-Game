using System.Collections.Generic;
using UnityEngine;

namespace GreenNacho.AI.Flocking
{
    [System.Serializable]
    public class SeparationBehavior : FlockBehavior
    {
        public override Vector3 ComputeVector(Boid boid, List<Boid> neighbors)
        {
            Vector3 separationVector = Vector3.zero;
            int avoidanceCount = 0;

            foreach (Boid b in neighbors)
            {
                Vector3 diff = boid.transform.position - b.transform.position;

                if (diff.sqrMagnitude <= flock.SeparationRadiusSquared)
                {
                    separationVector += diff;
                    avoidanceCount++;
                }
            }

            if (avoidanceCount > 0)
                separationVector /= neighbors.Count;

            ClampVectorToWeight(ref separationVector);

            return separationVector;
        }
    }
}