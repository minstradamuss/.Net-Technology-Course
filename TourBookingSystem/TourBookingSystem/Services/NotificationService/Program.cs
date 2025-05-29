
using System;
using System.ServiceModel;
using Contracts;
using Services;

namespace NotificationService
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(NotificationService)))
            {
                host.Open();
                Console.WriteLine("NotificationService is running...");
                Console.ReadLine();
            }
        }
    }
}
