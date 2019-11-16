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
            Weights = new float[numberOfWeights];
            this.bias = bias;
            this.slope = slope;
        }

        float GetSigmoid(float activation)
        {
            return (1.0f / (1.0f + Mathf.Exp(-activation / slope)));
        }

        public float DoSynapsis(float[] inputs)
        {
            float activation = 0;

            for (int i = 0; i < inputs.Length; i++)
                activation += Weights[i] * inputs[i];

            activation += bias * Weights[Weights.Length - 1];

            return GetSigmoid(activation);
        }
    }
}