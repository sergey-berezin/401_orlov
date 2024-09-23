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

        static private bool isRunningAlg = true;
        static void Main(string[] args)
        {
            bool isRunning = true;
            Console.CancelKeyPress += new ConsoleCancelEventHandler(CancelHandler);

            List<City>? cities = null;

            while (isRunning)
            {
                Console.Clear();
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1.Сгенерировать случайные города");
                Console.WriteLine("2.Загрузить города из файла CSV");
                Console.WriteLine("3.Выход");

                Console.Write("> ");
                string choice = Console.ReadLine();

                switch(choice)
                {
                    case "1":
                        Console.Write("Введите количество городов: ");
                        if (int.TryParse(Console.ReadLine(), out int num))
                        {
                            cities = GenerateCities(num);
                            Console.WriteLine($"Сгенерировано {num} городов");
                        }
                        else
                        {
                            Console.WriteLine("Некорректное число");
                        }
                        break;
                    case "2":
                        cities = LoadCitiesFromCSV();
                        break;
                    case "3":
                        isRunning = false;
                        continue;
                    default:
                        Console.WriteLine("Некорректный запрос");
                        break;
                }

                if (cities != null && cities.Count > 0)
                    RunAlgorithm(cities);

                Console.WriteLine("Нажмите любую клавишу");
                Console.ReadKey();
            }
        }

        static void RunAlgorithm(List<City> cities)
        {

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

            while(isRunningAlg)
            {
                bool continueEvolving = geneticAlgorithm.Evolve();

                var bestChromosome = GetBestChromosome(geneticAlgorithm.Population);
                if (geneticAlgorithm.Generation % logInterval == 0)
                    Console.WriteLine($"Поколение: {geneticAlgorithm.Generation}, Лучшая дистанция: {bestChromosome.GetTotalDistance():f2}");

                if (Console.KeyAvailable)
                {
                    Console.ReadKey(true);
                    isRunningAlg = false;
                }

                if (autoStop && !continueEvolving)
                {
                    Console.WriteLine($"Алгоритм остановлен из-за станганции на поколении: {geneticAlgorithm.Generation}");
                    break;
                }
            }

            var finalBest = GetBestChromosome(geneticAlgorithm.Population);
            PrintAdjacencyMatrix(cities);
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
            isRunningAlg = false;
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

        static List<City> LoadCitiesFromCSV()
        {
            Console.Write("Введите путь CSV файла с городами: ");
            string? filePath = Console.ReadLine();
            var cities = new List<City>();

            if (!File.Exists(filePath))
            {
                Console.WriteLine("Файл не найден!");
                return cities;
            }

            try
            {
                int id = 0;
                foreach(var line in File.ReadAllLines(filePath))
                {
                    var parts = line.Split(',');

                    if (parts.Length < 3)
                    {
                        Console.WriteLine($"Ошибка в строке: {line}");
                        continue;
                    }

                    string name = parts[0].Trim();
                    if (double.TryParse(parts[1], out double x) &&
                        double.TryParse(parts[2], out double y))
                    {
                        cities.Add(new City(id, name, x, y));
                    }
                    else Console.WriteLine($"Ошибка в строке: {line}");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Ошибка при чтении файла: {ex.Message}");
            }
            Console.WriteLine($"Загружено городов: {cities.Count}");
            return cities;
        }

        static void PrintAdjacencyMatrix(List<City> cities)
        {
            int n = cities.Count;
            Console.WriteLine("Матрица смежности: ");

            Console.Write("     ");
            foreach (var city in cities)
            {
                Console.Write($"{city.Name, -6} ");
            }
            Console.WriteLine();

            for (int i = 0; i < n; i++)
            {
                Console.Write($"{cities[i].Name, -6} ");
                for (int j = 0; j < n; j++)
                {
                    Console.Write($"{cities[i].DistanceTo(cities[j]):f2} ");
                }
                Console.WriteLine();
            }
        }
    }
}