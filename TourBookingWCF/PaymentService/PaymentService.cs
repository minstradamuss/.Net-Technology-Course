using System;
using System.ServiceModel;

public class PaymentService : IPaymentService
{
    public void MakePayment(string customerName)
    {
        Console.WriteLine($"[Payment] Charging customer {customerName}");
        var factory = new ChannelFactory<INotificationService>("NotificationServiceEndpoint");
        var proxy = factory.CreateChannel();
        proxy.Notify($"Payment processed for {customerName}");
    }
}