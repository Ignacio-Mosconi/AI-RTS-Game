using System.Collections.Generic;
using UnityEngine;

namespace GreenNacho.AI.Flocking
{
    [System.Serializable]
    public class SeparationBehavior : FlockBehavior
    {
        public SeparationBehavior(Flock flock) : base(flock)
        {

        }

        public override Vector3 ComputeVector(Boid boid, List<Boid> neighbors)
        {
            Vector3 separationVector = Vector3.zero;
            int avoidanceCount = 0;

            foreach (Boid b in neighbors)
            {
                Vector3 diff = b.transform.position - boid.transform.position;

                if (diff.sqrMagnitude <= flock.SeparationRadiusSquared)
                {
                    separationVector += diff;
                    avoidanceCount++;
                }
            }

            if (avoidanceCount > 0)
                separationVector /= neighbors.Count;

            return separationVector;
        }
    }
}