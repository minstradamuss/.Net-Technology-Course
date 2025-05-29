
using System;
using System.ServiceModel;
using System.Transactions;
using Contracts;

namespace Services
{
    [ServiceBehavior(TransactionScopeRequired = true, InstanceContextMode = InstanceContextMode.PerCall)]
    public class FlightService : IFlightService
    {
        public void BookFlight(BookingInfo info)
        {
            Console.WriteLine("Flight booked for " + info.CustomerName);
            string message = $"Flight booked for {info.CustomerName} (Tour ID: {info.TourId})";

            ChannelFactory<Contracts.INotificationService> factory = new ChannelFactory<Contracts.INotificationService>("NotificationServiceEndpoint");
            var notifier = factory.CreateChannel();
            notifier.SendNotification(message);

        }
    }
}
