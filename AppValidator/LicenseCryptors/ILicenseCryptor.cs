using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppValidator.LicenseCryptors
{
    public interface ILicenseCryptor
    {
        string EncryptLicense(License licenseText);
        License DecryptLicense(string licenseText);
    }
}
