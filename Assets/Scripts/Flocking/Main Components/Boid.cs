using UnityEngine;

namespace GreenNacho.AI.Flocking
{
    public class Boid : MonoBehaviour
    {
        public void Move(Vector3 velocity)
        {
            transform.forward = velocity.normalized;
            transform.position += velocity * Time.deltaTime;
        }
    }
}