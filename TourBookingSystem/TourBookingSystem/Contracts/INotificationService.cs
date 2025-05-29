
using System.ServiceModel;

namespace Contracts
{
    [ServiceContract]
    public interface INotificationService
    {
        [OperationContract(IsOneWay = true)]
        void SendNotification(string message);
    }
}
