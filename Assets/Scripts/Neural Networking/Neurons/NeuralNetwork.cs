using UnityEngine;
using System.Collections.Generic;

namespace GreenNacho.AI.NeuralNetworking
{
    public class NeuralNetwork
    {
        public int NumberOfInputs { get; private set; }
        public int NumberOfWeights { get; private set; }

        float bias;
        float slope;

        List<NeuronLayer> neuronLayers = new List<NeuronLayer>();

        public NeuralNetwork(float bias, float slope) 
        {
            this.bias = bias;
            this.slope = slope;
        }

        void AddNeuronLayer(int numberOfInputs, int numberOfNeurons, float bias, float slope)
        {         
            if (neuronLayers.Count > 0 && neuronLayers[neuronLayers.Count - 1].NumberOfOutputs != numberOfInputs)
            {
                Debug.LogError("The number of inputs must be equal to the number of outputs from the previous layer.");
                return;
            }

            NeuronLayer layer = new NeuronLayer(numberOfInputs, numberOfNeurons, bias, slope);

            NumberOfWeights += (numberOfInputs + 1) * numberOfNeurons;

            neuronLayers.Add(layer);
        }

        public void AddInputLayer(int numberOfInputs, float bias, float slope)
        {
            if (neuronLayers.Count > 0)
            {
                Debug.LogError("The neural network already has an input layer.");
                return;
            }

            NumberOfInputs = numberOfInputs;

            AddNeuronLayer(numberOfInputs, numberOfInputs, bias, slope);
        }

        public void AddLayer(int numberOfOutputs, float bias, float slope)
        {
            if (neuronLayers.Count == 0)
            {
                Debug.LogError("The neural network needs an input layer before being able to get an output or hidden layer.");
                return;
            }

            AddNeuronLayer(neuronLayers[neuronLayers.Count - 1].NumberOfOutputs, numberOfOutputs, bias, slope);
        }

        public void SetWeights(float[] newWeights)
        {
            int startIndex = 0;

            for (int i = 0; i < neuronLayers.Count; i++)
            {
                float[] neuronLayerWeights = new float[neuronLayers[i].NumberOfWeights];

                for (int j = 0; j < neuronLayerWeights.Length; j++)
                    neuronLayerWeights[j] = newWeights[startIndex + j];

                neuronLayers[i].SetWeights(neuronLayerWeights);
                startIndex += neuronLayers[i].NumberOfWeights;
            }	
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