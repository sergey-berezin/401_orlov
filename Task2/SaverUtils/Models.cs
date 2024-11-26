using System.Text.Json.Serialization;
using TSPGeneticAlgorithm.Models;

namespace Task2.SaverUtils
{
    public class ExperimentMetadata
    {
        public string Name { get; set; }
        public string PopulationFileName { get; set; }
        public DateTime CratedAt { get; set; }
        public ExperimentParameters Parameters { get; set; }

        [JsonIgnore]
        public ExperimentState State { get; set; }
    }

    public class ExperimentParameters
    {
        public int PopulationSize { get; set; }
        public double MutationRate { get; set; }
        public int MaxGenerations { get; set; }
        public int MaxStagnationCount {  get; set; }
        public double ImprovementThreshold { get; set; }
        public List<City> Cities { get; set; }
    }

    public class ExperimentState
    {
        public int CurrentGeneration { get; set; }
        public double BestFitness { get; set; }
        public double BestDistance { get; set; }
        public List<double> FitnessHistory { get; set; }
        public List<Chromosome> Population {  get; set; }
        public Chromosome BestChromosome { get; set; }
    }
}
