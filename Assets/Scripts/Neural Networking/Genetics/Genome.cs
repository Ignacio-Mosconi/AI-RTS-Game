using UnityEngine;

namespace GreenNacho.AI.NeuralNetworking
{
    public class Genome
    {
        public float[] Genes { get; set; }
        public float Fitness { get; set; }

        public Genome()
        {
            Fitness = 0;
        }

        public Genome(float[] genes)
        {
            this.Genes = genes;
            Fitness = 0;
        }

        public Genome(int numberOfGenes)
        {
            Genes = new float[numberOfGenes];

            for (int i = 0; i < numberOfGenes; i++)
                Genes[i] = Random.Range(-1.0f, 1.0f);

            Fitness = 0;
        }
    }
}