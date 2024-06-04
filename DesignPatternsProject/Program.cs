namespace DesignPatternsProject;

public interface IPrototype<T>
{
    T DeepCopy();
}
public class Person : IPrototype<Person>
{
    public string[] Names;
    public Address Address;

    public Person(string[] names, Address address)
    {
        if (names == null)
        {
            throw new ArgumentNullException(nameof(names));
        }

        if (address == null)
        {
            throw new ArgumentNullException(nameof(address));
        }
        Names = names;
        Address = address;
    }

    public Person(Person other)
    {
        Names = other.Names;
        Address = new Address(other.Address);
    }

    public Person DeepCopy()
    {
        return new Person(Names, Address.DeepCopy());
    }

    public override string ToString()
    {
        return $"{nameof(Names)}: {string.Join(" ", Names)}, {nameof(Address)}: {Address}";
    }
}

public class Address : IPrototype<Address>
{
    public string StreetName;
    public int HouseNumber;

    public Address(string streetName, int houseNumber)
    {
        StreetName = streetName;
        HouseNumber = houseNumber;
    }

    public Address(Address other)
    {
        StreetName = other.StreetName;
        HouseNumber = other.HouseNumber;
    }

    public Address DeepCopy()
    {
        return new Address(StreetName, HouseNumber);
    }

    public override string ToString()
    {
        return $"{nameof(StreetName)}: {StreetName}, {nameof(HouseNumber)}: {HouseNumber}";
    }
}

public class Program
{
    static void Main(string[] args)
    {
        var john = new Person(new[] { "John", "Smith" }, new Address("Long Road", 123));

        var jane = john.DeepCopy();
        jane.Address.HouseNumber = 312;

        Console.WriteLine(john);
        Console.WriteLine(jane);
    }
}