using System;
using System.IdentityModel.Configuration;
using System.ServiceModel;

namespace IronMacbeth.BFF
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.Title = "IronMacbeth.BFF";

            using (var host = new ServiceHost(typeof (Service)))
            using (var anonymousServiceHost = new ServiceHost(typeof (AnonymousService)))
            {
                host.Credentials.UseIdentityConfiguration = true;

                var idConfig = new IdentityConfiguration();

                idConfig.SecurityTokenHandlers.AddOrReplace(new UserNameSecurityTokenHandler());

                host.Credentials.IdentityConfiguration = idConfig;

                Console.WriteLine("Starting Server...");

                try
                {
                    anonymousServiceHost.Open();
                    host.Open();
                }
                catch (Exception exception)
                {
                    Console.WriteLine("Unable to start server");
                    Console.WriteLine("Error: ");
                    Console.WriteLine(exception.ToString());
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                    anonymousServiceHost.Abort();
                    host.Abort();
                    return;
                }
                Console.WriteLine("Server is running");
                Console.WriteLine("Type \"exit\" to  stop server");
                while (Console.ReadLine() != "exit") { }
            }
        }
    }
}
