using UnityEngine;

namespace GreenNacho.AI.Flocking
{
    public class Boid : MonoBehaviour
    {
        [SerializeField, Range(0f, 1f)] float steerSmoothTime = 0.5f;

        Vector3 currentVelocity;

        public void Move(Vector3 velocity)
        {
            velocity = Vector3.SmoothDamp(transform.forward, velocity, ref currentVelocity, steerSmoothTime);

            transform.forward = velocity.normalized;
            transform.position += velocity * Time.deltaTime;
        }
    }
}