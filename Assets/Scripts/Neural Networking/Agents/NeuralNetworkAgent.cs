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
            genome.Fitness = 1f;
        }

        public void SetNeuralNetwork(Genome genome, NeuralNetwork neuralNetwork)
        {
            this.genome = genome;
            this.NeuralNetwork = neuralNetwork;
            inputs = new float[neuralNetwork.NumberOfInputs];
            
            ResetFitness();
        }

        protected virtual void IncreaseFitness()
        {
            genome.Fitness++;
        }
        
        protected virtual void DecreaseFitness() 
        {
            if (genome.Fitness > 1)
                genome.Fitness--;
        }
        
        public abstract void Think();
    }
}