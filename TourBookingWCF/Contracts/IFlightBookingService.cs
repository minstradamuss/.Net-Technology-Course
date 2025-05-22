using System.ServiceModel;

[ServiceContract]
public interface IFlightBookingService
{
    [OperationContract]
    [TransactionFlow(TransactionFlowOption.Mandatory)]
    void BookFlight(string customerName);
}