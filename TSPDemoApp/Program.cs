using TSPGeneticAlgorithm;
using TSPGeneticAlgorithm.Crossovers;
using TSPGeneticAlgorithm.Evaluators;
using TSPGeneticAlgorithm.Models;
using TSPGeneticAlgorithm.Mutations;
using TSPGeneticAlgorithm.Selections;

namespace TSPDemoApp
{
    class Program
    {
        private static bool isRunning = true;
        
        static void Main(string[] args)
        {
            Console.CancelKeyPress += new ConsoleCancelEventHandler(CancelHandler);

            var cities = GenerateCities(20);

            var initialPopulation = GenerateInitialPopulation(cities, 5);

            var autoStop = false;

            var geneticAlgorithm = new GeneticAlgorithm(
                initialPopulation,
                new DistanceFitnessEvaluator(),
                new RouletteWheelSelections(),
                new OrderCrossover(),
                new SwapMutation(mutationRate: 0.05),
                maxStagnationCount: 100,
                improvementThreshold: 1e-4
            );

            int logInterval = 10;

            while(isRunning)
            {
                bool continueEvolving = geneticAlgorithm.Evolve();

                var bestChromosome = GetBestChromosome(geneticAlgorithm.Population);
                if (geneticAlgorithm.Generation % logInterval == 0)
                    Console.WriteLine($"Поколение: {geneticAlgorithm.Generation}, Лучшая дистанция: {bestChromosome.GetTotalDistance():f2}");

                if (Console.KeyAvailable)
                {
                    Console.ReadKey(true);
                    isRunning = false;
                }

                if (autoStop && !continueEvolving)
                {
                    Console.WriteLine($"Алгоритм остановлен из-за станганции на поколении: {geneticAlgorithm.Generation}");
                    break;
                }
            }

            var finalBest = GetBestChromosome(geneticAlgorithm.Population);
            Console.WriteLine("Найден лучший маршрут:");
            foreach(var city in finalBest.Cities)
            {
                Console.Write($"{city.ID + 1} => ");
            }
            Console.WriteLine(finalBest.Cities[0].ID + 1);
            Console.WriteLine($"Общая дистанция: {finalBest.GetTotalDistance():f2}");
        }

        static void CancelHandler(object sender, ConsoleCancelEventArgs args)
        {
            args.Cancel = true;
            isRunning = false;
        }

        static List<City> GenerateCities(int num)
        {
            var cities = new List<City>();
            var random = new Random();

            for(int i = 0; i < num; i++)
            {
                var city = new City(i, random.NextDouble() * 100, random.NextDouble() * 100);
                cities.Add(city);
            }

            return cities;
        }

        static List<Chromosome> GenerateInitialPopulation(List<City> cities, int populationSize)
        {
            var population = new List<Chromosome>();
            var random = new Random();
            
            for (int i = 0; i < populationSize; i++)
            {
                var shuffledCities = new List<City>(cities);
                shuffledCities = shuffledCities.OrderBy(c => random.Next()).ToList();

                var chromosome = new Chromosome(shuffledCities);
                population.Add(chromosome);
            }

            return population;
        }

        static Chromosome GetBestChromosome(List<Chromosome> population)
        {
            return population.OrderBy(c => c.GetTotalDistance()).First();
        }
    }
}