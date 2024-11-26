using System.Collections.Concurrent;
using System.Text.Json;
using TSPGeneticAlgorithm;
using TSPGeneticAlgorithm.Crossovers;
using TSPGeneticAlgorithm.Evaluators;
using TSPGeneticAlgorithm.Mutations;
using TSPGeneticAlgorithm.Selections;
using static System.Windows.Forms.AxHost;

namespace Task2.SaverUtils
{
    public class ExperimentStorageService
    {
        private readonly string _baseDir;
        private readonly string _runsFile;
        private readonly object _lock = new object();

        public ExperimentStorageService(string baseDir = "Experiments")
        {
            _baseDir = baseDir;
            _runsFile = Path.Combine(_baseDir, "runs.json");

            Directory.CreateDirectory(_baseDir);
            if (!File.Exists(_runsFile))
                File.WriteAllText(_runsFile, "[]");
        }

        public async Task SaveExperiment(string name, 
                                         GeneticAlgorithm algorithm, 
                                         ExperimentParameters parameters, 
                                         ConcurrentQueue<double> fitnessHistory)
        {
            lock (_lock)
            {
                var experiment = new ExperimentMetadata
                {
                    Name = name,
                    PopulationFileName = $"{Guid.NewGuid()}.json",
                    CratedAt = DateTime.Now,
                    Parameters = parameters,
                };

                var state = new ExperimentState
                {
                    CurrentGeneration = algorithm.Generation,
                    BestFitness = algorithm.BestChromosome?.Fitness ?? 0,
                    BestDistance = algorithm.BestChromosome?.GetTotalDistance() ?? 0,
                    FitnessHistory = fitnessHistory.ToList(),
                    Population = algorithm.Population,
                    BestChromosome = algorithm.BestChromosome
                };

                var populationPath = Path.Combine(_baseDir, experiment.PopulationFileName);
                var tempPopulationPath = populationPath + ".tmp";

                var json = JsonSerializer.Serialize(state, new JsonSerializerOptions
                {
                    WriteIndented = true,
                });

                File.WriteAllText(tempPopulationPath, json);
                if (File.Exists(populationPath))
                    File.Delete(populationPath);
                File.Move(tempPopulationPath, populationPath);

                var experiments = LoadExperimentsList();
                experiments.RemoveAll(e => e.Name == name);
                experiments.Add(experiment);

                var tempRunsFile = _runsFile + ".tmp";
                File.WriteAllText(tempRunsFile, 
                    JsonSerializer.Serialize(experiments, 
                        new JsonSerializerOptions
                        {
                            WriteIndented = true
                        }));

                File.Delete(_runsFile);
                File.Move(tempRunsFile, _runsFile);
            }
        }

        public async Task<(
            GeneticAlgorithm Algorithm, 
            ExperimentParameters Parameters, 
            List<double> FitnessHistory)> 
            LoadExperiment(string name)
        {
            ExperimentMetadata metadata;
            ExperimentState state;

            lock (_lock)
            {
                var experiments = LoadExperimentsList();
                metadata = experiments.FirstOrDefault(e => e.Name == name)
                    ?? throw new Exception($"Эксперимент '{name}' не найден");

                var populationPath = Path.Combine(_baseDir, metadata.PopulationFileName);
                var json = File.ReadAllText(populationPath);
                state = JsonSerializer.Deserialize<ExperimentState>(json);
            }

            var algorithm = new GeneticAlgorithm(
                state.Population,
                new DistanceFitnessEvaluator(),
                new RouletteWheelSelections(),
                new OrderCrossover(),
                new SwapMutation(metadata.Parameters.MutationRate),
                metadata.Parameters.MaxStagnationCount,
                metadata.Parameters.ImprovementThreshold,
                state.BestChromosome,
                state.CurrentGeneration
            );

            return (algorithm, metadata.Parameters, state.FitnessHistory);
        }

        public List<string> GetExperimentNames()
        {
            lock ( _lock)
            {
                return LoadExperimentsList().Select(e => e.Name).ToList();
            }
        }

        private List<ExperimentMetadata> LoadExperimentsList()
        {
            var json = File.ReadAllText(_runsFile);
            return JsonSerializer.Deserialize<List<ExperimentMetadata>>(json) ?? new List<ExperimentMetadata>();
        }
    }
}
