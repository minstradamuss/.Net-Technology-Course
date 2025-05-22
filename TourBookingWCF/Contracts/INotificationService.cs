using System.ServiceModel;

[ServiceContract]
public interface INotificationService
{
    [OperationContract(IsOneWay = true)]
    void Notify(string message);
}