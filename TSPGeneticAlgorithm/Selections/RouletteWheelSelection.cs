using TSPGeneticAlgorithm.Interfaces;
using TSPGeneticAlgorithm.Models;

namespace TSPGeneticAlgorithm.Selections
{
    public class RouletteWheelSelections: ISelectionOperator
    {
        private Random random  = new Random();
        public Chromosome SelectParent(List<Chromosome> population)
        {
            var totalFitness = population.Sum(c => c.Fitness);
            var minFitness = population.Min(c => c.Fitness);

            var normalizedPopulation = population.Select(c => new 
            {
                Chromosome = c,
                NormalizedFitness = (c.Fitness - minFitness) / (totalFitness - minFitness * population.Count) 
            }).ToList();

            var value = random.NextDouble();
            double cumulative =  0;

            foreach (var item in normalizedPopulation) 
            {
                cumulative += item.NormalizedFitness;
                if (cumulative >= value)
                    return item.Chromosome;
            }
            return normalizedPopulation.Last().Chromosome;
        }
    }
}