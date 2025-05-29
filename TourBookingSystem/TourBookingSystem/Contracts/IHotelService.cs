
using System.ServiceModel;

namespace Contracts
{
    [ServiceContract]
    public interface IHotelService
    {
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Mandatory)]
        void BookHotel(BookingInfo info);
    }
}
