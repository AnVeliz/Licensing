using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicensingApp
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

                Console.WriteLine("Enter the application name the license should be issued for");
                var appName = Console.ReadLine();
                Console.WriteLine($"The license will be issued for '{appName}'");

                (new AppValidator.LicenseCryptors.RSALicenseCryptor()).CreateCryptoLicense(
                    new AppValidator.LicenseCryptors.License()
                    {
                        AppName = appName,
                        ExpirationDate = expirationDate,
                        PlatformHash = new AppValidator.PlatformReaders.PlatformInfoReader().GetPlatformHash()
                    },
                    new AppValidator.LicenseCryptors.XMLLicenseSerializer()
                );
            }
        }
    }
}
