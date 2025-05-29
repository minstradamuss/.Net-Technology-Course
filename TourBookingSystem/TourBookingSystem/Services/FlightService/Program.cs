
using System;
using System.ServiceModel;
using Contracts;
using Services;

namespace FlightService
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(FlightService)))
            {
                host.Open();
                Console.WriteLine("FlightService is running...");
                Console.ReadLine();
            }
        }
    }
}
