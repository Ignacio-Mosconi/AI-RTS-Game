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
        [SerializeField] GameObject agentPrefab;

        [Header("Scene Set Up")]
        [SerializeField] Vector3 sceneHalfExtents = new Vector3(20f, 0f, 20f);

        [Header("Simulation Properties")]
        [SerializeField, Range(5f, 60f)] float generationDuration = 20f;
        [SerializeField, Range(0f, 1f)] float percentageOfElites = 0.1f;
        [SerializeField, Range(0f, 1f)] float mutationProbability = 0.1f;
        [SerializeField, Range(0f, 1f)] float mutationIntensity = 0.01f;
        [SerializeField, Range(0, 100)] int populationAmount = 40;
        [SerializeField, Range(0, 100)] int iterationsPerUpdate = 1;

        [Header("Neural Network Properties")]
        [SerializeField, Range(0, 100)] int inputs = 4;
        [SerializeField, Range(0, 100)] int hiddenLayers = 1;
        [SerializeField, Range(0, 100)] int outputs = 2;
        [SerializeField, Range(0, 100)] int neuronsPerHiddenLayer = 7;
        [SerializeField, Range(-10f, 0f)] float bias = -1f;
        [SerializeField, Range(0f, 1f)] float slope = 0.5f;

        GeneticAlgorithm geneticAlgorithm;

        List<NeuralNetworkAgent> populationAgents = new List<NeuralNetworkAgent>();
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

            for (int i = 0; i < iterationsPerUpdate; i++)
            {
                foreach (NeuralNetworkAgent agent in populationAgents)
                {
                    OnSimulationAgentUpdate(agent);
                    KeepAgentInSceneExtents(agent);
                }

                timer += Time.fixedDeltaTime;

                if (timer >= generationDuration)
                {
                    timer -= generationDuration;
                    GenerateNewGeneration();
                    break;
                }
            }
        }

        float GetBestFitness()
        {
            float bestFitness = 0;
            
            foreach (Genome genome in geneticAlgorithm.CurrentGeneration)
            {
                if (bestFitness < genome.Fitness)
                    bestFitness = genome.Fitness;
            }

            return bestFitness;
        }

        float GetAverageFitness()
        {
            float averageFitness = 0;

            foreach (Genome genome in geneticAlgorithm.CurrentGeneration)
                averageFitness += genome.Fitness;

            return (averageFitness / geneticAlgorithm.CurrentGeneration.Count);
        }

        float GetWorstFitness()
        {
            float worstFitness = float.MaxValue;
            
            foreach (Genome genome in geneticAlgorithm.CurrentGeneration)
                if (worstFitness > genome.Fitness)
                    worstFitness = genome.Fitness;

            return worstFitness;
        }

        NeuralNetwork CreateNeuralNetwork()
        {
            NeuralNetwork neuralNetwork = new NeuralNetwork(bias, slope);

            neuralNetwork.AddInputLayer(inputs, bias, slope);

            for (int i = 0; i < hiddenLayers; i++)
                neuralNetwork.AddLayer(neuronsPerHiddenLayer, bias, slope);

            neuralNetwork.AddLayer(outputs, bias, slope);

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

            for (int i = 0; i < populationAmount; i++)
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

            if (position.x > sceneHalfExtents.x)
                position.x -= sceneHalfExtents.x * 2f;
            else
                if (position.x < -sceneHalfExtents.x)
                position.x += sceneHalfExtents.x * 2f;

            if (position.z > sceneHalfExtents.z)
                position.z -= sceneHalfExtents.z * 2f;
            else
                if (position.z < -sceneHalfExtents.z)
                position.z += sceneHalfExtents.z * 2f;

            agent.transform.position = position;
        }

        void GenerateInitialPopulation()
        {
            Generation = 0;

            DestroyAgents();

            for (int i = 0; i < populationAmount; i++)
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
            return new Vector3(Random.value * sceneHalfExtents.x * 2f - sceneHalfExtents.x, 0f, 
                                Random.value * sceneHalfExtents.z * 2f - sceneHalfExtents.z);
        }

        protected Quaternion GetRandomRotation()
        {
            return Quaternion.AngleAxis(Random.value * 360f, Vector3.up);
        }

        protected abstract void OnSimulationAgentUpdate(NeuralNetworkAgent agent);
        protected abstract void OnStartSimulation();
        protected abstract void OnStopSimulation();

        public void StartSimulation()
        {
            geneticAlgorithm = new GeneticAlgorithm(percentageOfElites, mutationProbability, mutationIntensity);

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