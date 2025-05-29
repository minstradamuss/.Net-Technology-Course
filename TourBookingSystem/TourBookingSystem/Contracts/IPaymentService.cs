
using System.ServiceModel;

namespace Contracts
{
    [ServiceContract]
    public interface IPaymentService
    {
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Mandatory)]
        void MakePayment(BookingInfo info);
    }
}
