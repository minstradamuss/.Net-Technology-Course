using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatBook.Domain.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Status { get; set; }

        public int Rating { get; set; }

        public string Review { get; set; }

        public byte[] CoverImage { get; set; }
        public ICollection<Review> Reviews { get; set; } // Связанные обзоры
    }
}
