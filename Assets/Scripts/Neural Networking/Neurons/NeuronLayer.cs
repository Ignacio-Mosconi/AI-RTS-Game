namespace GreenNacho.AI.NeuralNetworking
{
    public class NeuronLayer
    {
        public int NumberOfWeights { get; private set; }
        public int NumberOfOutputs { get { return outputs.Length; } }

        Neuron[] neurons;
        float[] outputs;
        int numberOfInputs;

        public NeuronLayer(int numberOfInputs, int numberOfNeurons, float bias, float slope)
        {
            this.numberOfInputs = numberOfInputs;

            SetUpNeurons(numberOfNeurons, bias, slope);
        }

        void SetUpNeurons(int numberOfNeurons, float bias, float slope)
        {
            neurons = new Neuron[numberOfNeurons];

            for (int i = 0; i < neurons.Length; i++)
            {
                neurons[i] = new Neuron(numberOfInputs + 1, bias, slope);
                NumberOfWeights += numberOfInputs + 1;
            }

            outputs = new float[neurons.Length];
        }

        public void SetWeights(float[] weights)
        {
            int startIndex = 0;

            for (int i = 0; i < neurons.Length; i++)
            {
                float[] neuronWeights = new float[neurons[i].Weights.Length];
                
                for (int j = 0; j < neuronWeights.Length; j++)
                    neuronWeights[j] = weights[startIndex + j];
                
                neurons[i].Weights = neuronWeights;
                startIndex += neurons[i].Weights.Length;
            }
        }

        public float[] DoSynapsis(float[] inputs)
        {
            for (int i = 0; i < neurons.Length; i++)
                outputs[i] = neurons[i].DoSynapsis(inputs);

            return outputs;
        }
    }
}