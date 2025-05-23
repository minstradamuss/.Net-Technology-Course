
using System;
using System.ServiceModel;
using Services.FlightService;

namespace FlightServiceHost
{
    class Program
    {
        static void Main(string[] args)
        {
            using var host = new ServiceHost(typeof(FlightService));
            host.Open();
            Console.WriteLine("FlightService is running at http://localhost:8001/FlightService");
            Console.ReadLine();
        }
    }
}
