using System.Collections.Generic;
using UnityEngine;

namespace GreenNacho.AI.NeuralNetworking
{
    public class MinerTanksSimulationManager : SimulationManager
    {
        #region Singleton
        
        public static new MinerTanksSimulationManager Instance
        {
            get
            {
                if (!instance)
                    instance = FindObjectOfType<MinerTanksSimulationManager>();
                if (!instance)
                {
                    GameObject simulationManager = new GameObject("Miner Tanks Simulation Manager");
                    instance = simulationManager.AddComponent<MinerTanksSimulationManager>();
                }

                return instance as MinerTanksSimulationManager;
            }
        }

        #endregion

        [Header("Mines Properties")]
        [SerializeField] GameObject minePrefab;
        [SerializeField, Range(0, 100)] int numberOfMines = 50;

        List<GameObject> mines = new List<GameObject>();

        void CreateMines()
        {
            DestroyMines();

            for (int i = 0; i < numberOfMines; i++)
            {
                Vector3 position = GetRandomPosition();
                GameObject go = Instantiate(minePrefab, position, Quaternion.identity);

                mines.Add(go);
            }
        }

        void DestroyMines()
        {
            foreach (GameObject go in mines)
                Destroy(go);

            mines.Clear();
        }

        GameObject GetNearestMine(Vector3 position)
        {
            GameObject nearest = mines[0];
            float nearestSqrDistance = (position - nearest.transform.position).sqrMagnitude;

            foreach (GameObject go in mines)
            {
                float newSqrDistance = (go.transform.position - position).sqrMagnitude;

                if (newSqrDistance < nearestSqrDistance)
                {
                    nearest = go;
                    nearestSqrDistance = newSqrDistance;
                }
            }

            return nearest;
        }

        protected override void OnSimulationAgentUpdate(NeuralNetworkAgent agent)
        {
            MinerTankAgent minerTankAgent = agent as MinerTankAgent;

            GameObject mine = GetNearestMine(minerTankAgent.transform.position);
            
            minerTankAgent.NearestMine = mine;

            minerTankAgent.Think();
        }

        protected override void OnStartSimulation()
        {
            CreateMines();
        }

        protected override void OnStopSimulation()
        {
            DestroyMines();
        }

        public void RelocateMine(GameObject mine)
        {
            mine.transform.position = GetRandomPosition();
        }
    }
}