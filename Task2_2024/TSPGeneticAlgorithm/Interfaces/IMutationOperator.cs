using TSPGeneticAlgorithm.Models;

namespace TSPGeneticAlgorithm.Interfaces
{
    public interface IMutationOperator
    {
        void Mutate(Chromosome chromosome, ThreadLocal<Random> random);
    }
}