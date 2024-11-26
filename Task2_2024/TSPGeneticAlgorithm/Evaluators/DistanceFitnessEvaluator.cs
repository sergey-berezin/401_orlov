using TSPGeneticAlgorithm.Models;
using TSPGeneticAlgorithm.Interfaces;

namespace TSPGeneticAlgorithm.Evaluators
{
    public class DistanceFitnessEvaluator: IFitnessEvaluator
    {
        public double EvaluateFitness(Chromosome chromosome)
        {
            return ExpEvaluateFitness(chromosome);
            //return 1.0 / chromosome.GetTotalDistance();
        }

        public double ExpEvaluateFitness(Chromosome chromosome)
        {
            double distance = chromosome.GetTotalDistance();
            return Math.Exp(-1e-3*distance);
        }
    }
}