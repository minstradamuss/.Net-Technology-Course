
using System;
using System.ServiceModel;
using Services.NotificationService;

namespace NotificationServiceHost
{
    class Program
    {
        static void Main(string[] args)
        {
            using var host = new ServiceHost(typeof(NotificationService));
            host.Open();
            Console.WriteLine("NotificationService is running at http://localhost:8004/NotificationService");
            Console.ReadLine();
        }
    }
}
