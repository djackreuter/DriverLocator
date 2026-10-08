using System;
using System.IO;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace DriverLocator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            String[] sysFilePaths = { @"C:\Windows\System32\Drivers", @"C:\Windows" };
            String[] appFilePaths = { @"C:\Program Files", @"C:\Program Files (x86)" };

            String[] sysDrivers = { };
            String[] appDrivers = { };

            String[] vendors = {
                "Microsoft",
                "Broadcom",
                "Realtek",
                "VMware",
                "Avago",
                "NVIDIA", 
                "Mellanox",
                "Marvell",
                "LSI",
                "Intel",
                "PMC",
                "AMD",
                "Advanced Micro Devices",
                "Apple",
                "Windows",
                "Qlogic",
                "Promise",
                "Silicon",
                "Microsemi",
                "VIA"
            };

            foreach (string file in sysFilePaths)
            {
                try
                {
                    sysDrivers = sysDrivers.Concat(Directory.GetFiles(file, "*.sys", SearchOption.TopDirectoryOnly)).ToArray();
                }
                catch (UnauthorizedAccessException) { continue; }
            }

            foreach (string file in appFilePaths)
            {
                try
                {
                    appDrivers = appDrivers.Concat(Directory.GetFiles(file, "*.sys", SearchOption.AllDirectories)).ToArray();
                }
                catch (UnauthorizedAccessException) { continue; }
            }

            var drivers = sysDrivers.Concat(appDrivers).ToArray();

            foreach (string driver in drivers)
            {

                FileVersionInfo fileInfo = FileVersionInfo.GetVersionInfo(driver);
                String fileName = fileInfo.FileName;
                String companyName = fileInfo.CompanyName != null ? fileInfo.CompanyName : "";
                String fileDescription = fileInfo.FileDescription;
                String fileVersion = fileInfo.FileVersion;
                String copyright = fileInfo.LegalCopyright;
                if (companyName.Contains("Microsoft"))
                    continue;

                bool isVendor = vendors.Any(word => 
                    companyName.IndexOf(word, StringComparison.OrdinalIgnoreCase) >= 0); 

                if (!isVendor)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }

                Console.WriteLine("\n============================================================================");
                Console.WriteLine($"{companyName}");
                Console.WriteLine($"{fileName}");
                Console.WriteLine($"{fileDescription}");
                Console.WriteLine($"{fileVersion} - {copyright}");
                Console.ResetColor();
            }
        }
    }
}
