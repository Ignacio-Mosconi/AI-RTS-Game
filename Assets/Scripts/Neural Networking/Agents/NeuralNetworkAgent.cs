using UnityEngine;

namespace GreenNacho.AI.NeuralNetworking
{
    public abstract class NeuralNetworkAgent : MonoBehaviour
    {
        public NeuralNetwork NeuralNetwork { get; private set; }

        protected Genome genome;
        protected float[] inputs;

        void ResetFitness()
        {
            genome.Fitness = 0;
        }

        public void SetNeuralNetwork(Genome genome, NeuralNetwork neuralNetwork)
        {
            this.genome = genome;
            this.NeuralNetwork = neuralNetwork;
            inputs = new float[neuralNetwork.NumberOfInputs];
            
            ResetFitness();
        }

        public abstract void Think();
    }
}