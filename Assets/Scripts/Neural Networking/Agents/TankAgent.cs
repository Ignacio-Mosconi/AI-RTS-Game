using UnityEngine;

namespace GreenNacho.AI.NeuralNetworking
{
    public abstract class TankAgent : NeuralNetworkAgent
    {
        [SerializeField, Range(0f, 100f)] float movementSpeed = 10f;
        [SerializeField, Range(0f, 360f)] float rotationSpeed = 20f;

        protected void Move(float leftTrackForce, float rightTrackForce)
        {
            float rotationFactor = Mathf.Sign((rightTrackForce - leftTrackForce));
            float advanceFactor = Mathf.Abs(rightTrackForce + leftTrackForce) * 0.5f;

            transform.rotation *= Quaternion.AngleAxis(rotationFactor * rotationSpeed * Time.fixedDeltaTime, Vector3.up);
            transform.position += transform.forward * advanceFactor * movementSpeed * Time.fixedDeltaTime;
        }
    }
}