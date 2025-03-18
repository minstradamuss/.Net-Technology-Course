using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatBook.Domain.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        // Связь с пользователем (автор отзыва)
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public User User { get; set; } // Навигационное свойство

        // Связь с книгой
        [ForeignKey(nameof(Book))]
        public int BookId { get; set; }
        public Book Book { get; set; } // Навигационное свойство

        [Required]
        public string Content { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }
    }
}
