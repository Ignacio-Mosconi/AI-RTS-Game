using System.Collections.Generic;
using UnityEngine;

namespace GreenNacho.AI.NeuralNetworking
{
    public class GeneticAlgorithm 
    {
        public List<Genome> CurrentGeneration { get; private set; } = new List<Genome>();

        float populationFitness;
        float percentageOfElites;
        float mutationProbability;
        float mutationIntensity;

        public GeneticAlgorithm(float percentageOfElites, float mutationProbability, float mutationIntensity)
        {
            this.percentageOfElites = percentageOfElites;
            this.mutationProbability = mutationProbability;
            this.mutationIntensity = mutationIntensity;
        }

        bool ShouldMutate()
        {
            return Random.Range(0.0f, 1.0f) < mutationProbability;
        }

        Genome[] GetElites()
        {
            int numberOfElites = (int)(CurrentGeneration.Count * percentageOfElites);
            Genome[] elites = new Genome[numberOfElites];

            CurrentGeneration.Sort((g1, g2) => g2.Fitness.CompareTo(g1.Fitness));

            for (int i = 0; i < numberOfElites; i++)
                elites[i] = CurrentGeneration[i];

            return elites;
        }

        void DoCrossover()
        {
            Genome[] parents = GetParents();
            Genome[] children = GetChildren(parents);

            CurrentGeneration.Add(children[0]);
            CurrentGeneration.Add(children[1]);
        }

        Genome[] GetParents()
        {
            Genome[] parents = new Genome[2];

            for (int i = 0; i < parents.Length; i++)
                parents[i] = DoRouletteSelection();

            return parents;
        }

        Genome[] GetChildren(Genome[] parents)
        {
            Genome[] children = new Genome[2];

            for (int i = 0; i < children.Length; i++)
            {
                children[i] = new Genome();
                children[i].Genes = new float[parents[0].Genes.Length];
            }

            int divisionIndex = Random.Range(0, parents[0].Genes.Length);

            AddGenesToChildren(children, parents, 0, divisionIndex);
            AddGenesToChildren(children, parents, divisionIndex, parents[0].Genes.Length);

            return children;
        }

        void AddGenesToChildren(Genome[] children, Genome[] parents, int startIndex, int endIndex)
        {
            for (int i = startIndex; i < endIndex; i++)
                for (int j = 0; j < children.Length; i++)
                {
                    children[j].Genes[i] = parents[j].Genes[i];

                    if (ShouldMutate())
                        children[j].Genes[i] += Random.Range(-mutationIntensity, mutationIntensity);
                }
        }

        Genome DoRouletteSelection()
        {
            Genome chosenGenome = null;

            float random = Random.Range(0, Mathf.Max(populationFitness, 0));
            float fitness = 0;

            for (int i = 0; i < CurrentGeneration.Count; i++)
            {
                fitness += Mathf.Max(CurrentGeneration[i].Fitness, 0);
                if (fitness >= random)
                {
                    chosenGenome = CurrentGeneration[i];
                    break;
                }
            }

            return chosenGenome;
        }

        public void Randomizegeneration(int numberOfGenomes, int numberOfGenes)
        {
            CurrentGeneration = new List<Genome>(numberOfGenomes);

            for (int i = 0; i < numberOfGenomes; i++)
                CurrentGeneration.Add(new Genome(numberOfGenes));
        }

        public void UpdateGeneration()
        {
            List<Genome> newGeneration = new List<Genome>(CurrentGeneration);
            populationFitness = 0;
            
            foreach (Genome genome in CurrentGeneration)
                populationFitness += genome.Fitness;

            Genome[] elites = GetElites();

            newGeneration.AddRange(elites);

            while (newGeneration.Count < CurrentGeneration.Count)
                DoCrossover();

            CurrentGeneration = newGeneration;
        }
    }
}