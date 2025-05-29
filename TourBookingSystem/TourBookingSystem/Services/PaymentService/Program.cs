
using System;
using System.ServiceModel;
using Contracts;
using Services;

namespace PaymentService
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(PaymentService)))
            {
                host.Open();
                Console.WriteLine("PaymentService is running...");
                Console.ReadLine();
            }
        }
    }
}
