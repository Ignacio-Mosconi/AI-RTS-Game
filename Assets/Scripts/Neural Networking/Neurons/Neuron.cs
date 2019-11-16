using UnityEngine;

namespace GreenNacho.AI.NeuralNetworking
{
    public class Neuron
    {
        public float[] Weights { get; set; }
        
        float bias;
        float slope;

        public Neuron(int numberOfWeights, float bias, float slope)
        {
            this.bias = bias;
            this.slope = slope;

            Weights = new float[numberOfWeights];

            for (int i = 0; i < Weights.Length; i++)
                Weights[i] = Random.Range(-1.0f, 1.0f);
        }

        float GetSigmoid(float activation)
        {
            return (1.0f / (1.0f + Mathf.Exp(-activation / slope)));
        }

        public float DoSynapsis(float[] input)
        {
            float activation = 0;

            for (int i = 0; i < input.Length; i++)
                activation += Weights[i] * input[i];

            activation += bias * Weights[Weights.Length - 1];

            return GetSigmoid(activation);
        }
    }
}