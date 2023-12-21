namespace BuilderIneritance
{
    public class Person
    {
        public string Name;
        public string Possition;

        public class Builder : PersonJobBuilder<Builder>
        {

        }

        public static Builder New => new Builder();
        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Possition)}: {Possition}";
        }
    }

    public abstract class PersonBuilder
    {
        protected Person person = new Person();

        public Person Build()
        {
            return person;
        }
    }

    public class PersonInfoBuilder<SELF> : PersonBuilder
    where SELF : PersonInfoBuilder<SELF> 
    {
        

        public SELF Called(string name)
        {
            person.Name = name;
            return (SELF)this;
        }
    }

    public class PersonJobBuilder<SELF> : PersonInfoBuilder<PersonJobBuilder<SELF>>
    where SELF : PersonJobBuilder<SELF>
    {
        public SELF WorkAsA(string possition)
        {
            person.Possition = possition;
            return (SELF)this;
        }
    }
    public class Program
    {
        static void Main(string[] args)
        {
            var me = Person.New
                .Called("Nikolay")
                .WorkAsA("quant")
                .Build();

            Console.WriteLine(me);
        }
    }
}