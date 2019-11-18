using System;
using UnityEngine;

namespace GreenNacho.AI.NeuralNetworking
{
    public class TurretTankAgent : TankAgent
    {
        [SerializeField] GameObject projectilePrefab = default;
        [SerializeField] Transform projectileSpawnPoint = default;
        [SerializeField, Range(1f, 5f)] float fireRate = 2f; 

        public Collider NearestEnemy { get; set; }
        public bool HasShotTarget { get; private set; } = false;

        TankProjectile[] projectilePool = new TankProjectile[3];
        GameObject projectilesContainer;

        float fireRateCooldownTimer = 0f;
        bool canShoot = true;

        void Start()
        {
            projectilesContainer = Instantiate(new GameObject(gameObject.name + "Projectiles"));

            for (int i = 0; i < projectilePool.Length; i++)
            {
                GameObject projectile = Instantiate(projectilePrefab, 
                                                    projectileSpawnPoint.position, 
                                                    transform.rotation, 
                                                    projectilesContainer.transform);
                
                projectilePool[i] = projectile.GetComponent<TankProjectile>();
                
                projectilePool[i].OnTargetShot += IncreaseFitness;
                projectilePool[i].OnTargetMissed += DecreaseFitness;
                projectilePool[i].gameObject.SetActive(false);
            }
        }

        Vector3 GetDirectionToNearestEnemy()
        {
            return (NearestEnemy.transform.position - transform.position).normalized;
        }

        float GetSqrDistanceToNearestEnemy()
        {
            return (NearestEnemy.transform.position - transform.position).sqrMagnitude;
        }

        void ResetProjectile(TankProjectile projectile)
        {
            projectile.transform.position = projectileSpawnPoint.position;
            projectile.transform.rotation = transform.rotation;
        }

        void Shoot()
        {
            TankProjectile availableProjectile = Array.Find(projectilePool, p => !p.gameObject.activeInHierarchy);

            if (availableProjectile)
            {
                ResetProjectile(availableProjectile);
                availableProjectile.Launch(NearestEnemy);
                canShoot = false;
                HasShotTarget = true;
            }
        }

        protected override void IncreaseFitness()
        {
            if (!Array.Find(projectilePool, tp => tp.gameObject.activeInHierarchy))
                HasShotTarget = false;
            genome.Fitness *= 2f;
        }

        protected void DecreaseFitness(bool nearMiss)
        {
            if (!Array.Find(projectilePool, tp => tp.gameObject.activeInHierarchy))
                HasShotTarget = false;
            if (!nearMiss && genome.Fitness > 1)
                genome.Fitness /= 2f;
        }

        public override void Think()
        {
            Vector3 currentDirection = transform.forward;
            Vector3 targetDirection = GetDirectionToNearestEnemy();
            float sqrDistanceToEnemy = GetSqrDistanceToNearestEnemy(); 

            inputs[0] = targetDirection.x;
            inputs[1] = targetDirection.z;
            inputs[2] = currentDirection.x;
            inputs[3] = currentDirection.z;
            inputs[4] = sqrDistanceToEnemy;

            float[] outputs = NeuralNetwork.DoSynapsis(inputs);

            Move(outputs[0], outputs[1]);
            
            if (outputs[2] > 0.5f && canShoot)
                Shoot();
        }

        public void UpdateFiringCooldown()
        {
            fireRateCooldownTimer += Time.fixedDeltaTime;

            if (fireRateCooldownTimer >= 1f / fireRate)
            {
                fireRateCooldownTimer = 0f;
                canShoot = true;
            }
        }

        public void UpdateCurrentProjectiles()
        {
            for (int i = 0; i < projectilePool.Length; i++)
                projectilePool[i].UpdateTrajectory();
        }

        public void DestroyProjectiles()
        {
            Destroy(projectilesContainer);
        }
    }
}