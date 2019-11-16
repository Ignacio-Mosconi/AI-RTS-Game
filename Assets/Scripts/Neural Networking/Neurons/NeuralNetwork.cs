using UnityEngine;
using System.Collections.Generic;

namespace GreenNacho.AI.NeuralNetworking
{
    public class NeuralNetwork
    {
        public int NumberOfInputs { get; private set; }
        public int NetworkWeights { get; private set; }

        List<NeuronLayer> neuronLayers = new List<NeuronLayer>();

        public NeuralNetwork() {}

        public void AddNeuronLayer(int numberOfInputs, int numberOfNeurons, float bias, float slope)
        {
            if (neuronLayers.Count == 0 && numberOfInputs != numberOfNeurons)
            {
                Debug.LogError("The number of inputs must be equal to the number of neurons for the input layer.");
                return;
            }
            
            if (neuronLayers.Count > 0 && neuronLayers[neuronLayers.Count - 1].NumberOfOutputs != numberOfInputs)
            {
                Debug.LogError("The number of inputs must be equal to the number of outputs from the previous layer.");
                return;
            }

            if (neuronLayers.Count == 0)
                NumberOfInputs = numberOfInputs;

            NeuronLayer layer = new NeuronLayer(numberOfInputs, numberOfNeurons, bias, slope);

            NetworkWeights += (numberOfInputs + 1) * numberOfNeurons;

            neuronLayers.Add(layer);
        }

        public void SetWeights(float[][][] newWeights)
        {
            for (int i = 0; i < neuronLayers.Count; i++)
                neuronLayers[i].SetWeights(newWeights[i]);		
        }

        public float[] DoSynapsis(float[] inputs)
        {
            float[] outputs = null;

            for (int i = 0; i < neuronLayers.Count; i++)
            {
                outputs = neuronLayers[i].DoSynapsis(inputs);
                inputs = outputs;
            }

            return outputs;
        }
    }
}