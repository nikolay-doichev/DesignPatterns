using System.Security.Cryptography.X509Certificates;

namespace DependencyInversionPrinciple
{
    public class Program
    {
        public enum Relationship
        {
            Parent, Child, Sibling
        }
        public class  Person
        {
            public string Name;
            //public DateTime DateOfBirth;

        }

        public interface IReletionshipBrowser
        {
            IEnumerable<Person> FindAllChildrenOf(string name);
        }
        
        //low-level
        public class Relationships : IReletionshipBrowser
        {
            private List<(Person, Relationship, Person)> relations = new List<(Person, Relationship, Person)>();

            public void AddParentAndChild(Person parent, Person child)
            {
                relations.Add((parent, Relationship.Parent, child));
                relations.Add((child, Relationship.Child, parent));
            }

            //public List<(Person, Relationship, Person)> Relations => relations;
            public IEnumerable<Person> FindAllChildrenOf(string name)
            {
                return relations.Where(x =>
                    x.Item1.Name == name &&
                    x.Item2 == Relationship.Parent).Select(valueTuple => valueTuple.Item3);
            }
        }

        public class Research
        {
            //public Research(Relationships relationships)
            //{
            //    var relations = relationships.Relations;
            //    foreach (var valueTuple in relations.Where(x => 
            //                 x.Item1.Name == "John" && 
            //                 x.Item2 == Relationship.Parent))
            //    {
            //        Console.WriteLine($"John has a child called {valueTuple.Item3.Name}");
            //    }
            //}

            public Research(IReletionshipBrowser browser)
            {
                foreach (var person in browser.FindAllChildrenOf(("John")))
                {
                    Console.WriteLine($"John has a child called {person.Name}");
                }
            }
            static void Main(string[] args)
            {
                var parent = new Person {Name = "John"};
                var child = new Person { Name = "Chris" };
                var child2 = new Person { Name = "Mary" };

                var relationships = new Relationships();
                relationships.AddParentAndChild(parent, child);
                relationships.AddParentAndChild(parent, child2);

                new Research(relationships);
            }
        }
    }
}