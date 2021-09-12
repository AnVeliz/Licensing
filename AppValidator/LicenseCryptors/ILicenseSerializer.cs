using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppValidator.LicenseCryptors
{
    public interface ILicenseSerializer
    {
        License Deserialize(string licenseText);
        string Serialize(License license);

        LicenseWrapper DeserializeWrapper(string licenseText);
        string SerializeWrapper(LicenseWrapper license);
    }
}
