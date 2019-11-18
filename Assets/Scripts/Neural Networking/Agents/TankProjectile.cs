using System;
using UnityEngine;

namespace GreenNacho.AI.NeuralNetworking
{
    [RequireComponent(typeof(SphereCollider))]
    public class TankProjectile : MonoBehaviour
    {
        [SerializeField, Range(10f, 50f)] float speed = 20f;
        [SerializeField, Range(50f, 1000f)] float maxTravelDistance = 200f; 

        public Action OnTargetShot { get; set; }
        public Action<bool> OnTargetMissed { get; set; }
        
        SphereCollider sphereCollider;
        Collider target;
        float distanceTraveled = 0f;
        float sqrDistanceToTarget = 0f;
        bool nearMiss = false;

        const float NearMissSqrDistance = 10f;

        void Awake()
        {
            sphereCollider = GetComponent<SphereCollider>();
        }

        public void UpdateTrajectory()
        {
            if (!gameObject.activeInHierarchy)
                return;

            float moveDistance = speed * Time.fixedDeltaTime;
            
            distanceTraveled += moveDistance;
            transform.position += transform.forward * moveDistance;

            float previousSqrDistance = sqrDistanceToTarget;

            sqrDistanceToTarget = sphereCollider.bounds.SqrDistance(target.transform.position);

            if (previousSqrDistance < sqrDistanceToTarget)
                nearMiss = true;

            if (sphereCollider.bounds.Intersects(target.bounds))
            {
                gameObject.SetActive(false);
                OnTargetShot.Invoke();
            }

            if (distanceTraveled >= maxTravelDistance)
            {
                gameObject.SetActive(false);
                OnTargetMissed.Invoke(nearMiss);
            }
        }

        public void Launch(Collider target)
        {
            this.target = target;
            distanceTraveled = 0f;
            sqrDistanceToTarget = 0f;
            nearMiss = false;
            gameObject.SetActive(true);
        }
    }
}