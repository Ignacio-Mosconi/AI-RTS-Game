using System;
using System.Collections.Generic;
using UnityEngine;

namespace GreenNacho.AI.Flocking
{
    [System.Serializable]
    public class FlockCompositeBehavior : ISerializationCallbackReceiver
    {
        [SerializeField] int behaviorCount = CoreBeaviorsCount;
        [SerializeField] float[] behaviorWeights = new float[CoreBeaviorsCount];

        const int CoreBeaviorsCount = 3;
        const float MinBehaviorWeight = 0f;
        const float MaxBehaviorWeight = 10f;

        List<FlockBehavior> behaviors;

        public void OnAfterDeserialize()
        {
            if (behaviorCount < CoreBeaviorsCount)
                behaviorCount = 3;
            Array.Resize(ref behaviorWeights, behaviorCount);
            for (int i = 0; i < behaviorWeights.Length; i++)
                behaviorWeights[i] = Mathf.Clamp(behaviorWeights[i], MinBehaviorWeight, MaxBehaviorWeight);
        }

        public void OnBeforeSerialize()
        {

        }

        public void Initialize(Flock flock)
        {
            behaviors = new List<FlockBehavior>();

            behaviors.Add(new AlignmentBehavior(flock));
            behaviors.Add(new CohesionBehavior(flock));
            behaviors.Add(new SeparationBehavior(flock));
        }

        public Vector3 UpdateBoidVelocity(Boid boid, List<Boid> neighbors)
        {
            Vector3 velocity = Vector3.zero;

            for (int i = 0; i < behaviors.Count; i++)
            {
                Vector3 partialVelocity = behaviors[i].ComputeVector(boid, neighbors) * behaviorWeights[i];

                if (partialVelocity.sqrMagnitude > behaviorWeights[i] * behaviorWeights[i])
                    partialVelocity = partialVelocity.normalized * behaviorWeights[i];

                velocity += partialVelocity;
            }

            if (velocity == Vector3.zero)
                velocity = boid.transform.forward;

            return velocity;
        }
    }
}