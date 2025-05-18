using ChatBook.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatBook.Entities
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; }

        public string Content { get; set; }

        public int Rating { get; set; }
    }
}
