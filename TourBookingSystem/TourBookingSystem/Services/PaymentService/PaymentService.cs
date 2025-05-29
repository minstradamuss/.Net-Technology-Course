
using System;
using System.ServiceModel;
using System.Transactions;
using Contracts;

namespace Services
{
    [ServiceBehavior(TransactionScopeRequired = true, InstanceContextMode = InstanceContextMode.PerCall)]
    public class PaymentService : IPaymentService
    {
        public void MakePayment(BookingInfo info)
        {
            Console.WriteLine("Payment processed for " + info.CustomerName);
            string message = $"Payment processed for {info.CustomerName} (Tour ID: {info.TourId})";

            ChannelFactory<Contracts.INotificationService> factory = new ChannelFactory<Contracts.INotificationService>("NotificationServiceEndpoint");
            var notifier = factory.CreateChannel();
            notifier.SendNotification(message);

        }
    }
}
