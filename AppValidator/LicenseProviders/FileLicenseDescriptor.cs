using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppValidator.LicenseProviders
{
    public class FileLicenseDescriptor: ILicenseDescriptor
    {
        public string FileName { get; set; }
    }
}
