using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppValidator.LicenseProviders
{
    public class FileLicenseProvider: ILicenseProvider
    {
        public string GetLicenseText(ILicenseDescriptor licenseDescriptor)
        {
            if (licenseDescriptor.GetType() != typeof(FileLicenseDescriptor))
            {
                throw new Exception("Wrong file license descriptor");
            }

            var fileName = ((FileLicenseDescriptor)licenseDescriptor).FileName;
            return File.ReadAllText(fileName);
        }
    }
}
