using TSPGeneticAlgorithm.Interfaces;
using TSPGeneticAlgorithm.Models;

namespace TSPGeneticAlgorithm.Crossovers
{
    public class OrderCrossover: ICrossoverOperator
    {
        private Random random  = new Random();
        public Chromosome Crossover(Chromosome parent1, Chromosome parent2)
        {
            int size = parent1.Cities.Count;
            int start = random.Next(size);
            int end = random.Next(start, size);

            var childCities = new List<City>(new City[size]);

            for (int i = start; i <= end; i++)
            {
                childCities[i] = parent1.Cities[i];
            }

            int currentIndex = (end + 1) % size;
            
            foreach (var city in parent2.Cities.Concat(parent2.Cities))
            {
                if (!childCities.Contains(city))
                {
                    childCities[currentIndex] = city;
                    currentIndex = (currentIndex + 1) % size;
                    if (currentIndex == start)
                        break;
                }
            }
            
            return new Chromosome(childCities);
        }
    }
}