namespace GreenNacho.AI.NeuralNetworking
{
    public class NeuronLayer
    {
        Neuron[] neurons;
        float[] outputs;
        int layerWeights = 0;
        int numberOfInputs = 0;
        float bias = 1;
        float slope = 0.5f;

        public NeuronLayer(int numberOfNeurons, int numberOfInputs, float bias, float slope)
        {
            this.numberOfInputs = numberOfInputs;
            this.bias = bias;
            this.slope = slope;

            SetUpNeurons(numberOfNeurons);
        }

        void SetUpNeurons(int numberOfNeurons)
        {
            neurons = new Neuron[numberOfNeurons];

            for (int i = 0; i < neurons.Length; i++)
            {
                neurons[i] = new Neuron(numberOfInputs + 1, bias, slope);
                layerWeights += numberOfInputs + 1;
            }

            outputs = new float[neurons.Length];
        }

        public void SetWeights(float[][] weights)
        {
            for (int i = 0; i < neurons.Length; i++)
                neurons[i].Weights = weights[i];
        }

        public float[] DoSynapsis(float[] inputs)
        {
            for (int i = 0; i < neurons.Length; i++)
                outputs[i] = neurons[i].DoSynapsis(inputs);

            return outputs;
        }

        public int NumberOfOutputs
        {
            get { return outputs.Length; }
        }
    }
}