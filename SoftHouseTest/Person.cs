
using System.Xml.Linq;

namespace SoftHouseTest
{
    public class Person
    {
        public string FirstName { get; }
        public string LastName { get; }
        public List<Phone> Phones { get; } = new();
        public List<Address> Addresses { get; } = new();
        public List<Family> Families { get; } = new();

        public Person(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public XElement ToXml()
        {
            return new XElement("person",
                new XElement("firstname", FirstName),
                new XElement("lastname", LastName),
                Addresses.Select(a => a.ToXml()),
                Phones.Select(p => p.ToXml()),
                Families.Select(f => f.ToXml())
            );
        }
    }
}
