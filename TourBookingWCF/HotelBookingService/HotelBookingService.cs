using System;
using System.ServiceModel;

public class HotelBookingService : IHotelBookingService
{
    public void BookHotel(string customerName)
    {
        Console.WriteLine($"[Hotel] Booking hotel for {customerName}");
        var factory = new ChannelFactory<INotificationService>("NotificationServiceEndpoint");
        var proxy = factory.CreateChannel();
        proxy.Notify($"Hotel booked for {customerName}");
    }
}