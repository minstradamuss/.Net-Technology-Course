using System;
using System.ServiceModel;

public class FlightBookingService : IFlightBookingService
{
    public void BookFlight(string customerName)
    {
        Console.WriteLine($"[Flight] Booking flight for {customerName}");
        var factory = new ChannelFactory<INotificationService>("NotificationServiceEndpoint");
        var proxy = factory.CreateChannel();
        proxy.Notify($"Flight booked for {customerName}");
    }
}