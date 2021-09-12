using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppValidator.PlatformReaders
{
    public interface IPlatformReader
    {
        int GetPlatformHash();
    }
}
