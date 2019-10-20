using System.Collections.Generic;
using UnityEngine;

namespace GreenNacho.AI.Flocking
{
    [System.Serializable]
    public class FlockCompositeBehavior
    {
        [Header("Core Behaviors")]
        [SerializeField] AlignmentBehavior alignmentBehavior = default;
        [SerializeField] CohesionBehavior cohesionBehavior = default;
        [SerializeField] SeparationBehavior separationBehavior = default;

        protected List<FlockBehavior> flockBehaviors;

        public void Initialize(Flock flock)
        {
            flockBehaviors = new List<FlockBehavior>();

            flockBehaviors.Add(alignmentBehavior);
            flockBehaviors.Add(cohesionBehavior);
            flockBehaviors.Add(separationBehavior);

            OnInitialize();

            foreach (FlockBehavior flockBehavior in flockBehaviors)
                flockBehavior.Initialize(flock);
        }

        public virtual void OnInitialize()
        {

        }

        public Vector3 UpdateBoidVelocity(Boid boid, List<Boid> neighbors)
        {
            Vector3 velocity = Vector3.zero;

            for (int i = 0; i < flockBehaviors.Count; i++)
                velocity += flockBehaviors[i].ComputeVector(boid, neighbors);

            if (velocity == Vector3.zero)
                velocity = boid.transform.forward;

            return velocity;
        }
    }
}