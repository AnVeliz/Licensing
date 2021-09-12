using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppValidator.LicenseCryptors
{
    [Serializable]
    public class LicenseWrapper
    {
        public License License { get; set; }
        public string LicenseCryptoGuard { get; set; }
        public string PublicKey { get; set; }
    }
}
