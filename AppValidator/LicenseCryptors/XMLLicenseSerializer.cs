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
        private T XmlDeserialize<T>(string content)
        {
            var xmlSerializer = new XmlSerializer(typeof(T));
            using (var textReader = new StringReader(content))
            {
                return (T)xmlSerializer.Deserialize(textReader);
            }
        }

        private string XmlSerialize<T>(T obj)
        {
            var xmlSerializer = new XmlSerializer(typeof(T));
            using (var textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, obj);
                return textWriter.ToString();
            }
        }

        public License Deserialize(string licenseText)
        {
            return XmlDeserialize<License>(licenseText);
        }

        public string Serialize(License license)
        {
            return XmlSerialize(license);
        }

        public LicenseWrapper DeserializeWrapper(string licenseText)
        {
            return XmlDeserialize<LicenseWrapper>(licenseText);
        }

        public string SerializeWrapper(LicenseWrapper license)
        {
            return XmlSerialize(license);
        }
    }
}
