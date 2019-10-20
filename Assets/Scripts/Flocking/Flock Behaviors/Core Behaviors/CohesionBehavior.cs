using System.Collections.Generic;
using UnityEngine;

namespace GreenNacho.AI.Flocking
{
    [System.Serializable]
    public class CohesionBehavior : FlockBehavior
    {
        public override Vector3 ComputeVector(Boid boid, List<Boid> neighbors)
        {
            Vector3 cohesionVector = Vector3.zero;

            if (neighbors.Count > 0)
            {
                foreach (Boid b in neighbors)
                    cohesionVector += b.transform.position;

                cohesionVector = (cohesionVector / neighbors.Count) - boid.transform.position;
            }

            ClampVectorToWeight(ref cohesionVector);

            return cohesionVector;
        }
    }
}