
using System;
using System.ServiceModel;
using Services.PaymentService;

namespace PaymentServiceHost
{
    class Program
    {
        static void Main(string[] args)
        {
            using var host = new ServiceHost(typeof(PaymentService));
            host.Open();
            Console.WriteLine("PaymentService is running at http://localhost:8003/PaymentService");
            Console.ReadLine();
        }
    }
}
