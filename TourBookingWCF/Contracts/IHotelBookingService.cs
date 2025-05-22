using System.ServiceModel;

[ServiceContract]
public interface IHotelBookingService
{
    [OperationContract]
    [TransactionFlow(TransactionFlowOption.Mandatory)]
    void BookHotel(string customerName);
}