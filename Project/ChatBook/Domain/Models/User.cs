using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChatBook.Domain.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nickname { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] Avatar { get; set; } // Теперь изображение хранится в BLOB

        public string PhoneNumber { get; set; }

        public string Password { get; set; }

        //public ICollection<Review> Reviews { get; set; } // Список отзывов пользователя
    }
}

