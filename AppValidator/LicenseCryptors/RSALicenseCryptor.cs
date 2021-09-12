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
        private readonly int keyLength = 2048;

        private string serializeRSAKey(RSAParameters parameters)
        {
            var stringWriter = new System.IO.StringWriter();
            var xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));
            xmlSerializer.Serialize(stringWriter, parameters);

            return stringWriter.ToString();
        }

        public LicenseWrapper CreateLicense(License license, ILicenseSerializer serializer)
        {
            var cryptoServiceProvider = new RSACryptoServiceProvider(keyLength);
            var publicKey = cryptoServiceProvider.ExportParameters(false);
            var privateKey = cryptoServiceProvider.ExportParameters(true);

            var serializedLicense = serializer.Serialize(license);
            var licenseStringHash = serializedLicense.GetHashCode().ToString();

            return new LicenseWrapper()
            {
                License = license,
                LicenseCryptoGuard = encryptLicenseGuard(serializeRSAKey(privateKey), licenseStringHash),
                PublicKey = serializeRSAKey(publicKey)
            };
        }

        public License DecryptLicense(string licenseWrapperText, ILicenseSerializer serializer)
        {
            var licenseWrapper = serializer.DeserializeWrapper(licenseWrapperText);
            var licenseGuardHash = decryptLicenseGuard(licenseWrapper.PublicKey, licenseWrapper.LicenseCryptoGuard);

            var serializedLicense = serializer.Serialize(licenseWrapper.License);
            return serializedLicense.GetHashCode().ToString() == licenseGuardHash
                ? licenseWrapper.License
                : null;
        }

        private string decryptLicenseGuard(string xmlPublicKey, string licenseGuard)
        {
            using (var rsa = new RSACryptoServiceProvider(keyLength))
            {
                try
                {
                    var bytesToEncrypt = Convert.FromBase64String(licenseGuard);
                    rsa.FromXmlString(xmlPublicKey);
                    
                    return rsa.Decrypt(bytesToEncrypt, true).ToString();
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }
        }

        private string encryptLicenseGuard(string xmlPrivateKey, string licenseGuardText)
        {
            using (var rsa = new RSACryptoServiceProvider(keyLength))
            {
                try
                {
                    rsa.FromXmlString(xmlPrivateKey);

                    var bytesToEncrypt = Encoding.UTF8.GetBytes(licenseGuardText);
                    var encryptedData = rsa.Encrypt(bytesToEncrypt, true);
                    var base64Encrypted = Convert.ToBase64String(encryptedData);
                    
                    return base64Encrypted;
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }
        }
    }
}
