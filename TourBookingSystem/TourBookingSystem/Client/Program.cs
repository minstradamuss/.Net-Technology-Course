using System;
using System.ServiceModel;
using System.Transactions;
using Contracts;
using Client.FlightService;
using Client.HotelService;
using Client.PaymentService;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var info = new BookingInfo
            {
                CustomerName = "Ivan Ivanov",
                TourId = "T123"
            };

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    var flightClient = new FlightServiceClient("FlightServiceEndpoint");
                    var hotelClient = new HotelServiceClient("HotelServiceEndpoint");
                    var paymentClient = new PaymentServiceClient("PaymentServiceEndpoint");

                    Console.WriteLine("Booking started...");

                    flightClient.BookFlight(info);
                    hotelClient.BookHotel(info);
                    paymentClient.MakePayment(info);

                    scope.Complete(); 
                }

                Console.WriteLine("Tour successfully booked.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred: " + ex.Message);
            }

            Console.ReadKey();
        }
    }
}
