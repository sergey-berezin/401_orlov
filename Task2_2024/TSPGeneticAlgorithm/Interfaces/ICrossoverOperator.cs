using TSPGeneticAlgorithm.Models;

namespace TSPGeneticAlgorithm.Interfaces
{
    public interface ICrossoverOperator
    {
        Chromosome Crossover(Chromosome parent1, Chromosome parent2, ThreadLocal<Random> random);
    }
}