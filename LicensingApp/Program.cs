using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var currentExeLocation = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var currentExeFolder = System.IO.Path.GetDirectoryName(currentExeLocation);

            var appValidator = new AppValidator.Validator(
                new AppValidator.PlatformReaders.PlatformInfoReader(),
                new AppValidator.LicenseProviders.FileLicenseProvider(),
                new AppValidator.LicenseProviders.FileLicenseDescriptor() { FileName = System.IO.Path.Combine(currentExeFolder, "license.xml") },
                new AppValidator.LicenseCryptors.RSALicenseCryptor(),
                new AppValidator.LicenseCryptors.XMLLicenseSerializer());

            if (args.Length == 0)
            {
                Console.WriteLine("Please set a parameter 'createlic' or 'validatelic'.");
            }
            
            if (args[0] == "validatelic")
            {
                Console.WriteLine(appValidator.Validate());
                return;
            }
            if (args[0] == "createlic")
            {
                Console.WriteLine("Enter an expiration date in:");
                var expirationDate = DateTime.Parse(Console.ReadLine());
                Console.WriteLine($"The expiration date will be {expirationDate.ToString("ddd dd MMM yyyy h:mm tt zzz")}");

                (new AppValidator.LicenseCryptors.RSALicenseCryptor()).CreateLicense(
                    new AppValidator.LicenseCryptors.License() 
                    { 
                        ExpirationDate = expirationDate,
                        PlatformHash = new AppValidator.PlatformReaders.PlatformInfoReader().GetPlatformHash()
                    },
                    new AppValidator.LicenseCryptors.XMLLicenseSerializer()
                );
            }
        }
    }
}
