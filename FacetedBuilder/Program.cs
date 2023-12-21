namespace FacetedBuilder
{
    public class Person
    {
        //address
        public string StreetAdress, PostCode, City;

        //employment
        public string CompanyName, Position;
        public int AnnualIncome;

        public override string ToString()
        {
            return $"{nameof(StreetAdress)}: {StreetAdress}, {nameof(PostCode)}: {PostCode}, {nameof(City)}: {City}, {nameof(CompanyName)}: {CompanyName}, {nameof(Position)}: {Position}, {nameof(AnnualIncome)}: {AnnualIncome}";
        }
    }

    public class PersonBuilder //facade
    {
        // reference!
        protected Person person = new Person();

        public PersonJobBuilder Works => new PersonJobBuilder(person);
        public PersonAddressBuilder Lives => new PersonAddressBuilder(person);

        public static implicit operator Person(PersonBuilder pb)
        {
            return pb.person;
        }
    }

    public class PersonAddressBuilder : PersonBuilder
    {
        public PersonAddressBuilder(Person person)
        {
            this.person = person;
        }

        public PersonAddressBuilder At(string streetAddress)
        {
            this.person.StreetAdress = streetAddress;
            return this;
        }

        public PersonAddressBuilder WithPostcode(string postcode)
        {
            this.person.PostCode = postcode;
            return this;
        }

        public PersonAddressBuilder In(string City)
        {
            this.person.City = City;
            return this;
        }
}

    public class PersonJobBuilder : PersonBuilder
    {
        public PersonJobBuilder(Person person)
        {
            this.person = person;
        }

        public PersonJobBuilder At(string companyName)
        {
            person.CompanyName = companyName;
            return this;
        }

        public PersonJobBuilder As(string position)
        {
            person.Position = position;
            return this;
        }

        public PersonJobBuilder Earning(int amount)
        {
            person.AnnualIncome = amount;
            return this;
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            var pb = new PersonBuilder();
            Person person = pb
                .Lives.At("123 London Road")
                      .In("London")
                      .WithPostcode("SW12AC")
                .Works.At("Fabrikam")
                      .As("Engineer")
                      .Earning(123000);

            Console.WriteLine(person);
        }
    }
}