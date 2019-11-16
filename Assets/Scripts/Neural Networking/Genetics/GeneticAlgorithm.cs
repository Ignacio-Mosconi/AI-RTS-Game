using System.Collections.Generic;

namespace GreenNacho.AI.NeuralNetworking
{
    public class GeneticAlgorithm 
    {
        public List<Genome> CurrentPopulation { get; private set; } = new List<Genome>();
        public List<Genome> NextPopulation { get; private set; } = new List<Genome>();

        float populationFitness;
        int elites = 0;
        float mutationProbability = 0f;
        float mutationIntensity = 0f;

        public GeneticAlgorithm(int elites, float mutationProbability, float mutationIntensity)
        {
            this.elites = elites;
            this.mutationProbability = mutationProbability;
            this.mutationIntensity = mutationIntensity;
        }

        public List<Genome> GetRandomGenomes(int numberOfGenomes, int numberOfGenes)
        {
            CurrentPopulation.Clear();
            CurrentPopulation = new List<Genome>(numberOfGenomes);

            for (int i = 0; i < numberOfGenomes; i++)
                CurrentPopulation.Add(new Genome(numberOfGenes));

            return CurrentPopulation;
        }

        public List<Genome> GetNewGeneration()
        {
            NextPopulation.Clear();
            
            populationFitness = 0;

            CurrentPopulation.Sort((g1, g2) => g1.Fitness.CompareTo(g2.Fitness));
            NextPopulation.AddRange(CurrentPopulation);

            foreach (Genome genome in nextPopulation)
                populationFitness += genome.Fitness;

            SelectElite();

            while (nextPopulation.Count < oldGenomes.Count)
                Crossover();

            return nextPopulation;
        }

        void SelectElite()
        {
            for (int i = 0; i < elites && NextPopulation.Count < CurrentPopulation.Count; i++)
            {
                NextPopulation.Add(CurrentPopulation[i]);
            }
        }

        void Crossover()
        {
            Genome mom = RouletteSelection();
            Genome dad = RouletteSelection();

            Genome child1;
            Genome child2;

            Crossover(mom, dad, out child1, out child2);

            NextPopulation.Add(child1);
            NextPopulation.Add(child2);
        }

        void Crossover(Genome mom, Genome dad, out Genome child1, out Genome child2)
        {
            child1 = new Genome();
            child2 = new Genome();

            child1.genome = new float[mom.genome.Length];
            child2.genome = new float[mom.genome.Length];

            int pivot = Random.Range(0, mom.genome.Length);

            for (int i = 0; i < pivot; i++)
            {
                child1.genome[i] = mom.genome[i];

                if (ShouldMutate())
                    child1.genome[i] += Random.Range(-mutationRate, mutationRate);

                child2.genome[i] = dad.genome[i];

                if (ShouldMutate())
                    child2.genome[i] += Random.Range(-mutationRate, mutationRate);
            }

            for (int i = pivot; i < mom.genome.Length; i++)
            {
                child2.genome[i] = mom.genome[i];

                if (ShouldMutate())
                    child2.genome[i] += Random.Range(-mutationRate, mutationRate);
                
                child1.genome[i] = dad.genome[i];

                if (ShouldMutate())
                    child1.genome[i] += Random.Range(-mutationRate, mutationRate);
            }
        }

        bool ShouldMutate()
        {
            return Random.Range(0.0f, 1.0f) < mutationProbability;
        }

        int HandleComparison(Genome x, Genome y)
        {
            return x.fitness > y.fitness ? 1 : x.fitness < y.fitness ? -1 : 0;
        }


        public Genome RouletteSelection()
        {
            float rnd = Random.Range(0, Mathf.Max(populationFitness, 0));

            float fitness = 0;

            for (int i = 0; i < CurrentPopulation.Count; i++)
            {
                fitness += Mathf.Max(CurrentPopulation[i].fitness, 0);
                if (fitness >= rnd)
                    return CurrentPopulation[i];
            }

            return null;
        }
    }
}