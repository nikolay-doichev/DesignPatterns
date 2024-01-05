namespace ExerciseFactory
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }

        internal Person(int id, string name)
        {
            Id = id;
            Name = name;
        }        

        public override string ToString()
        {
            return $"Id:{Id}=> Name:{Name}";
        }
    }

    public class PersonFactory
    {
        private int index = 0;
        public Person CreatePerson(string name)
        {
            return new Person(index++, name);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var personFactory = new PersonFactory();
            var person = personFactory.CreatePerson("Niko");
            var person1 = personFactory.CreatePerson("Poli");
            var person3 = personFactory.CreatePerson("Ivo");

            Console.WriteLine(person);
            Console.WriteLine(person1);
            Console.WriteLine(person3);
        }
    }
}