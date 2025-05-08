using System.ComponentModel.DataAnnotations;

namespace ChatBook.Entities
{
    public class BaseUser
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nickname { get; set; }
    }
}
