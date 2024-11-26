using TSPGeneticAlgorithm.Interfaces;
using TSPGeneticAlgorithm.Models;

namespace TSPGeneticAlgorithm.Mutations
{
    public class SwapMutation : IMutationOperator
    {
        private double _mutationRate;

        public SwapMutation(double mutationRate = 0.01)
        {
            _mutationRate = mutationRate;
        }

        public void Mutate(Chromosome chromosome, ThreadLocal<Random> random)
        {
            if (random.Value.NextDouble() >= _mutationRate)
                return;

            int index1 = random.Value.Next(chromosome.Cities.Count);
            int index2 = random.Value.Next(chromosome.Cities.Count);

            var tmp = chromosome.Cities[index1];
            chromosome.Cities[index1] = chromosome.Cities[index2];
            chromosome.Cities[index2] = tmp;
        }
    }
}