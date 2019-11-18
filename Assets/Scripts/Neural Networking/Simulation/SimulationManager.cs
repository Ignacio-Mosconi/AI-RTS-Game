using UnityEngine;
using System.Collections.Generic;

namespace GreenNacho.AI.NeuralNetworking
{
    public abstract class SimulationManager : MonoBehaviour
    {
        #region Singleton

        protected static SimulationManager instance;

        void SetUpSingleton()
        {
            if (Instance == this)
                DontDestroyOnLoad(gameObject);
            else
                Destroy(gameObject);
        }

        public static SimulationManager Instance
        {
            get
            {
                if (!instance)
                    instance = FindObjectOfType<SimulationManager>();
                if (!instance)
                {
                    GameObject simulationManager = new GameObject("Simulation Manager");
                    instance = simulationManager.AddComponent<SimulationManager>();
                }

                return instance;
            }
        }

        #endregion
        
        [Header("Simulation Agent Prefab")]
        [SerializeField] GameObject agentPrefab = default;

        [Header("Scene Set Up")]
        [SerializeField] Vector3 sceneHalfExtents = new Vector3(20f, 0f, 20f);

        [Header("Basic Neural Network Properties")]
        [SerializeField, Range(0, 100)] int inputs = 4;
        [SerializeField, Range(0, 100)] int outputs = 2;

        public float GenerationDuration { get; set; } = 20f;
        public float PercentageOfElites { get; set; } = 0.1f;
        public float MutationProbability { get; set; } = 0.1f;
        public float MutationIntensity { get; set; } = 0.1f;
        public int PopulationAmount { get; set; } = 40;
        public int HiddenLayers { get; set; } = 1;
        public int NeuronsPerHiddenLayer { get; set; } = 7;
        public float Bias { get; set; } = -1f;
        public float Slope { get; set; } = 0.5f;
        public int IterationsPerUpdate { get; set; } = 1;

        GeneticAlgorithm geneticAlgorithm;

        protected List<NeuralNetworkAgent> populationAgents = new List<NeuralNetworkAgent>();
        List<NeuralNetwork> neuralNetworks = new List<NeuralNetwork>();

        public int Generation { get; private set; }
        public float BestFitness { get; private set; }
        public float AverageFitness { get; private set; }
        public float WorstFitness { get; private set; }

        float timer = 0;
        bool isRunning = false;

        void FixedUpdate()
        {
            if (!isRunning)
                return;

            for (int i = 0; i < IterationsPerUpdate; i++)
            {
                foreach (NeuralNetworkAgent agent in populationAgents)
                {
                    OnSimulationAgentUpdate(agent);
                    KeepAgentInSceneExtents(agent);
                }

                timer += Time.fixedDeltaTime;

                if (timer >= GenerationDuration)
                {
                    timer -= GenerationDuration;
                    GenerateNewGeneration();
                    break;
                }
            }
        }

        float GetBestFitness()
        {
            float bestFitness = 0;
            
            foreach (Genome genome in geneticAlgorithm.CurrentGeneration)
                if (genome.Fitness > bestFitness)
                    bestFitness = genome.Fitness;

            return bestFitness;
        }

        float GetAverageFitness()
        {
            float totalFitness = 0;

            foreach (Genome genome in geneticAlgorithm.CurrentGeneration)
                totalFitness += genome.Fitness;

            return (totalFitness / geneticAlgorithm.CurrentGeneration.Count);
        }

        float GetWorstFitness()
        {
            float worstFitness = float.MaxValue;
            
            foreach (Genome genome in geneticAlgorithm.CurrentGeneration)
                if (genome.Fitness < worstFitness)
                    worstFitness = genome.Fitness;

            return worstFitness;
        }

        NeuralNetwork CreateNeuralNetwork()
        {
            NeuralNetwork neuralNetwork = new NeuralNetwork(Bias, Slope);

            neuralNetwork.AddInputLayer(inputs, Bias, Slope);

            for (int i = 0; i < HiddenLayers; i++)
                neuralNetwork.AddLayer(NeuronsPerHiddenLayer, Bias, Slope);

            neuralNetwork.AddLayer(outputs, Bias, Slope);

            return neuralNetwork;
        }

        NeuralNetworkAgent CreateAgent(Genome genome, NeuralNetwork neuralNetwork)
        {
            GameObject go = Instantiate(agentPrefab, GetRandomPosition(), GetRandomRotation());
            NeuralNetworkAgent agent = go.GetComponent<NeuralNetworkAgent>();

            agent.SetNeuralNetwork(genome, neuralNetwork);

            return agent;
        }

        void GenerateNewGeneration()
        {
            Generation++;

            BestFitness = GetBestFitness();
            AverageFitness = GetAverageFitness();
            WorstFitness = GetWorstFitness();

            geneticAlgorithm.UpdateGeneration();

            for (int i = 0; i < PopulationAmount; i++)
            {
                NeuralNetwork neuralNetwork = neuralNetworks[i];

                neuralNetwork.SetWeights(geneticAlgorithm.CurrentGeneration[i].Genes);

                populationAgents[i].SetNeuralNetwork(geneticAlgorithm.CurrentGeneration[i], neuralNetwork);
                populationAgents[i].transform.position = GetRandomPosition();
                populationAgents[i].transform.rotation = GetRandomRotation();
            }
        }

        void KeepAgentInSceneExtents(NeuralNetworkAgent agent)
        {
            Vector3 position = agent.transform.position;

            if (position.x > transform.position.x + sceneHalfExtents.x)
                position.x -= sceneHalfExtents.x * 2f;
            else
                if (position.x < transform.position.x - sceneHalfExtents.x)
                    position.x += sceneHalfExtents.x * 2f;

            if (position.z > transform.position.x + sceneHalfExtents.z)
                position.z -= sceneHalfExtents.z * 2f;
            else
                if (position.z < transform.position.x - sceneHalfExtents.z)
                    position.z += sceneHalfExtents.z * 2f;

            agent.transform.position = position;
        }

        void GenerateInitialPopulation()
        {
            Generation = 0;

            DestroyAgents();

            for (int i = 0; i < PopulationAmount; i++)
            {
                NeuralNetwork neuralNetwork = CreateNeuralNetwork();

                Genome genome = new Genome(neuralNetwork.NumberOfWeights);

                neuralNetwork.SetWeights(genome.Genes);
                neuralNetworks.Add(neuralNetwork);

                geneticAlgorithm.CurrentGeneration.Add(genome);
                populationAgents.Add(CreateAgent(genome, neuralNetwork));
            }

            timer = 0f;
        }

        void DestroyAgents()
        {
            foreach (NeuralNetworkAgent neuralNetworkAgent in populationAgents)
                Destroy(neuralNetworkAgent.gameObject);

            populationAgents.Clear();
            neuralNetworks.Clear();
        }

        protected Vector3 GetRandomPosition()
        {
            return new Vector3(Random.value * transform.position.x + sceneHalfExtents.x * 2f - sceneHalfExtents.x, 0f, 
                                Random.value * transform.position.z + sceneHalfExtents.z * 2f - sceneHalfExtents.z);
        }

        protected Quaternion GetRandomRotation()
        {
            return Quaternion.AngleAxis(Random.value * 360f, Vector3.up);
        }

        protected abstract void OnSimulationAgentUpdate(NeuralNetworkAgent agent);
        protected virtual void OnStartSimulation() {}
        protected virtual void OnStopSimulation() {}

        public void StartSimulation()
        {
            geneticAlgorithm = new GeneticAlgorithm(PercentageOfElites, MutationProbability, MutationIntensity);

            GenerateInitialPopulation();
            OnStartSimulation();

            isRunning = true;
        }

        public void ChangeSimulationPauseState()
        {
            isRunning = !isRunning;
        }

        public void StopSimulation()
        {
            isRunning = false;
            Generation = 0;

            DestroyAgents();
            OnStopSimulation();
        }
    }
}