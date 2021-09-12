using AppValidator.LicenseCryptors;
using AppValidator.LicenseProviders;
using AppValidator.PlatformReaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppValidator
{
    public class Validator
    {
        private IPlatformReader _platformReader;
        private ILicenseProvider _licenseProvider;
        private ILicenseDescriptor _licenseDescriptor;
        private ILicenseCryptor _licenseCryptor;
        private ILicenseSerializer _licenseSerializer;

        public Validator(IPlatformReader platformReader, ILicenseProvider licenseProvider, ILicenseDescriptor licenseDescriptor, ILicenseCryptor licenseCryptor, ILicenseSerializer licenseSerializer)
        {
            _platformReader = platformReader;
            _licenseProvider = licenseProvider;
            _licenseDescriptor = licenseDescriptor;
            _licenseCryptor = licenseCryptor;
            _licenseSerializer = licenseSerializer;
        }

        public enum ErrorCodes
        {
            Ok,
            CorruptedLicense,
            ValidationFailure,
            ValidationDidntPass,
            LicenseExpired,
            InvalidLicense
        }

        public ErrorCodes Validate()
        {
            try
            {
                var licenseStringContent = _licenseProvider.GetLicenseText(_licenseDescriptor);
                var license = _licenseCryptor.DecryptLicense(licenseStringContent, _licenseSerializer);
                if (license == null)
                {
                    return ErrorCodes.CorruptedLicense;
                }

                var platformHash = _platformReader.GetPlatformHash();

                if (platformHash != license.PlatformHash)
                {
                    return ErrorCodes.InvalidLicense;
                }

                if (license.ExpirationDate < DateTime.Now)
                {
                    return ErrorCodes.LicenseExpired;
                }
            }
            catch(Exception)
            {
                return ErrorCodes.ValidationFailure;
            }

            return ErrorCodes.ValidationDidntPass;
        }
    }
}
