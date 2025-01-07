using MoreLinq;

namespace SingletonDependencyInjection
{
    public class Program
    {
        public interface IDatabase
        {
            int GetPopulation(string name);
        }
        public class SingletonDatabase : IDatabase
        {
            private Dictionary<string, int> capitals;
            private static int instanceCount;
            public static int Count => instanceCount;

            private SingletonDatabase()
            {
                instanceCount++;
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

        public class SingletonRecordFinder
        {
            public int GetTotalPopulation(IEnumerable<string> names)
            {
                int result = 0;
                foreach (var name in names)
                {
                    result += SingletonDatabase.Instance.GetPopulation(name);
                }

                return result;
            }
        }

        public class ConfigurableRecordFinder
        {
            private IDatabase database;

            public ConfigurableRecordFinder(IDatabase database)
            {
                this.database = database ?? throw new ArgumentNullException(nameof(database));
            }

            public int GetTotalPopulation(IEnumerable<string> names)
            {
                int result = 0;
                foreach (var name in names)
                {
                    result += database.GetPopulation(name);
                }

                return result;
            }
        }

        public class OrdinaryDatabase : IDatabase
        {
            private Dictionary<string, int> capitals;
            
            private OrdinaryDatabase()
            {
                Console.WriteLine("Initializing database!");
                capitals = File.ReadAllLines("capitals.txt").Batch(2).ToDictionary(
                    list => list.ElementAt(0).Trim(),
                    list => int.Parse(list.ElementAt(1))
                );
            }

            public int GetPopulation(string name)
            {
                return capitals[name];
            }
        }

        public class DummyDatabase : IDatabase
        {
            public int GetPopulation(string name)
            {
                return new Dictionary<string, int>
                {
                    ["alpha"] = 1,
                    ["beta"] = 2,
                    ["gamma"] = 3
                }[name];
            }
        }

        static void Main(string[] args)
        {
            var db = SingletonDatabase.Instance;
            var city = "Tokyo";
            Console.WriteLine($"{city} ha population {db.GetPopulation(city)}");

        }
    }
}
