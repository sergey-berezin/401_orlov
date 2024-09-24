namespace TSPGeneticAlgorithm.Models
{
    public class Chromosome
    {
        public List<City> Cities { get; }

        public double Fitness { get; set; }
        public Chromosome(List<City> cities)
        {
            Cities = new List<City>(cities);
        }

        public double GetTotalDistance()
        {
            double distance = 0;
            for (int i = 0; i < Cities.Count - 1; i++)
            {
                distance += Cities[i].DistanceTo(Cities[i+1]);
            }
            distance += Cities[^1].DistanceTo(Cities[0]);
            return distance;
        }

    }
}