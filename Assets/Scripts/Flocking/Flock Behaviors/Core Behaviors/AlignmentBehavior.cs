using System.Collections.Generic;
using UnityEngine;

namespace GreenNacho.AI.Flocking
{
    [System.Serializable]
    public class AlignmentBehavior : FlockBehavior
    {
        public override Vector3 ComputeVector(Boid boid, List<Boid> neighbors)
        {
            Vector3 alignmentVector = Vector3.zero;

            if (neighbors.Count != 0)
            {
                foreach (Boid b in neighbors)
                    alignmentVector += b.transform.forward;

                alignmentVector /= neighbors.Count;
            }
            else
                alignmentVector = boid.transform.forward;

            float angleBetweenDirs = Vector3.Angle(boid.transform.forward, alignmentVector);
            float dynamicWeight = Mathf.Clamp01(angleBetweenDirs / 180f);

            alignmentVector *= staticWeight * dynamicWeight;

            return alignmentVector;
        }
    }
}