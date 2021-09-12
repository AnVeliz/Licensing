using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppValidator.PlatformReaders
{
    public class PlatformInfoReader : IPlatformReader
    {
        private string readWMI(string wmiClass, string wmiProperty)
        {
            var systemClassManagement = new System.Management.ManagementClass(wmiClass).GetInstances();
            foreach (var managementObject in systemClassManagement)
            {
                var res = managementObject[wmiProperty].ToString();
                return res;
            }

            return string.Empty;
        }

        private string getCPUId()
        {
            return string.Format($"{readWMI("Win32_Processor", "UniqueId")}__${readWMI("Win32_Processor", "ProcessorId")}__${readWMI("Win32_Processor", "Name")}${readWMI("Win32_Processor", "Manufacturer")}");
        }

        private string getBaseBoardId()
        {
            return string.Format($"{readWMI("Win32_Baseboard", "SerialNumber")}");
        }

        public int GetPlatformHash()
        {
            return getCPUId().GetHashCode() & getBaseBoardId().GetHashCode();
        }
    }
}
