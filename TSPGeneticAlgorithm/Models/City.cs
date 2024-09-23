namespace TSPGeneticAlgorithm.Models
{
    public class City
    {
        public int ID { get; }
        public double X  { get; }
        public double Y { get; }
        public string Name { get; }

        public City(int id, double x, double y)
        {
            ID = id;
            X = x;
            Y = y;
            Name = $"City_{ID+1}";
        }

        public City(int id, string name, double x, double y)
        {
            ID = id;
            X = x;
            Y = y;
            Name = name;
        }

        public double DistanceTo(City other)
        {
            double D_x =  X - other.X,
                   D_y  = Y - other.Y;
            return Math.Sqrt(D_x* D_x + D_y * D_y);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}

