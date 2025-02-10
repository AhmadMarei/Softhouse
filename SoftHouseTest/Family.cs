
using System.Xml.Linq;

namespace SoftHouseTest
{
    public class Family
    {
        public string Name { get; }
        public string BirthYear { get; }
        public List<Address> Addresses { get; } = new();
        public List<Phone> Phones { get; } = new();

        public Family(string name, string birthYear)
        {
            Name = name;
            BirthYear = birthYear;
        }

        public XElement ToXml()
        {
            return new XElement("family",
                new XElement("name", Name),
                new XElement("born", BirthYear),
                Addresses.Select(a => a.ToXml()),
                Phones.Select(p => p.ToXml())
            );
        }
    }
}
