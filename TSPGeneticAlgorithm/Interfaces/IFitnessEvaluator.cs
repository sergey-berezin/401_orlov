using TSPGeneticAlgorithm.Models;

namespace TSPGeneticAlgorithm.Interfaces
{
    public interface IFitnessEvaluator
    {
        double EvaluateFitness(Chromosome chromosome);
    }
}