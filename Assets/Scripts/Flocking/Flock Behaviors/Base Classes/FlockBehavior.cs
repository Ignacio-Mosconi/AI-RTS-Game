using System.Collections.Generic;
using UnityEngine;

namespace GreenNacho.AI.Flocking
{
    [System.Serializable]
    public abstract class FlockBehavior
    {
        [SerializeField, Min(1f)] protected float weight = 1f;

        protected Flock flock;

        public virtual void Initialize(Flock flock)
        {
            this.flock = flock;
        }

        protected void ClampVectorToWeight(ref Vector3 vector)
        {
            if (vector.sqrMagnitude > weight * weight)
                vector = vector.normalized * weight;
        }

        public abstract Vector3 ComputeVector(Boid boid, List<Boid> neighbors);
    }
}