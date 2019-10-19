using System.Collections.Generic;
using UnityEngine;

namespace GreenNacho.AI.Flocking
{
    public class Boid : MonoBehaviour
    {
        [SerializeField] float neighborRadius = default;

        List<Boid> neighbors = new List<Boid>();
        Vector3 velocity = Vector3.zero;

        void Update()
        {
            
        }

        Vector3 ComputeAlignment()
        {
            Vector3 alignment = Vector3.zero;
            Vector3 directionSum = Vector3.zero;

            foreach (Boid neighbor in neighbors)
                directionSum += neighbor.transform.forward;

            alignment = directionSum / neighbors.Count;

            return alignment;
        }

        Vector3 ComputeCohesion()
        {
            Vector3 cohesion = Vector3.zero;
            Vector3 positionSum = Vector3.zero;
         
            foreach (Boid neighbor in neighbors)
                positionSum += neighbor.transform.position;

            cohesion = ((positionSum / neighbors.Count) - transform.position).normalized;

            return cohesion;
        }
        
        Vector3 ComputeSeparation()
        {
            Vector3 separation = Vector3.zero;
            Vector3 separationSum = Vector3.zero;
         
            foreach (Boid neighbor in neighbors)
                separationSum += (neighbor.transform.position - transform.position).normalized;

            separation = -separationSum / neighbors.Count;

            return separation;
        }

        public void CheckIfNeighbor(Boid boid)
        {
            Vector3 diff = boid.transform.position - transform.position;
            float sqrDistance = diff.sqrMagnitude;

            if (neighbors.Contains(boid))
            {
                if (sqrDistance >= neighborRadius * neighborRadius)
                    neighbors.Remove(boid);
            }
            else
            {
                if (sqrDistance <= neighborRadius * neighborRadius)
                    neighbors.Add(boid);
            }
        }

        public void UpdateVelocity()
        {
            Vector3 alignment = ComputeAlignment();
            Vector3 cohesion = ComputeAlignment();
            Vector3 separation = ComputeAlignment();

            velocity = (alignment + cohesion + separation) / 3;
        }
    }
}