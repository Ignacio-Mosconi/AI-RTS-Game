using System.Collections.Generic;
using UnityEngine;

namespace GreenNacho.AI.Flocking
{
    [System.Serializable]
    public abstract class FlockBehavior
    {
        [SerializeField, Min(1f)] protected float staticWeight = 1f;

        protected Flock flock;

        public virtual void Initialize(Flock flock)
        {
            this.flock = flock;
        }

        public abstract Vector3 ComputeVector(Boid boid, List<Boid> neighbors);
    }
}