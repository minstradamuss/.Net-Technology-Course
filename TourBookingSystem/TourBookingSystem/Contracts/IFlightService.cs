
using System.ServiceModel;

namespace Contracts
{
    [ServiceContract]
    public interface IFlightService
    {
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Mandatory)]
        void BookFlight(BookingInfo info);
    }
}
