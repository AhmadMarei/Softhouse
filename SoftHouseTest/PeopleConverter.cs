
using System.Xml.Linq;

namespace SoftHouseTest
{
    public class PeopleConverter
    {
        private readonly List<Person> _people = new();
        private Person? _currentPerson;
        private Family? _currentFamily;

        public XElement ConvertToXml(string[] inputLines)
        {
            foreach (var line in inputLines)
            {
                ParseLine(line);
            }

            AddCurrentPersonIfExists();
            return new XElement("people", _people.Select(p => p.ToXml()));
        }

        private void ParseLine(string line)
        {
            var parts = line.Split('|');
            if (parts.Length < 2) return;

            var prefix = parts[0];

            switch (prefix)
            {
                case "P":
                    StartNewPerson(parts);
                    break;
                case "F":
                    StartNewFamily(parts);
                    break;
                case "T":
                    AddPhone(parts);
                    break;
                case "A":
                    AddAddress(parts);
                    break;
                default:
                    throw new InvalidOperationException($"Unknown line prefix: {prefix}");
            }
        }

        private void StartNewPerson(string[] parts)
        {
            AddCurrentPersonIfExists();
            _currentPerson = new Person(parts[1], parts[2]);
            _currentFamily = null;
        }

        private void StartNewFamily(string[] parts)
        {
            _currentFamily = new Family(parts[1], parts[2]);
            _currentPerson?.Families.Add(_currentFamily);
        }

        private void AddPhone(string[] parts)
        {
            Phone phone = new Phone(parts[1], parts.ElementAtOrDefault(2));
            if (_currentFamily != null)
                _currentFamily.Phones.Add(phone);
            else
                _currentPerson?.Phones.Add(phone);
        }

        private void AddAddress(string[] parts)
        {
            var address = new Address(
                parts[1],
                parts.ElementAtOrDefault(2) ?? string.Empty,
                parts.ElementAtOrDefault(3) ?? string.Empty
            );

            if (_currentFamily != null)
                _currentFamily.Addresses.Add(address);
            else
                _currentPerson?.Addresses.Add(address);
        }

        private void AddCurrentPersonIfExists()
        {
            if (_currentPerson != null)
                _people.Add(_currentPerson);
        }
    }
}
