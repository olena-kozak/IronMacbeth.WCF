using System;
using System.ServiceModel;

namespace IronMacbeth.FileStorage
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "IronMacbeth.FileStorage";

            using (ServiceHost host = new ServiceHost(typeof(FileStorageService)))
            {
                host.Open();
                Console.WriteLine("The service is ready at {0}");
                Console.WriteLine("Press <Enter> to stop the service.");
                Console.ReadLine();
                host.Close();
            }
        }
    }
}

