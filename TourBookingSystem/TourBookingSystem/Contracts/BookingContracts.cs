
using System.Runtime.Serialization;

namespace Contracts
{
    [DataContract]
    public class BookingInfo
    {
        [DataMember] public string CustomerName { get; set; }
        [DataMember] public string TourId { get; set; }
    }

    [DataContract]
    public class Notification
    {
        [DataMember] public string Message { get; set; }
    }
}
