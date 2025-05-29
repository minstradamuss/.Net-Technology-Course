
using System;
using System.ServiceModel;
using Contracts;
using Services;

namespace HotelService
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(HotelService)))
            {
                host.Open();
                Console.WriteLine("HotelService is running...");
                Console.ReadLine();
            }
        }
    }
}
