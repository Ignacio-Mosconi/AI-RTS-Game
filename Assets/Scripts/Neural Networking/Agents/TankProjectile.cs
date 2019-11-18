using System;
using UnityEngine;

namespace GreenNacho.AI.NeuralNetworking
{
    [RequireComponent(typeof(SphereCollider))]
    public class TankProjectile : MonoBehaviour
    {
        [SerializeField, Range(10f, 50f)] float speed = 20f;
        [SerializeField, Range(100f, 1000f)] float maxTravelDistance = 200f; 

        public Action OnTargetShot { get; set; }
        public Action OnTargetMissed { get; set; }
        
        SphereCollider sphereCollider;
        Collider target;
        float travelDistance = 0f;

        void Awake()
        {
            sphereCollider = GetComponent<SphereCollider>();
        }

        public void UpdateTrajectory()
        {
            if (!gameObject.activeInHierarchy)
                return;

            float moveDistance = speed * Time.fixedDeltaTime;
            
            travelDistance += moveDistance;
            transform.position += transform.forward * moveDistance;

            if (sphereCollider.bounds.Intersects(target.bounds))
            {
                gameObject.SetActive(false);
                OnTargetShot.Invoke();
            }

            if (travelDistance >= maxTravelDistance)
            {
                gameObject.SetActive(false);
                OnTargetMissed.Invoke();
            }
        }

        public void Launch(Collider target)
        {
            this.target = target;
            travelDistance = 0f;
            gameObject.SetActive(true);
        }
    }
}