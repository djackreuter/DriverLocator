using System;
using System.IO;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverLocator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            String[] drivers = Directory.GetFiles(@"C:\Windows\System32\Drivers");

            for (int i = 0; i < drivers.Length - 1; i++)
            {
                if (drivers[i] == null)
                    continue;

                FileVersionInfo fileInfo = FileVersionInfo.GetVersionInfo(drivers[i]);
                String fileName = fileInfo.FileName;
                String companyName = fileInfo.CompanyName != null ? fileInfo.CompanyName : "";
                String fileDescription = fileInfo.FileDescription;
                String fileVersion = fileInfo.FileVersion;
                String copyright = fileInfo.LegalCopyright;
                if (companyName.Contains("Microsoft"))
                    continue;

                Console.WriteLine("====================");
                Console.WriteLine($"{companyName}");
                Console.WriteLine($"{fileName}");
                Console.WriteLine($"{fileDescription}");
                Console.WriteLine($"{fileVersion} - {copyright}");
            }
        }
    }
}
