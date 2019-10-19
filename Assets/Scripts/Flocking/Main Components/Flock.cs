using System.Collections.Generic;
using UnityEngine;

namespace GreenNacho.AI.Flocking
{
    public class Flock : MonoBehaviour
    {
        [SerializeField] FlockCompositeBehavior flockBehavior = default;
        [SerializeField] GameObject boidPrefab = default;
        [SerializeField] Vector3 spawnPoint = Vector3.zero;
        [SerializeField, Range(0, 100)] int numberOfBoids = 10;
        [SerializeField, Range(1f, 5f)] float boidsDensity = 2.5f;
        [SerializeField, Range(1f, 10f)] float boidsSpeed = 5f;
        [SerializeField, Range(1f, 100f)] float maxBoidSpeed = 5f;
        [SerializeField, Range(1f, 10f)] float neighborRadius = 2f;
        [SerializeField, Range(0f, 1f)] float separationRadiusPerc = 0.5f;

        public float MaxBoidSpeedSquared { get; private set; }
        public float NeighborRadiusSquared { get; private set; }
        public float SeparationRadiusSquared { get; private set; }

        List<Boid> flock = new List<Boid>();

        void Awake()
        {
            MaxBoidSpeedSquared = maxBoidSpeed * maxBoidSpeed;
            NeighborRadiusSquared = neighborRadius * neighborRadius;
            SeparationRadiusSquared = NeighborRadiusSquared * separationRadiusPerc * separationRadiusPerc;
        }

        void Start()
        {
            flockBehavior.Initialize(this);

            for (int i = 0; i < numberOfBoids; i++)
            {
                Vector3 spawnPosition = spawnPoint + Random.insideUnitSphere * (numberOfBoids / boidsDensity);
                Quaternion spawnRotation = Quaternion.Euler(Vector3.forward * Random.Range(0f, 360f));
                GameObject boidObject = Instantiate(boidPrefab, spawnPosition, spawnRotation, transform);
                Boid boid = boidObject.GetComponent<Boid>();

                boid.name = boid.name.Replace("(Clone)", "") + " " + i;
                flock.Add(boid);
            }
        }

        void Update()
        {
            foreach (Boid boid in flock)
            {
                List<Boid> neighbors = GetNeighbors(boid);
                Vector3 velocity = flockBehavior.UpdateBoidVelocity(boid, neighbors);

                velocity *= boidsSpeed;

                if (velocity.sqrMagnitude > MaxBoidSpeedSquared)
                    velocity = velocity.normalized * maxBoidSpeed;

                boid.Move(velocity);
            }
        }

        List<Boid> GetNeighbors(Boid boid)
        {
            List<Boid> neighbors = new List<Boid>();

            foreach (Boid b in flock)
                if (b != boid && (b.transform.position - boid.transform.position).sqrMagnitude <= NeighborRadiusSquared)
                    neighbors.Add(b);

            return neighbors;
        }
    }
}