using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace CopyThroughSerialization
{
    //public interface IPrototype<T>
    //{
    //    T DeepCopy();
    //}
    public static class ExtensionMethods
    {
        public static T DeepCopy<T>(this T self)
        {
            var stream = new MemoryStream();
            var formater = new BinaryFormatter();
            formater.Serialize(stream, self);
            stream.Seek(0, SeekOrigin.Begin);
            object copy = formater.Deserialize(stream);
            stream.Close();
            return (T)copy;
        }

        public static T DeepCopyXML<T>(this T self)
        {
            using (var ms = new MemoryStream())
            {
                var s = new XmlSerializer(typeof(T));
                s.Serialize(ms, self);
                ms.Position = 0;
                return (T)s.Deserialize(ms);

            }
        }
    }
    public class Person
    {
        public string[] Names;
        public Address Address;

        public Person()
        {
            
        }
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

        public override string ToString()
        {
            return $"{nameof(Names)}: {string.Join(" ", Names)}, {nameof(Address)}: {Address}";
        }
    }

    public class Address
    {
        public string StreetName;
        public int HouseNumber;

        public Address()
        {
            
        }
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

            var jane = john.DeepCopyXML();
            jane.Names[0] = "Jane";
            jane.Address.HouseNumber = 312;

            Console.WriteLine(john);
            Console.WriteLine(jane);
        }
    }
}
