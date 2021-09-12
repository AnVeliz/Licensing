using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppValidator.LicenseProviders
{
    public class NetworkLicenseProvider : ILicenseProvider
    {
        public string GetLicenseText(ILicenseDescriptor licenseDescriptor)
        {
            throw new NotImplementedException();
        }
    }
}
