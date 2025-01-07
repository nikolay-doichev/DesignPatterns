using Autofac;
using TestabilityIssues;
namespace SingletonTests
{
    [TestFixture]
    public class SingletonTests
    {
        [Test]
        public async Task IsSingletonTest()
        {
            var db = Program.SingletonDatabase.Instance;
            var db2 = Program.SingletonDatabase.Instance;

            Assert.That(db, Is.SameAs(db2));
            Assert.That(Program.SingletonDatabase.Count, Is.EqualTo(1));

        }

        [Test]
        public async Task SingletonTotalPopulationTest()
        {
            var rf = new Program.SingletonRecordFinder();
            var names = new[] { "Seoul", "Mexico City" };

            int tp = rf.GetTotalPopulation(names);

            Assert.That(tp, Is.EqualTo(17500000 + 17400000));
        }

        [Test]
        public async Task ConfigurablePopulationTest()
        {
            var rf = new SingletonDependencyInjection.Program.ConfigurableRecordFinder(new SingletonDependencyInjection.Program.DummyDatabase());
            var names = new[] { "alpha", "gamma" };
            int tp = rf.GetTotalPopulation(names);

            Assert.That(tp, Is.EqualTo(4));
        }

        [Test]
        public async Task DIPopulationTest()
        {
            var cb = new ContainerBuilder();
            cb.RegisterType<SingletonDependencyInjection.Program.OrdinaryDatabase>()
              .As<SingletonDependencyInjection.Program.IDatabase>().SingleInstance();
            cb.RegisterType<SingletonDependencyInjection.Program.ConfigurableRecordFinder>();

            using (var c = cb.Build())
            {
                var rf = c.Resolve<SingletonDependencyInjection.Program.ConfigurableRecordFinder>();
            }
        }
    }
}