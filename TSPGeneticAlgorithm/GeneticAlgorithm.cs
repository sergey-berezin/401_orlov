using TSPGeneticAlgorithm.Interfaces;
using TSPGeneticAlgorithm.Models;

namespace TSPGeneticAlgorithm;

public class GeneticAlgorithm
{
    public List<Chromosome> Population { get; private set; }
    public IFitnessEvaluator FitnessEvaluator{ get; set; }
    public ISelectionOperator SelectionOperator{ get; set; }
    public ICrossoverOperator CrossoverOperator{ get; set; }
    public IMutationOperator MutationOperator{ get; set; }
    public int Generation { get; private set; }
    public Chromosome? BestChromosome { get; private set; }
    
    private int _stagnationCount;
    private int _maxStagnationCount;
    private double _improvementThreshold;

    public GeneticAlgorithm(
        List<Chromosome> initialPopulation,
        IFitnessEvaluator fitnessEvaluator,
        ISelectionOperator selectionOperator,
        ICrossoverOperator crossoverOperator,
        IMutationOperator mutationOperator,
        int maxStagnationCount,
        double improvementThreshold)
    {
        Population = initialPopulation;
        FitnessEvaluator = fitnessEvaluator;
        SelectionOperator = selectionOperator;
        CrossoverOperator = crossoverOperator;
        MutationOperator = mutationOperator;
        Generation = 0;
        BestChromosome = null;
        
        _stagnationCount = 0;
        _maxStagnationCount = maxStagnationCount;
        _improvementThreshold = improvementThreshold;
    }

    public bool Evolve()
    {
        foreach(var chromosome in Population )
            chromosome.Fitness = FitnessEvaluator.EvaluateFitness(chromosome);

        var currentBest = Population.OrderByDescending(c => c.Fitness).First();

        if (BestChromosome == null || currentBest.Fitness > BestChromosome.Fitness * (1 + _improvementThreshold))
        {
            BestChromosome = new Chromosome(currentBest.Cities);
            BestChromosome.Fitness = currentBest.Fitness;
            _stagnationCount = 0;
        }
        else
            _stagnationCount++;

        Population = Population.OrderByDescending(c => c.Fitness).ToList();

        var newPopulation = new List<Chromosome>();

        newPopulation.Add(BestChromosome);
        int elitismCount = (int)(Population.Count * 0.05) - 1;
        newPopulation.AddRange(Population.Take(elitismCount));

        while(newPopulation.Count < Population.Count)
        {
            var parent1 = SelectionOperator.SelectParent(Population);
            var parent2 = SelectionOperator.SelectParent(Population);

            var child = CrossoverOperator.Crossover(parent1, parent2);

            MutationOperator.Mutate(child);

            newPopulation.Add(child);
        }

        Population = newPopulation;
        Generation++;

        return _stagnationCount < _maxStagnationCount;
    }

}
