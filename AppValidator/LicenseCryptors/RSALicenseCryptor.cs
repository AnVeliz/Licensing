using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AppValidator.LicenseCryptors
{
    public class RSALicenseCryptor : ILicenseCryptor
    {
        private string serializeRSAKey(RSAParameters parameters)
        {
            var stringWriter = new System.IO.StringWriter();
            var xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));
            xmlSerializer.Serialize(stringWriter, parameters);

            return stringWriter.ToString();
        }

        public void SaveLicense(License license, ILicenseSerializer serializer)
        {
            var cryptoServiceProvider = new RSACryptoServiceProvider(2048);

            var publicKey = cryptoServiceProvider.ExportParameters(false);
            var privateKey = cryptoServiceProvider.ExportParameters(true);

            var serializedLicense = serializer.Serialize(license);
            var bytesToEncrypt = Encoding.UTF8.GetBytes(serializedLicense);
            var cryptedLicense = serializeRSAKey(publicKey);
        }

        public License DecryptLicense(string licenseText)
        {
            throw new NotImplementedException();
        }

        public string EncryptLicense(License licenseText)
        {
            throw new NotImplementedException();
        }
    }
}
