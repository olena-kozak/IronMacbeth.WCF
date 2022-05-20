using System;
using System.ServiceModel;

namespace IronMacbeth.UserManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "IronMacbeth.UserManagement";

            using (ServiceHost host = new ServiceHost(typeof(UserManagementService)))
            {
                host.Open();

                Console.WriteLine("Press <Enter> to stop the service.");
                Console.ReadLine();

                host.Close();
            }
        }
    }
}
