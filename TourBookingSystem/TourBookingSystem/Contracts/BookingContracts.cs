
using System.Runtime.Serialization;

namespace Contracts
{

    [DataContract]
    public class Notification
    {
        [DataMember] public string Message { get; set; }
    }
}
