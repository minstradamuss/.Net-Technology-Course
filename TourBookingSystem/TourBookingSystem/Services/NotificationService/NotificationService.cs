
using System;
using Contracts;

namespace Services
{
    public class NotificationService : INotificationService
    {
        public void SendNotification(string message)
        {
            Console.WriteLine("[Notification] " + message);
        }
    }
}
