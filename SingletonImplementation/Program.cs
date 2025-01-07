using System.ComponentModel;
using MoreLinq;
using MoreLinq.Experimental;
using File = System.IO.File;

namespace SingletonImplementation
{
    internal class Program
    {
        public interface IDatabase
        {
            int GetPopulation(string name);
        }
        public class SingletonDatabase : IDatabase
        {
            private Dictionary<string, int> capitals;

            private SingletonDatabase()
            {
                Console.WriteLine("Initializing database!");
                capitals = File.ReadAllLines("capitals.txt").Batch(2).ToDictionary(
                    list => list.ElementAt(0).Trim(),
                    list => int.Parse(list.ElementAt(1))
                    );
            }

            public int GetPopulation(string name)
            {
                return this.capitals[name];
            }

            private static Lazy<SingletonDatabase>
                instance = new Lazy<SingletonDatabase>(() => new SingletonDatabase());

            public static SingletonDatabase Instance => instance.Value;
        }
        static void Main(string[] args)
        {
            var db = SingletonDatabase.Instance;
            var city = "Tokyo";
            Console.WriteLine($"{city} ha population {db.GetPopulation(city)}");

        }
    }
}
