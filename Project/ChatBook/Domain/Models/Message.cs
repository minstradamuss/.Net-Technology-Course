using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatBook.Domain.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }

        // Связь с отправителем
        [ForeignKey(nameof(Sender))]
        public int SenderId { get; set; }
        public User Sender { get; set; } // Навигационное свойство

        // Связь с получателем
        [ForeignKey(nameof(Receiver))]
        public int ReceiverId { get; set; }
        public User Receiver { get; set; } // Навигационное свойство

        [Required]
        public string Content { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
