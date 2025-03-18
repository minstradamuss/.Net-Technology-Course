using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatBook.Domain.Models
{
    public class Friendship
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(User1))]
        public int User1Id { get; set; }
        public User User1 { get; set; } 

        [ForeignKey(nameof(User2))]
        public int User2Id { get; set; }
        public User User2 { get; set; }
    }
}
