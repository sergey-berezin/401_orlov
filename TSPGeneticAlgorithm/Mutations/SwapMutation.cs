using TSPGeneticAlgorithm.Interfaces;
using TSPGeneticAlgorithm.Models;

namespace TSPGeneticAlgorithm.Mutations
{
    public class SwapMutation : IMutationOperator
    {
        private Random random = new Random();
        private double _mutationRate;

        public SwapMutation(double mutationRate = 0.01)
        {
            _mutationRate = mutationRate;
        }

        public void Mutate(Chromosome chromosome)
        {
            if (random.NextDouble() >= _mutationRate)
                return;
            
            int index1 = random.Next(chromosome.Cities.Count);
            int index2 = random.Next(chromosome.Cities.Count);
            
            var tmp = chromosome.Cities[index1];
            chromosome.Cities[index1] = chromosome.Cities[index2];
            chromosome.Cities[index2] = tmp;
        }
    }
}