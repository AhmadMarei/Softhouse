using SoftHouseTest;
using System.Xml.Linq;

namespace SoftHouseTestXunitTest
{
    public class PeopleConverterXunitTests
    {
        [Fact]
        public void ConvertToXml_ShouldReturnExpectedXmlStructure()
        {
            //This test checks that the XML output contains two <person> elements and verifies details for each person—including phone, address, and family information.

            var inputLines = new[]
            {
                "P|Carl Gustaf|Bernadotte",
                "T|0768-101801|08-101801",
                "A|Drottningholms slott|Stockholm|10001",
                "F|Victoria|1977",
                "A|Haga Slott|Stockholm|10002",
                "F|Carl Philip|1979",
                "T|0768-101802|08-101802",
                "P|Barack|Obama",
                "A|1600 Pennsylvania Avenue|Washington, D.C"
            };

            var converter = new PeopleConverter();


            XElement xmlOutput = converter.ConvertToXml(inputLines);

            // Assert
            var persons = xmlOutput.Elements("person").ToList();
            Assert.Equal(2, persons.Count);

            // First person: Carl Gustaf Bernadotte.
            var person1 = persons[0];
            Assert.Equal("Carl Gustaf", person1.Element("firstname")?.Value);
            Assert.Equal("Bernadotte", person1.Element("lastname")?.Value);

            var phone1 = person1.Elements("phone").FirstOrDefault();
            Assert.NotNull(phone1);
            Assert.Equal("0768-101801", phone1.Element("mobile")?.Value);
            Assert.Equal("08-101801", phone1.Element("landline")?.Value);

            var addr1 = person1.Elements("address").FirstOrDefault();
            Assert.NotNull(addr1);
            Assert.Equal("Drottningholms slott", addr1.Element("street")?.Value);
            Assert.Equal("Stockholm", addr1.Element("city")?.Value);
            Assert.Equal("10001", addr1.Element("postalcode")?.Value);

            var families = person1.Elements("family").ToList();
            Assert.Equal(2, families.Count);

            var family1 = families[0];
            Assert.Equal("Victoria", family1.Element("name")?.Value);
            Assert.Equal("1977", family1.Element("born")?.Value);
            var fam1Addr = family1.Elements("address").FirstOrDefault();
            Assert.NotNull(fam1Addr);
            Assert.Equal("Haga Slott", fam1Addr.Element("street")?.Value);
            Assert.Equal("Stockholm", fam1Addr.Element("city")?.Value);
            Assert.Equal("10002", fam1Addr.Element("postalcode")?.Value);

            var family2 = families[1];
            Assert.Equal("Carl Philip", family2.Element("name")?.Value);
            Assert.Equal("1979", family2.Element("born")?.Value);
            var fam2Phone = family2.Elements("phone").FirstOrDefault();
            Assert.NotNull(fam2Phone);
            Assert.Equal("0768-101802", fam2Phone.Element("mobile")?.Value);
            Assert.Equal("08-101802", fam2Phone.Element("landline")?.Value);

            // Second person: Barack Obama.
            var person2 = persons[1];
            Assert.Equal("Barack", person2.Element("firstname")?.Value);
            Assert.Equal("Obama", person2.Element("lastname")?.Value);
            var addr2 = person2.Elements("address").FirstOrDefault();
            Assert.NotNull(addr2);
            Assert.Equal("1600 Pennsylvania Avenue", addr2.Element("street")?.Value);
            Assert.Equal("Washington, D.C", addr2.Element("city")?.Value);
            Assert.Empty(person2.Elements("phone"));
            Assert.Empty(person2.Elements("family"));
        }

        [Fact]
        public void ConvertToXml_EmptyInput_ReturnsEmptyPeopleElement()
        {
            //This test confirms that when no input lines are provided, the converter returns a <people> element with no children.
            var inputLines = new string[] { };
            var converter = new PeopleConverter();

            XElement xmlOutput = converter.ConvertToXml(inputLines);

            Assert.NotNull(xmlOutput);
            Assert.Equal("people", xmlOutput.Name.LocalName);
            Assert.Empty(xmlOutput.Elements());
        }

        [Fact]
        public void ConvertToXml_SinglePersonOnly_ReturnsCorrectPerson()
        {
            //This test validates that when only a single person record is provided, the XML output correctly reflects that person with no child elements.
            var inputLines = new[] { "P|Alice|Wonderland" };
            var converter = new PeopleConverter();


            XElement xmlOutput = converter.ConvertToXml(inputLines);


            var person = xmlOutput.Element("person");
            Assert.NotNull(person);
            Assert.Equal("Alice", person.Element("firstname")?.Value);
            Assert.Equal("Wonderland", person.Element("lastname")?.Value);
            Assert.Empty(person.Elements("address"));
            Assert.Empty(person.Elements("phone"));
            Assert.Empty(person.Elements("family"));
        }
    }
}

