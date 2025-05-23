
using System;
using System.ServiceModel;
using Services.HotelService;

namespace HotelServiceHost
{
    class Program
    {
        static void Main(string[] args)
        {
            using var host = new ServiceHost(typeof(HotelService));
            host.Open();
            Console.WriteLine("HotelService is running at http://localhost:8002/HotelService");
            Console.ReadLine();
        }
    }
}
