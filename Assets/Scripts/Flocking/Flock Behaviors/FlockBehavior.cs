using System;
using System.Collections.Generic;
using UnityEngine;

namespace GreenNacho.AI.Flocking
{
    public abstract class FlockBehavior
    {
        protected Flock flock;

        public FlockBehavior(Flock flock)
        {
            this.flock = flock;
        }

        public abstract Vector3 ComputeVector(Boid boid, List<Boid> neighbors);
    }
}