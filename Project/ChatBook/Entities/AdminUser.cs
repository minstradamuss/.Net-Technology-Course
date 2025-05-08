using System.ComponentModel.DataAnnotations.Schema;

namespace ChatBook.Entities
{
    [Table("AdminUsers")]
    public class AdminUser : BaseUser
    {
        public string AdminCode { get; set; }
    }
}
