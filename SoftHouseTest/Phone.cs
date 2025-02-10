
using System.Xml.Linq;

namespace SoftHouseTest
{
    public class Phone
    {
        public string Mobile { get; }
        public string Landline { get; }

        public Phone(string mobile, string landline)
        {
            Mobile = mobile;
            Landline = landline;
        }

        public XElement ToXml()
        {
            var phoneElement = new XElement("phone", new XElement("mobile", Mobile));
            AddElementIfNotEmpty(phoneElement, "landline", Landline);
            return phoneElement;
        }

        private void AddElementIfNotEmpty(XElement parent, string elementName, string value)
        {
            if (!string.IsNullOrEmpty(value))
                parent.Add(new XElement(elementName, value));
        }
    }
}
