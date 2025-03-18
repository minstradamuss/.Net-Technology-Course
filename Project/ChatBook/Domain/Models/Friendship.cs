using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatBook.Domain.Models
{
    public class Friendship
    {
        [Key]
        public int Id { get; set; }

        // Связь с User (первый пользователь)
        [ForeignKey(nameof(User1))]
        public int User1Id { get; set; }
        public User User1 { get; set; } // Навигационное свойство

        // Связь с User (второй пользователь)
        [ForeignKey(nameof(User2))]
        public int User2Id { get; set; }
        public User User2 { get; set; } // Навигационное свойство

        public bool IsAccepted { get; set; } // Подтверждено ли добавление в друзья
    }
}
