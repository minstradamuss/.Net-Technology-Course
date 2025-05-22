using System.ServiceModel;

[ServiceContract]
public interface IPaymentService
{
    [OperationContract]
    [TransactionFlow(TransactionFlowOption.Mandatory)]
    void MakePayment(string customerName);
}