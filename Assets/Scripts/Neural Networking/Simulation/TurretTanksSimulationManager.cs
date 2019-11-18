using System.Collections.Generic;
using UnityEngine;

namespace GreenNacho.AI.NeuralNetworking
{
    public class TurretTanksSimulationManager : SimulationManager
    {
        #region Singleton

        public static new TurretTanksSimulationManager Instance
        {
            get
            {
                if (!instance)
                    instance = FindObjectOfType<TurretTanksSimulationManager>();
                if (!instance)
                {
                    GameObject simulationManager = new GameObject("Turret Tanks Simulation Manager");
                    instance = simulationManager.AddComponent<TurretTanksSimulationManager>();
                }

                return instance as TurretTanksSimulationManager;
            }
        }

        #endregion

        Collider GetNearestEnemy(Transform agentTransform)
        {
            GameObject nearest = populationAgents[0].gameObject;
            int i = 1;

            while (nearest.transform == agentTransform)
            {
                nearest = populationAgents[i].gameObject;
                i++;
            }

            float nearestSqrDistance = (agentTransform.position - nearest.transform.position).sqrMagnitude;

            foreach (NeuralNetworkAgent agent in populationAgents)
            {
                if (agent.transform != agentTransform)
                {
                    float newSqrDistance = (agent.transform.position - agentTransform.position).sqrMagnitude;

                    if (newSqrDistance < nearestSqrDistance)
                    {
                        nearest = agent.gameObject;
                        nearestSqrDistance = newSqrDistance;
                    }
                }
            }

            return nearest.GetComponent<Collider>();
        }

        protected override void OnSimulationAgentUpdate(NeuralNetworkAgent agent)
        {
            TurretTankAgent turretTankAgent = agent as TurretTankAgent;
            
            if (!turretTankAgent.HasShotTarget)
            {
                Collider enemy = GetNearestEnemy(turretTankAgent.transform);
                turretTankAgent.NearestEnemy = enemy;
            }
            else
            {
                turretTankAgent.UpdateCurrentProjectiles();
                turretTankAgent.UpdateFiringCooldown();
            }

            turretTankAgent.Think();
        }

        protected override void OnStopSimulation()
        {
            for (int i = 0; i < populationAgents.Count; i++)
            {
                TurretTankAgent turretTankAgent = populationAgents[i] as TurretTankAgent;
                turretTankAgent.DestroyProjectiles();
            }
        }
    }
}