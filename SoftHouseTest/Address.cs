
using System.Xml.Linq;

namespace SoftHouseTest
{
    public class Address
    {
        public string Street { get; }
        public string City { get; }
        public string PostalCode { get; }

        public Address(string street, string city, string postalCode)
        {
            Street = street;
            City = city;
            PostalCode = postalCode;
        }

        public XElement ToXml()
        {
            var addressElement = new XElement("address",
                new XElement("street", Street),
                new XElement("city", City)
            );

            AddElementIfNotEmpty(addressElement, "postalcode", PostalCode);
            return addressElement;
        }

        private void AddElementIfNotEmpty(XElement parent, string elementName, string value)
        {
            if (!string.IsNullOrEmpty(value))
                parent.Add(new XElement(elementName, value));
        }
    }
}
