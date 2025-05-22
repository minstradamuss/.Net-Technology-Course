using System;
using System.ServiceModel;
using System.Transactions;

class Program
{
    static void Main(string[] args)
    {
        using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
        {
            var flightProxy = new ChannelFactory<IFlightBookingService>("FlightBookingServiceEndpoint").CreateChannel();
            var hotelProxy = new ChannelFactory<IHotelBookingService>("HotelBookingServiceEndpoint").CreateChannel();
            var paymentProxy = new ChannelFactory<IPaymentService>("PaymentServiceEndpoint").CreateChannel();

            string customerName = "Иван Иванов";
            flightProxy.BookFlight(customerName);
            hotelProxy.BookHotel(customerName);
            paymentProxy.MakePayment(customerName);

            scope.Complete();
            Console.WriteLine("[CLIENT] All steps completed successfully.");
        }
        Console.ReadLine();
    }
}