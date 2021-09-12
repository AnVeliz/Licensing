using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AppValidator.LicenseCryptors
{
    public class XMLLicenseSerializer: ILicenseSerializer
    {
        public License Deserialize(string licenseText)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(License));
            using (var textReader = new StringReader(licenseText))
            {
                return (License)xmlSerializer.Deserialize(textReader);
            }
        }

        public string Serialize(License license)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(License));
            using (var textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, license);
                return textWriter.ToString();
            }
        }
    }
}
