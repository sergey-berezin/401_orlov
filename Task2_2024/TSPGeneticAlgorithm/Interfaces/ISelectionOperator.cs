using TSPGeneticAlgorithm.Models;

namespace TSPGeneticAlgorithm.Interfaces
{
    public interface ISelectionOperator
    {
        Chromosome SelectParent(List<Chromosome> population, ThreadLocal<Random> random);
    }
}